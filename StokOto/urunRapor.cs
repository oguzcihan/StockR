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
using CrystalDecisions.CrystalReports.Engine;

namespace StokOto
{
    public partial class urunRapor : Form
    {
        baglanti con = new baglanti();
        SqlConnection baglanti;
        public urunRapor()
        {
            baglanti = new SqlConnection(con.adres);
            InitializeComponent();
        }

        private void urunRapor_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            crystalReportViewer1.RefreshReport();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("toplamStok", baglanti);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            ReportDocument rpt = new ReportDocument();
            rpt.Load(@"D:\StokOtomasyon\StokOto\Report\RaporUrun.rpt");
            rpt.SetDataSource(dt);
            crystalReportViewer1.ReportSource = rpt;
            baglanti.Close();
        }
        ReportDocument rpt = new ReportDocument();
        DataTable dt = new DataTable();
        public void birimler()
        {
            if (comboBox1.Text == "Ürün Adı")
            {
                baglanti.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("urunTablo", baglanti);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@ad", textBox1.Text);
                dt.Clear();
                adapter.Fill(dt);
                rpt.Load(@"D:\StokOtomasyon\StokOto\Report\RaporUrun.rpt");
                rpt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rpt;
                baglanti.Close();
            }
            else if (comboBox1.Text == "Ürün Grubu")
            {
                baglanti.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("urunTablo", baglanti);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@tanim", textBox1.Text);
                dt.Clear();
                adapter.Fill(dt);

                rpt.Load(@"D:\StokOtomasyon\StokOto\Report\RaporUrun.rpt");
                rpt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rpt;
                baglanti.Close();
            }
            else if (comboBox1.Text == "Depo")
            {
                baglanti.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("urunTablo", baglanti);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@depo", textBox1.Text);
                dt.Clear();
                adapter.Fill(dt);

                rpt.Load(@"D:\StokOtomasyon\StokOto\Report\RaporUrun.rpt");
                rpt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rpt;
                baglanti.Close();
            }
            else if (comboBox1.Text == "Kullanıcı")
            {
                baglanti.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("urunTablo", baglanti);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@kullanici", textBox1.Text);
                dt.Clear();
                adapter.Fill(dt);

                rpt.Load(@"D:\StokOtomasyon\StokOto\Report\RaporUrun.rpt");
                rpt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rpt;
                baglanti.Close();
            }
            else if (comboBox1.Text == "Miktar")
            {
                baglanti.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("urunTablo", baglanti);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@miktar", textBox1.Text);
                dt.Clear();
                adapter.Fill(dt);

                rpt.Load(@"D:\StokOtomasyon\StokOto\Report\RaporUrun.rpt");
                rpt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rpt;
                baglanti.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            birimler();
        }
    }
}
