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
    public partial class GirisForm : Form
    {
        baglanti con = new baglanti();
        SqlConnection baglanti;
        public GirisForm()
        {
            baglanti = new SqlConnection(con.adres);
            InitializeComponent();
        }
        

      
        public static string ykullaniciAdi;
        public static string unvan;
        bool durum = false;

        private void button2_Click(object sender, EventArgs e)
        {

            Application.Exit();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult k;
            k = MessageBox.Show("Kapatmak istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (k == DialogResult.Yes) { Application.Exit(); }

        }




        private void button3_Click(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Minimized;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          

            if (textBox1.Text == "")
            {
                textBox2.Clear();

                textBox4.Clear();
            }
            try
            {
                baglanti.Open();
                SqlCommand k = new SqlCommand("select*from kullaniciBilgi where kullaniciAdi=@ad", baglanti);
                k.Parameters.AddWithValue("@ad", textBox1.Text);
                SqlDataReader oku = k.ExecuteReader();
                while (oku.Read())
                {

                    textBox4.Text = oku["unvan"].ToString();

                }
                oku.Close();
                baglanti.Close();
            }
            catch (Exception hata) { MessageBox.Show(hata.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }



        private void button1_Click(object sender, EventArgs e)
        {
            giris();
        }

        public void yetki()
        {
            Anamenu n = new Anamenu();
            n.menuStrip2.Visible = true;

        }
        public void giris()
        {
            //kayıt 
            try
            {
                if (textBox1.Text == "" && textBox2.Text == "") { MessageBox.Show("Giriş yapmak için tüm boşlukları doldurunuz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                else
                {

                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("select* from kullaniciBilgi where kullaniciAdi=@ad", baglanti);
                    komut.Parameters.AddWithValue("@ad", textBox1.Text);
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        if (oku["kullaniciAdi"].ToString() == textBox1.Text && oku["sifre"].ToString() == textBox2.Text && oku["unvan"].ToString() == "YÖNETİCİ")
                        {

                            ykullaniciAdi = textBox1.Text;
                            unvan = textBox4.Text;
                            this.Hide();
                            Form frm1 = new Anamenu();
                            frm1.Show();
                            break;

                        }
                        else if (oku["kullaniciAdi"].ToString() == textBox1.Text && oku["sifre"].ToString() == textBox2.Text && oku["unvan"].ToString() == "PERSONEL")
                        {


                            ykullaniciAdi = textBox1.Text;
                            unvan = textBox4.Text;
                            this.Hide();
                            Form frm1 = new Anamenu();
                            frm1.Show();

                            break;

                        }
                        else { MessageBox.Show("Giriş bilgilerinizi kontrol ediniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

                    }

                    baglanti.Close();
                }
            }
            catch (Exception hata) { MessageBox.Show(hata.Message.ToString(), "EROR", MessageBoxButtons.OK, MessageBoxIcon.Stop); }

        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';

        }



        private void GirisForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //x tuşu kapatır.
            DialogResult g = MessageBox.Show("Uygulamayı kapatmak istiyor musunuz ? ", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (g == DialogResult.No)
            {
                e.Cancel = true;
                return;

            }
            Environment.Exit(1);

        }

        private void GirisForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void GirisForm_Load(object sender, EventArgs e)
        {

        }
    }
}
