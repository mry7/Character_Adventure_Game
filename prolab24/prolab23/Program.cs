using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prolab24
{
    internal static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var izgaraFormuBoyutu = new kullanici_girisi())
            {
                if (izgaraFormuBoyutu.ShowDialog() == DialogResult.OK)
                {
                    int kareBoyutu = 35; 
                    int dolguu = 5; 
                    int width = kareBoyutu * izgaraFormuBoyutu.izgaraSutunuu + 2 * dolguu;
                    int yukseklikk = kareBoyutu * izgaraFormuBoyutu.izgaraSatirii + 2 * dolguu;

                    Application.Run(new Form1(izgaraFormuBoyutu.izgaraSatirii, izgaraFormuBoyutu.izgaraSutunuu));
                 //   Application.Run(new Form1());



                }
            }
            }
    }
}
