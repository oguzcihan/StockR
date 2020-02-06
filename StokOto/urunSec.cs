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
    public partial class urunSec : Form, IUrunSecme
    {
        SqlConnection baglanti;
        SqlDataAdapter da;
        DataTable dt;
        baglanti con = new baglanti();
        public urunSec()
        {
            InitializeComponent();
            baglanti = new SqlConnection(con.adres);
            dt = new DataTable();
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

        //interface kullanıldı
        public event UrunSecildiHandle UrunSecildi;

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void urunSec_Load(object sender, EventArgs e)
        {
            listele();
            comboBox1.SelectedIndex = 4;
        }

        public string SecilenUrun { get; set; }
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //interface
            SecilenUrun = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            UrunSecildi?.Invoke();
            this.Hide();

        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            birimler();
        }
        public void birimler()
        {
            if(comboBox1.Text=="Ürün Adı")
            {
                listele();
                baglanti.Open();
                SqlDataAdapter ada = new SqlDataAdapter("select*from tabloStok where urunAdi like '" +
                txtAra.Text + "%'", baglanti);
                dt.Clear();
                ada.Fill(dt);
                dataGridView1.DataSource = dt;
                baglanti.Close();
                
            }
            else if(comboBox1.Text=="Ürün Grubu")
            {
                listele();
                baglanti.Open();
                SqlDataAdapter ada = new SqlDataAdapter("select*from tabloStok where urunTanimi like '" +
                txtAra.Text + "%'", baglanti);
                dt.Clear();
                ada.Fill(dt);
                dataGridView1.DataSource = dt;
                baglanti.Close();
            }
            else if (comboBox1.Text == "Depo")
            {
                listele();
                baglanti.Open();
                SqlDataAdapter ada = new SqlDataAdapter("select*from tabloStok where depo like '" +
                txtAra.Text + "%'", baglanti);
                dt.Clear();
                ada.Fill(dt);
                dataGridView1.DataSource = dt;
                baglanti.Close();               
            }
            else if(comboBox1.Text=="Ürün Kodu")
            {
                listele();
                baglanti.Open();
                SqlDataAdapter ada = new SqlDataAdapter("select*from tabloStok where urunKodu like '" +
                txtAra.Text + "%'", baglanti);
                dt.Clear();
                ada.Fill(dt);
                dataGridView1.DataSource = dt;
                baglanti.Close();              
            }
        }
    }
}
