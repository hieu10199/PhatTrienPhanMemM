using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapVeNha
{
    public partial class Form1 : Form
    {
        TextBox t = new TextBox();
        Button b = new Button();
        Button b1 = new Button();
        Label lb = new Label();
        int n=0;
        private bool laNguoiChoiA;

        private int[,] banCo;

        public Form1()
        {
            InitializeComponent();
            laNguoiChoiA = true;
        }
        public void addControl()
        { 
            b.Name = "btn";
            b.Text = "In";
            b.Top = 50;
            b.Left = 700;
            this.Controls.Add(b);
            b.Click += new System.EventHandler(bt_Click);
            
            t.Name = "tb";
            t.Top = 20;
            t.Left = 700;
            this.Controls.Add(t);

            lb.Name = "lb1";
            lb.Text = "Nhap so dong x cot";
            lb.Top = 5;
            lb.Left = 700;
            this.Controls.Add(lb);

            b1.Name = "btn1";
            b1.Text = "Remove";
            b1.Top = 75;
            b1.Left = 700;
            this.Controls.Add(b1);
            b1.Click += new System.EventHandler(bt3_Click);
            

        }

        private void bt3_Click(object sender, EventArgs e)
        {

            foreach (Button button in this.Controls.OfType<Button>())
            {
                   // button.Enabled = true;
                    button.Text = "";
                    b.Text = "In";
                    b1.Text = "Remove";
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
               Button b2=(Button)sender;
               Point toaDo = (Point)b2.Tag;
  
               if (laNguoiChoiA)
               {
                   b2.Text = "X";
                  // b2.Enabled = false;
                   banCo[toaDo.X, toaDo.Y] = 1;
               }
               else
               {
                   b2.Text = "O";
                  // b2.Enabled = false;
                   banCo[toaDo.X, toaDo.Y] = 0;
               }
               
               if (kt_Ngang(banCo, toaDo)
                   || kt_Doc(banCo, toaDo)
                   || kt_CheoChinh(banCo, toaDo)
                   || kt_CheoPhu(banCo, toaDo))
               {
                   DialogResult res = MessageBox.Show("Game Over","Thong bao!",MessageBoxButtons.OK);
                   if (res == DialogResult.OK)
                   {
                       foreach (Button button in this.Controls.OfType<Button>())
                       {
                          // button.Enabled = true;
                           button.Text = "";
                           b.Text = "In";
                           b1.Text = "Remove";
                       }
                       
                   }      
                  return;
               }

               laNguoiChoiA = !laNguoiChoiA;

            //xet tat ca button khong click duoc nua
        }

        private void bt_Click(object sender, EventArgs e)
        {
            if (int.Parse(t.Text) == null)
            {
                Console.Write("");
            }
            else
            {
                n = int.Parse(t.Text);
                banCo = new int[n, n];
                addNhiuControl();
            }

        }

        public void addNhiuControl()
        {
            
            int top = 100;
            int left = 0;           
            for(int i=0; i < n; i++)
            {
                
                for (int j = 0; j < n;j++ )
                {
                    Button bn = new Button();
                    bn.Name = "bntCaro";
                    bn.Size = new Size(40, 40);
                    bn.Left = left;
                    bn.Top = top;
                    left +=40;
                    Controls.Add(bn);
                    banCo[i, j] = -1;
                    bn.Tag = new Point(i, j);
                    bn.Click += new System.EventHandler(btn_Click);
                }                
                left=0;
                top += 40;                 
            }
            
        }

        public bool kt_Ngang(int[,] banCo, Point toaDo)
        {
            ///tim vi tri start index
            int x = toaDo.X, y = toaDo.Y;
            int nguoiChoiHienTai = banCo[x,y];
            while (y >= 1 && banCo[x, y-1] == nguoiChoiHienTai)
                y -= 1;

            int dem = 1;
            while (y < n - 1 && banCo[x, y + 1] == nguoiChoiHienTai)
            {
                y += 1;
                dem += 1;
            }

            return dem >= 5;
        }

        public bool kt_Doc(int[,] banCo, Point toaDo)
        {
            int x = toaDo.X, y = toaDo.Y;
            int nguoiChoiHienTai = banCo[x, y];
            while (x >= 1 && banCo[x-1, y] == nguoiChoiHienTai)
                x -= 1;

            int dem = 1;
            while (x < n - 1 && banCo[x+1, y] == nguoiChoiHienTai)
            {
                x+= 1;
                dem += 1;
            }

            return dem >= 5;
            
        }
        int duongcheo = 0;
        public bool kt_CheoChinh(int[,] banCo, Point toaDo)
        {
            int x = toaDo.X, y = toaDo.Y;
            int nguoiChoiHienTai = banCo[x, y];
            while (x >= 1 && y >= 1 && banCo[x - 1, y - 1] == nguoiChoiHienTai)
            {
                x -= 1;
                y -= 1;
            }
            //dem
            int dem = 1;
            while (x < n - 1 && y < n - 1 && banCo[x + 1, y + 1] == nguoiChoiHienTai)
            {
                dem += 1;
                x += 1;
                y += 1;            
            }
            return dem >= 5;
        }

        public bool kt_CheoPhu(int[,] banCo, Point toaDo)
        {
            int x = toaDo.X, y = toaDo.Y;
            int nguoiChoiHienTai = banCo[x, y];
            while (x >= 1 && y < n - 1 && banCo[x - 1, y + 1] == nguoiChoiHienTai)
            {
                x -= 1;
                y += 1;
            }
            int dem = 1;
            while (x < n - 1 && y >= 1 && banCo[x + 1, y - 1] == nguoiChoiHienTai)
            {
                dem += 1;
                x += 1;
                y -= 1;
               
            }
            return dem >= 5;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            addControl();  
        }
    }
}
