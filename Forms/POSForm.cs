using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace PharmacyManagementSystem
{
    public partial class POSForm : Form
    {
        private ComboBox cmbMedicines;
        private NumericUpDown numQty;
        private Button btnSell;
        private Label lblPrice, lblTotal;
        private Panel headerPanel;
        private decimal currentPrice = 0;

        public POSForm()
        {
            InitializeComponent();
            LoadMedicines();
        }

        private void InitializeComponent()
        {
            this.headerPanel = new Panel();
            Label lblHeader = new Label();
            this.cmbMedicines = new ComboBox();
            this.numQty = new NumericUpDown();
            this.btnSell = new Button();
            this.lblPrice = new Label();
            this.lblTotal = new Label();

            this.headerPanel.BackColor = Color.FromArgb(46, 125, 50);
            this.headerPanel.Dock = DockStyle.Top;
            this.headerPanel.Height = 55;

            lblHeader.Text = "POS BILLING COUNTER";
            lblHeader.ForeColor = Color.White;
            lblHeader.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblHeader.AutoSize = true;
            lblHeader.Location = new Point(100, 16);
            this.headerPanel.Controls.Add(lblHeader);

            Label lblItem = new Label() { Text = "Select Medicine:", Location = new Point(30, 80), AutoSize = true, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold), ForeColor = Color.FromArgb(46, 125, 50) };
            this.cmbMedicines.Location = new Point(160, 78);
            this.cmbMedicines.Size = new Size(180, 25);
            this.cmbMedicines.Font = new Font("Segoe UI", 9.5f);
            this.cmbMedicines.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbMedicines.SelectedIndexChanged += CmbMedicines_SelectedIndexChanged;

            Label lblQuantity = new Label() { Text = "Quantity:", Location = new Point(30, 125), AutoSize = true, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold), ForeColor = Color.FromArgb(46, 125, 50) };
            this.numQty.Location = new Point(160, 123);
            this.numQty.Size = new Size(80, 25);
            this.numQty.Font = new Font("Segoe UI", 9.5f);
            this.numQty.Minimum = 1;
            this.numQty.Maximum = 1000;
            this.numQty.ValueChanged += NumQty_ValueChanged;

            this.lblPrice.Text = "Unit Price: RS. 0.00";
            this.lblPrice.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.lblPrice.ForeColor = Color.FromArgb(66, 66, 66);
            this.lblPrice.Location = new Point(30, 165);
            this.lblPrice.AutoSize = true;

            this.lblTotal.Text = "Total Bill: RS. 0.00";
            this.lblTotal.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.lblTotal.ForeColor = Color.FromArgb(46, 125, 50);
            this.lblTotal.Location = new Point(30, 195);
            this.lblTotal.AutoSize = true;

            this.btnSell.Text = "GENERATE INVOICE / SELL";
            this.btnSell.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.btnSell.BackColor = Color.FromArgb(46, 125, 50);
            this.btnSell.ForeColor = Color.White;
            this.btnSell.FlatStyle = FlatStyle.Flat;
            this.btnSell.FlatAppearance.BorderSize = 0;
            this.btnSell.Location = new Point(30, 240);
            this.btnSell.Size = new Size(310, 42);
            this.btnSell.Cursor = Cursors.Hand;
            this.btnSell.Click += BtnSell_Click;

            this.BackColor = Color.White;
            this.ClientSize = new Size(370, 310);
            this.Controls.Add(headerPanel);
            this.Controls.Add(lblItem);
            this.Controls.Add(cmbMedicines);
            this.Controls.Add(lblQuantity);
            this.Controls.Add(numQty);
            this.Controls.Add(lblPrice);
            this.Controls.Add(lblTotal);
            this.Controls.Add(btnSell);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Text = "POS Sales Counter";
        }

        private void LoadMedicines()
        {
            DataTable dt = DbHelper.ExecuteQuery("SELECT MedicineID, MedicineName FROM Medicines WHERE Quantity > 0");
            cmbMedicines.DataSource = dt;
            cmbMedicines.DisplayMember = "MedicineName";
            cmbMedicines.ValueMember = "MedicineID";
            cmbMedicines.SelectedIndex = -1;
            currentPrice = 0;
            lblPrice.Text = "Unit Price: RS. 0.00";
            UpdateTotal();
        }

        private void CmbMedicines_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbMedicines.SelectedIndex != -1 && cmbMedicines.Text != "")
                {
                    string medName = cmbMedicines.Text;
                    DataTable dt = DbHelper.ExecuteQuery("SELECT Price FROM Medicines WHERE MedicineName = '" + medName + "'");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        currentPrice = Convert.ToDecimal(dt.Rows[0]["Price"]);
                    }
                    else
                    {
                        currentPrice = 0;
                    }
                }
                else
                {
                    currentPrice = 0;
                }
                lblPrice.Text = "Unit Price: RS. " + currentPrice.ToString("0.00");
                UpdateTotal();
            }
            catch (Exception)
            {
                currentPrice = 0;
            }
        }

        private void NumQty_ValueChanged(object sender, EventArgs e)
        {
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            decimal total = currentPrice * numQty.Value;
            lblTotal.Text = "Total Bill: RS. " + total.ToString("0.00");
        }

        private void BtnSell_Click(object sender, EventArgs e)
        {
            if (cmbMedicines.SelectedItem == null || currentPrice == 0)
            {
                MessageBox.Show("Please select a valid medicine!", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string medName = cmbMedicines.Text;
            int qty = (int)numQty.Value;
            decimal total = currentPrice * qty;

            DataTable dt = DbHelper.ExecuteQuery("SELECT Quantity FROM Medicines WHERE MedicineName='" + medName + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                int currentQty = Convert.ToInt32(dt.Rows[0]["Quantity"]);
                if (currentQty < qty)
                {
                    MessageBox.Show("Not enough stock available! Remaining: " + currentQty, "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DbHelper.ExecuteNonQuery("UPDATE Medicines SET Quantity = Quantity - " + qty + " WHERE MedicineName='" + medName + "'");

                SQLiteParameter[] param = {
                    new SQLiteParameter("@name", medName),
                    new SQLiteParameter("@qty", qty),
                    new SQLiteParameter("@total", total),
                    new SQLiteParameter("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                };
                DbHelper.ExecuteNonQuery("INSERT INTO Sales (MedicineName, Quantity, TotalPrice, SaleDate) VALUES (@name, @qty, @total, @date)", param);

                MessageBox.Show($"Invoice Generated Successfully!\n\nItem: {medName}\nQty: {qty}\nTotal Paid: RS. {total}", "Bill Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMedicines();
            }
        }
    }
}