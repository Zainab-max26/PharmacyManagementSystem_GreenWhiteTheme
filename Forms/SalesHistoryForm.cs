using System;
using System.Drawing;
using System.Windows.Forms;

namespace PharmacyManagementSystem
{
    public partial class SalesHistoryForm : Form
    {
        private DataGridView dgvSales;
        private Panel headerPanel;

        public SalesHistoryForm()
        {
            InitializeComponent();
            LoadSales();
        }

        private void InitializeComponent()
        {
            this.headerPanel = new Panel();
            Label lblHeader = new Label();
            this.dgvSales = new DataGridView();

            this.headerPanel.BackColor = Color.FromArgb(46, 125, 50);
            this.headerPanel.Dock = DockStyle.Top;
            this.headerPanel.Height = 50;

            lblHeader.Text = "SALES & TRANSACTION HISTORY";
            lblHeader.ForeColor = Color.White;
            lblHeader.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblHeader.AutoSize = true;
            lblHeader.Location = new Point(170, 13);
            this.headerPanel.Controls.Add(lblHeader);

            this.dgvSales.Location = new Point(20, 70);
            this.dgvSales.Size = new Size(580, 270);
            this.dgvSales.BackgroundColor = Color.White;
            this.dgvSales.BorderStyle = BorderStyle.Fixed3D;
            this.dgvSales.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 125, 50);
            this.dgvSales.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dgvSales.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            this.dgvSales.EnableHeadersVisualStyles = false;
            this.dgvSales.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 230, 201);
            this.dgvSales.DefaultCellStyle.SelectionForeColor = Color.Black;

            this.BackColor = Color.White;
            this.ClientSize = new Size(620, 360);
            this.Controls.Add(headerPanel);
            this.Controls.Add(dgvSales);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Text = "Sales Reports";
        }

        private void LoadSales()
        {
            dgvSales.DataSource = DbHelper.ExecuteQuery("SELECT SaleID, MedicineName, Quantity, TotalPrice, SaleDate FROM Sales ORDER BY SaleID DESC");
        }
    }
}
