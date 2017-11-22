using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fingerprint_attendance_mgt
{
    public partial class Enroll : Form
    {
        private String name, id_num, department, gender, position, email, phone;

        private string path = Path.GetFullPath(Environment.CurrentDirectory);
        private string dbName = "Attendance_mgt.mdf";

        #region
        public string Name1
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Id_num
        {
            get
            {
                return id_num;
            }

            set
            {
                id_num = value;
            }
        }

        public string Department
        {
            get
            {
                return department;
            }

            set
            {
                department = value;
            }
        }

        public string Gender
        {
            get
            {
                return gender;
            }

            set
            {
                gender = value;
            }
        }

        public string Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }

        public string Phone
        {
            get
            {
                return phone;
            }

            set
            {
                phone = value;
            }
        }
#endregion
        public Enroll()
        {
            InitializeComponent();
            //string path = Path.GetFullPath(Environment.CurrentDirectory);
            

        }

        public void staffDetails()
        {
            using (var conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + path + @"\" + dbName + ";Integrated Security=True;Connect Timeout=30"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Staff_Enroll() VALUES ()", conn);
            }
        }

       
        
     
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpeg;*.bmp;*.png;*.jpg)|*.jpeg;*.bmp;*.png;*.jpg";
            if(open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(open.FileName);
            }
        }
        private void update_button_Click(object sender, EventArgs e)
        {
            if(Name_text.Text == string.Empty)
            {
                MessageBox.Show("Enter Name");
            }
            else if(Id_text.Text == string.Empty)
            {
                MessageBox.Show("Enter ID");
            }
            else if (Dept_text.Text == string.Empty)
            {
                MessageBox.Show("Enter Department");
            }
            else if (Gender_comboBox.Text == string.Empty)
            {
                MessageBox.Show("Enter Gender");
            }
            else if (Position_comboBox.Text == string.Empty)
            {
                MessageBox.Show("Enter Position");
            }
            else if (Phone_text.Text == string.Empty)
            {
                MessageBox.Show("Enter Phone Number");
            }
            else if (Email_text.Text == string.Empty)
            {
                MessageBox.Show("Enter email");
            }

            Name1 = Name_text.Text;
            Id_num = Id_text.Text;
            Department = Dept_text.Text;
            Gender = Gender_comboBox.Text;
            Position = Position_comboBox.Text;
            Phone = Phone_text.Text;
            Email = Email_text.Text;
        }
    }
}
