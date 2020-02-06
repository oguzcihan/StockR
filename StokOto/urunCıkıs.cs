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
    public partial class urunCıkıs : Form
    {
        baglanti con = new baglanti();
        SqlConnection baglanti;
        SqlDataAdapter da;
        DataTable dt;
        public urunCıkıs()
        {
            InitializeComponent();
            baglanti = new SqlConnection(con.adres);
            dt = new DataTable();
        }

        private void urunCıkıs_Load(object sender, EventArgs e)
        {
            GirisForm info = new GirisForm();
            label9.Text = GirisForm.ykullaniciAdi;
            listele();
            isim();
            load();
            if (textBox1.Text == "")
            {
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
            }
            comboBox1.SelectedIndex = 0;
        }
         
        private void button1_Click(object sender, EventArgs e)
        {
            cıkıs();
        }

        int a, b, c;
        public void cıkıs()
        {
            if (textBox1.Text == "" || comboBox1.Text == "" || textBox7.Text == "" || textBox8.Text == "") { MessageBox.Show("Tüm alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else if (comboBox1.SelectedIndex == 0)
            {
                MessageBox.Show("Geçerli bir firma seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                baglanti.Open();
                SqlCommand kmt = new SqlCommand("select * from tabloStok where urunKodu='" + textBox1.Text + "'", baglanti);
                SqlDataReader oku = kmt.ExecuteReader();
                if (oku.Read())
                {

                    if (textBox3.Text == "")
                    {
                        MessageBox.Show("Verilen miktar boş kalamaz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);



                    }
                   
                    else
                    {
                        a = Convert.ToInt32(textBox3.Text);
                        b = Convert.ToInt32(oku["miktar"]);
                        oku.Close();
                        c = b - a;
                        label11.Text = c.ToString();
                        if (c < 0)
                        {
                            label11.Text = "";
                            MessageBox.Show("Stokta yeterli ürün yok", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            DialogResult k;
                            k = MessageBox.Show("Ürün stoktan düşülsün mü?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (k == DialogResult.Yes)
                            {

                                guncel();

                                SqlCommand komut = new SqlCommand("insert into urunCikis (urunKodu,urunAdi,teslimAlan, verilenMiktar,birimFiyati,depoKalan,kullanici,tarihSaat,ozelNot,kullanımAlanı,firmaAdi) values (@kod,@ad,@teslim,@verMiktar,@brFiyat,@depoKalan,@kullanici,@tarih,@not,@alan,@firma)", baglanti);
                                komut.Parameters.AddWithValue("@kod", textBox1.Text);
                                komut.Parameters.AddWithValue("@ad", textBox2.Text);
                                komut.Parameters.AddWithValue("@teslim", textBox7.Text);
                                komut.Parameters.AddWithValue("@verMiktar", textBox3.Text);
                                komut.Parameters.AddWithValue("@brFiyat", textBox4.Text);
                                komut.Parameters.AddWithValue("@depoKalan", label11.Text);
                                komut.Parameters.AddWithValue("@kullanici", label9.Text);
                                komut.Parameters.AddWithValue("@tarih", label10.Text);
                                komut.Parameters.AddWithValue("@not", textBox6.Text);
                                komut.Parameters.AddWithValue("@alan", textBox8.Text);
                                komut.Parameters.AddWithValue("@firma", comboBox1.Text);
                                komut.ExecuteNonQuery();
                                listele();
                                komut.Dispose();
                                MessageBox.Show("Ürün stoktan düşüldü", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                textBox1.Clear();
                                textBox2.Clear();
                                textBox3.Clear();
                                textBox4.Clear();
                                textBox6.Clear();
                                textBox7.Clear();
                                textBox8.Clear();
                                comboBox1.SelectedIndex = 0;

                            }
                        }


                    }

                }

                baglanti.Close();
            }
        }
        public void load()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select firmaAdi from firmaCikis", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                
                comboBox1.Items.Add(oku["firmaAdi"].ToString());
                

            }
            oku.Close();
            baglanti.Close();
        }
        public void guncel()
        {
            SqlCommand command = new SqlCommand("update tabloStok set miktar='" + c + "' where urunKodu='" + textBox1.Text + "'", baglanti);
            command.ExecuteNonQuery();
            command.Dispose();

        }
        public void listele()
        {
            if (baglanti.State == ConnectionState.Open)
                baglanti.Close();
            dt.Clear();

            baglanti.Open();
            SqlCommand com = new SqlCommand("select*from urunCikis", baglanti);
            da = new SqlDataAdapter(com);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            com.Dispose();
            baglanti.Close();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label10.Text = DateTime.Now.ToString();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

     

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            oku();

        }
        ///////////////////////////////////////////7
        urunSec frm1 = new urunSec();
        private void button2_Click(object sender, EventArgs e)
        {

            if (frm1 == null)
            {
                frm1 = new urunSec();
            }
            frm1.UrunSecildi += Frm1_UrunSecildi;
            frm1.ShowDialog();

        }       
        ////////////////////////////////////////////    
        private void Frm1_UrunSecildi()
        {
            textBox1.Text = frm1.SecilenUrun;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form ac = new firmaKayıt();
            ac.ShowDialog();
        }

   

        ////////////////////////////////////////////////////        
        public void oku()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from tabloStok where urunKodu=@kod", baglanti);
            komut.Parameters.AddWithValue("@kod", textBox1.Text);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                textBox2.Text = oku["urunAdi"].ToString();
                textBox4.Text = oku["birimFiyati"].ToString();
            }
            oku.Close();
            baglanti.Close();

        }
        public void isim()
        {
            dataGridView1.Columns[0].HeaderText = "Ürün Kodu";
            dataGridView1.Columns[1].HeaderText = "Ürün Adı";
            dataGridView1.Columns[2].HeaderText = "Birim Fiyatı";
            dataGridView1.Columns[3].HeaderText = "Verilen Miktar";
            dataGridView1.Columns[4].HeaderText = "Depo Kalan";
            dataGridView1.Columns[5].HeaderText = "Firma Adı";
            dataGridView1.Columns[6].HeaderText = "Teslim Alan";
            dataGridView1.Columns[7].HeaderText = "Kullanım Alanı";
            dataGridView1.Columns[8].HeaderText = "Özel Notlar";
            dataGridView1.Columns[9].HeaderText = "Kullanıcı";
            dataGridView1.Columns[10].HeaderText = "Tarih/Saat";

            dataGridView1.Columns[10].Width = 130;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[5].Width = 130;
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[8].Width = 150;
            dataGridView1.Columns[7].Width = 135;

        }



    }
}
