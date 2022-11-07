using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace kirtasiye_takip_programi
{
    public partial class Form5 : Form
    {
        public Form1 frm1;
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            frm1.dtst.Tables["satisbil"].Clear();
            frm1.satislistele();
        }

        private void btnCik_Click(object sender, EventArgs e)
        {
            frm1.dtst.Tables["satisbil"].Clear();
            frm1.satislistele();
            this.Close();
        }

        private void btnGoster_Click(object sender, EventArgs e)
        {
            try
            {
                frm1.kasapara();
                frm1.dtst.Tables["satisbil"].Clear();
                frm1.filter();
            }
            catch
            {
                ;
            }
        }
    }
}
