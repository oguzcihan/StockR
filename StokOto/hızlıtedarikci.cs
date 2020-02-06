using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace StokOto
{
    public partial class hızlıtedarikci : Form
    {
        baglanti con = new baglanti();
        SqlConnection baglanti;
        public ComboBox ComboBox { get; set; }
        public hızlıtedarikci(ComboBox comboBox = null)
        {
            baglanti = new SqlConnection(con.adres);
            InitializeComponent();
            if (comboBox != null)
                ComboBox = comboBox;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            kaydet();
         
        }
        public void kaydet()
        {
            //e-mail
            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (Regex.IsMatch(textBox5.Text, pattern))
            {
                errorProvider1.Clear();
            }
            else
            {
                MessageBox.Show("E-mail hatası.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                errorProvider1.SetError(this.textBox5, "example@gmail.com şeklinde olmalıdır.");
                return;
            }
            //
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select*from tedarikciKayit where tedarikciAdi='" + textBox1.Text + "'", baglanti);
                SqlDataReader oku = komut.ExecuteReader();
                if (oku.Read())
                {
                    MessageBox.Show("Tedarikçi isimleri aynı olamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (textBox1.Text == "")
                    {
                        MessageBox.Show("Tedarikçi adı boş olamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        oku.Close();

                        SqlCommand kayıt = new SqlCommand("insert into tedarikciKayit (tedarikciAdi,adres,telefonNo,yetkiliAdSoyad,mail,webSite,vergiNo,kullanici,tarihSaat,tedarikciID) values (@ad,@adres,@telNo,@yad,@mail,@web,@vno,@kullanici,@tarih,@ID)", baglanti);
                        kayıt.Parameters.AddWithValue("@ad", textBox1.Text);
                        kayıt.Parameters.AddWithValue("@adres", textBox2.Text);
                        kayıt.Parameters.AddWithValue("@telNo", maskedTextBox1.Text);
                        kayıt.Parameters.AddWithValue("@yad", textBox4.Text);
                        kayıt.Parameters.AddWithValue("@mail", textBox5.Text);
                        kayıt.Parameters.AddWithValue("@web", textBox6.Text);
                        kayıt.Parameters.AddWithValue("@vno", textBox7.Text);
                        kayıt.Parameters.AddWithValue("@kullanici", label9.Text);
                        kayıt.Parameters.AddWithValue("@tarih", label8.Text);
                        kayıt.Parameters.AddWithValue("@ID", lblrnd.Text);
                        kayıt.ExecuteNonQuery();
                        kayıt.Dispose();
                        if (ComboBox != null)
                            ComboBox.Items.Add(textBox1.Text);
                        MessageBox.Show(lblrnd.Text + " ID'li tedarikçi eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        baglanti.Close();
                        temizle();
                        random();

                    }
                }
            }
            catch (Exception hata) { MessageBox.Show(hata.Message.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
            
        }
        public void random()
        {
            try
            {
                Random rnd = new Random();
                int sayi = rnd.Next(1, 9999);
                lblrnd.Text = sayi.ToString();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        public void temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            maskedTextBox1.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label8.Text = DateTime.Now.ToString();
        }

        private void hızlıtedarikci_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
