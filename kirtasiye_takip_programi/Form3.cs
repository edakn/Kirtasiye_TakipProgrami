﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace kirtasiye_takip_programi
{
    public partial class Form3 : Form
    {
        public Form1 frm1;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {

            
    
            try
            {
                frm1.bag.Open();
                frm1.kmt.Connection = frm1.bag;
                frm1.kmt.CommandText = "UPDATE musbil SET MusteriNo='" + textBox1.Text + "',TcKimlik='" + textBox2.Text + "',Ad='" + textBox3.Text + "',Soyad='" + textBox4.Text + "',Telefon='" + textBox5.Text + "',Adres='" + textBox6.Text + "' WHERE MusteriNo='" + frm1.dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                frm1.kmt.ExecuteNonQuery();
                frm1.kmt.Dispose();
                frm1.bag.Close();
                frm1.dtst.Tables["musbil"].Clear();
                frm1.listele();
                this.Close();
            }
            catch
            {
                ;
            }
        }
    }
}
