using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Uygulama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DersleriYenile();
        }

        bool KayitlariKontrolEt(out string Kod,out string Ad,out int Kredi, out string onSart, out string Aciklama, out string DonecekHataMesaji)
        {
            Kod = textBox_kod.Text.Trim();
            Ad = textBox_ad.Text; ;
            onSart = "";
            if (!checkBox_onsartVarmi.Checked)
            {
                onSart = (comboBox_dersler.SelectedItem as Ders).Kod;
            }
            Aciklama = textBox_aciklama.Text;
            Kredi = 0;
            try
            {
                Kredi = Int16.Parse(textBox_kredi.Text);
            }
            catch (Exception)
            {
                DonecekHataMesaji = "Ders kredisi hatalı";
                return false;
            }
            

            if (Kod.Length != 7)
            {
                DonecekHataMesaji = "Ders kodu hatalı";
                return false;
            }

            for (int i = 0; i < 3; i++)
            {
                if (!Char.IsLetter(textBox_kod.Text[i]))
                {
                    DonecekHataMesaji = "Ders kodu hatalı";
                    return false;
                }
            }

            for (int i = 3; i < 7; i++)
            {
                if (!Char.IsDigit(textBox_kod.Text[i]))
                {
                    DonecekHataMesaji = "Ders kodu hatalı";
                    return false;
                }
            }

            if (Ad.Length == 0)
            {
                DonecekHataMesaji = "Ders adı hatalı";
                return false;
            }

            if (Aciklama.Length == 0)
            {
                DonecekHataMesaji = "Açıklama yanlış";
                return false;
            }
            DonecekHataMesaji = "";
            return true;
        }

        private void DersleriYenile()
        {
            List<Ders> dersler = Ders.DersleriListele("input.txt");
            comboBox_dersler.DataSource = dersler;

            kodunaGoreToolStripMenuItem_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kod = "", ad = "", onsart = "", aciklama = "", donecekMesaj;
            int kredi = 0;

            if(!KayitlariKontrolEt(out kod, out ad, out kredi, out onsart, out aciklama, out donecekMesaj))
            {
                toolStripStatusLabel_kayit.Text = donecekMesaj;
                return;
            }

            Ders ders_ = new Ders(kod, ad, kredi, onsart, aciklama);

            if (ders_.DosyayaYaz("input.txt"))
            {
                toolStripStatusLabel_kayit.Text = "Ders kaydı başarılı";
                foreach (var item in groupBox_dersKayit.Controls)
                {
                    if (item is TextBox)
                    {
                        (item as TextBox).Text = "";
                    }
                }

                checkBox_onsartVarmi.Checked = true;

                DersleriYenile();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string kod = textBox_kodd.Text.Trim().ToUpper();

            Ders sonuc = Ders.DersAra("input.txt", kod);

            if (sonuc==null)
            {
                MessageBox.Show("Aranan ders bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            textBox_add.Text = sonuc.Ad;
            textBox_kredii.Text = sonuc.Kredi.ToString();
            textBox_onsartt.Text = sonuc.OnSart;
            textBox_aciklamaa.Text = sonuc.Aciklama;
        }

        private void kodunaGoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Ders> dersler = Ders.DersleriListele("input.txt");
            dersler = Ders.Sirala(dersler, true, Ders.SiralamaOlcudu.KodunaGore);

            listView_derslerinlistesi.Items.Clear();
            foreach (Ders EklenecekDers in dersler)
            {
                ListViewItem item_kod = new ListViewItem(EklenecekDers.Kod);
                item_kod.SubItems.Add(EklenecekDers.Ad);
                item_kod.SubItems.Add(EklenecekDers.Kredi.ToString());
                item_kod.SubItems.Add(EklenecekDers.OnSart);
                item_kod.SubItems.Add(EklenecekDers.Aciklama);
                listView_derslerinlistesi.Items.Add(item_kod);
            }

        }

        private void textBox_kod_Leave(object sender, EventArgs e)
        {
            (sender as TextBox).Text = (sender as TextBox).Text.ToUpper();
        }

        private void adinaGoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Ders> dersler = Ders.DersleriListele("input.txt");
            dersler = Ders.Sirala(dersler, true, Ders.SiralamaOlcudu.AdinaGore);

            listView_derslerinlistesi.Items.Clear();
            foreach (Ders EklenecekDers in dersler)
            {
                ListViewItem item_kod = new ListViewItem(EklenecekDers.Kod);
                item_kod.SubItems.Add(EklenecekDers.Ad);
                item_kod.SubItems.Add(EklenecekDers.Kredi.ToString());
                item_kod.SubItems.Add(EklenecekDers.OnSart);
                item_kod.SubItems.Add(EklenecekDers.Aciklama);
                listView_derslerinlistesi.Items.Add(item_kod);
            }
        }

        private void kredisineGoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Ders> dersler = Ders.DersleriListele("input.txt");
            dersler = Ders.Sirala(dersler, true, Ders.SiralamaOlcudu.KredisineGore);

            listView_derslerinlistesi.Items.Clear();
            foreach (Ders EklenecekDers in dersler)
            {
                ListViewItem item_kod = new ListViewItem(EklenecekDers.Kod);
                item_kod.SubItems.Add(EklenecekDers.Ad);
                item_kod.SubItems.Add(EklenecekDers.Kredi.ToString());
                item_kod.SubItems.Add(EklenecekDers.OnSart);
                item_kod.SubItems.Add(EklenecekDers.Aciklama);
                listView_derslerinlistesi.Items.Add(item_kod);
            }
        }

        private void checkBox_onsartVarmi_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_onsartVarmi.Checked)
            {
                comboBox_dersler.Enabled = false;
            }

            else
            {
                comboBox_dersler.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView_derslerinlistesi.Items.Clear();

            Dictionary<string, int> mesaj_listesi = new Dictionary<string, int>();
            List<Ders> silinecek_dersler = new List<Ders>();
            List<Ders> dersler = Ders.DersleriListele("input.txt");
            dersler = Ders.Sirala(dersler, false, Ders.SiralamaOlcudu.KodunaGore);

            for (int i = 0; i < dersler.Count-1; i++)
            {
                Ders ayni_dersler =null;
                int ayniSayisi = 0;
                while (dersler[i].Kod == dersler[i + 1].Kod && i < dersler.Count - 1)
                {
                    ayni_dersler = dersler[i];
                    ayniSayisi++;

                    if (ayniSayisi==1)
                    {
                        ListViewItem item = new ListViewItem(ayni_dersler.Kod);
                        item.SubItems.Add(ayni_dersler.Ad);
                        item.SubItems.Add(ayni_dersler.Kredi.ToString());
                        item.SubItems.Add(ayni_dersler.OnSart);
                        item.SubItems.Add(ayni_dersler.Aciklama);
                        listView_derslerinlistesi.Items.Add(item);
                        mesaj_listesi.Add(ayni_dersler.Ad, 0);
                    }

                    silinecek_dersler.Add(ayni_dersler);
                    mesaj_listesi[ayni_dersler.Ad]++;

                    if (i < dersler.Count - 3)
                        i++;
                }

                ayniSayisi = 0;
                ayni_dersler = null;
            }

            string mesaj = "Aşağıdaki kayıtlar silinecektir. Onaylıyor musunuz?";
            foreach (var item in mesaj_listesi)
            {
                mesaj += "\n" + item.Key + " dersinden " + item.Value + " adet ";
            }

            DialogResult result = MessageBox.Show(mesaj, "Uyarı!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            foreach (Ders item in silinecek_dersler)
            {
                dersler.Remove(item);
            }

            Ders.DosyayiBosalt("output.txt");

            listView_derslerinlistesi.Items.Clear();
            foreach (Ders item in dersler)
            {
                item.DosyayaYaz("output.txt");
                ListViewItem yeni_item = new ListViewItem(item.Kod);
                yeni_item.SubItems.Add(item.Ad);
                yeni_item.SubItems.Add(item.Kredi.ToString());
                yeni_item.SubItems.Add(item.OnSart);
                yeni_item.SubItems.Add(item.Aciklama);
                listView_derslerinlistesi.Items.Add(yeni_item);
            }
        }

        private void kodaGoreKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string kod = "", ad = "", onsart = "", aciklama = "", donecekMesaj;
            int kredi = 0;

            if (!KayitlariKontrolEt(out kod, out ad, out kredi, out onsart, out aciklama, out donecekMesaj))
            {
                toolStripStatusLabel_kayit.Text = donecekMesaj;
                return;
            }

            Ders ders_ = new Ders(kod, ad, kredi, onsart, aciklama);

            List<Ders> Dersler = Ders.DersleriListele("output.txt");
            int i = 0;
            for (;i < Dersler.Count && Dersler[i].Kod.CompareTo(ders_.Kod) < 0; i++) ;

            if (i < Dersler.Count)
                Dersler.Insert(i, ders_);
            else if (i == Dersler.Count)
                Dersler.Add(ders_);
            Ders.DosyayiBosalt("output.txt");
            foreach (Ders item in Dersler)
            {
                item.DosyayaYaz("output.txt");
            }
        }

    }
}
