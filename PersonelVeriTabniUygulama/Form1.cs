using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-3341JEP\\SQLEXPRESS;Initial Catalog=PersonelVeriTabani;Integrated Security=True");
        void Temizle()
        {
            txt_id.Text = "";
            txt_ad.Text = "";
            txt_soyad.Text = "";
            txt_maas.Text = "";
            radıo_evli.Checked = false;
            radıo_bekar.Checked = false;
             txt_meslek.Text = "";
            txt_ad.Focus();
            txt_sehir.Text = "";

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'personelVeriTabaniDataSet.Tbl_Personel' table. You can move, or remove it, as needed.
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel);

        }

        private void btn_kaydet_Click(object sender, EventArgs e)
        {
        
            
                baglanti.Open();//sql baglantısını actık.
                SqlCommand komutekle = new SqlCommand("insert into Tbl_Personel(PerAd,PerSoyad,PerSehir,PerMaas,PerMeslek,PerDurum) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);//ref noktası olusturarak tabloya ınsert ıslemı yapılacak tablo adı ve hangı tabloya sutunlara eklenecekse parantez ıcıne yaazıp values dedıkten sonra paramter adlarını yazdık.
                komutekle.Parameters.AddWithValue("@p1",txt_ad.Text);
                komutekle.Parameters.AddWithValue("@p2",txt_soyad.Text);
                komutekle.Parameters.AddWithValue("@p3",txt_sehir.Text);
                komutekle.Parameters.AddWithValue("@p4",txt_maas.Text);
                komutekle.Parameters.AddWithValue("@p5",txt_meslek.Text);
                komutekle.Parameters.AddWithValue("@p6", label8.Text);
            //hangı paramtereye degerler nereden gidecek.


            komutekle.ExecuteNonQuery();//ekle sil guncellede kullanılır.
                baglanti.Close();//sql baglantı kapat.
                MessageBox.Show("personel basarıyla eklendi.");
            
        }

        private void btn_listele_Click(object sender, EventArgs e)
        {
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel);
        }

        private void radıo_evli_CheckedChanged(object sender, EventArgs e)
        {
            if (radıo_evli.Checked==true)
            {
                label8.Text = "True";
            }
            
        }

        private void radıo_bekar_CheckedChanged(object sender, EventArgs e)
        {
            if (radıo_bekar.Checked==true)
            {
                label8.Text = "False";
            }
           
        }

        private void btn_temızle_Click(object sender, EventArgs e)
        {
            Temizle();

        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutgüncelle = new SqlCommand("Update Tbl_Personel Set perAd=@a1,PerSoyad=@a2,PerSehir=@a3,PerMaas=@a4,perDurum=@a5,perMeslek=@a6 where Perid=@a7",baglanti);
            komutgüncelle.Parameters.AddWithValue("@a1", txt_ad.Text);
            komutgüncelle.Parameters.AddWithValue("@a2", txt_soyad.Text);
            komutgüncelle.Parameters.AddWithValue("@a3", txt_sehir.Text);
            komutgüncelle.Parameters.AddWithValue("@a4", txt_maas.Text);
            komutgüncelle.Parameters.AddWithValue("@a5", label8.Text);
            komutgüncelle.Parameters.AddWithValue("@a6", txt_meslek.Text);
            komutgüncelle.Parameters.AddWithValue("@a7", txt_id.Text);
            komutgüncelle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("personel basarıyla guncellendi.");
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int seçilen = dataGridView1.SelectedCells[0].RowIndex;
            txt_id.Text = dataGridView1.Rows[seçilen].Cells[0].Value.ToString();
            txt_ad.Text = dataGridView1.Rows[seçilen].Cells[1].Value.ToString();
            txt_soyad.Text = dataGridView1.Rows[seçilen].Cells[2].Value.ToString();
            txt_sehir.Text = dataGridView1.Rows[seçilen].Cells[3].Value.ToString();
            txt_maas.Text = dataGridView1.Rows[seçilen].Cells[4].Value.ToString();
            label8.Text= dataGridView1.Rows[seçilen].Cells[5].Value.ToString();
            txt_meslek.Text= dataGridView1.Rows[seçilen].Cells[6].Value.ToString();
         

        }

        private void label8_TextChanged(object sender, EventArgs e)
        {
            if (label8.Text=="True")
            {
                radıo_evli.Checked = true;
            }
            if (label8.Text=="False")
            {
                radıo_bekar.Checked = true;
            }
        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Delete From Tbl_Personel Where Perid=@k1",baglanti);
            komut.Parameters.AddWithValue("@k1",txt_id.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Basarıyla silindi.");

        }
    }
}
