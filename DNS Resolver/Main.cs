using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DNS_Resolver
{
    public partial class Main : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
    (
        int nLeftRect,
        int nTopRect,
        int nRightRect,
        int nBottomRect,
        int nWidthEllipse,
        int nHeightEllipse
    );
        Point lastPoint;
        public Main()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 18, 18));

        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    if (string.IsNullOrEmpty(textBox1.Text))
                    {
                        MessageBox.Show("NO DNS!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        string domain = textBox1.Text;
                        var Address = Dns.GetHostAddresses(domain).FirstOrDefault();
                        label3.Text = Address.ToString();
                    }
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("ArgumentNullException", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("NullReferenceException", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception)
                {
                    MessageBox.Show("Exception", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (label3.Text == "DNS IP")
            {
                MessageBox.Show("No IP!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else Clipboard.SetText(label3.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var url = "https://github.com/0xRubin";
            var process = new System.Diagnostics.ProcessStartInfo();
            process.UseShellExecute = true;
            process.FileName = url;
            System.Diagnostics.Process.Start(process);
        }

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void Main_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
    }
}
