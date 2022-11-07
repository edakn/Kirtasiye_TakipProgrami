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
    public partial class Form4 : Form
    {
        public Form1 frm1;
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear(); comboBox1.Items.Clear(); comboBox1.Items.Clear();            
            frm1.stoklistele();          
            frm1.urunad();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void btnCik_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            btnEkle.Text = "Kaydet";                
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" && comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "")
                {
                    if (radioButton1.Checked == true)
                    {
                        frm1.barkodkontrol();
                        if (frm1.durum == false)
                        {
                            frm1.bag.Open();
                            frm1.kmt.Connection = frm1.bag;
                            frm1.kmt.CommandText = "INSERT INTO stokbil(BarkodNo,UrunAd,Fiyat,Adet) VALUES ('" + comboBox1.Text + "','" + comboBox2.Text + "','" + comboBox3.Text + "','" + textBox1.Text + "') ";
                            frm1.kmt.ExecuteNonQuery();
                            frm1.kmt.Dispose();
                            frm1.bag.Close();
                            MessageBox.Show("Kayıt işlemi tamamlandı ! ");
                            frm1.dtst.Tables["stokbil"].Clear();
                            frm1.stoklistele();
                            comboBox1.Items.Clear();
                            comboBox2.Items.Clear();
                            comboBox3.Items.Clear();                           
                            frm1.urunad();
                        }
                        else MessageBox.Show("Girmiş olduğunuz Barkod No zaten var !");
                    }

                    if (radioButton2.Checked == true)
                    {
                        frm1.barkodkontrol();
                        if (frm1.durum == true)
                        {
                            frm1.bag.Open();
                            frm1.kmt.Connection = frm1.bag;
                            frm1.kmt.CommandText = "UPDATE stokbil SET Adet=Adet+'" + int.Parse(textBox1.Text) + "' WHERE BarkodNo='" + comboBox1.Text + "'";
                            frm1.kmt.ExecuteNonQuery();
                            frm1.kmt.Dispose();
                            frm1.bag.Close();
                            MessageBox.Show("Kayıt işlemi tamamlandı ! ");
                            comboBox1.Items.Clear();
                            comboBox2.Items.Clear();
                            comboBox3.Items.Clear();                           
                            frm1.dtst.Tables["stokbil"].Clear();
                            frm1.stoklistele();
                        }
                        else MessageBox.Show("Kayıtlı ürün bulunamadı! ");
                    }
                }
                else MessageBox.Show("Boş alanları doldurunuz !!!");
            }
            catch
            {
                ;
            }

        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            btnEkle.Text = "Ekle";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index;
            index = comboBox1.SelectedIndex;
            comboBox2.SelectedIndex = index;
            comboBox3.SelectedIndex = index;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult cevap;
                cevap = MessageBox.Show("Kaydı silmek istediğinizden eminmisiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (cevap == DialogResult.Yes)
                {
                    frm1.bag.Open();
                    frm1.kmt.Connection = frm1.bag;
                    frm1.kmt.CommandText = "DELETE from stokbil WHERE BarkodNo='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                    frm1.kmt.ExecuteNonQuery();
                    frm1.kmt.Dispose();
                    frm1.bag.Close();
                    frm1.dtst.Tables["stokbil"].Clear();
                    frm1.stoklistele();
                }
            }
            catch
            {
                ;
            }
        }
    }
}
