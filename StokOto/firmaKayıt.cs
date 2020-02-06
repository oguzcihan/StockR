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
    public partial class firmaKayıt : Form
    {
        baglanti con = new baglanti();
        SqlConnection baglanti;
        public firmaKayıt()
        {
            baglanti = new SqlConnection(con.adres);
            InitializeComponent();
        }
       
        private void firmaKayıt_Load(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int sayi = rnd.Next(1, 5999);
            label6.Text = sayi.ToString();

            listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select*from firmaCikis where firmaAdi='" + textBox1.Text + "'", baglanti);
                SqlDataReader oku = komut.ExecuteReader();

                if (oku.Read())
                {
                    MessageBox.Show(textBox1.Text + " adlı firma zaten var.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (textBox1.Text == "")
                    {
                        MessageBox.Show("Firma adı boş bırakılamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        oku.Close();
                        SqlCommand cmd = new SqlCommand("insert into firmaCikis (firmaID,firmaAdi,telefonNo,adress,eMail) values (@ıd,@ad,@no,@adres,@mail)", baglanti);
                        cmd.Parameters.AddWithValue("@ıd", label6.Text);
                        cmd.Parameters.AddWithValue("@ad", textBox1.Text);
                        cmd.Parameters.AddWithValue("@no", maskedTextBox1.Text);
                        cmd.Parameters.AddWithValue("@adres", textBox2.Text);
                        cmd.Parameters.AddWithValue("@mail", textBox3.Text);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();

                        MessageBox.Show(textBox1.Text + " Adlı firma eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Clear();


                    }
                }
                baglanti.Close();
            }
            catch (Exception hata) { MessageBox.Show(hata.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow drow in dataGridView1.SelectedRows)  //Seçili Satırları Silme
            {
                int kod = Convert.ToInt32(drow.Cells[0].Value);
                cokSil(kod);
            }
            listele();
        }
        public void cokSil(int kod)
        {
            DialogResult k;
            k = MessageBox.Show("Silmek istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (k == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from firmaCikis where firmaID=@ıd", baglanti);
                komut.Parameters.AddWithValue("@ıd", kod);
                komut.ExecuteNonQuery();
                komut.Dispose();
                baglanti.Close();
            }
            MessageBox.Show("Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();
        void listele()
        {
            if (baglanti.State == ConnectionState.Open)
                baglanti.Close();
            dt.Clear();
            try
            {
                baglanti.Open();
                SqlCommand com = new SqlCommand("select*from firmaCikis", baglanti);
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
    }
}
