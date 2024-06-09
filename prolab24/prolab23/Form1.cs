using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System;
using System.Linq;

namespace prolab24
{
    public partial class Form1 : Form
    {
        bool yukarii, asagii, solaa, sagaa;
        int puann, karakterHizii;
        int minAltinSayisi = 15;
        int maxAltinSayisi = 20;
        int minHareketliEngel = 3;
        int maxHareketliEngel = 7;
        int minHareketsizEngelAgac = 8;
        int maxHareketsizEngelAgac = 10;
        int minHareketsizEngelKaya = 8;
        int maxHareketsizEngelKaya = 10;

        public Form1(int satiirr, int sutuunn)
        {
            InitializeComponent();
            İzgaraCiz(satiirr, sutuunn);
            AltinlariEkle(satiirr, sutuunn);
            KayaHareketsizEngelEkle(satiirr, sutuunn);
            AgacHareketsizEngelEkle(satiirr, sutuunn);
            YatayHareketliAriEkle();
            DikeyHareketliKusEkle();
            OyunuSifirlaa();
        }

        // Formun arka planına ızgara deseni ekleyen metod.
        private void İzgaraCiz(int satiirr, int sutuunn)
        {
            // GridOlusturr metodunu çağırarak ızgara desenini oluşturur ve arka plana ekler.
            this.BackgroundImage = GridOlusturr(satiirr, sutuunn, this.ClientSize.Width, this.ClientSize.Height);
        }

        // Belirtilen boyutlarda bir ızgara deseni oluşturan metod.
        private Bitmap GridOlusturr(int satiirr, int sutuunn, int genislikk, int yukseklikk)
        {
            // Her bir kare hücresinin boyutunu hesaplar.
            int karehucresiBoyutuu = Math.Min(genislikk / sutuunn, yukseklikk / satiirr);
            // Dolgu miktarını hesaplar.
            int dolguu = karehucresiBoyutuu / 7;

            // ızgaranın genişliğini ve yüksekliğini hesaplar.
            int gridGenislikk = sutuunn * karehucresiBoyutuu + 2 * dolguu;
            int gridYukseklikk = satiirr * karehucresiBoyutuu + 2 * dolguu;

            // Formun boyutunu ızgara boyutuna göre ayarlar.
            this.ClientSize = new Size(gridGenislikk, gridYukseklikk);

            // Bitmap oluşturur.
            Bitmap bitmapp = new Bitmap(genislikk, yukseklikk);
            using (Graphics g = Graphics.FromImage(bitmapp))
            {
                // Kalemleri oluşturur.
                Pen kalem = new Pen(Color.Black);
                Pen gridCizgisiKalemi = new Pen(Color.WhiteSmoke);

                // ızgarayı çizer.
                for (int x = 0; x < gridGenislikk; x += karehucresiBoyutuu)
                {
                    for (int y = 0; y < gridYukseklikk; y += karehucresiBoyutuu)
                    {
                        // Grid çizgilerini ve dolguuyu çizer.
                        g.FillRectangle(gridCizgisiKalemi.Brush, x + 1, y + 1, karehucresiBoyutuu - 1, karehucresiBoyutuu - 1);
                        g.DrawRectangle(kalem, x, y, karehucresiBoyutuu, karehucresiBoyutuu);
                        g.FillRectangle(Brushes.Transparent, x + dolguu, y + dolguu, karehucresiBoyutuu - 2 * dolguu, karehucresiBoyutuu - 2 * dolguu);
                    }
                }
            }
            // Oluşturulan ızgaranın Bitmap'ini döndürür.
            return bitmapp;
        }


        private void Oyunn(object sender, EventArgs e)
        {
            // Skoru güncelle
            skorr.Text = "Puan: " + puann;

            // Altınların konumlarını depolamak için bir liste oluştur
            List<Point> altinKonumlarii = new List<Point>();

            // Tüm kontrolleri kontrol et
            foreach (Control x in this.Controls)
            {
                // PictureBox ise ve altın etiketine ve görünür durumuna sahipse
                if (x is PictureBox && (string)x.Tag == "altin" && x.Visible == true)
                {
                    // Altının merkez koordinatlarını listeye ekle
                    altinKonumlarii.Add(new Point(x.Left + x.Width / 2, x.Top + x.Height / 2));
                }
            }

            // Hedef belirleme
            Point enYakinAltiniBul = new Point(karakter.Left + karakter.Width / 2, karakter.Top + karakter.Height / 2);
            if (altinKonumlarii.Count > 0)
            {
                // Eğer altın varsa, oyuncunun en yakın altına gitmesi gereken konumu belirle
                enYakinAltiniBul = altinKonumlarii.OrderBy(p => Math.Abs(p.X - (karakter.Left + karakter.Width / 2)) + Math.Abs(p.Y - (karakter.Top + karakter.Height / 2))).First();
            }

            // Hareket yönünün belirlenmesi
            // Eğer en yakın altının X koordinatı, karakterin sol kenarının X koordinatından küçükse
            if (enYakinAltiniBul.X < karakter.Left)
            {
                solaa = true;//sol
                sagaa = false;
                yukarii = false;
                asagii = false;
            }
            // Eğer en yakın altının X koordinatı, karakterin sag kenarının X koordinatından büyükse
            else if (enYakinAltiniBul.X > karakter.Right)
            {
                sagaa = true;//sag
                solaa = false;
                yukarii = false;
                asagii = false;
            }
            // Eğer en yakın altının Y koordinatı, karakterin yukari kenarının Y koordinatından küçükse
            else if (enYakinAltiniBul.Y < karakter.Top)
            {
                yukarii = true;//yukari
                asagii = false;
                solaa = false;
                sagaa = false;
            }
            // Eğer en yakın altının Y koordinatı, karakterin asagi kenarının Y koordinatından büyükse 
            else if (enYakinAltiniBul.Y > karakter.Bottom)
            {
                asagii = true;//asagi
                yukarii = false;
                solaa = false;
                sagaa = false;
            }

            // Karakterin hareketi
            // Eğer solaa flag'i true ise ve karakterin sol kenarı ekranın sol kenarından büyükse
            if (solaa && karakter.Left > 0)
            {
                // Karakterin sol kenarını hız kadar sola kaydır
                karakter.Left -= karakterHizii;
                // Karakterin görüntüsünü sola dönük olarak güncelle
                karakter.Image = Properties.Resources.left;
            }

            // Eğer sagaa flag'i true ise ve karakterin sağ kenarı ekranın sağ kenarından küçükse
            if (sagaa && karakter.Right < ClientSize.Width)
            {
                // Karakterin sol kenarını hız kadar sağa kaydır
                karakter.Left += karakterHizii;
                // Karakterin görüntüsünü sağa dönük olarak güncelle
                karakter.Image = Properties.Resources.right;
            }

            // Eğer asagii flag'i true ise ve karakterin alt kenarı ekranın alt kenarından küçükse
            if (asagii && karakter.Bottom < ClientSize.Height)
            {
                // Karakterin üst kenarını hız kadar aşağı kaydır
                karakter.Top += karakterHizii;
                // Karakterin görüntüsünü aşağı bakar pozisyona güncelle
                karakter.Image = Properties.Resources.down;
            }

            // Eğer yukarii flag'i true ise ve karakterin üst kenarı ekranın üst kenarından büyükse
            if (yukarii && karakter.Top > 0)
            {
                // Karakterin üst kenarını hız kadar yukarı kaydır
                karakter.Top -= karakterHizii;
                // Karakterin görüntüsünü yukarı bakar pozisyona güncelle
                karakter.Image = Properties.Resources.Up;
            }


            // Engellerden kaçınma
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    // Hareketsiz veya hareketli bir engelse
                    if ((string)x.Tag == "hareketsizEngel" || (string)x.Tag == "hareketliEngel")
                    {
                        // Karakterin sınırları ile engel arasında çarpışma kontrolü yap
                        if (karakter.Bounds.IntersectsWith(x.Bounds))
                        {
                            // Eğer çarpışma varsa, hareketi durdur ve yeni hareket yönünü belirle
                            // Tüm hareket yönü flag'lerini sıfırla
                            solaa = sagaa = yukarii = asagii = false;

                            // Eğer en yakın altının X koordinatı, karakterin sol kenarından daha küçükse
                            if (enYakinAltiniBul.X < karakter.Left)
                            {
                                // Sol hareket flag'ini true yap
                                solaa = true;
                            }
                            // Eğer en yakın altının X koordinatı, karakterin sağ kenarından daha büyükse
                            else if (enYakinAltiniBul.X > karakter.Right)
                            {
                                // Sağ hareket flag'ini true yap
                                sagaa = true;
                            }
                            // Eğer en yakın altının Y koordinatı, karakterin üst kenarından daha küçükse
                            else if (enYakinAltiniBul.Y < karakter.Top)
                            {
                                // Yukarı hareket flag'ini true yap
                                yukarii = true;
                            }
                            // Eğer en yakın altının Y koordinatı, karakterin alt kenarından daha büyükse
                            else if (enYakinAltiniBul.Y > karakter.Bottom)
                            {
                                // Aşağı hareket flag'ini true yap
                                asagii = true;
                            }

                        }
                    }
                }
            }

            // Altınları toplama
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    // Eğer bir PictureBox bir altınsa
                    if ((string)x.Tag == "altin" && x.Visible == true)
                    {
                        // Karakter ile altın arasında çarpışma kontrol et
                        if (karakter.Bounds.IntersectsWith(x.Bounds))
                        {
                            // Eğer çarpışma varsa, puanı artır ve altını görünmez yap
                            puann += 1;
                            x.Visible = false;
                        }
                    }
                }
            }

            // Kalan altın sayısını kontrol et
            int aaltinSayisii = 0;
            foreach (Control control in Controls)
            {
                if (control is PictureBox && control.Tag?.ToString() == "altin" && control.Visible)
                {
                    aaltinSayisii++;
                }
            }
            // Eğer hiç altın kalmamışsa oyunu sonlandır
            if (aaltinSayisii == 0)
            {
                oyunSonu("Kazandınız!");
            }
        }


        private void AgacHareketsizEngelEkle(int satiirr, int sutuunn)
        {
            // Rasgele sayı üretmek için Random nesnesi oluşturulur.
            Random randomm = new Random();

            // Kare hücresi boyutu, pencere boyutuna ve satır/sütun sayısına bağlı olarak belirlenir.
            int karehucresiBoyutuu = Math.Min(this.ClientSize.Width / sutuunn, this.ClientSize.Height / satiirr);

            // Hücreler arasındaki dolguu miktarı hesaplanır.
            int dolguu = karehucresiBoyutuu / 7;

            // Daha önce işgal edilen konumları takip etmek için bir HashSet oluşturulur.
            HashSet<Point> isgalEdilmisKonumlar = new HashSet<Point>();

            // Alanın maksimum X ve Y koordinatları belirlenir.
            int maksX = sutuunn * karehucresiBoyutuu - karehucresiBoyutuu * 2;
            int maksY = satiirr * karehucresiBoyutuu - karehucresiBoyutuu * 2;

            // Belirlenen aralıkta rastgele bir sayıda hareketsiz ağaç engeli oluşturulur.
            int nesneSayisii = randomm.Next(minHareketsizEngelAgac, maxHareketsizEngelAgac + 1);
            for (int i = 0; i < nesneSayisii; i++)
            {
                // PictureBox nesnesi oluşturulur ve özellikleri atanır.
                PictureBox hareketsizEngel = new PictureBox();
                hareketsizEngel.Image = Properties.Resources.agac;
                hareketsizEngel.SizeMode = PictureBoxSizeMode.StretchImage;
                hareketsizEngel.Size = new Size(karehucresiBoyutuu * 2 - dolguu * 2, karehucresiBoyutuu * 2 - dolguu * 2);

                // Engelin konumu belirlenir.
                Point konumm;
                do
                {
                    int x = randomm.Next(0, maksX);
                    int y = randomm.Next(0, maksY);
                    konumm = new Point((x / karehucresiBoyutuu) * karehucresiBoyutuu + dolguu, (y / karehucresiBoyutuu) * karehucresiBoyutuu + dolguu);
                } while (isgalEdilmisKonumlar.Contains(konumm));

                // İşgal edilen konumlar listesine eklenir.
                isgalEdilmisKonumlar.Add(konumm);

                // PictureBox'in konumu ve diğer özellikleri atanır.
                hareketsizEngel.Location = konumm;
                hareketsizEngel.Tag = "hareketliEngel";

                // PictureBox formun kontrollerine eklenir.
                Controls.Add(hareketsizEngel);
            }
        }

        private void AltinlariEkle(int satiirr, int sutuunn)
        {
            // Rasgele sayı üretmek için Random nesnesi oluşturulur.
            Random randomm = new Random();

            // Kare hücresi boyutu, pencere boyutuna ve satır/sütun sayısına bağlı olarak belirlenir.
            int karehucresiBoyutuu = Math.Min(this.ClientSize.Width / sutuunn, this.ClientSize.Height / satiirr);

            // Hücreler arasındaki dolguu miktarı hesaplanır.
            int dolguu = karehucresiBoyutuu / 7;

            // Daha önce işgal edilen konumları takip etmek için bir HashSet oluşturulur.
            HashSet<Point> isgalEdilmisKonumlar = new HashSet<Point>();

            // Alanın maksimum X ve Y koordinatları belirlenir.
            int maksX = sutuunn * karehucresiBoyutuu - karehucresiBoyutuu;
            int maksY = satiirr * karehucresiBoyutuu - karehucresiBoyutuu;

            // Belirlenen aralıkta rastgele bir sayıda altın oluşturulur.
            int altinSayisi = randomm.Next(minAltinSayisi, maxAltinSayisi + 1);
            for (int i = 0; i < altinSayisi; i++)
            {
                // PictureBox nesnesi oluşturulur ve özellikleri atanır.
                PictureBox altin = new PictureBox();
                altin.Image = Properties.Resources.coin;
                altin.SizeMode = PictureBoxSizeMode.StretchImage;
                altin.Size = new Size(karehucresiBoyutuu - dolguu * 2, karehucresiBoyutuu - dolguu * 2);

                // Altının konumu belirlenir.
                Point konumm;
                do
                {
                    int x = randomm.Next(0, maksX);
                    int y = randomm.Next(0, maksY);
                    konumm = new Point((x / karehucresiBoyutuu) * karehucresiBoyutuu + dolguu, (y / karehucresiBoyutuu) * karehucresiBoyutuu + dolguu);
                } while (isgalEdilmisKonumlar.Contains(konumm));

                // İşgal edilen konumlar listesine eklenir.
                isgalEdilmisKonumlar.Add(konumm);

                // PictureBox'in konumu ve diğer özellikleri atanır.
                altin.Location = konumm;
                altin.Tag = "altin";

                // PictureBox formun kontrollerine eklenir.
                Controls.Add(altin);
            }
        }

        private void KayaHareketsizEngelEkle(int satiirr, int sutuunn)
        {
            // Rasgele sayı üretmek için Random nesnesi oluşturulur.
            Random randomm = new Random();

            // Kare hücresi boyutu, pencere boyutuna ve satır/sütun sayısına bağlı olarak belirlenir.
            int karehucresiBoyutuu = Math.Min(this.ClientSize.Width / sutuunn, this.ClientSize.Height / satiirr);

            // Hücreler arasındaki dolguu miktarı hesaplanır.
            int dolguu = karehucresiBoyutuu / 7;

            // Daha önce işgal edilen konumları takip etmek için bir HashSet oluşturulur.
            HashSet<Point> isgalEdilmisKonumlar = new HashSet<Point>();

            // Alanın maksimum X ve Y koordinatları belirlenir.
            int maksX = sutuunn * karehucresiBoyutuu - karehucresiBoyutuu * 2;
            int maksY = satiirr * karehucresiBoyutuu - karehucresiBoyutuu * 2;

            // Belirlenen aralıkta rastgele bir sayıda kaya oluşturulur.
            int nesneSayisii = randomm.Next(minHareketsizEngelKaya, maxHareketsizEngelKaya + 1);
            for (int i = 0; i < nesneSayisii; i++)
            {
                // PictureBox nesnesi oluşturulur ve özellikleri atanır.
                PictureBox hareketsizEngel = new PictureBox();
                hareketsizEngel.Image = Properties.Resources.kaya;
                hareketsizEngel.SizeMode = PictureBoxSizeMode.StretchImage;
                hareketsizEngel.Size = new Size(karehucresiBoyutuu * 2 - dolguu * 2, karehucresiBoyutuu * 2 - dolguu * 2);

                // Kaya'nın konumu belirlenir.
                Point konumm;
                do
                {
                    int x = randomm.Next(0, maksX);
                    int y = randomm.Next(0, maksY);
                    konumm = new Point((x / karehucresiBoyutuu) * karehucresiBoyutuu + dolguu, (y / karehucresiBoyutuu) * karehucresiBoyutuu + dolguu);
                } while (isgalEdilmisKonumlar.Contains(konumm));

                // İşgal edilen konumlar listesine eklenir.
                isgalEdilmisKonumlar.Add(konumm);

                // PictureBox'in konumu ve diğer özellikleri atanır.
                hareketsizEngel.Location = konumm;
                hareketsizEngel.Tag = "hareketliEngel";

                // PictureBox formun kontrollerine eklenir.
                Controls.Add(hareketsizEngel);
            }
        }

        private void YatayHareketliAriEkle()
        {
            // Rastgele sayı üretmek için Random nesnesi oluşturulur.
            Random randomm = new Random();

            // Belirtilen aralıkta rastgele bir sayıda hareketli arı oluşturulur.
            int nesneSayisii = randomm.Next(minHareketliEngel, maxHareketliEngel + 1);

            // Oluşturulan arılar için döngü başlatılır.
            for (int i = 0; i < nesneSayisii; i++)
            {
                // PictureBox nesnesi oluşturulur ve özellikleri atanır.
                PictureBox hareketliEngel = new PictureBox();
                hareketliEngel.Image = Properties.Resources.ari;
                hareketliEngel.SizeMode = PictureBoxSizeMode.StretchImage;
                hareketliEngel.Size = new Size(30, 30);

                // PictureBox'in konumu belirlenir.
                hareketliEngel.Location = new Point(randomm.Next(ClientSize.Width - hareketliEngel.Width), randomm.Next(ClientSize.Height - hareketliEngel.Height));

                // PictureBox'in etiketi atanır.
                hareketliEngel.Tag = "hareketliEngel";

                // PictureBox formun kontrollerine eklenir.
                Controls.Add(hareketliEngel);

                // PictureBox'in hareketini kontrol etmek için bir zamanlayıcı oluşturulur.
                Timer nesneZamanii = new Timer();
                nesneZamanii.Interval = 100;
                nesneZamanii.Tick += (sender, e) =>
                {
                    YatayHareketEt(hareketliEngel);
                };
                nesneZamanii.Start();
            }
        }

        private void DikeyHareketliKusEkle()
        {
            // Rastgele sayı üretmek için Random nesnesi oluşturulur.
            Random randomm = new Random();

            // Belirtilen aralıkta rastgele bir sayıda hareketli kuş oluşturulur.
            int nesneSayisii = randomm.Next(minHareketliEngel, maxHareketliEngel + 1);

            // Oluşturulan kuşlar için döngü başlatılır.
            for (int i = 0; i < nesneSayisii; i++)
            {
                // PictureBox nesnesi oluşturulur ve özellikleri atanır.
                PictureBox hareketliEngel = new PictureBox();
                hareketliEngel.Image = Properties.Resources.kus2;
                hareketliEngel.SizeMode = PictureBoxSizeMode.StretchImage;
                hareketliEngel.Size = new Size(30, 30);

                // PictureBox'in konumu belirlenir.
                hareketliEngel.Location = new Point(randomm.Next(ClientSize.Width - hareketliEngel.Width), randomm.Next(ClientSize.Height - hareketliEngel.Height));

                // PictureBox'in etiketi atanır.
                hareketliEngel.Tag = "hareketliEngel";

                // PictureBox formun kontrollerine eklenir.
                Controls.Add(hareketliEngel);

                // PictureBox'in hareketini kontrol etmek için bir zamanlayıcı oluşturulur.
                Timer nesneZamanii = new Timer();
                nesneZamanii.Interval = 100;
                nesneZamanii.Tick += (sender, e) =>
                {
                    DikeyHareketEt(hareketliEngel);
                };
                nesneZamanii.Start();
            }
        }

        private void YatayHareketEt(PictureBox hareketliEngel)
        {
            // PictureBox'in mevcut konumu alınır.
            int x = hareketliEngel.Left;
            int y = hareketliEngel.Top;

            // Hareket mesafesi belirlenir.
            int hareketMesafesii = 50;

            // Hareket yönüne göre hareketliEngel'in Tag özelliği değiştirilir.
            int yon = hareketliEngel.Tag.ToString() == "hayalet_sol" ? -1 : 1;
            hareketliEngel.Tag = hareketliEngel.Tag.ToString() == "hayalet_sol" ? "hayalet_sag" : "hayalet_sol";

            // Yatay konum güncellenir.
            x += hareketMesafesii * yon;

            // Yatay konum, belirli bir aralıkta sınırlanır.
            x = Math.Max(Math.Min(x, hareketliEngel.Left + 5 * hareketMesafesii), hareketliEngel.Left - 5 * hareketMesafesii);

            // PictureBox'in yeni konumu atanır.
            hareketliEngel.Location = new Point(x, y);
        }

        private void DikeyHareketEt(PictureBox hareketliEngel)
        {
            // PictureBox'in mevcut konumu alınır.
            int x = hareketliEngel.Left;
            int y = hareketliEngel.Top;

            // Hareket mesafesi belirlenir.
            int hareketMesafesii = 50;

            // Hareket yönüne göre hareketliEngel'in Tag özelliği değiştirilir.
            int yon = hareketliEngel.Tag.ToString() == "hayalet_yukarii" ? -1 : 1;
            hareketliEngel.Tag = hareketliEngel.Tag.ToString() == "hayalet_yukarii" ? "hayalet_asagii" : "hayalet_yukarii";

            // Dikey konum güncellenir.
            y += hareketMesafesii * yon;

            // Dikey konum, belirli bir aralıkta sınırlanır.
            y = Math.Max(Math.Min(y, hareketliEngel.Top + 5 * hareketMesafesii), hareketliEngel.Top - 5 * hareketMesafesii);

            // PictureBox'in yeni konumu atanır.
            hareketliEngel.Location = new Point(x, y);
        }

        private void OyunuSifirlaa()
        {
            // Skor metni sıfırlanır ve puan değişkeni sıfırlanır.
            skorr.Text = "Puan: 0";
            puann = 0;

            // Karakter hızı sıfırlanır.
            karakterHizii = 30;

            // Karakterin başlangıç konumu ayarlanır.
            karakter.Left = 31;
            karakter.Top = 46;

            // Formdaki tüm PictureBox kontrolü olan nesneler görünür hale getirilir.
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    x.Visible = true;
                }
            }
            // Zamanlayıcı başlatılır.
            zamanlayicii.Start();
        }

        private void oyunSonu(string mesaj)
        {
            // Zamanlayıcı durdurulur.
            zamanlayicii.Stop();
            // Skor metni ve oyun sonu mesajı gösterilir.
            skorr.Text = "Puan: " + puann + Environment.NewLine + mesaj;
        }
    }
}
