
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;



namespace kirtasiye_takip_programi
{
    public partial class Form1 : Form
    {
        public Form2 frm2;
        public Form3 frm3;
        public Form4 frm4;
        public Form5 frm5;       
        public Form1()
        {
            InitializeComponent();
            frm2 = new Form2();
            frm3 = new Form3();
            frm4 = new Form4();
            frm5 = new Form5();            
            frm2.frm1 = this;
            frm3.frm1 = this;
            frm4.frm1 = this;
            frm5.frm1 = this;            
        }
        public OleDbConnection bag = new OleDbConnection("Provider=Microsoft.Jet.Oledb.4.0;Data Source=data.mdb");
        public OleDbCommand kmt = new OleDbCommand();
        public OleDbDataAdapter adtr = new OleDbDataAdapter();
        public DataSet dtst = new DataSet();
        public Boolean durum = false;
       string  toplam;
        public void listele()
        {
            bag.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From musbil ", bag);
            adtr.Fill(dtst, "musbil");
            dataGridView1.DataSource = dtst.Tables["musbil"];
            adtr.Dispose();
            bag.Close();
        }
        public void stoklistele()
        {
            bag.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From stokbil WHERE Adet>0 ", bag);
            adtr.Fill(dtst, "stokbil");
            frm4.dataGridView1.DataSource = dtst.Tables["stokbil"];
            adtr.Dispose();
            bag.Close();
        }
        public void urunlistele()
        {
            bag.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From stokbil WHERE Adet>0", bag);
            adtr.Fill(dtst, "stokbil");
            dataGridView2.DataSource = dtst.Tables["stokbil"];
            adtr.Dispose();
            bag.Close();
        }
        public void sepetlistele()
        {
            bag.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From sepet ", bag);
            adtr.Fill(dtst, "sepet");
            dataGridView3.DataSource = dtst.Tables["sepet"];
            adtr.Dispose();
            bag.Close();
        }
        public void satislistele()
        {
            bag.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From satisbil where Tarih='"+DateTime.Now.ToShortDateString()+"'", bag);
            adtr.Fill(dtst, "satisbil");
            dataGridView4.DataSource = dtst.Tables["satisbil"];
            frm5.dataGridView1.DataSource = dtst.Tables["satisbil"];
            adtr.Dispose();
            bag.Close();
        }
        public void urunad()
        {
            bag.Open();
            kmt.Connection = bag;
            kmt.CommandText = "Select * from stokbil";
            OleDbDataReader oku;
            oku = kmt.ExecuteReader();
            while (oku.Read())
            {
                frm4.comboBox1.Items.Add(oku[0].ToString());
                frm4.comboBox2.Items.Add(oku[1].ToString());
                frm4.comboBox3.Items.Add(oku[2].ToString());
            }
            bag.Close();
            oku.Dispose();
        }
        public void barkodkontrol()
        {
            durum = false;
            bag.Open();
            kmt.Connection = bag;
            kmt.CommandText = "Select BarkodNo from stokbil";
            OleDbDataReader oku;
            oku = kmt.ExecuteReader();
            while (oku.Read())
            {

                if (frm4.comboBox1.Text == oku[0].ToString()) durum = true;

            }
            bag.Close();
            oku.Dispose();
        }
        public void nokontrol()
        {
            durum = false;
            bag.Open();
            kmt.Connection = bag;
            kmt.CommandText = "Select MusteriNo,TcKimlik from musbil";
            OleDbDataReader oku;
            oku = kmt.ExecuteReader();
            while (oku.Read())
            {

                if (frm2.textBox1.Text == oku[0].ToString() || frm2.textBox2.Text == oku[1].ToString()) durum = true;

            }
            bag.Close();
            oku.Dispose();
        }
        private void btnMusteriKayit_Click(object sender, EventArgs e)
        {
            frm2.ShowDialog();
        }
        public void filter()
        {
            bag.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From satisbil WHERE Tarih='" + frm5.dateTimePicker1.Text + "'", bag);
            adtr.Fill(dtst, "satisbil");
            frm5.dataGridView1.DataSource = dtst.Tables["satisbil"];
            adtr.Dispose();
            bag.Close();
        }
        public void kasapara()
        {

            bag.Open();
            kmt.Connection = bag;
            kmt.CommandText = "SELECT SUM(Fiyat) FROM satisbil Where Tarih='" + frm5.dateTimePicker1.Text + "'";
            toplam = kmt.ExecuteScalar().ToString() + "  TL";
            frm5.label4.Text = toplam.ToString();
            kmt.Dispose();
            bag.Close();        
        }
       

        private void Form1_Load(object sender, EventArgs e)
        {
            satislistele();
            listele();
            urunlistele();
            sepetlistele();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView2.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView3.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView4.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView1.Columns[0].HeaderText = "Müşteri No";
            dataGridView1.Columns[1].HeaderText = "Tc Kimlik";
            dataGridView1.Columns[2].HeaderText = "Adı";
            dataGridView1.Columns[3].HeaderText = "Soyadı";         
            dataGridView1.Columns[0].Width = 35;
            dataGridView1.Columns[1].Width = 75;
            dataGridView1.Columns[2].Width = 75;
            dataGridView1.Columns[4].Width = 80;
            dataGridView2.Columns[0].HeaderText = "Barkod No";
            dataGridView2.Columns[1].HeaderText = "Ürün Adı";            
            dataGridView2.Columns[0].Width = 45;
            dataGridView2.Columns[1].Width = 80;
            dataGridView2.Columns[2].Width = 65;
            dataGridView2.Columns[3].Width = 50;
            dataGridView3.Columns[0].HeaderText = "Müşteri No";
            dataGridView3.Columns[1].HeaderText = "Ürün Adı";
            dataGridView3.Columns[0].Width = 60;
            
            dataGridView4.Columns[0].HeaderText = "Müşteri No";
            dataGridView4.Columns[1].HeaderText = "Ürün Adı";
            dataGridView4.Columns[0].Width = 45;
            dataGridView4.Columns[1].Width = 80;
            dataGridView4.Columns[2].Width = 55;
            dataGridView4.Columns[3].Width = 65;
            dataGridView4.Columns[4].Width = 45;
            label6.Text = "0  TL";            
        }

        private void btnMusteriDuzenle_Click(object sender, EventArgs e)
        {
            frm3.ShowDialog();
        }

        private void btnStok_Click(object sender, EventArgs e)
        {
            dtst.Tables["stokbil"].Clear();
            frm4.ShowDialog();
            
            
        }
        
        private void btnKasa_Click(object sender, EventArgs e)
        {
            bag.Open();
            kmt.Connection = bag;
            kmt.CommandText = "SELECT SUM(Fiyat) FROM satisbil Where Tarih='" + DateTime.Now.ToShortDateString() + "'";
            toplam = kmt.ExecuteScalar().ToString() + "  TL";
            frm5.label4.Text = toplam.ToString();
            kmt.Dispose();
            bag.Close();       
            frm5.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            frm3.textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            frm3.textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            frm3.textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            frm3.textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            frm3.textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            frm3.textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void btnSepetEkle_Click(object sender, EventArgs e)
        {
            try
            {
                bag.Open();
                kmt.Connection = bag;
                kmt.CommandText = "INSERT INTO sepet(MusteriNo,UrunAd,Fiyat) VALUES ('" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "','" + dataGridView2.CurrentRow.Cells[1].Value.ToString() + "','" + dataGridView2.CurrentRow.Cells[2].Value.ToString() + "') ";
                kmt.ExecuteNonQuery();
                kmt.CommandText = "INSERT INTO satisbil(MusteriNo,UrunAd,Fiyat,Tarih,Saat) VALUES ('" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "','" + dataGridView2.CurrentRow.Cells[1].Value.ToString() + "','" + dataGridView2.CurrentRow.Cells[2].Value.ToString() + "','" + DateTime.Now.ToShortDateString() + "', '" + DateTime.Now.ToShortTimeString() + "') ";
                kmt.ExecuteNonQuery();
                kmt.CommandText = "UPDATE stokbil SET Adet=Adet-1 WHERE BarkodNo='" + dataGridView2.CurrentRow.Cells[0].Value.ToString() + "'";
                kmt.ExecuteNonQuery();
                kmt.CommandText = "SELECT SUM(Fiyat) FROM sepet ";
                toplam = kmt.ExecuteScalar().ToString() + "  TL";
                label6.Text = toplam.ToString();
                kmt.Dispose();
                bag.Close();
                dtst.Tables["stokbil"].Clear();
                stoklistele();
                dtst.Tables["sepet"].Clear();
                sepetlistele();
            }
            catch
            {
                ;
            }
        }
        private void btnSatis_Click(object sender, EventArgs e)
        {
            try
            {
                bag.Open();
                kmt.Connection = bag;
                kmt.CommandText = "DELETE * From sepet";
                kmt.ExecuteNonQuery();
                kmt.Dispose();
                bag.Close();
                dtst.Tables["sepet"].Clear();
                sepetlistele();
                dtst.Tables["satisbil"].Clear();
                satislistele();
                label6.Text = "0  TL";
            }
            catch
            {
                ;
            }
        }

        private void txtAraMusNo_TextChanged(object sender, EventArgs e)
        {
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From musbil", bag);
            if (txtAraMusNo.Text == "")
            {
                kmt.Connection = bag;
                kmt.CommandText = "Select * from musbil";
                adtr.SelectCommand = kmt;
                adtr.Fill(dtst, "musbil");
            }
            if (Convert.ToBoolean(bag.State) == false)
            {
                bag.Open();
            }
            adtr.SelectCommand.CommandText = " Select * From musbil" +
                 " where(MusteriNo like '%" + txtAraMusNo.Text + "%' )";
            dtst.Tables["musbil"].Clear();
            adtr.Fill(dtst, "musbil");
            bag.Close();     
        }

        private void txtAraTcKimlik_TextChanged(object sender, EventArgs e)
        {
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From musbil", bag);
            if (txtAraTcKimlik.Text == "")
            {
                kmt.Connection = bag;
                kmt.CommandText = "Select * from musbil";
                adtr.SelectCommand = kmt;
                adtr.Fill(dtst, "musbil");
            }
            if (Convert.ToBoolean(bag.State) == false)
            {
                bag.Open();
            }
            adtr.SelectCommand.CommandText = " Select * From musbil" +
                 " where(TcKimlik like '%" + txtAraTcKimlik.Text + "%' )";
            dtst.Tables["musbil"].Clear();
            adtr.Fill(dtst, "musbil");
            bag.Close();     
        }

        private void txtAraAd_TextChanged(object sender, EventArgs e)
        {
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From musbil", bag);
            if (txtAraAd.Text == "")
            {
                kmt.Connection = bag;
                kmt.CommandText = "Select * from musbil";
                adtr.SelectCommand = kmt;
                adtr.Fill(dtst, "musbil");
            }
            if (Convert.ToBoolean(bag.State) == false)
            {
                bag.Open();
            }
            adtr.SelectCommand.CommandText = " Select * From musbil" +
                 " where(Ad like '%" + txtAraAd.Text + "%' )";
            dtst.Tables["musbil"].Clear();
            adtr.Fill(dtst, "musbil");
            bag.Close();    
        }

        private void txtAraSoyad_TextChanged(object sender, EventArgs e)
        {
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From musbil", bag);
            if (txtAraSoyad.Text == "")
            {
                kmt.Connection = bag;
                kmt.CommandText = "Select * from musbil";
                adtr.SelectCommand = kmt;
                adtr.Fill(dtst, "musbil");
            }
            if (Convert.ToBoolean(bag.State) == false)
            {
                bag.Open();
            }
            adtr.SelectCommand.CommandText = " Select * From musbil" +
                 " where(Soyad like '%" + txtAraSoyad.Text + "%' )";
            dtst.Tables["musbil"].Clear();
            adtr.Fill(dtst, "musbil");
            bag.Close();    
        }

        private void txtBarkodNo_TextChanged(object sender, EventArgs e)
        {
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From stokbil", bag);
            if (txtBarkodNo.Text == "")
            {
                kmt.Connection = bag;
                kmt.CommandText = "Select * from stokbil ";
                adtr.SelectCommand = kmt;
                adtr.Fill(dtst, "stokbil");
            }
            if (Convert.ToBoolean(bag.State) == false)
            {
                bag.Open();
            }
            adtr.SelectCommand.CommandText = " Select * From stokbil " +
                 " where(BarkodNo like '%" + txtBarkodNo.Text + "%' And Adet>0)";
            dtst.Tables["stokbil"].Clear();
            adtr.Fill(dtst, "stokbil");
            bag.Close();    
        }

        private void txtUrunAd_TextChanged(object sender, EventArgs e)
        {
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From stokbil", bag);
            if (txtUrunAd.Text == "")
            {
                kmt.Connection = bag;
                kmt.CommandText = "Select * from stokbil";
                adtr.SelectCommand = kmt;
                adtr.Fill(dtst, "stokbil");
            }
            if (Convert.ToBoolean(bag.State) == false)
            {
                bag.Open();
            }
            adtr.SelectCommand.CommandText = " Select * From stokbil" +
                 " where(UrunAd like '%" + txtUrunAd.Text + "%' And Adet>0)";
            dtst.Tables["stokbil"].Clear();
            adtr.Fill(dtst, "stokbil");
            bag.Close();    
        }

        private void txtAraMusNoSatis_TextChanged(object sender, EventArgs e)
        {
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From satisbil", bag);
            if (txtAraMusNoSatis.Text == "")
            {
                kmt.Connection = bag;
                kmt.CommandText = "Select * from satisbil ";
                adtr.SelectCommand = kmt;
                adtr.Fill(dtst, "satisbil");
            }
            if (Convert.ToBoolean(bag.State) == false)
            {
                bag.Open();
            }
            adtr.SelectCommand.CommandText = " Select * From satisbil " +
                 " where(MusteriNo like '%" + txtAraMusNoSatis.Text + "%' And Tarih='" + DateTime.Now.ToShortDateString() + "')";
            dtst.Tables["satisbil"].Clear();
            adtr.Fill(dtst, "satisbil");
            bag.Close();    
        }

        private void txtUrunAdSatis_TextChanged(object sender, EventArgs e)
        {
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From satisbil", bag);
            if (txtUrunAdSatis.Text == "")
            {
                kmt.Connection = bag;
                kmt.CommandText = "Select * from satisbil ";
                adtr.SelectCommand = kmt;
                adtr.Fill(dtst, "satisbil");
            }
            if (Convert.ToBoolean(bag.State) == false)
            {
                bag.Open();
            }
            adtr.SelectCommand.CommandText = " Select * From satisbil " +
                 " where(UrunAd like '%" + txtUrunAdSatis.Text + "%' And Tarih='" + DateTime.Now.ToShortDateString() + "')";
            dtst.Tables["satisbil"].Clear();
            adtr.Fill(dtst, "satisbil");
            bag.Close();    
        }

        private void btnMusteriSil_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult cevap;
                cevap = MessageBox.Show("Kaydı silmek istediğinizden eminmisiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (cevap == DialogResult.Yes)
                {
                    bag.Open();
                    kmt.Connection = bag;
                    kmt.CommandText = "DELETE from musbil WHERE MusteriNo='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "' AND TcKimlik='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "'";
                    kmt.ExecuteNonQuery();
                    kmt.Dispose();
                    bag.Close();
                    dtst.Tables["musbil"].Clear();
                    listele();
                }
            }
            catch
            {
                ;
            }
        }
        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }        
    }
}
