using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdamAsmaca
{
    public partial class Form1 : Form
    {
        string secilenKelime;
        char[] harfler;
        int hataSayac = 0;
        int hataIndex = 0;
        char[] bilinenHarfler = new char[15];
        int kazandiSayac = 0;
        string[] hangmanImages = new string[]
            {
                    Application.StartupPath + @"\image\adamasmaca.png",
                    Application.StartupPath + @"\image\adamasmaca1.png",
                    Application.StartupPath + @"\image\adamasmaca2.png",
                    Application.StartupPath + @"\image\adamasmaca3.png",
                    Application.StartupPath + @"\image\adamasmaca4.png",
                    Application.StartupPath + @"\image\adamasmaca5.png",
                    Application.StartupPath + @"\image\adamasmaca6.png"
                };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public String rasgeleKelime()
        {
            string[] sehirler = { "ADANA", "ADIYAMAN", "AFYONKARAHİSAR", "AĞRI", "AKSARAY", "AMASYA", "ANKARA", "ANTALYA", "ARDAHAN", "ARTVİN", "AYDIN", "BALIKESİR", "BARTIN", "BATMAN", "BAYBURT", "BİLECİK", "BİNGÖL", "BİTLİS", "BOLU", "BURDUR", "BURSA", "ÇANAKKALE", "ÇANKIRI", "ÇORUM", "DENİZLİ", "DİYARBAKIR", "DÜZCE", "EDİRNE", "ELAZIĞ", "ERZİNCAN", "ERZURUM", "ESKİŞEHİR", "GAZİANTEP", "GİRESUN", "GÜMÜŞHANE", "HAKKARİ", "HATAY", "IĞDIR", "ISPARTA", "İSTANBUL", "İZMİR", "KAHRAMANMARAŞ", "KARABÜK", "KARAMAN", "KARS", "KASTAMONU", "KAYSERİ", "KIRIKKALE", "KIRKLARELİ", "KIRŞEHİR", "KİLİS", "KOCAELİ", "KONYA", "KÜTAHYA", "MALATYA", "MANİSA", "MARDİN", "MERSİN", "MUĞLA", "MUŞ", "NEVŞEHİR", "NİĞDE", "ORDU", "OSMANİYE", "RİZE", "SAKARYA", "SAMSUN", "ŞANLIURFA", "SİİRT", "SİNOP", "SİVAS", "ŞIRNAK", "TEKİRDAĞ", "TOKAT", "TRABZON", "TUNCELİ", "UŞAK", "VAN", "YALOVA", "YOZGAT", "ZONGULDAK" };
            Random random = new Random();
            int sayi = random.Next(1,82);
            secilenKelime = sehirler[sayi];
            return secilenKelime;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string secilenKelime = rasgeleKelime();
            
            harfSayisi(secilenKelime);
            
            for(int i = 2; i < 31; i++)
            {
                Control btn =this.Controls["button" + i.ToString()] ;
                btn.Enabled = true;
            }
            button1.Enabled = false;


        }
        public string harfSayisi(string kelime)
        {
            label2.Text = "";
            harfler = kelime.ToCharArray();
            for(int i=0;i<harfler.Length; i++)
            {
                label2.Text += "_ ";
            }

            return kelime;
        }
        public void kelimeKontrol(char harf)
        {

            label2.Text = "";
            for (int i=0;i<harfler.Length; i++)
            {
                
                if (harfler[i].Equals(harf))
                {
                    bilinenHarfler[i] = harf ;
                    hataSayac++;
                }
                
                
            }

            hataKontrol(hataSayac);
            hataSayac = 0;
            ekranaYazdir();


            

        }
        public void ekranaYazdir()
        {
            kazandiSayac = 0;
            for (int i = 0; i < harfler.Length; i++)
            {
                if (bilinenHarfler[i] == '\0')
                {
                    label2.Text += "_ ";
                    kazandiSayac++;
                }
                else
                {
                    
                    label2.Text += bilinenHarfler[i].ToString() + " ";

                }

            }
            if (kazandiSayac ==0)
            {
                MessageBox.Show("Kazandınız", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                hataIndex = 0;
                pictureBox1.Image = Image.FromFile(hangmanImages[hataIndex]);
                label2.Text = "";
                button1.Enabled = true;

                for (int i = 0; i < bilinenHarfler.Length; i++)
                {
                    bilinenHarfler[i] = '\0';
                }

                for (int i = 2; i < 31; i++)
                {
                    Control btn = this.Controls["button" + i.ToString()];
                    btn.Enabled = false;

                }



            }
        }

        public void hataKontrol(int hataSayac)
        {
            if (hataSayac == 0)
            {
                hataIndex++;

                if (hataIndex > 5)
                {
                    pictureBox1.Image = Image.FromFile(hangmanImages[hataIndex]);
                    DialogResult result =  MessageBox.Show("Kaybettiniz. Cevap: " + secilenKelime,"Mesaj",MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                    


                    for (int i = 2; i < 31; i++)
                       {
                          Control btn = this.Controls["button" + i.ToString()];
                          btn.Enabled = true;
                                                 
                       }

                    if (result == DialogResult.Retry)
                    {
                        for(int i = 0; i < bilinenHarfler.Length; i++)
                            {
                                bilinenHarfler[i] = '\0';
                            }                                            
                        hataIndex = 0;
                        

                        string secilenKelime = rasgeleKelime();
                        
                        harfSayisi(secilenKelime);
                        label2.Text = "";

                        pictureBox1.Image = Image.FromFile(hangmanImages[hataIndex]);

                        

                    }
                    else if(result == DialogResult.Cancel)
                    {
                        Application.Exit();
                    }
                

                }
                else
                {
                    pictureBox1.Image = Image.FromFile(hangmanImages[hataIndex]);
                }
                
                
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            kelimeKontrol('A');
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;
            kelimeKontrol('B');
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            kelimeKontrol('C');
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            kelimeKontrol('Ç');
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            button10.Enabled = false;
            kelimeKontrol('D');
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            button9.Enabled = false;
            kelimeKontrol('E');
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            button8.Enabled = false;
            kelimeKontrol('F');
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button7.Enabled = false;
            kelimeKontrol('G');
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button6.Enabled = false;
            kelimeKontrol('Ğ');
            
        }

        private void button11_Click(object sender, EventArgs e)
        {
            button11.Enabled = false;
            kelimeKontrol('H');
            
        }

        private void button13_Click(object sender, EventArgs e)
        {
            button13.Enabled = false;
            kelimeKontrol('I');
            
        }

        private void button12_Click(object sender, EventArgs e)
        {
            button12.Enabled = false;
            kelimeKontrol('İ');
            
        }

        private void button20_Click(object sender, EventArgs e)
        {
            button20.Enabled = false;
            kelimeKontrol('J');
            
        }

        private void button19_Click(object sender, EventArgs e)
        {
            button19.Enabled = false;
            kelimeKontrol('K');
            
        }

        private void button18_Click(object sender, EventArgs e)
        {
            button18.Enabled = false;
            kelimeKontrol('L');
            
        }

        private void button17_Click(object sender, EventArgs e)
        {
            button17.Enabled = false;
            kelimeKontrol('M');
            
        }

        private void button16_Click(object sender, EventArgs e)
        {
            button16.Enabled = false;
            kelimeKontrol('N');
            
        }

        private void button15_Click(object sender, EventArgs e)
        {
            button15.Enabled = false;
            kelimeKontrol('O');
            
        }

        private void button14_Click(object sender, EventArgs e)
        {
            button14.Enabled = false;
            kelimeKontrol('Ö');
            
        }

        private void button29_Click(object sender, EventArgs e)
        {
            button29.Enabled = false;
            kelimeKontrol('P');
            
        }

        private void button28_Click(object sender, EventArgs e)
        {
            button28.Enabled = false;
            kelimeKontrol('R');
            
        }

        private void button27_Click(object sender, EventArgs e)
        {
            button27.Enabled = false;
            kelimeKontrol('S');
            
        }

        private void button26_Click(object sender, EventArgs e)
        {
            button26.Enabled = false;
            kelimeKontrol('Ş');
            
        }

        private void button25_Click(object sender, EventArgs e)
        {
            button25.Enabled = false;
            kelimeKontrol('T');
            
        }

        private void button24_Click(object sender, EventArgs e)
        {
            button24.Enabled = false;
            kelimeKontrol('U');
            
        }

        private void button23_Click(object sender, EventArgs e)
        {
            button23.Enabled = false;
            kelimeKontrol('Ü');
            
        }

        private void button22_Click(object sender, EventArgs e)
        {
            button22.Enabled = false;
            kelimeKontrol('V');
            
        }

        private void button21_Click(object sender, EventArgs e)
        {
            button21.Enabled = false;
            kelimeKontrol('Y');
            
        }

        private void button30_Click(object sender, EventArgs e)
        {
            button30.Enabled = false;
            kelimeKontrol('Z');
           
        }
    }
}
