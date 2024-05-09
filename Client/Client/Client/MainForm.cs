using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BookstoreManagementApp
{
    public partial class MainForm : Form
    {
        private DataGridView dataGridView;
        private ListBox viewListBox;


        public MainForm()
        {
            InitializeComponent();
            this.ClientSize = new Size(800, 600);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeDataGridView();
            InitializeViewListBox();
            LoadData();
        }

        private void InitializeDataGridView()
        {
            dataGridView = new DataGridView();
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.BackgroundColor = Color.FromArgb(240, 240, 240);
            dataGridView.GridColor = Color.FromArgb(200, 200, 200);
            dataGridView.DefaultCellStyle.BackColor = Color.White;
            dataGridView.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 120, 215);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.RowHeadersVisible = false;
            dataGridView.DataBindingComplete += DataGridView_DataBindingComplete;
            Controls.Add(dataGridView);
        }

        private void InitializeViewListBox()
        {
            viewListBox = new ListBox();
            viewListBox.Dock = DockStyle.Left;
            viewListBox.Width = 140;
            viewListBox.BorderStyle = BorderStyle.None;
            viewListBox.BackColor = Color.LightGray;
            viewListBox.ForeColor = Color.Black;
            viewListBox.Font = new Font("Below", 18);
            viewListBox.SelectedIndexChanged += ViewListBox_SelectedIndexChanged;
            viewListBox.DrawMode = DrawMode.OwnerDrawFixed;
            viewListBox.DrawItem += ViewListBox_DrawItem;
            viewListBox.ItemHeight = 60;
            Controls.Add(viewListBox);
        }

        private void ViewListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            e.DrawBackground();

            string itemText = viewListBox.Items[e.Index].ToString();
            Color textColor = e.ForeColor;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                textColor = Color.Yellow;
            }

            using (SolidBrush brush = new SolidBrush(textColor))
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                e.Graphics.DrawString(itemText, viewListBox.Font, brush, e.Bounds, sf);
            }

            using (Pen pen = new Pen(Color.FromArgb(200, 200, 200), 5))
            {
                e.Graphics.DrawRectangle(pen, e.Bounds);
            }

            e.DrawFocusRectangle();
        }
        private void LoadData()
        {
            viewListBox.Items.Add("Authors");
            viewListBox.Items.Add("Genres");
            viewListBox.Items.Add("Books");
            viewListBox.Items.Add("Customers");
            viewListBox.Items.Add("Orders");
            viewListBox.Items.Add("Sales");
        }

        private void ViewListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedView = viewListBox.SelectedItem.ToString();
            switch (selectedView)
            {
                case "Authors":
                    dataGridView.DataSource = DataLoader.LoadAuthors();
                    break;
                case "Genres":
                    dataGridView.DataSource = DataLoader.LoadGenres();
                    break;
                case "Books":
                    dataGridView.DataSource = DataLoader.LoadBooks();
                    break;
                case "Customers":
                    dataGridView.DataSource = DataLoader.LoadCustomers();
                    break;
                case "Orders":
                    dataGridView.DataSource = DataLoader.LoadOrders();
                    break;
                case "Sales":
                    dataGridView.DataSource = DataLoader.LoadSales();
                    break;
                default:
                    break;
            }
        }
        private void DataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dataGridView.DataSource != null)
            {
                int columnCount = dataGridView.Columns.Count;
                int totalWidth = dataGridView.ClientSize.Width - dataGridView.RowHeadersWidth;
                int columnWidth = totalWidth / columnCount;

                for (int i = 0; i < columnCount; i++)
                {
                    dataGridView.Columns[i].Width = columnWidth;
                }
            }
        }
    }
}

