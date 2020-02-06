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
    public partial class kullaniciIslemleri : Form
    {
        baglanti con = new baglanti();
        SqlConnection baglanti;
        public kullaniciIslemleri()
        {
            baglanti = new SqlConnection(con.adres);
            InitializeComponent();
        }

        private void YyoneticiDuzenle_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 2;
            
            ToolTip Aciklama = new ToolTip();
            Aciklama.SetToolTip(button2, "Rastgele Numara Ver");
            Aciklama.ToolTipTitle = "BİLGİ";
            Aciklama.ToolTipIcon = ToolTipIcon.Warning;
            Aciklama.IsBalloon = true;
            Aciklama.SetToolTip(pictureBox3, "Kullanıcı adı ile arama yapmanız gerekmektedir.");
            Aciklama.SetToolTip(button6, "Yeni Kullanıcı Ekle");

            textBox1.MaxLength = 15;

            listele();
            dataGridView1.Columns[0].HeaderText = "Kullanıcı No";
            dataGridView1.Columns[1].HeaderText = "Kullanıcı Adı";
            dataGridView1.Columns[2].HeaderText = "Şifre";
            dataGridView1.Columns[3].HeaderText = "Ünvan";
            dataGridView1.Columns[2].Visible = false;

        }
        
        SqlDataAdapter da;
        DataTable dt = new DataTable();
        public const int minlen = 3;
        public const int maxlen = 8;

        public void sifre()
        {
            if (textBox2.Text.Length > minlen && textBox2.Text.Length < maxlen)
            {
                errorProvider1.Clear();

            }
            else
            {
                MessageBox.Show("Şifre 3 veya 8 karakter giriniz. ", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                errorProvider1.SetError(this.textBox2, "Şifre 3 veya 8 karakterli olabilir");
                return;
            }
        }
    
        public void duzenle()
        {
            try
            {

                if (textBox1.Text == "") { MessageBox.Show("Tablodan seçim yapmalısınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                else if (comboBox1.SelectedIndex == 2) { MessageBox.Show("Geçerli bir unvan seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                else
                {



                    DialogResult p;
                    p = MessageBox.Show("Düzenlensin mi?", "Onay", MessageBoxButtons.YesNo,
                         MessageBoxIcon.Question);

                    if (p == DialogResult.Yes)
                    {
                        baglanti.Open();
                        SqlCommand komut = new SqlCommand("update kullaniciBilgi set kullaniciAdi=@ad,sifre=@sifre,unvan=@unvan where kullaniciNo=@no", baglanti);
                        komut.Parameters.AddWithValue("@no", textBox4.Text);
                        komut.Parameters.AddWithValue("@ad", textBox1.Text);
                        komut.Parameters.AddWithValue("@sifre", textBox2.Text);
                        komut.Parameters.AddWithValue("@unvan", comboBox1.Text);
                        komut.ExecuteNonQuery();
                        listele();
                        baglanti.Close();
                        MessageBox.Show(textBox1.Text + " Adlı kullanıcı düzenlendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        komut.Dispose();
                        temizle();
                    }


                }
            }
            catch (Exception hata) { MessageBox.Show(hata.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


        private void textBox4_TextChanged(object sender, EventArgs e)
        {



        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';


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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                textBox4.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                comboBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();

            }
            catch (Exception hata) { MessageBox.Show(hata.Message.ToString(), "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
        }
        void listele()
        {
            if (baglanti.State == ConnectionState.Open)
                baglanti.Close();
            dt.Clear();
            try
            {
                baglanti.Open();
                SqlCommand com = new SqlCommand("select*from kullaniciBilgi", baglanti);
                da = new SqlDataAdapter(com);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                baglanti.Close();
                com.Dispose();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message.ToString(), "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            //arama butonu
            try
            {
                baglanti.Open();
                dt.Clear();
                SqlDataAdapter adap = new SqlDataAdapter("select*from kullaniciBilgi where kullaniciAdi like'" + textBox5.Text + "%'", baglanti);
                adap.Fill(dt);
                dataGridView1.DataSource = dt;
                baglanti.Close();
            }
            catch (Exception hata) { MessageBox.Show(hata.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int sayi = rnd.Next(1, 500);
            textBox4.Text = sayi.ToString();
        }
        bool durum2;
        public void b()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select* from kullaniciBilgi where kullaniciAdi=@ad", baglanti);
            komut.Parameters.AddWithValue("@ad", textBox1.Text);
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                durum2 = false;
            }
            else { durum2 = true; }
            komut.Dispose();
            baglanti.Close();
        }
        public void kayıt()
        {
            try
            {
                b();
                if (durum2)
                {
                    if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "" || textBox4.Text == "")
                    { MessageBox.Show("Lütfen boş alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                    else if (comboBox1.SelectedIndex == 2) { MessageBox.Show("Geçerli bir unvan seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                    else
                    {

                        baglanti.Open();
                        DialogResult k;
                        k = MessageBox.Show(textBox1.Text + " Adlı kullanıcı eklensin mi? ", "ONAY", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (k == DialogResult.Yes && baglanti.State == ConnectionState.Open)
                        {

                            SqlCommand komut2 = new SqlCommand("insert into kullaniciBilgi (kullaniciNo,kullaniciAdi,sifre,unvan) values (@no,@ad,@sifre,@unvan)", baglanti);
                            komut2.Parameters.AddWithValue("@no", textBox4.Text);
                            komut2.Parameters.AddWithValue("@ad", textBox1.Text);
                            komut2.Parameters.AddWithValue("@sifre", textBox2.Text);
                            komut2.Parameters.AddWithValue("@unvan", comboBox1.Text);
                            komut2.ExecuteNonQuery();
                            komut2.Dispose();
                            baglanti.Close();
                            MessageBox.Show(textBox1.Text + " Adlı yönetici eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            baglanti.Close();
                            temizle();
                          

                        }
                    }
                }
                else { MessageBox.Show("Kullanıcı adı kullanılıyor.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

            }
            catch (Exception hata) { MessageBox.Show(hata.Message.ToString(), "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void btnKaydet_Click_1(object sender, EventArgs e)
        {
            kayıt();
        }

        private void btnduzenle_Click(object sender, EventArgs e)
        {
            duzenle();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            temizle();
        }
        public void temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox4.Clear();
            comboBox1.SelectedIndex = 2;
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow drow in dataGridView1.SelectedRows)  //Seçili Satırları Silme
            {
                int no = Convert.ToInt32(drow.Cells[0].Value);
                sil(no);
            }
            listele();
        }
        public void sil(int no)
        {
            try
            {
                DialogResult d;
                d = MessageBox.Show("Silmek istiyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    baglanti.Open();
                    SqlCommand ole = new SqlCommand("delete from kullaniciBilgi where kullaniciNo=@no", baglanti);
                    ole.Parameters.AddWithValue("@no", no);
                    ole.ExecuteNonQuery();
                    ole.Dispose();
                    baglanti.Close();
                    temizle();
                }
            }
            catch (Exception hata) { MessageBox.Show(hata.Message.ToString(), "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
    }
}
