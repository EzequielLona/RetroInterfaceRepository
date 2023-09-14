using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.IO;

namespace mroSoft
{
    public partial class mainMenuForm : Form
    {
        public mainMenuForm()
        {
            InitializeComponent();
        }
        int lx, ly;
        int sw, sh;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        } 

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            lx = this.Location.X;
            ly = this.Location.Y;
            sw = this.Size.Width;
            sh = this.Size.Height;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;

            btnMaximizar.Visible = false;
            btnNormal.Visible = true;
            panelTitleBar.Width = this.ClientSize.Width - 2;

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnNormal_Click(object sender, EventArgs e)
        {

            this.Size = new Size(sw, sh);
            this.Location = new Point(lx, ly);
            btnNormal.Visible = false;
            btnMaximizar.Visible = true;
            panelTitleBar.Width = this.ClientSize.Width - 2;
        }

        private void roundTextBox1_Load(object sender, EventArgs e)
        {
            documentNumber.KeyPress += documentNumber_KeyPress;
        }

        private void documentNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Evitar que se escriban caracteres no numéricos
            }
        }

        private void docNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Evitar que se escriban caracteres no numéricos
            }
        }

        private void textBoxButton3_Click(object sender, EventArgs e)
        {

        }

        private void curvostylebutton2_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(titemNumber.Text))
            {
                using (SqlConnection cnn00 = connectBD.getconnection())
                {
                    cnn00.Open();
                    using (SqlCommand comando = new SqlCommand("SELECT LILOCN, LIMCU, LIITM, LIGLPT, LILRCJ, LIPQOH, LIJOBN, LIPID, LIUPMJ, LIUSER, IMAITM, IMDSC1, IMSRTX, LITDAY FROM [JDE_CRP].[CRPDTA].[F41021] AS T1 INNER JOIN [JDE_CRP].[CRPDTA].[F4101] AS T2 ON T1.[LIITM] = T2.[IMITM] WHERE T1.[LIITM] = (SELECT IMITM FROM [JDE_CRP].[CRPDTA].[F4101] WHERE IMLITM = @IMLITM);", cnn00))
                    {
                        comando.Parameters.AddWithValue("@IMLITM", titemNumber.Text);

                        using (SqlDataAdapter da = new SqlDataAdapter(comando))
                        {
                            DataTable table = new DataTable();
                            da.Fill(table);
                            ItitemDescription.Text = table.Rows[0]["IMDSC1"].ToString();
                            tbranchPlant.Text = table.Rows[0]["LIMCU"].ToString();
                            tbranchPlant2.Text = table.Rows[0]["LIMCU"].ToString();
                            tproductInventory.Text = table.Rows[0]["LIPQOH"].ToString();
                            tLocation.Text = table.Rows[0]["LILOCN"].ToString();
                            
                        }
                    }
                }
            }
        }
    }
}
