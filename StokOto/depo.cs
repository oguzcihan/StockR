using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StokOto
{
    public partial class depo : Form
    {
        baglanti con = new baglanti();
        SqlConnection baglanti;
        public depo()
        {
            baglanti = new SqlConnection(con.adres);
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();
        
        private void depo_Load(object sender, EventArgs e)
        {
            listele();
        }
        public void listele()
        {
            if (baglanti.State == ConnectionState.Open)
                baglanti.Close();
            dt.Clear();
            try
            {
                baglanti.Open();
                SqlCommand com = new SqlCommand("select*from depoKayit", baglanti);
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

        private void button1_Click(object sender, EventArgs e)
        {
            Form kayıt = new depoEkle();
            kayıt.ShowDialog();
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
                d=MessageBox.Show("Silmek istiyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    baglanti.Open();
                    SqlCommand ole = new SqlCommand("delete from depoKayit where depoNo=@no", baglanti);
                    ole.Parameters.AddWithValue("@no", no);
                    ole.ExecuteNonQuery();
                    ole.Dispose();
                    baglanti.Close();
                }
            }
            catch (Exception hata) { MessageBox.Show(hata.Message.ToString(), "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void yenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listele();
        }
    }
}
