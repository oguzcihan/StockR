using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StokOto
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }

        Timer t = new Timer();
        private void GirisSec_Load(object sender, EventArgs e)
        {
            t.Interval = 2500;
            t.Tick += new EventHandler(onTimerTicked);
            t.Start();

        }
        public void onTimerTicked(object sender, EventArgs e)
        {
            t.Stop();
            Giris kapat = new Giris();
            kapat.Close();
            GirisForm frm1 = new GirisForm();
            frm1.Show();
            this.Hide();
           
        }

       
    }
}
