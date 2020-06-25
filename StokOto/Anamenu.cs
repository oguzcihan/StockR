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
using Microsoft.Win32;

namespace StokOto
{

    public partial class Anamenu : Form
    {
        baglanti con = new baglanti();
        SqlConnection baglanti;
        public Anamenu()
        {
            baglanti = new SqlConnection(con.adres);
            InitializeComponent();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString();
        }


        SqlDataAdapter da;
        DataTable dt = new DataTable();
        public static string ykullaniciadi;
        public static string unvan;
        string text;
        string o;
        string ogz;
        string ConOku;
        private void YAnamenu_Load(object sender, EventArgs e)
        {
            GirisForm h = new GirisForm();
            label2.Text = GirisForm.ykullaniciAdi;
            label3.Text = GirisForm.unvan;           

            o = @"D:\StockR\StokOto\bin\Debug\" + "Write2.txt";
            ogz = @"D:\StockR\StokOto\bin\Debug\" + "Read2.ogz";
            Yarat();
            ConOku = System.IO.File.ReadAllText(o);
            Oku();

            /////////////////////////////////////////////////////////
            if (label3.Text == "PERSONEL")
            {





            }

        }
        public void Yarat()
        {   //notdefteri
            if (!System.IO.File.Exists(ogz))
            {
                System.IO.File.CreateText(ogz);
            }
            if (!System.IO.File.Exists(o))
            {
                System.IO.File.CreateText(o);
            }
        }
        public void Yaz()
        {
            System.IO.File.WriteAllText(o, text);
        }
        public void Oku()
        {
            string textoku = System.IO.File.ReadAllText(o);
            textBox1.Text = textoku;

            int TBL;
            textBox1.SelectionStart = 0;
            textBox1.SelectionLength = 0;
            TBL = (textBox1.Text.Length);
            if (TBL > 0)
            {
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.SelectionLength = 0;
            }
        }
     

        private void kullanıcıDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Anamenu kapat = new Anamenu();
            kapat.Close();
            GirisForm frm1 = new GirisForm();
            frm1.Show();
            this.Hide();

        }

      

        private void YAnamenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //x tuşu kapatır.
                DialogResult g = MessageBox.Show("Uygulamayı kapatmak istiyor musunuz ? ", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (g == DialogResult.No)
                {
                    e.Cancel = true;
                    return;

                }
                Environment.Exit(-1);

            }
            catch (Exception hata) { MessageBox.Show(hata.Message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }






        private void yedekleToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "(*.BAK) | *.BAK|(*.rar)|*.rar";
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (panel1.Height == 30)
            {
                timer2.Start();
            }
            else
            {
                timer3.Start();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            panel1.Height = panel1.Height + 5;
            if (panel1.Height == 340)
            {
                timer2.Stop();
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            panel1.Height = panel1.Height -5 ;
            if (panel1.Height == 30)
            {
                timer3.Stop();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form cikis = new urunCıkıs();
            cikis.ShowDialog();
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            Form kayit = new urunislemleri();
            kayit.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form kullanici = new kullaniciIslemleri();
            kullanici.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void ürünRaporuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form report = new urunRapor();
            report.ShowDialog();
        }

        private void çıkışRaporuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form report2 = new raporCikis();
            report2.ShowDialog();
        }

        private void yardımToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form yardim = new Yardim();
            yardim.ShowDialog();
        }

        private void kullanıcıDeğiştirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Anamenu kapat = new Anamenu();
            kapat.Close();
            GirisForm frm1 = new GirisForm();
            frm1.Show();
            this.Hide();
        }

        private void hesapMakinesiToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc.exe");
        }

        private void kaydir_Tick(object sender, EventArgs e)
        {
            if (label6.Left > -340)
            {
                label6.Left -= 1;
            }
            else
            {
                label6.Left = 1220;
            }
        }

        private void kullanıcılarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kullaniciIslemleri oznur = new kullaniciIslemleri();
            oznur.ShowDialog();
        }
    }
}
