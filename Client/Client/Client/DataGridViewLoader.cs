using System.Data;
using System.Windows.Forms;

namespace BookstoreManagementApp
{
    public static class DataGridViewLoader
    {
        public static void LoadDataIntoDataGridView(DataGridView dataGridView, DataTable dataTable)
        {
            dataGridView.DataSource = dataTable;
        }
    }
}