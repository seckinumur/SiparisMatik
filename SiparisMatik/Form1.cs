using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiparisMatik
{
    public partial class Form1 : Form
    {
        Doviz al = new Doviz(); // al bakim gülüm
        SPMEntities db; // veritabanı nesnesi tanımladıkkkkkk
        public string buton;
        public Form1()
        {
            InitializeComponent();
            db = new SPMEntities(); // yarattım allahım seni :)
        }

        private void Form1_Load(object sender, EventArgs e) // he tabi
        {
            m2.BackColor = Color.DarkGreen;
            m3.BackColor = Color.DarkGreen;
            m4.BackColor = Color.DarkGreen;
            m5.BackColor = Color.DarkGreen;
            m6.BackColor = Color.DarkGreen;
            m7.BackColor = Color.DarkGreen;
            m8.BackColor = Color.DarkGreen;
            m9.BackColor = Color.DarkGreen;
            m10.BackColor = Color.DarkGreen;
            m11.BackColor = Color.DarkGreen;
            m12.BackColor = Color.DarkGreen;
            m13.BackColor = Color.DarkGreen;
            m14.BackColor = Color.DarkGreen;
            m15.BackColor = Color.DarkGreen;
            try
            {
                var bul = db.Masalar.ToList();
                foreach (var m in bul)
                {
                    if (m.MasaNo == m1.Text) { m1.Enabled = false; m1.BackColor = Color.Red; }
                    if (m.MasaNo == m2.Text) { m2.Enabled = false; m2.BackColor = Color.Red; }
                    if (m.MasaNo == m3.Text) { m3.Enabled = false; m3.BackColor = Color.Red; }
                    if (m.MasaNo == m4.Text) { m4.Enabled = false; m4.BackColor = Color.Red; }
                    if (m.MasaNo == m5.Text) { m5.Enabled = false; m5.BackColor = Color.Red; }
                    if (m.MasaNo == m6.Text) { m6.Enabled = false; m6.BackColor = Color.Red; }
                    if (m.MasaNo == m7.Text) { m7.Enabled = false; m7.BackColor = Color.Red; }
                    if (m.MasaNo == m8.Text) { m8.Enabled = false; m8.BackColor = Color.Red; }
                    if (m.MasaNo == m9.Text) { m9.Enabled = false; m9.BackColor = Color.Red; }
                    if (m.MasaNo == m10.Text) { m10.Enabled = false; m10.BackColor = Color.Red; }
                    if (m.MasaNo == m11.Text) { m11.Enabled = false; m11.BackColor = Color.Red; }
                    if (m.MasaNo == m12.Text) { m12.Enabled = false; m12.BackColor = Color.Red; }
                    if (m.MasaNo == m13.Text) { m13.Enabled = false; m13.BackColor = Color.Red; }
                    if (m.MasaNo == m14.Text) { m14.Enabled = false; m14.BackColor = Color.Red; }
                    if (m.MasaNo == m15.Text) { m15.Enabled = false; m15.BackColor = Color.Red; }
                }
            }
            catch
            {

            }
            // TODO: This line of code loads data into the 'sPMDataSet1.Masalar' table. You can move, or remove it, as needed.
            this.masalarTableAdapter.Fill(this.sPMDataSet1.Masalar);
            // TODO: This line of code loads data into the 'sPMDataSet.Urunler' table. You can move, or remove it, as needed.
            this.urunlerTableAdapter.Fill(this.sPMDataSet.Urunler);
            dolar.Text = al.DolarDovizSatis();
            euro.Text = al.EuroDovizSatis();
            int counter = 1;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button btn = new Button();
                    btn.Click += ButtonClick;
                    btn.Width = 90;
                    btn.Height = 90;
                    btn.Top = 50;
                    btn.Text = counter + "";
                    btn.BackColor = Color.Black;
                    btn.ForeColor = Color.White;
                    btn.Left = btn.Width * j;
                    btn.Top = btn.Height * i;
                    groupBox1.Controls.Add(btn);
                    counter++;
                }
            }
        }
        private void ButtonClick(object sender, EventArgs e)  // boktan butonların tıklama yeri film burada başlıyor hacı
        {
            tlgoster.Text = "";
            yabancigoster.Text = "";
            odeparabirimisec.SelectedItem = null;
            KurHesapla kuragorehesapla = new KurHesapla();
            Button seçilensaçmasapanbutonoluşturmaşekli = sender as Button;
            masano.Text = seçilensaçmasapanbutonoluşturmaşekli.Text;
            if (masano.Text == buton) { } // buraya uyarı vericektim bilmem böyle kalsın bakarım.
            else
            {
                dataGridView1.Rows.Clear();
                try
                {
                    var bul = db.Masalar.Where(p => p.MasaNo == masano.Text).ToList();
                    int i = 0;
                    double urunfiyati, Toplam = 0;
                    foreach (var m in bul)
                    {
                        var bul1 = db.Urunler.Where(p => p.UrunAdi == m.MasaSiparisi).FirstOrDefault();
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].Cells[0].Value = m.MasaSiparisi;
                        dataGridView1.Rows[i].Cells[1].Value = bul1.UrunFiyati;
                        i++;
                        if (bul1.UrunParaBirimi == "DOLAR") { urunfiyati = kuragorehesapla.DolarCevirTurkLirasi(bul1.UrunFiyati); }
                        else if (bul1.UrunParaBirimi == "EURO") { urunfiyati = kuragorehesapla.EuroCevirTurkLirası(bul1.UrunFiyati); }
                        else { urunfiyati = Convert.ToDouble(bul1.UrunFiyati); }
                        Toplam = Toplam + urunfiyati;
                    }
                    buton = masano.Text;
                    tlgoster.Text = Toplam.ToString();

                }
                catch (Exception cb)
                {
                    MessageBox.Show(cb.Message);
                }
            }

        }

        private void ode_Click(object sender, EventArgs e) //sipariş ödeme yeridir.
        {
            if (tlgoster.Text == "")
            {
                MessageBox.Show("Bu masada sipariş yok");
            }
            else
            {
                DialogResult Uyari = new DialogResult();
                Uyari = MessageBox.Show("Bu Masa Ödenecek Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
                if (Uyari == DialogResult.Yes)
                {
                    if (masano.Text == "") { MessageBox.Show("Önce Masa Seçin"); }
                    else
                    {
                        try
                        {
                            var sil = db.Masalar.Where(p => p.MasaNo == masano.Text).ToList();
                            foreach (var m in sil)
                            {
                                db.Masalar.Remove(m);
                                db.SaveChanges();
                            }
                            MessageBox.Show("masa ödendi!");
                            Form1_Load(sender, e);
                            tlgoster.Text = "";
                            yabancigoster.Text = "";
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void urunkaydet_Click(object sender, EventArgs e)
        {
            if (urunadi.Text == "" && urunfiyati.Text == "")
            {
                MessageBox.Show("ürün adını ve fiyatını doğru girin");
            }
            else
            {
                try
                {
                    try
                    {
                        var kotukullanicimodu = db.Urunler.Where(p => p.UrunAdi == urunadi.Text && p.UrunFiyati == urunfiyati.Text).FirstOrDefault();
                        if (urunadi.Text == kotukullanicimodu.UrunAdi)
                        {
                            MessageBox.Show("kayıt var");
                        }
                    }
                    catch
                    {
                        Urunler eklebabam = new Urunler();
                        eklebabam.UrunAdi = urunadi.Text;
                        eklebabam.UrunFiyati = urunfiyati.Text;
                        eklebabam.UrunParaBirimi = parabirimisec.SelectedItem.ToString();
                        db.Urunler.Add(eklebabam);
                        db.SaveChanges();
                        MessageBox.Show("Ürün başarıyla Eklendi");
                        urunadi.Clear();
                        urunfiyati.Clear();
                        dataGridView2.Refresh();
                        Form1_Load(sender, e);
                    }
                }
                catch (Exception mm)
                {
                    MessageBox.Show(mm.Message);
                }
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int xkoordinat = dataGridView2.CurrentCellAddress.X; //Seçili satırın X koordinatı
            int ykoordinat = dataGridView2.CurrentCellAddress.Y;  //Seçili satırın Y koordinatı
            string str = "";
            str = dataGridView2.Rows[ykoordinat].Cells[xkoordinat].Value.ToString();
            if (e.RowIndex == -1)
            {
                return;
            }
            try
            {
                var bul = db.Urunler.Where(p => p.UrunAdi == str).FirstOrDefault();
                urunadi.Text = bul.UrunAdi;
                urunfiyati.Text = bul.UrunFiyati;
                parabirimisec.SelectedItem = bul.UrunParaBirimi;
            }
            catch
            {
                MessageBox.Show("Ürün adını seçin");
            }
        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (label7.Text == "0") { MessageBox.Show("Masa seç"); }
            else
            {
                int xkoordinat = dataGridView3.CurrentCellAddress.X; //Seçili satırın X koordinatı
                int ykoordinat = dataGridView3.CurrentCellAddress.Y;  //Seçili satırın Y koordinatı
                string str = "";
                str = dataGridView3.Rows[ykoordinat].Cells[xkoordinat].Value.ToString();
                if (e.RowIndex == -1)
                {
                    return;
                }
                try
                {
                    var bul = db.Urunler.Where(p => p.UrunAdi == str).FirstOrDefault();
                    urunadi.Text = bul.UrunAdi;
                    urunfiyati.Text = bul.UrunFiyati;
                    parabirimisec.SelectedItem = bul.UrunParaBirimi;
                    Masalar ekle = new Masalar();
                    ekle.MasaNo = label7.Text;
                    ekle.MasaSiparisi = bul.UrunAdi;
                    db.Masalar.Add(ekle);
                    listBox1.Items.Add(ekle.MasaSiparisi);
                    db.SaveChanges();


                }
                catch
                {
                    MessageBox.Show("Ürün adını seçin");
                }
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            label7.Text = "1";
            Form1_Load(sender, e);
            m1.BackColor = Color.Yellow;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            label7.Text = "2";
            Form1_Load(sender, e);
            m2.BackColor = Color.Yellow;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label7.Text = "3";
            Form1_Load(sender, e);
            m3.BackColor = Color.Yellow;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            label7.Text = "4";
            Form1_Load(sender, e);
            m4.BackColor = Color.Yellow;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            label7.Text = "5";
            Form1_Load(sender, e);
            m5.BackColor = Color.Yellow;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            label7.Text = "6";
            Form1_Load(sender, e);
            m6.BackColor = Color.Yellow;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            label7.Text = "7";
            Form1_Load(sender, e);
            m7.BackColor = Color.Yellow;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            label7.Text = "8";
            Form1_Load(sender, e);
            m8.BackColor = Color.Yellow;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            label7.Text = "9";
            Form1_Load(sender, e);
            m9.BackColor = Color.Yellow;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            label7.Text = "10";
            Form1_Load(sender, e);
            m10.BackColor = Color.Yellow;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            label7.Text = "11";
            Form1_Load(sender, e);
            m11.BackColor = Color.Yellow;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            label7.Text = "12";
            Form1_Load(sender, e);
            m12.BackColor = Color.Yellow;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            label7.Text = "13";
            Form1_Load(sender, e);
            m13.BackColor = Color.Yellow;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            label7.Text = "14";
            Form1_Load(sender, e);
            m14.BackColor = Color.Yellow;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            label7.Text = "15";
            Form1_Load(sender, e);
            m15.BackColor = Color.Yellow;
        }

        private void urunadi_TextChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox txBox = (System.Windows.Forms.TextBox)sender;
            int pos = txBox.SelectionStart;
            int slen = txBox.SelectionLength;
            txBox.Text = txBox.Text.ToUpper();
            txBox.SelectionStart = pos;
            txBox.SelectionLength = slen;
            txBox.Focus();
        }

        private void urunfiyati_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetter(e.KeyChar) || Char.IsSymbol(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar);
        }

        private void parabirimisec_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Char.IsLetter(e.KeyChar) || Char.IsSymbol(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar) || Char.IsDigit(e.KeyChar);
        }

        private void odeparabirimisec_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tlgoster.Text != "")
            {
                KurHesapla kuragorehesapla = new KurHesapla();
                if (odeparabirimisec.SelectedItem.ToString() == "DOLAR")
                {
                    yabancigoster.Text = kuragorehesapla.TurkLirasiCevirDolar(tlgoster.Text).ToString();
                    PARA.Text = "$";
                    yabancigoster.BackColor = Color.DarkGreen;
                }
                else if (odeparabirimisec.SelectedItem.ToString() == "EURO")
                {
                    yabancigoster.Text = kuragorehesapla.TurkLirasiCevirEuro(tlgoster.Text).ToString();
                    PARA.Text = "€";
                    yabancigoster.BackColor = Color.DarkBlue;
                }
            }
        }

        private void siparisisil_Click(object sender, EventArgs e)
        {
            DialogResult Uyari = new DialogResult();
            Uyari = MessageBox.Show("Bu Masa Silinecek Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
            if (Uyari == DialogResult.Yes)
            {
                if (masano.Text == "") { MessageBox.Show("Önce Masa Seçin"); }
                else
                {
                    try
                    {
                        var sil = db.Masalar.Where(p => p.MasaNo == masano.Text).ToList();
                        foreach (var m in sil)
                        {
                            db.Masalar.Remove(m);
                            db.SaveChanges();
                        }
                        MessageBox.Show("masa silindi!");
                        Form1_Load(sender, e);
                        tlgoster.Text = "";
                        yabancigoster.Text = "";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
            listBox1.Items.Clear();
            MessageBox.Show("Masa kapandı");
            label7.Text = "0";
        }

        private void urunsil_Click(object sender, EventArgs e)
        {
            DialogResult Uyari = new DialogResult();
            Uyari = MessageBox.Show("Bu ürün Silinecek Devam Edilsin mi?", "DİKKAT!", MessageBoxButtons.YesNo);
            if (Uyari == DialogResult.Yes)
            {
                if (urunadi.Text == "" && urunfiyati.Text == "")
                {
                    MessageBox.Show("önce ürün seçin");
                }
                else
                {
                    try
                    {
                        var kotukullanicimodu = db.Urunler.Where(p => p.UrunAdi == urunadi.Text && p.UrunFiyati == urunfiyati.Text).FirstOrDefault();
                        db.Urunler.Remove(kotukullanicimodu);
                        db.SaveChanges();
                        MessageBox.Show("Ürün başarıyla silindi");
                        urunadi.Clear();
                        urunfiyati.Clear();
                        dataGridView2.Refresh();
                        Form1_Load(sender, e);
                    }
                    catch (Exception mm)
                    {
                        MessageBox.Show(mm.Message);
                    }
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int xkoordinat = dataGridView1.CurrentCellAddress.X; //Seçili satırın X koordinatı
            int ykoordinat = dataGridView1.CurrentCellAddress.Y;  //Seçili satırın Y koordinatı
            string str = "";
            str = dataGridView1.Rows[ykoordinat].Cells[xkoordinat].Value.ToString();
            string fiyatal = dataGridView1.Rows[ykoordinat].Cells[1].Value.ToString();
            if (e.RowIndex == -1)
            {
                return;
            }
            try
            {
                var bul1 = db.Urunler.Where(p => p.UrunAdi == str && p.UrunFiyati == fiyatal).FirstOrDefault();
                var bul = db.Masalar.Where(p => p.MasaSiparisi == bul1.UrunAdi && p.MasaNo==masano.Text).FirstOrDefault();
                db.Masalar.Remove(bul);
                db.SaveChanges();
                Form1_Load(sender, e);
                dataGridView1.Rows.Clear();
                try
                {
                    KurHesapla kuragorehesapla = new KurHesapla();
                    var bul2 = db.Masalar.Where(p => p.MasaNo == masano.Text).ToList();
                    int i = 0;
                    double urunfiyati, Toplam = 0;
                    foreach (var m in bul2)
                    {
                        var bul3 = db.Urunler.Where(p => p.UrunAdi == m.MasaSiparisi).FirstOrDefault();
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].Cells[0].Value = m.MasaSiparisi;
                        dataGridView1.Rows[i].Cells[1].Value = bul3.UrunFiyati;
                        i++;
                        if (bul3.UrunParaBirimi == "DOLAR") { urunfiyati = kuragorehesapla.DolarCevirTurkLirasi(bul3.UrunFiyati); }
                        else if (bul1.UrunParaBirimi == "EURO") { urunfiyati = kuragorehesapla.EuroCevirTurkLirası(bul3.UrunFiyati); }
                        else { urunfiyati = Convert.ToDouble(bul3.UrunFiyati); }
                        Toplam = Toplam + urunfiyati;
                    }
                    buton = masano.Text;
                    tlgoster.Text = Toplam.ToString();

                }
                catch (Exception cb)
                {
                    MessageBox.Show(cb.Message);
                }

                MessageBox.Show("Sipariş Ödendi");
                
            }
            catch (Exception ms)
            {
                MessageBox.Show(ms.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("seckinumur@gmail.com Bilge Adam Sınavı için Yapılmıştır. Güncelleme Ve Geliştirme Gelmeyecektir. Tüm Sorumluluk Kullanıcaya Aittir.");
        }
    }
}

