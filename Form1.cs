using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Globalization;

namespace App1
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        int Hatasayac = 0;
        string[] kelimeler = { "Sehirler" }; // Klasör adı
        Random rnd = new Random();
        List<string> secilenKelimeler = new List<string>();
        string kelime = "";
        string guncelTahmin = ""; // Guncel tahmin için gerekli değişken yeniden tanımlandı

        private void Form1_Load(object sender, EventArgs e)
        {
            // Resim yükleniyor, hatasayacına göre
            pictureBox1.Load("Resimler/" + Hatasayac + ".png");

            // Txt dosyası sehirler dizininden rastgele bir txt dosyası alınıyor
            sehirler.Text = kelimeler[rnd.Next(kelimeler.Length)];

            // Dosya okuma işlemi
            FileStream fs = new FileStream("Kelimeler/" + sehirler.Text + ".txt", FileMode.Open, FileAccess.Read);
            StreamReader sw = new StreamReader(fs);
            string yazi = sw.ReadLine();
            while (yazi != null)
            {
                secilenKelimeler.Add(yazi.ToUpper(new CultureInfo("tr-TR"))); // Txt dosyasındaki her satır bir şehir ismi
                yazi = sw.ReadLine();
            }
            sw.Close();
            fs.Close();

            // Rastgele bir şehir seçiliyor
            kelime = secilenKelimeler[rnd.Next(secilenKelimeler.Count)];
            labelKontrol.Text = kelime;

            // Tahmin ekranı için seçilen kelimenin her harfini boşluklu bir şekilde "_" ile gösteriyoruz
            guncelTahmin = "";
            for (int i = 0; i < kelime.Length; i++)
            {
                guncelTahmin += "_ "; // Her harf yerine boşluk ve alt çizgi
            }

            // labelTahmin'e boşluklu alt çizgiler atanıyor
            labelTahmin.Text = guncelTahmin;
        }

        private void Oyun(object sender, EventArgs e)
        {
            Button seciliButton = sender as Button;
            seciliButton.Enabled = false;
            string harf = seciliButton.Text.ToUpper(new CultureInfo("tr-TR"));


            if (!kelime.Contains(seciliButton.Text.ToUpper(new CultureInfo("tr-TR"))))
            {
                Hatasayac++;
                pictureBox1.Load("Resimler/" + Hatasayac + ".png");
                Haksayısı.Text = (9 - Hatasayac).ToString();
            }
            else
            {
                // Tahmin edilen harfleri güncellemek için
                char[] tahminArray = guncelTahmin.Replace(" ", "").ToCharArray(); // Boşluksuz haliyle karşılaştırma yapıyoruz
                for (int i = 0; i < kelime.Length; i++)
                {
                    if (kelime[i].ToString(new CultureInfo("tr-TR")).ToUpper() == seciliButton.Text.ToUpper(new CultureInfo("tr-TR")))
                    {
                        tahminArray[i] = seciliButton.Text.ToUpper(new CultureInfo("tr-TR"))[0];
                    }
                }
                guncelTahmin = string.Join(" ", tahminArray); // Boşluklu olarak yeniden güncelliyoruz
                labelTahmin.Text = guncelTahmin; // Yeni tahmin label'a yazılıyor
            }

            if (Haksayısı.Text == "0")
            {
                OyunBitir();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Boş metot
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            // Boş metot
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OyunBitir()
        {
            MessageBox.Show("Oyun Bitti!");
        }
    }
}
