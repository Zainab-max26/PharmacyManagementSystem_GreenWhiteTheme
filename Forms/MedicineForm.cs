using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace PharmacyManagementSystem
{
    public partial class MedicineForm : Form
    {
        private DataGridView dgvMedicines;
        private TextBox txtName, txtPrice, txtQty;
        private Button btnAdd;
        private Panel headerPanel;

        public MedicineForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.headerPanel = new Panel();
            Label lblHeader = new Label();
            this.dgvMedicines = new DataGridView();
            this.txtName = new TextBox();
            this.txtPrice = new TextBox();
            this.txtQty = new TextBox();
            this.btnAdd = new Button();

            this.headerPanel.BackColor = Color.FromArgb(46, 125, 50);
            this.headerPanel.Dock = DockStyle.Top;
            this.headerPanel.Height = 50;

            lblHeader.Text = "MEDICINE INVENTORY";
            lblHeader.ForeColor = Color.White;
            lblHeader.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblHeader.AutoSize = true;
            lblHeader.Location = new Point(230, 13);
            this.headerPanel.Controls.Add(lblHeader);

            // DataGridView Styling
            this.dgvMedicines.Location = new Point(20, 70);
            this.dgvMedicines.Size = new Size(610, 220);
            this.dgvMedicines.BackgroundColor = Color.White;
            this.dgvMedicines.BorderStyle = BorderStyle.Fixed3D;
            this.dgvMedicines.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 125, 50);
            this.dgvMedicines.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dgvMedicines.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            this.dgvMedicines.EnableHeadersVisualStyles = false;
            this.dgvMedicines.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 230, 201);
            this.dgvMedicines.DefaultCellStyle.SelectionForeColor = Color.Black;

            Label lblName = new Label() { Text = "Name:", Location = new Point(20, 310), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(46, 125, 50) };
            this.txtName.Location = new Point(70, 307);
            this.txtName.Size = new Size(130, 23);

            Label lblPrice = new Label() { Text = "Price:", Location = new Point(215, 310), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(46, 125, 50) };
            this.txtPrice.Location = new Point(260, 307);
            this.txtPrice.Size = new Size(80, 23);

            Label lblQty = new Label() { Text = "Qty:", Location = new Point(355, 310), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(46, 125, 50) };
            this.txtQty.Location = new Point(390, 307);
            this.txtQty.Size = new Size(70, 23);

            this.btnAdd.Text = "+ Add Stock";
            this.btnAdd.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            this.btnAdd.BackColor = Color.FromArgb(46, 125, 50);
            this.btnAdd.ForeColor = Color.White;
            this.btnAdd.FlatStyle = FlatStyle.Flat;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.Location = new Point(480, 304);
            this.btnAdd.Size = new Size(150, 30);
            this.btnAdd.Cursor = Cursors.Hand;
            this.btnAdd.Click += BtnAdd_Click;

            this.BackColor = Color.White;
            this.ClientSize = new Size(650, 360);
            this.Controls.Add(headerPanel);
            this.Controls.Add(dgvMedicines);
            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            this.Controls.Add(lblPrice);
            this.Controls.Add(txtPrice);
            this.Controls.Add(lblQty);
            this.Controls.Add(txtQty);
            this.Controls.Add(btnAdd);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Text = "Medicine Inventory";
        }

        private void LoadData()
        {
            dgvMedicines.DataSource = DbHelper.ExecuteQuery("SELECT * FROM Medicines");
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text) && decimal.TryParse(txtPrice.Text, out decimal p) && int.TryParse(txtQty.Text, out int q))
            {
                string query = "INSERT INTO Medicines (MedicineName, Price, Quantity) VALUES (@n, @p, @q)";
                SQLiteParameter[] param = {
                    new SQLiteParameter("@n", txtName.Text),
                    new SQLiteParameter("@p", p),
                    new SQLiteParameter("@q", q)
                };
                DbHelper.ExecuteNonQuery(query, param);
                MessageBox.Show("Medicine Added Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Clear(); txtPrice.Clear(); txtQty.Clear();
                LoadData();
            }
            else
            {
                MessageBox.Show("Please enter valid medicine details!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
