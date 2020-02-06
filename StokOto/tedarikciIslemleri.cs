using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StokOto
{
    public partial class tedarikciIslemleri : Form
    {
        baglanti con = new baglanti();
        SqlConnection baglanti;
        public tedarikciIslemleri()
        {
            baglanti = new SqlConnection(con.adres);
            InitializeComponent();
        }

      
        SqlDataAdapter da;
        DataTable dt = new DataTable();


        private void tedarikciDuzenle_Load(object sender, EventArgs e)
        {

            ToolTip Aciklama = new ToolTip();
            Aciklama.SetToolTip(button2, "Rastgele Numara Ver");
            Aciklama.ToolTipTitle = "BİLGİ";
            Aciklama.ToolTipIcon = ToolTipIcon.Warning;
            Aciklama.IsBalloon = true;
            Aciklama.SetToolTip(button6, "Yeni Tedarikçi Ekle");
            listele();

            GirisForm k = new GirisForm();
            label9.Text = GirisForm.ykullaniciAdi;
            random();
           ///////////////////////////////////////////////////////
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Tedarikci Adı";
            dataGridView1.Columns[2].HeaderText = "Adres";
            dataGridView1.Columns[3].HeaderText = "Tel No";
            dataGridView1.Columns[4].HeaderText = "Yetkili";
            dataGridView1.Columns[5].HeaderText = "Mail";
            dataGridView1.Columns[6].HeaderText = "Web Adres";
            dataGridView1.Columns[7].HeaderText = "Vergi No";
            dataGridView1.Columns[8].HeaderText = "Kullanıcı";
            dataGridView1.Columns[9].HeaderText = "Tarih/Saat";
       
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label8.Text = DateTime.Now.ToString();
        }
        string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
        bool  durum1,durum2, durum3, durum5, durum6, durum7;
        public void email()
        {
           
            if (Regex.IsMatch(textBox5.Text, pattern))
            {
                errorProvider1.Clear();
            }
            else
            {
               errorProvider1.SetError(this.textBox5, "example@gmail.com şeklinde olmalıdır.");
                return;
            }
        }
        public void a()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select*from tedarikciKayit where tedarikciAdi=@ad", baglanti);
            komut.Parameters.AddWithValue("@ad", textBox1.Text);
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                durum1 = false;
            }
            else { durum1 = true; }
            baglanti.Close();
            komut.Dispose();
        }
        public void b()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select*from tedarikciKayit where adres=@adres", baglanti);
            komut.Parameters.AddWithValue("@adres", textBox2.Text);
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                durum2 = false;
            }
            else { durum2 = true; }
            baglanti.Close();
            komut.Dispose();
        }
        public void c()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select*from tedarikciKayit where telefonNo=@telno", baglanti);
            komut.Parameters.AddWithValue("@telno", maskedTextBox1.Text);
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                durum3 = false;
            }
            else { durum3 = true; }
            baglanti.Close();
            komut.Dispose();
        }    
        public void E()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select*from tedarikciKayit where mail=@mail", baglanti);
            komut.Parameters.AddWithValue("@mail", textBox5.Text);
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                durum5 = false;
            }
            else { durum5 = true; }
            baglanti.Close();
            komut.Dispose();
        }
        public void f()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select*from tedarikciKayit where webSite=@web", baglanti);
            komut.Parameters.AddWithValue("@web", textBox6.Text);
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                durum6 = false;
            }
            else { durum6 = true; }
            baglanti.Close();
            komut.Dispose();
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                dt.Clear();
                SqlDataAdapter adap = new SqlDataAdapter("select*from tedarikciKayit where tedarikciAdi like'" + textBox3.Text + "%'", baglanti);
                adap.Fill(dt);
                dataGridView1.DataSource = dt;
                baglanti.Close();
            }
            catch (Exception hata) { MessageBox.Show(hata.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
        }

        public void g()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select*from tedarikciKayit where vergiNo=@no", baglanti);
            komut.Parameters.AddWithValue("@no", textBox7.Text);
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                durum7 = false;
            }
            else { durum7 = true; }
            baglanti.Close();
            komut.Dispose();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            
            try
            {
                a(); b(); c(); f(); g(); E();
                if (durum1||durum2||durum3||durum5||durum6||durum7)
                {
                    
                    if (textBox1.Text == "" || textBox2.Text == "" || maskedTextBox1.Text == "" || textBox4.Text == "" ||  textBox6.Text == "" || textBox7.Text == "")
                    {
                        MessageBox.Show("Lütfen düzenlenecek tedarikci seçiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (textBox5.Text =="")
                    {
                        email();
                    }
                    else
                    {
                        
                        DialogResult o;
                        o = MessageBox.Show(lblrnd.Text + " Nolu ürünü düzenlemek istiyor musunuz? ", "ONAY", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (o == DialogResult.Yes)
                        {
                            baglanti.Open();

                            SqlCommand komut = new SqlCommand("UPDATE tedarikciKayit SET tedarikciAdi=@ad,adres=@adres,telefonNo=@telNo,yetkiliAdSoyad=@yad,mail=@mail,webSite=@web,vergiNo=@vno,kullanici=@kullanici,tarihSaat=@tarih where tedarikciID=@Id", baglanti);
                            komut.Parameters.AddWithValue("@ad", textBox1.Text);
                            komut.Parameters.AddWithValue("@adres", textBox2.Text);
                            komut.Parameters.AddWithValue("@telNo", maskedTextBox1.Text);
                            komut.Parameters.AddWithValue("@yad", textBox4.Text);
                            komut.Parameters.AddWithValue("@mail", textBox5.Text);
                            komut.Parameters.AddWithValue("@web", textBox6.Text);
                            komut.Parameters.AddWithValue("@vno", textBox7.Text);
                            komut.Parameters.AddWithValue("@kullanici", label9.Text);
                            komut.Parameters.AddWithValue("@tarih", label8.Text);
                            komut.Parameters.AddWithValue("@Id", lblrnd.Text);
                            komut.ExecuteNonQuery();
                            komut.Dispose();
                            baglanti.Close();
                            MessageBox.Show(lblrnd.Text + " No'lu ürün düzenlenmiştir.", "BİLGİ",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            temizle();
                            random();
                        }
                    }
                }
                else { MessageBox.Show("Zaten kayıtlı tedarikci","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning); }
            }

            catch (Exception hata) { MessageBox.Show(hata.Message.ToString()); }

            finally{ listele();   }
            
            

        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        void listele()
        {
            if (baglanti.State == ConnectionState.Open)
                baglanti.Close();
            dt.Clear();
            try
            {
                baglanti.Open();
                SqlCommand com = new SqlCommand("select*from tedarikciKayit", baglanti);
                da = new SqlDataAdapter(com);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                baglanti.Close();
                com.Dispose();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message.ToString(),"ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lblrnd.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            maskedTextBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
           

        }

        private void button6_Click(object sender, EventArgs e)
        {
            temizle();
            random();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            kaydet();
        }

        private void button2_Click(object sender, EventArgs e)
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
                    SqlCommand ole = new SqlCommand("delete from tedarikciKayit where tedarikciID=@no", baglanti);
                    ole.Parameters.AddWithValue("@no", no);
                    ole.ExecuteNonQuery();
                    ole.Dispose();
                    baglanti.Close();
                    temizle();
                    random();
                }
            }
            catch (Exception hata) { MessageBox.Show(hata.Message.ToString(), "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
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
                        MessageBox.Show(lblrnd.Text + " ID'li tedarikçi eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        listele();
                        temizle();
                        random();

                    }
                }
            }
            catch (Exception hata) { MessageBox.Show(hata.Message.ToString(), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
            baglanti.Close();
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
    }
}
