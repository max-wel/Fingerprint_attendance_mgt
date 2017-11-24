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
    public partial class attendance_form : Form
    {
        public attendance_form()
        {
            InitializeComponent();
        }

        //private void toolStripButton1_Click(object sender, EventArgs e)
        //{

        //}

        //private void pictureBox1_Click(object sender, EventArgs e)
        //{

        //}

        //private void label1_Click(object sender, EventArgs e)
        //{

        //}

        //private void label5_Click(object sender, EventArgs e)
        //{

        //}

        //private void label4_Click(object sender, EventArgs e)
        //{

        //}

        //private void groupBox1_Enter(object sender, EventArgs e)
        //{

        //}

        private void staffEnrollmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var enrollForm = new Enroll();
            enrollForm.OnTemplate += this.OnTemplate;
            enrollForm.ShowDialog();
        }

        private void OnTemplate(DPFP.Template template)
        {
            this.Invoke(new Function(delegate ()
            {
                Template = template;
                //VerifyButton.Enabled = SaveButton.Enabled = (Template != null);
                if (Template != null)
                    MessageBox.Show("The fingerprint template is ready for fingerprint verification.", "Fingerprint Enrollment");
                else
                    MessageBox.Show("The fingerprint template is not valid. Repeat fingerprint enrollment.", "Fingerprint Enrollment");
            }));
        }
        private DPFP.Template Template;
    }
}
