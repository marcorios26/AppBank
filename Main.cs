using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
namespace BankApplication
{
    public partial class Main : Form
    {
        //fields
        private IconButton currentBtn;
        private Panel topBorderBtn;
        private Form CurrentChildForm;

        public Main()
        {
            InitializeComponent();
            topBorderBtn = new Panel();
            topBorderBtn.Size = new Size(7, 75);
            panelMenu.Controls.Add(topBorderBtn);
            /*this.Text = String.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;*/
        
        }
        //structure
        private struct RGBColors {
            public static Color color1 = Color.FromArgb(252, 202, 66);
            public static Color color2 = Color.FromArgb(252, 202, 66);
            public static Color color3 = Color.FromArgb(252, 202, 66);
            public static Color color4 = Color.FromArgb(252, 202, 66);
            public static Color color5 = Color.FromArgb(252, 202, 66);
            public static Color color6 = Color.FromArgb(252, 202, 66);
            public static Color color7 = Color.FromArgb(252, 202, 66);
        }

        //methods
        private void ActivateButton(object senderBtn, Color color)
        {
            DisableButton();
            if(senderBtn != null)
            {
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(50, 71, 91);
                currentBtn.ForeColor= color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextAboveImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleCenter;

                //left
                topBorderBtn.BackColor = color;
                topBorderBtn.Location = new Point(currentBtn.Location.X, 0);
                topBorderBtn.Visible = true;
                topBorderBtn.BringToFront();
                //title bar
                iconCurrentChildForm.IconChar = currentBtn.IconChar;
                iconCurrentChildForm.IconColor = color;
                label1TitleChildForm.Text = currentBtn.Text;
            }
        }
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(12, 92, 138);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageAboveText;
                currentBtn.ImageAlign = ContentAlignment.MiddleCenter;
            }
        }

        private void OpenChildForm(Form childForm)
        {
            if (CurrentChildForm != null)
            {
                CurrentChildForm.Close();
            }
            CurrentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            label1TitleChildForm.Text = currentBtn.Text;

        }
        private void icon_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new Forms.Almacenar());
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color2);
            OpenChildForm(new Forms.Modificar());
        }
        private void iconButton3_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
            OpenChildForm(new Forms.Eliminar());

            
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
            OpenChildForm(new Forms.Listar());
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color5);
            OpenChildForm(new Forms.Listado_Simple());

           
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6);
            OpenChildForm(new Forms.Movimientos());
        }
        private void iconButton1_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color7);
            OpenChildForm(new Forms.Transferencia());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            CurrentChildForm.Close();
            Reset();
        }
        private void Reset()
        {
            DisableButton();
            topBorderBtn.Visible = false;
            iconCurrentChildForm.IconChar = IconChar.Home;
            iconCurrentChildForm.IconColor = Color.Blue;
            label1TitleChildForm.Text = "HOME";
            
        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1TitleChildForm_Click(object sender, EventArgs e)
        {

        }
        //codes
        /*[DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        */
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            /*
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);*/
        }
        
        private void Main_Load(object sender, EventArgs e)
        {

        }
        
        
    }
}
