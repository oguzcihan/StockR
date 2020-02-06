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
    public partial class grupEkle : Form
    {
        baglanti conn = new baglanti();
        SqlConnection baglanti;
        public ComboBox ComboBox { get; set; }
        public grupEkle(ComboBox comboBox = null)
        {
            InitializeComponent();
            baglanti = new SqlConnection(conn.adres);
            if (comboBox != null)
                ComboBox = comboBox;
        }
        
        private void unvanEkle_Load(object sender, EventArgs e)
        {
            this.Refresh();
            Random rnd = new Random();
            int sayi = rnd.Next(1, 5000);
            label3.Text = sayi.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select*from urunGrubu where grubAdi='" + textBox1.Text + "'", baglanti);
                SqlDataReader oku = komut.ExecuteReader();

                if (oku.Read())
                {
                    MessageBox.Show(textBox1.Text + " adlı ürün grubu zaten var.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (textBox1.Text == "")
                    {
                        MessageBox.Show("Grup adı boş bırakılamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                            oku.Close();
                            SqlCommand cmd = new SqlCommand("insert into urunGrubu (grubID,grubAdi) values (@no,@ad)", baglanti);
                            cmd.Parameters.AddWithValue("@no", label3.Text);
                            cmd.Parameters.AddWithValue("@ad", textBox1.Text);
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                        if (ComboBox != null)
                            ComboBox.Items.Add(textBox1.Text);

                        MessageBox.Show(textBox1.Text + " Adlı grup eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox1.Clear();

                    
                    }
                }
                baglanti.Close();
            }
            catch (Exception hata) { MessageBox.Show(hata.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

       
    }
}
