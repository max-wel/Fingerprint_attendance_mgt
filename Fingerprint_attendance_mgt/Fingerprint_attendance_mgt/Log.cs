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
    public partial class Log : Form
    {
        public Log()
        {
            InitializeComponent();
        }

        private void Log_Load(object sender, EventArgs e)
        {
            Database db = new Database();
            //var table = db.dailylog_retrieve();
            //dataGridView1.DataSource = table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string date_from = textBox_from.Text.Replace(" ", string.Empty);
            string date_to = textBox_to.Text.Replace(" ", string.Empty);

            Database db = new Database();
            var table = db.dailylog_retrieve(date_from, date_to);
            dataGridView1.DataSource = table;
        }
    }
}
