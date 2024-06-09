using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prolab24
{
    public partial class kullanici_girisi : Form
    {
        public int izgaraSatirii { get; private set; }
        public int izgaraSutunuu { get; private set; }
        public kullanici_girisi()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int satiirr) && int.TryParse(textBox2.Text, out int sutuunn))
            {
                izgaraSatirii = satiirr;
                izgaraSutunuu = sutuunn;
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("lütfen değer giriniz.");
            }
        }

    }
    }
