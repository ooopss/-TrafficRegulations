using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ПДД_Б14
{
    public partial class Form1 : Form
    {
        //Созраним логин для программы
        public int idLogin;
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            this.usersTableAdapter.Fill(this.pddDataSet.Users);
        }

        private void saveUsers()
        {
            try
            {
                this.Validate();
                this.usersBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.pddDataSet);

                LoadData();
            }
            catch (System.Exception)
            {
                MessageBox.Show("Регистрация не удалась!");
                usersDataGridView.EndEdit();
            }
        }

        private void usersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.usersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.pddDataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String login = textBox1.Text.Replace(" ", "");
            String password = textBox2.Text.Replace(" ", "");

            bool flag = false;

            for (int i = 0; i < usersDataGridView.RowCount - 1; i++)
            {
                String log = usersDataGridView.Rows[i].Cells[1].Value.ToString().Replace(" ","");
                String pass = usersDataGridView.Rows[i].Cells[2].Value.ToString().Replace(" ", "");
                if (login.Equals(log) && password.Equals(pass))
                {
                    //Открыть форму с вопросами
                    if (login.Equals("admin"))
                    {
                        Form2 frm2 = new Form2();

                        frm2.ShowDialog();
                    }
                    else
                    {
                        idLogin = Convert.ToInt32(usersDataGridView.Rows[i].Cells[0].Value);

                        saveData s = new saveData();

                        s.IdUser = idLogin;

                        Form3 frm3 = new Form3();

                        frm3.ShowDialog();
                    }
             
                        
                    flag = true;
                }
                   
            }

            if (!flag)
            {
                MessageBox.Show("Не верный логин или пароль!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String login = textBox1.Text.Replace(" ", "");
            String password = textBox2.Text.Replace(" ", "");

            
            bindingNavigatorAddNewItem.PerformClick();

            int i = usersDataGridView.RowCount - 2;
            usersDataGridView.Rows[i].Cells[1].Value = login;
            usersDataGridView.Rows[i].Cells[2].Value = password;
            saveUsers();

            LoadData();
        }
    }
}
