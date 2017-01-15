using System;
using System.Globalization;

namespace SiparisMatik
{
    class KurHesapla
    {
        public KurHesapla()
        {
        }
        Doviz DovizAl = new Doviz();
        public double DolarCevirTurkLirasi(string AlinanSonuc)
        {
            Double sonuc = Convert.ToDouble(AlinanSonuc);
            double dolar;
            if (!double.TryParse(DovizAl.DolarDovizSatis(), NumberStyles.Any, CultureInfo.InvariantCulture, out dolar))
            {
                return sonuc = 0;
            }
            else
            {
                sonuc = sonuc * dolar;
                return Math.Round(sonuc, 2);
            }
        }
        public double DolarCevirEuro(string AlinanSonuc)
        {
            Double sonuc = Convert.ToDouble(AlinanSonuc);
            double dolar, euro;
            if (!double.TryParse(DovizAl.EuroDovizSatis(), NumberStyles.Any, CultureInfo.InvariantCulture, out euro) || !double.TryParse(DovizAl.DolarDovizSatis(), NumberStyles.Any, CultureInfo.InvariantCulture, out dolar))
            {
                return sonuc = 0;
            }
            else
            {
                sonuc = (sonuc * dolar) / euro;
                return Math.Round(sonuc, 2);
            }
        }
        public double EuroCevirTurkLirası(string AlinanSonuc)
        {
            Double sonuc = Convert.ToDouble(AlinanSonuc);
            double euro;
            if (!double.TryParse(DovizAl.EuroDovizSatis(), NumberStyles.Any, CultureInfo.InvariantCulture, out euro))
            {
                return sonuc = 0;
            }
            else
            {
                sonuc = sonuc * euro;
                return Math.Round(sonuc, 2);
            }
        }
        public double EuroCevirDolar(string AlinanSonuc)
        {
            Double sonuc = Convert.ToDouble(AlinanSonuc);
            double dolar, euro;
            if (!double.TryParse(DovizAl.EuroDovizSatis(), NumberStyles.Any, CultureInfo.InvariantCulture, out euro) || !double.TryParse(DovizAl.DolarDovizSatis(), NumberStyles.Any, CultureInfo.InvariantCulture, out dolar))
            {
                return sonuc = 0;
            }
            else
            {
                sonuc = (sonuc / dolar) * euro;
                return Math.Round(sonuc, 2);
            }
        }
        public double TurkLirasiCevirDolar(string AlinanSonuc)
        {
            Double sonuc = Convert.ToDouble(AlinanSonuc);
            double dolar;
            if (!double.TryParse(DovizAl.DolarDovizSatis(), NumberStyles.Any, CultureInfo.InvariantCulture, out dolar))
            {
                return sonuc = 0;
            }
            else
            {
                sonuc = sonuc / dolar;
                return Math.Round(sonuc, 2);
            }
        }
        public double TurkLirasiCevirEuro(string AlinanSonuc)
        {
            Double sonuc = Convert.ToDouble(AlinanSonuc);
            double euro;
            if (!double.TryParse(DovizAl.EuroDovizSatis(), NumberStyles.Any, CultureInfo.InvariantCulture, out euro))
            {
                return sonuc = 0;
            }
            else
            {
                sonuc = sonuc / euro;
                return Math.Round(sonuc, 2);
            }
        }
    }
}