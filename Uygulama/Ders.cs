using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Uygulama
{
    class Ders
    {
        public enum SiralamaOlcudu
        {
            KodunaGore,
            AdinaGore,
            KredisineGore
        }

        private string _Kod;
        private string _Ad;
        private int _Kredi;
        private string _OnSart;
        private string _Aciklama;

        public Ders()
        {

        }

        public Ders(string Kod, string Ad, int Kredi, string OnSart, string Aciklama)
        {
            this.Kod = Kod;
            this.Ad = Ad;
            this.Kredi = Kredi;
            this.OnSart = OnSart;
            this.Aciklama = Aciklama;
        }

        public string Aciklama
        {
            get { return _Aciklama; }
            set { _Aciklama = value; }
        }
        

        public string OnSart
        {
            get { return _OnSart; }
            set { _OnSart = value; }
        }
        

        public int Kredi
        {
            get { return _Kredi; }
            set { _Kredi = value; }
        }
        

        public string Ad
        {
            get { return _Ad; }
            set { _Ad = value; }
        }
        

        public string Kod
        {
            get { return _Kod; }
            set { _Kod = value; }
        }

        public override string ToString()
        {
            return Ad;
        }

        public static Ders DersAra(string dosya, string DersKodu)
        {
            FileStream fs = new FileStream(dosya, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(fs, Encoding.Unicode);

            Ders bulunanDers = null;
            string[] suAnkiDers;
            string bulunanDersKodu="";

            do
            {
                suAnkiDers = reader.ReadLine().Split('|');
                bulunanDersKodu = suAnkiDers[0];
            } while (!reader.EndOfStream && bulunanDersKodu != DersKodu);

            if (bulunanDersKodu == DersKodu)
            {
                bulunanDers = new Ders(suAnkiDers[0], suAnkiDers[1], Convert.ToInt32(suAnkiDers[2]), suAnkiDers[3], suAnkiDers[4]);
                reader.Close();
                return bulunanDers;
            }
            reader.Close();
            return bulunanDers;
        }

        public static List<Ders> Sirala(List<Ders> Siralanacak, bool DosyayaYazilsinMi, SiralamaOlcudu NeyeGore)
        {
            int kapasite = Siralanacak.Count - 1;

            for (int i = 0; i < kapasite; i++)
            {
                for (int j = 0; j < kapasite - i; j++)
                {
                    switch (NeyeGore)
                    {
                        case SiralamaOlcudu.KodunaGore:
                            if (Siralanacak[j].Kod.CompareTo(Siralanacak[j + 1].Kod) > 0)
                            {
                                Ders temp = Siralanacak[j];
                                Siralanacak[j] = Siralanacak[j + 1];
                                Siralanacak[j + 1] = temp;
                            }
                            break;
                        case SiralamaOlcudu.AdinaGore:
                            if (Siralanacak[j].Ad.CompareTo(Siralanacak[j + 1].Ad) > 0)
                            {
                                Ders temp = Siralanacak[j];
                                Siralanacak[j] = Siralanacak[j + 1];
                                Siralanacak[j + 1] = temp;
                            }
                            break;
                        case SiralamaOlcudu.KredisineGore:
                            if (Siralanacak[j].Kredi > Siralanacak[j + 1].Kredi)
                            {
                                Ders temp = Siralanacak[j];
                                Siralanacak[j] = Siralanacak[j + 1];
                                Siralanacak[j + 1] = temp;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            if (DosyayaYazilsinMi)
            {
                switch (NeyeGore)
                {
                    case SiralamaOlcudu.KodunaGore:
                        DosyayiBosalt("output.txt");
                        foreach (Ders item in Siralanacak)
                        {
                            item.SiraliDosyayaYaz("output.txt");
                        }
                        break;
                    case SiralamaOlcudu.AdinaGore:
                        DosyayiBosalt("outputad.txt");
                        foreach (Ders item in Siralanacak)
                        {
                            item.SiraliDosyayaYaz("outputad.txt");
                        }
                        break;
                    case SiralamaOlcudu.KredisineGore:
                        DosyayiBosalt("outputkredi.txt");
                        foreach (Ders item in Siralanacak)
                        {
                            item.SiraliDosyayaYaz("outputkredi.txt");
                        }
                        break;
                    default:
                        break;
                }
            }
            return Siralanacak;
        }

        public static List<Ders> DersleriListele(string dosya)
        {
            List<Ders> Dersler = new List<Ders>();

            FileStream fs = new FileStream(dosya, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(fs, Encoding.Unicode);

            do
            {
                string[] suAnkiDers = reader.ReadLine().Split('|');
                Ders ders = new Ders(suAnkiDers[0], suAnkiDers[1], Int16.Parse(suAnkiDers[2]), suAnkiDers[3], suAnkiDers[4]);
                Dersler.Add(ders);
            } while (!reader.EndOfStream);
            reader.Close();

            return Dersler;
        }

        public bool DosyayaYaz(string dosya)
        {
            if (!File.Exists(dosya))
            {
                return false;
            }

            FileStream fs = new FileStream(dosya, FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs, Encoding.Unicode);

            try
            {
                string yazilacakMetin = Kod + '|' + Ad + '|' + Kredi + '|' + OnSart + '|' + Aciklama;
                writer.WriteLine(yazilacakMetin);
                writer.Flush();
                writer.Close();
                return true;
            }
            catch (Exception)
            {
                writer.Close();
                return false;
            }
        }

        public static void DosyayiBosalt(string dosya)
        {
            FileStream fs = new FileStream(dosya, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs, Encoding.Unicode);
            writer.Write("");
            writer.Flush();
            writer.Close();
        }

        public bool SiraliDosyayaYaz(string dosya)
        {
            if (!File.Exists(dosya))
            {
                return false;
            }

            FileStream fs = new FileStream(dosya, FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs, Encoding.Unicode);

            try
            {
                string yazilacakMetin = Kod + '|' + Ad + '|' + Kredi + '|' + OnSart + '|' + Aciklama;
                writer.WriteLine(yazilacakMetin);
                writer.Flush();
                writer.Close();
                return true;
            }
            catch (Exception)
            {
                writer.Close();
                return false;
            }
        }
    }
}
