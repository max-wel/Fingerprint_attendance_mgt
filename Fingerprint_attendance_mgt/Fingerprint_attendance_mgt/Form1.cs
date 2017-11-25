using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fingerprint_attendance_mgt
{
    delegate void Function();
    public partial class attendance_form : Form, DPFP.Capture.EventHandler
    {
        private List<string> list;
        private byte[] f_print;
        public DPFP.Capture.Capture Capturer;
        private DPFP.Template Template;
        private DPFP.Verification.Verification Verificator;
        public attendance_form()
        {
            InitializeComponent();
            
        }


        protected  void  Init()
        {
            Capturer = new DPFP.Capture.Capture();                  // Create a capture operation.
            Capturer.EventHandler = this;                           // Subscribe for capturing events.
            Verificator = new DPFP.Verification.Verification();     // Create a fingerprint template verificator
            //UpdateStatus(0);
        }
        #region EventHandler Members:

        public void OnComplete(object Capture, string ReaderSerialNumber, DPFP.Sample Sample)
        {
            MakeReport("The fingerprint sample was captured.");
            SetPrompt("Scan the same fingerprint again.");
            Process(Sample);
        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The finger was removed from the fingerprint reader.");
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The fingerprint reader was touched.");
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The fingerprint reader was connected.");
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The fingerprint reader was disconnected.");
        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, DPFP.Capture.CaptureFeedback CaptureFeedback)
        {
            if (CaptureFeedback == DPFP.Capture.CaptureFeedback.Good)
                MakeReport("The quality of the fingerprint sample is good.");
            else
                MakeReport("The quality of the fingerprint sample is poor.");
        }
        #endregion

        //protected void SetStatus(string status)
        //{
        //    this.Invoke(new Function(delegate () {
        //        StatusLine.Text = status;
        //    }));
        //}

        protected void SetPrompt(string prompt)
        {
            this.Invoke(new Function(delegate () {
                Prompt.Text = prompt;
            }));
        }



        protected void MakeReport(string message)
        {
            this.Invoke(new Function(delegate () {
                StatusText.AppendText(message + "\r\n");
            }));
        }

        public void DrawPicture(Bitmap bitmap)
        {
            this.Invoke(new Function(delegate () {
                Picture.Image = new Bitmap(bitmap, Picture.Size);   // fit the image into the picture box
            }));
        }

        protected  void Process(DPFP.Sample Sample)
        {
            // Draw fingerprint sample image.
            DrawPicture(ConvertSampleToBitmap(Sample));

            // Process the sample and create a feature set for the enrollment purpose.
            DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Verification);

            // Check quality of the sample and start verification if it's good
            // TODO: move to a separate task
            if (features != null)
            {
                foreach (var val in list)
                {
                    f_print = Convert.FromBase64String(val);
                    DPFP.Template temp = new DPFP.Template();
                    temp.DeSerialize(f_print);
                    Template = temp;

                    DPFP.Verification.Verification.Result result = new DPFP.Verification.Verification.Result();
                    Verificator.Verify(features, Template, ref result);
                    //UpdateStatus(result.FARAchieved);
                    if (result.Verified)
                        MakeReport("The fingerprint was VERIFIED.");
                    else
                        MakeReport("The fingerprint wan NOT VERIFIED.");
                }
                // Compare the feature set with our template
                
            }
        }

        public void get_fprint()
        {
           
            Database db = new Database();
            list = new List<string>();
            list = db.db_retrieve();
            
        }

        protected void Start()
        {
            Capturer.StartCapture();
            SetPrompt("Using the fingerprint reader, scan your fingerprint.");
        }

        protected void Stop()
        {
            Capturer.StopCapture();
        }

        protected Bitmap ConvertSampleToBitmap(DPFP.Sample Sample)
        {
            DPFP.Capture.SampleConversion Convertor = new DPFP.Capture.SampleConversion();  // Create a sample convertor.
            Bitmap bitmap = null;                                                           // TODO: the size doesn't matter
            Convertor.ConvertToPicture(Sample, ref bitmap);                                 // TODO: return bitmap as a result
            return bitmap;
        }

        protected DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample, DPFP.Processing.DataPurpose Purpose)
        {
            DPFP.Processing.FeatureExtraction Extractor = new DPFP.Processing.FeatureExtraction();  // Create a feature extractor
            DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
            DPFP.FeatureSet features = new DPFP.FeatureSet();
            Extractor.CreateFeatureSet(Sample, Purpose, ref feedback, ref features);            // TODO: return features as a result?
            if (feedback == DPFP.Capture.CaptureFeedback.Good)
                return features;
            else
                return null;
        }
        private void staffEnrollmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stop();
            var enrollForm = new Enroll();
            
            enrollForm.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            date.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            time.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void attendance_form_Load(object sender, EventArgs e)
        {
            get_fprint();
            Init();
            Start();
            
        }

        private void attendance_form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Stop();
        }
    }
}
