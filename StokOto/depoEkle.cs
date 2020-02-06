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
    public partial class depoEkle : Form
    {
        baglanti con = new baglanti();
        SqlConnection baglanti;
        public ComboBox ComboBox { get; set; }
        public depoEkle(ComboBox comboBox = null)
        {
            baglanti = new SqlConnection(con.adres);

            InitializeComponent();
            if (comboBox != null)
                ComboBox = comboBox;
        }

        private void depoEkle_Load(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int sayi = rnd.Next(1, 12345);
            textBox1.Text = sayi.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            SqlCommand komut = new SqlCommand("select*from depoKayit where depoNo='" + textBox1.Text + "'", baglanti);
            SqlDataReader oku = komut.ExecuteReader();

            if (oku.Read())
            {
                MessageBox.Show(textBox1.Text + " Nolu başka bir depo var.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Depo no boş bırakılamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtAd.Text == "")
                {
                    MessageBox.Show("Depo adı boş bırakılamaz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    oku.Close();
                    SqlCommand cmd = new SqlCommand("insert into depoKayit (depoNo,depoAdi) values (@no,@ad)", baglanti);
                    cmd.Parameters.AddWithValue("@no", textBox1.Text);
                    cmd.Parameters.AddWithValue("@ad", txtAd.Text);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
            
                    if (ComboBox != null)
                        ComboBox.Items.Add(txtAd.Text);

                    MessageBox.Show(textBox1.Text + " Nolu depo eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear();
                    txtAd.Clear();


                }
            }
            baglanti.Close();

        }


    }
}
