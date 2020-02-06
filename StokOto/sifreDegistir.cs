using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StokOto
{
    public partial class sifreDegistir : Form
    {
        baglanti con = new baglanti();
        SqlConnection baglanti;
        public sifreDegistir()
        {
            baglanti = new SqlConnection(con.adres);
            InitializeComponent();
        }       
        private void sifreDegistir_Load(object sender, EventArgs e)
        {
            GirisForm g = new GirisForm();
            textBox1.Text = GirisForm.ykullaniciAdi;
        }
        bool durum;
        public void m()
        {
            
            try
            {
                baglanti.Open();
                SqlCommand k = new SqlCommand("select*from kullaniciBilgi where sifre=@eski", baglanti);
                k.Parameters.AddWithValue("@eski", textBox2.Text);
                SqlDataReader oku = k.ExecuteReader();
                if (oku.Read())
                {
                    durum = true;
                }
                else
                    durum = false;
                oku.Close();
                baglanti.Close();
            }
            catch (Exception hata) { MessageBox.Show(hata.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "")
                {
                    MessageBox.Show("Tüm boşluklar doldurulmalıdır", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (textBox3.Text =="") { MessageBox.Show("Yeni şifre alanı boş kalamaz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                else
                {
                    m();
                    if (durum)
                    {
                        baglanti.Open();
                        SqlCommand komut = new SqlCommand("update kullaniciBilgi set sifre=@sifre where kullaniciAdi=@ad", baglanti);
                        komut.Parameters.AddWithValue("@sifre", textBox3.Text);
                        komut.Parameters.AddWithValue("@ad", textBox1.Text);
                        komut.ExecuteNonQuery();
                        komut.Dispose();
                        baglanti.Close();
                        MessageBox.Show("Şifre değiştirildi.", "Onay", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                    }
                    else MessageBox.Show("Eski şifreniz hatalı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }catch(Exception hata) { MessageBox.Show(hata.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox3.PasswordChar = '\0';
            }
            else
                textBox3.PasswordChar = '*';
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.PasswordChar = '*';
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
