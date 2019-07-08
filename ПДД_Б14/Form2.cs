using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ПДД_Б14
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void forAdminBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.forAdminBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.pddDataSet);

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "pddDataSet.ForAdmin". При необходимости она может быть перемещена или удалена.
            this.forAdminTableAdapter.Fill(this.pddDataSet.ForAdmin);

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            ExcelApp.Columns.ColumnWidth = 15;

            ExcelApp.Cells[1, 1] = "id";
            ExcelApp.Cells[1, 2] = "id_user";
            ExcelApp.Cells[1, 3] = "id_question";
            ExcelApp.Cells[1, 4] = "count_true";
            ExcelApp.Cells[1, 5] = "count_false";

        
            for (int i = 0; i < forAdminDataGridView.ColumnCount; i++)
            {
                for (int j = 0; j < forAdminDataGridView.RowCount - 1; j++)
                {
                    ExcelApp.Cells[j + 2, i + 1] = (forAdminDataGridView[i, j].Value).ToString();
                }
            }
            ExcelApp.Visible = true;
        }
    }
}
