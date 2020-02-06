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
    public partial class sifreYetki : Form
    {
        baglanti con = new baglanti();
        SqlConnection baglanti;
        public sifreYetki()
        {
            baglanti = new SqlConnection(con.adres);
            InitializeComponent();
        }
       
        private void sifreYetki_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try { 
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select* from kullaniciBilgi where kullaniciAdi=@ad", baglanti);
            komut.Parameters.AddWithValue("@ad", textBox1.Text);
            SqlDataReader oku = komut.ExecuteReader();

                if (oku.Read())
                {
                    if (oku["kullaniciAdi"].ToString() == textBox1.Text && oku["sifre"].ToString() == textBox2.Text)
                    {
                        baglanti.Close();
                        this.Hide();

                        try
                        {

                            SaveFileDialog save = new SaveFileDialog();
                            save.Filter = "(*.bak) | *.bak|(*.rar)|*.rar";
                            save.FilterIndex = 0;
                            if (save.ShowDialog() == DialogResult.OK)
                            {
                                string sql = string.Format(@"BACKUP database StokData to disk='{0}'", save.FileName);
                                SqlCommand cmd = new SqlCommand(sql, baglanti);
                                baglanti.Open();
                                cmd.ExecuteNonQuery();
                                baglanti.Close();
                                MessageBox.Show("Yedeklendi", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }


                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop); }





                    }
                    else { MessageBox.Show("Giriş bilgilerinizi kontrol ediniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
            }catch(Exception ex) { MessageBox.Show(ex.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
            
        }
    }
}
