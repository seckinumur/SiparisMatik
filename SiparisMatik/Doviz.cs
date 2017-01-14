using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SiparisMatik
{
    class Doviz
    {
        public Doviz()
        {
        }
        public string DolarDovizSatis()      // Döviz Satış
        {
            string Sonuc;
            try
            {

                string today = "http://www.tcmb.gov.tr/kurlar/today.xml";
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(today);
                Sonuc = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/ForexSelling").InnerXml;
                return Sonuc;
            }
            catch
            {
                return Sonuc = "internet yok";
            }
        }

        public string EuroDovizSatis()    // Euro Satiş
        {
            string Sonuc;
            try
            {

                string today = "http://www.tcmb.gov.tr/kurlar/today.xml";
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(today);
                Sonuc = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/ForexSelling").InnerXml;
                return Sonuc;
            }
            catch
            {
                return Sonuc = "internet yok";
            }
        }
    }
}
