using System;
using System.Drawing;
using System.Windows.Forms;

namespace PharmacyManagementSystem
{
    public partial class LoginForm : Form
    {
        private TextBox txtUser;
        private TextBox txtPass;
        private Button btnLogin;
        private Label lblTitle, lblUser, lblPass;
        private Panel headerPanel;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.headerPanel = new Panel();
            this.lblTitle = new Label();
            this.lblUser = new Label();
            this.txtUser = new TextBox();
            this.lblPass = new Label();
            this.txtPass = new TextBox();
            this.btnLogin = new Button();

            this.headerPanel.BackColor = Color.FromArgb(46, 125, 50); // Dark Green
            this.headerPanel.Dock = DockStyle.Top;
            this.headerPanel.Height = 60;

            this.lblTitle.Text = "PHARMACY LOGIN";
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new Point(90, 18);
            this.headerPanel.Controls.Add(lblTitle);

            this.lblUser.Text = "Username:";
            this.lblUser.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            this.lblUser.ForeColor = Color.FromArgb(46, 125, 50);
            this.lblUser.Location = new Point(40, 85);

            this.txtUser.Location = new Point(40, 108);
            this.txtUser.Size = new Size(270, 25);
            this.txtUser.Font = new Font("Segoe UI", 10);
            this.txtUser.Text = "admin";

            this.lblPass.Text = "Password:";
            this.lblPass.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            this.lblPass.ForeColor = Color.FromArgb(46, 125, 50);
            this.lblPass.Location = new Point(40, 145);

            this.txtPass.Location = new Point(40, 168);
            this.txtPass.Size = new Size(270, 25);
            this.txtPass.Font = new Font("Segoe UI", 10);
            this.txtPass.PasswordChar = '*';
            this.txtPass.Text = "admin123";

            this.btnLogin.Text = "LOGIN";
            this.btnLogin.Font = new Font("Segoe UI", 10.5f, FontStyle.Bold);
            this.btnLogin.BackColor = Color.FromArgb(46, 125, 50);
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.Location = new Point(40, 215);
            this.btnLogin.Size = new Size(270, 40);
            this.btnLogin.Cursor = Cursors.Hand;
            this.btnLogin.Click += new EventHandler(this.BtnLogin_Click);

            this.BackColor = Color.White;
            this.ClientSize = new Size(350, 290);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.lblPass);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.btnLogin);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Text = "Pharmacy System";
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (txtUser.Text == "admin" && txtPass.Text == "admin123")
            {
                MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MainForm main = new MainForm();
                this.Hide();
                main.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Credentials", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
