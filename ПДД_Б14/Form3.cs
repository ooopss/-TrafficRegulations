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
    public partial class Form3 : Form
    {
        public int per;
        public int true_option;

        public int true_answer;
        public int false_answer;
        int id_ques;

        int h, m, s;
        public Form3()
        {
            InitializeComponent();
            per = 0;
            true_answer = 0;
            false_answer = 0;
            h = m = s = 0;
        }

        public void checkTrueQue()
        {
            int k = true_answer;
            if (radioButton1.Checked)
            {
                if (Convert.ToInt32(radioButton1.Tag) == true_option)
                {
                    true_answer++;
                }

            }

            if (radioButton2.Checked)
            {
                if (Convert.ToInt32(radioButton2.Tag) == true_option)
                {
                    true_answer++;
                }

            }

            if (radioButton3.Checked)
            {
                if (Convert.ToInt32(radioButton3.Tag) == true_option)
                {
                    true_answer++;
                }

            }

            if (radioButton4.Checked)
            {
                if (Convert.ToInt32(radioButton4.Tag) == true_option)
                {
                    true_answer++;
                }

            }

            if (k == true_answer)
            {
                false_answer++;
            }

            label2.Text = "Верные ответы: " + true_answer;
            label3.Text = "Неверные ответы: " + false_answer;
        }

        public void nextQues()
        {
            label1.Text = questionsDataGridView1.Rows[per].Cells[1].Value.ToString().Replace("  ", "");

            radioButton1.Text = questionsDataGridView1.Rows[per].Cells[2].Value.ToString().Replace("  ", "");
            radioButton2.Text = questionsDataGridView1.Rows[per].Cells[3].Value.ToString().Replace("  ", "");
            radioButton3.Text = questionsDataGridView1.Rows[per].Cells[4].Value.ToString().Replace("  ", "");

            if (questionsDataGridView1.Rows[per].Cells[5].Value.ToString().Replace(" ", "") != "")
            {
                radioButton4.Visible = true;
                radioButton4.Text = questionsDataGridView1.Rows[per].Cells[5].Value.ToString().Replace("  ", "");
            }
            else
            {
                radioButton4.Visible = false;
            }

            true_option = Convert.ToInt32(questionsDataGridView1.Rows[per].Cells[6].Value);
            if (questionsDataGridView1.Rows[per].Cells[7].Value.ToString().Replace("  ", "") != "")
            {
                pictureBox1.ImageLocation = @"" + questionsDataGridView1.Rows[per].Cells[7].Value.ToString().Replace(" ", "");
            }
            

        }

        private void questionsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.questionsBindingSource1.EndEdit();
            this.tableAdapterManager1.UpdateAll(this.pddDataSet1);

        }

        public void LoadData()
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "pddDataSet1.ForAdmin". При необходимости она может быть перемещена или удалена.
            this.forAdminTableAdapter1.Fill(this.pddDataSet1.ForAdmin);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "pddDataSet1.Questions". При необходимости она может быть перемещена или удалена.
            this.questionsTableAdapter1.Fill(this.pddDataSet1.Questions);
        }

       


        private void Form3_Load(object sender, EventArgs e)
        {
            
            LoadData();
            nextQues();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (per < 10)
            {
                checkTrueQue();
                saveAdmin();
                per++;
                id_ques = per;
            }
            if (per < 10)
            { 
                nextQues();
            }

            if (per==10)
            {
                MessageBox.Show("Фамилия:  " +
                    Environment.NewLine+"Верные: 4"+
                    Environment.NewLine + "Неверные: 6"+
                    Environment.NewLine + "Время: 0:1:1");
            }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_2(object sender, EventArgs e)
        {
            s++;
            if (s == 60)
            {
                m++;
                s = 0;
                if (m == 60)
                {
                    h++;
                    m = 0;
                }
            }

            label4.Text = "Время: " + h + " : "  + m + " : " + s;
        }

        private void saveForAdmin()
        {
            try
            {
                this.Validate();
                this.forAdminBindingSource1.EndEdit();
                this.tableAdapterManager1.UpdateAll(this.pddDataSet1);
                
                
            }
            catch (System.Exception)
            {

                forAdminDataGridView1.EndEdit();
            }
        }

        private void saveAdmin()
        {
            saveData s = new saveData();


            int id = s.IdUser;
            
            

            bindingNavigatorAddNewItem1.PerformClick();

            int i = forAdminDataGridView1.RowCount - 2;
            forAdminDataGridView1.Rows[i].Cells[1].Value = id;
            forAdminDataGridView1.Rows[i].Cells[2].Value = id_ques + 1;
            forAdminDataGridView1.Rows[i].Cells[3].Value = true_answer;
            forAdminDataGridView1.Rows[i].Cells[4].Value = false_answer;
            saveForAdmin();

            LoadData();
        }
    }
}
