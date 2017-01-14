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
        public Form1()
        {
            InitializeComponent();
            db = new SPMEntities(); // yarattım allahım seni :)
        }

        private void Form1_Load(object sender, EventArgs e) // he tabi
        {
            try
            {
                var bul = db.Masalar.ToList();
                foreach(var m in bul)
                {
                    if(m.MasaNo==m1.Text) { m1.Visible = false; }
                    if (m.MasaNo == m2.Text) { m2.Visible = false; }
                    if (m.MasaNo == m3.Text) { m3.Visible = false; }
                    if (m.MasaNo == m2.Text) { m2.Visible = false; }

                }
            }catch
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
            Button seçilensaçmasapanbutonoluşturmaşekli = sender as Button;
            masano.Text = seçilensaçmasapanbutonoluşturmaşekli.Text;
            try
            {
                var bul= db.Masalar.Where(p => p.MasaNo == masano.Text).ToList();
                int i = 0;
                foreach(var m in bul)
                {
                    var bul1 = db.Urunler.Where(p => p.UrunAdi == m.MasaSiparisi).FirstOrDefault();
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = m.MasaSiparisi;
                    dataGridView1.Rows[i].Cells[1].Value = bul1.UrunFiyati;
                    i++;
                }
            }
            catch (Exception cb)
            {
                MessageBox.Show(cb.Message);
            }
        }

        private void ode_Click(object sender, EventArgs e) //sipariş ödeme yeridir.
        {
            if(tlgoster.Text =="" && yabancigoster.Text == "")
            {
                MessageBox.Show("Bu masada sipariş yok");
            }
            else
            {

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
            if(urunadi.Text=="" && urunfiyati.Text=="")
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
            string fiyatal = dataGridView2.Rows[ykoordinat].Cells[1].Value.ToString();
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
            }catch
            {
                MessageBox.Show("Ürün adını seçin");
            }
        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (label7.Text=="0") { MessageBox.Show("Masa seç"); }
            else
            {
                int xkoordinat = dataGridView3.CurrentCellAddress.X; //Seçili satırın X koordinatı
                int ykoordinat = dataGridView3.CurrentCellAddress.Y;  //Seçili satırın Y koordinatı
                string str = "";
                str = dataGridView3.Rows[ykoordinat].Cells[xkoordinat].Value.ToString();
                string fiyatal = dataGridView2.Rows[ykoordinat].Cells[1].Value.ToString();
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
                    durum.Text = "EKLENDİ";
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
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label7.Text = "2";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label7.Text = "3";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            label7.Text = "4";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            label7.Text = "5";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            label7.Text = "6";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            label7.Text = "7";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            label7.Text = "8";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            label7.Text = "9";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            label7.Text = "10";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            label7.Text = "11";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            label7.Text = "12";
        }

        private void button16_Click(object sender, EventArgs e)
        {
            label7.Text = "13";
        }

        private void button17_Click(object sender, EventArgs e)
        {
            label7.Text = "14";
        }

        private void button18_Click(object sender, EventArgs e)
        {
            label7.Text = "15";
        }

        
        }
    }

