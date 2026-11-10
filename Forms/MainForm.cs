using System;
using System.Drawing;
using System.Windows.Forms;

namespace PharmacyManagementSystem
{
    public partial class MainForm : Form
    {
        private Button btnMedicines, btnPOS, btnSalesHistory;
        private Panel headerPanel;
        private Label lblHeader;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.headerPanel = new Panel();
            this.lblHeader = new Label();
            this.btnMedicines = new Button();
            this.btnPOS = new Button();
            this.btnSalesHistory = new Button();

            this.headerPanel.BackColor = Color.FromArgb(46, 125, 50);
            this.headerPanel.Dock = DockStyle.Top;
            this.headerPanel.Height = 65;

            this.lblHeader.Text = "PHARMACY MANAGEMENT";
            this.lblHeader.ForeColor = Color.White;
            this.lblHeader.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            this.lblHeader.AutoSize = true;
            this.lblHeader.Location = new Point(60, 20);
            this.headerPanel.Controls.Add(lblHeader);

            // Styling Helper for Buttons
            StyleButton(this.btnMedicines, "1. Medicine Inventory", 100);
            this.btnMedicines.Click += (s, e) => { new MedicineForm().ShowDialog(); };

            StyleButton(this.btnPOS, "2. POS Billing Counter", 165);
            this.btnPOS.Click += (s, e) => { new POSForm().ShowDialog(); };

            StyleButton(this.btnSalesHistory, "3. Sales Reports & History", 230);
            this.btnSalesHistory.Click += (s, e) => { new SalesHistoryForm().ShowDialog(); };

            this.BackColor = Color.White;
            this.ClientSize = new Size(380, 320);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.btnMedicines);
            this.Controls.Add(this.btnPOS);
            this.Controls.Add(this.btnSalesHistory);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Text = "Main Dashboard";
        }

        private void StyleButton(Button btn, string text, int top)
        {
            btn.Text = text;
            btn.Font = new Font("Segoe UI", 10.5f, FontStyle.Bold);
            btn.BackColor = Color.FromArgb(232, 245, 233); // Soft Light Green
            btn.ForeColor = Color.FromArgb(46, 125, 50); // Dark Green Text
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = Color.FromArgb(46, 125, 50);
            btn.FlatAppearance.BorderSize = 1;
            btn.Location = new Point(50, top);
            btn.Size = new Size(280, 48);
            btn.Cursor = Cursors.Hand;
        }
    }
}
