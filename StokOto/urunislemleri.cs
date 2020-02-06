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
using System.Collections;

namespace StokOto
{
    public partial class urunislemleri : Form
    {
        public urunislemleri()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=LENOVO\\SQLEXPRESS;Initial Catalog=StokData;Integrated Security=True");
        SqlDataAdapter da;
        DataTable dt = new DataTable();


        public void random()
        {
            Random rnd = new Random();
            int sayi = rnd.Next(1, 1234567);
            lblKod.Text = sayi.ToString();
        }
        public void temizle()
        {
            txtad.Clear();
            txtfiyat.Clear();
            txtMiktar.Clear();
            txtToplamfiyat.Clear();
            textBox6.Clear();
            cmbDepo.SelectedIndex = 0;
            cmbTedarikci.SelectedIndex = 0;
            cmbtanim.SelectedIndex = 0;
        }
        private void UrunDüzenle_Load(object sender, EventArgs e)
        {
            random();
            cmbDepo.SelectedIndex = 0;
            cmbTedarikci.SelectedIndex = 0;
            cmbtanim.SelectedIndex = 0;

            ToolTip Aciklama = new ToolTip();
            Aciklama.ToolTipTitle = "BİLGİ";
            Aciklama.ToolTipIcon = ToolTipIcon.Warning;
            Aciklama.IsBalloon = true;
            Aciklama.SetToolTip(pictureBox1, "Tablodan ürün seçiniz.");
            Aciklama.SetToolTip(btnyenikayit, "Yeni Kayıt Ekle");


            listele();
            dataGridView1.Columns[0].HeaderText = "Ürün Kodu";
            dataGridView1.Columns[1].HeaderText = "Ürün Adı";
            dataGridView1.Columns[2].HeaderText = "Ürün Grubu";
            dataGridView1.Columns[3].HeaderText = "Depo";
            dataGridView1.Columns[4].HeaderText = "Geliş Tarihi";
            dataGridView1.Columns[5].HeaderText = "Tedarikçi";
            dataGridView1.Columns[6].HeaderText = "Garanti Bitiş";
            dataGridView1.Columns[7].HeaderText = "Birim Fiyatı";
            dataGridView1.Columns[8].HeaderText = "Miktar";
            dataGridView1.Columns[9].HeaderText = "Toplam Fiyat";
            dataGridView1.Columns[10].HeaderText = "Kullanıcı";
            dataGridView1.Columns[11].HeaderText = "Tarih/Saat";
            //////////////////////////////////////////////////////
            GirisForm a = new GirisForm();
            lblKullanici.Text = GirisForm.ykullaniciAdi;
            /////////////////////////////////////////////////////
            okuma();



        }
        public void okuma()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select depoAdi from depoKayit", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                cmbDepo.Items.Add(oku["depoAdi"].ToString());

            }
            oku.Close();
            baglanti.Close();
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select tedarikciAdi from tedarikciKayit", baglanti);
            SqlDataReader oku1 = cmd.ExecuteReader();
            while (oku1.Read())
            {
                cmbTedarikci.Items.Add(oku1["tedarikciAdi"].ToString());
            }
            oku1.Close();
            baglanti.Close();
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("select grubAdi from urunGrubu", baglanti);
            SqlDataReader oku2 = komut1.ExecuteReader();
            while (oku2.Read())
            {
                cmbtanim.Items.Add(oku2["grubAdi"].ToString());
            }
            oku2.Close();
            baglanti.Close();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            update();

        }
        public void update()
        {
            try
            {

                if (txtad.Text == "" || cmbtanim.Text == "" || txtfiyat.Text == "" || txtMiktar.Text == "" || txtToplamfiyat.Text == "" || cmbDepo.Text == "" || cmbTedarikci.Text == "") { MessageBox.Show("Lütfen tablodan ürün seçiniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                else if (cmbDepo.SelectedIndex == 0) { MessageBox.Show("Geçersiz Depo.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                else if (cmbtanim.SelectedIndex == 0) { MessageBox.Show("Geçersiz Tanım.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                else if (cmbTedarikci.SelectedIndex == 0) { MessageBox.Show("Geçersiz Tedarikçi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                else
                {
                    DialogResult o;
                    o = MessageBox.Show(lblKod.Text + " Nolu ürünü düzenlemek istiyor musunuz? ", "ONAY", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (o == DialogResult.Yes)
                    {
                        double sayi1, sayi2, c;
                        sayi1 = Convert.ToDouble(txtfiyat.Text);
                        sayi2 = Convert.ToDouble(txtMiktar.Text);
                        c = (sayi1 * sayi2);
                        txtToplamfiyat.Text = c.ToString();

                        baglanti.Open();
                        SqlCommand komut = new SqlCommand("update tabloStok set urunAdi=@ad,urunTanimi=@tanim,depo=@depo,gelisTarih=@gelis,tedarikci=@tedarikci,garantiBitis=@garanti,birimFiyati=@bfiyat,miktar=@miktar,toplamFiyat=@tfiyat,islemYapan=@islem,tarihSaat=@tarihsaat where urunKodu=@Id", baglanti);
                        komut.Parameters.AddWithValue("@ad", txtad.Text);
                        komut.Parameters.AddWithValue("@tanim", cmbtanim.Text);
                        komut.Parameters.AddWithValue("@depo", cmbDepo.Text);
                        komut.Parameters.AddWithValue("@gelis", DateTime.Parse(dateTimePicker1.Text));
                        komut.Parameters.AddWithValue("@tedarikci", cmbTedarikci.Text);
                        komut.Parameters.AddWithValue("@garanti", DateTime.Parse(dateTimePicker2.Text));
                        komut.Parameters.AddWithValue("@bfiyat", txtfiyat.Text);
                        komut.Parameters.AddWithValue("@miktar", txtMiktar.Text);
                        komut.Parameters.AddWithValue("@tfiyat", txtToplamfiyat.Text);
                        komut.Parameters.AddWithValue("@islem", lblKullanici.Text);
                        komut.Parameters.AddWithValue("@tarihsaat", lblSaat.Text);
                        komut.Parameters.AddWithValue("@Id", lblKod.Text);
                        komut.ExecuteNonQuery();
                        komut.Dispose();
                        baglanti.Close();
                        MessageBox.Show(lblKod.Text + " No'lu ürün düzenlenmiştir.", "BİLGİ",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        temizle();
                        random();

                    }
                }


            }
            catch (Exception hata) { MessageBox.Show(hata.Message.ToString(), "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop); }

            finally { listele(); }
        }

        public void listele()
        {
            if (baglanti.State == ConnectionState.Open)
                baglanti.Close();
            dt.Clear();
            try
            {
                baglanti.Open();
                SqlCommand com = new SqlCommand("select*from tabloStok", baglanti);
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
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            //arama butonu
            try
            {
                baglanti.Open();
                dt.Clear();
                SqlDataAdapter adap = new SqlDataAdapter("select*from tabloStok where urunAdi like'" + textBox6.Text + "%'", baglanti);
                adap.Fill(dt);
                dataGridView1.DataSource = dt;
                baglanti.Close();
            }
            catch (Exception hata) { MessageBox.Show(hata.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form frm = new depoEkle(this.cmbDepo);
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form tedarikci = new hızlıtedarikci(this.cmbTedarikci);
            tedarikci.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form n = new grupEkle(this.cmbtanim);
            n.ShowDialog();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',';
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblSaat.Text = DateTime.Now.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                lblKod.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtad.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                cmbtanim.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                txtfiyat.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                cmbDepo.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                cmbTedarikci.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                dateTimePicker2.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                txtMiktar.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                txtToplamfiyat.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            }
            catch (Exception hata) { MessageBox.Show(hata.Message.ToString(), "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop); }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtad.Text == "" || txtfiyat.Text == "" || txtMiktar.Text == "") { MessageBox.Show("Silinecek Ürün Seçiniz. ", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else
            {
                foreach (DataGridViewRow drow in dataGridView1.SelectedRows)  //Seçili Satırları Silme
                {
                    int no = Convert.ToInt32(drow.Cells[0].Value);
                    sil(no);
                }
                listele();
            }


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
                    SqlCommand ole = new SqlCommand("delete from tabloStok where urunKodu=@no", baglanti);
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

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            kayıt();
        }

        public void kayıt()
        {
            if (txtad.Text == "" || txtfiyat.Text == "" || txtMiktar.Text == "")
            {
                MessageBox.Show("Alanların dolu olduğundan emin olunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cmbDepo.SelectedIndex == 0) { MessageBox.Show("Geçersiz Depo.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else if (cmbtanim.SelectedIndex == 0) { MessageBox.Show("Geçersiz Tanım.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else if (cmbTedarikci.SelectedIndex == 0) { MessageBox.Show("Geçersiz Tedarikçi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else
            {

                try
                {
                    DialogResult d;
                    d = MessageBox.Show("Kaydetmek istiyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (d == DialogResult.Yes)
                    {
                        baglanti.Open();

                        double sayi1, sayi2, c;
                        sayi1 = Convert.ToDouble(txtfiyat.Text);
                        sayi2 = Convert.ToDouble(txtMiktar.Text);
                        c = sayi1 * sayi2;
                        txtToplamfiyat.Text = c.ToString();

                        SqlCommand komut = new SqlCommand("insert into tabloStok(urunKodu,urunAdi,urunTanimi,depo,tedarikci,garantiBitis,gelisTarih,birimFiyati,miktar,toplamFiyat,islemYapan,tarihSaat) values (@kod,@ad,@tanim,@depo,@tedarikci,@garanti,@gelis,@bfiyat,@miktar,@tfiyat,@islem,@tarih)", baglanti);

                        komut.Parameters.AddWithValue("@kod", lblKod.Text);
                        komut.Parameters.AddWithValue("@ad", txtad.Text);
                        komut.Parameters.AddWithValue("@tanim", cmbtanim.Text);
                        komut.Parameters.AddWithValue("@depo", cmbDepo.Text);
                        komut.Parameters.AddWithValue("@tedarikci", cmbTedarikci.Text);
                        komut.Parameters.AddWithValue("@garanti", DateTime.Parse(dateTimePicker2.Text));
                        komut.Parameters.AddWithValue("@gelis", DateTime.Parse(dateTimePicker1.Text));
                        komut.Parameters.AddWithValue("@bfiyat", txtfiyat.Text);
                        komut.Parameters.AddWithValue("@miktar", txtMiktar.Text);
                        komut.Parameters.AddWithValue("@tfiyat", txtToplamfiyat.Text);
                        komut.Parameters.AddWithValue("@islem", lblKullanici.Text);
                        komut.Parameters.AddWithValue("@tarih", lblSaat.Text);
                        komut.ExecuteNonQuery();

                        MessageBox.Show("Ürün Kaydedildi", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        komut.Dispose();
                        listele();
                        baglanti.Close();

                    }


                }
                catch (Exception hata) { MessageBox.Show(hata.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnyenikayit_Click(object sender, EventArgs e)
        {
            temizle();
            random();
        }
    }
}
