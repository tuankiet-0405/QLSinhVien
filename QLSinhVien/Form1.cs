using QLSinhVien.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSinhVien
{
    public partial class Form1 : Form
    {
        StudentContextDB contextDB = new StudentContextDB();
        


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StudentContextDB context = new StudentContextDB();
            List<Student> liststudents = context.Students.ToList();
            List<Faculty> listfaculties = context.Faculties.ToList();
            FillFalcultyCombobox(listfaculties);
            BindGrid(liststudents);
        }

        private void FillFalcultyCombobox(List<Faculty> listFalcultys)
        {
            this.cmbFaculty.DataSource = listFalcultys;
            this.cmbFaculty.DisplayMember = "FacultyName";
            this.cmbFaculty.ValueMember = "FacultyID";
        }
        private void BindGrid(List<Student> listStudent)
        {
            dgvBang.Rows.Clear();
            foreach (var item in listStudent)
            {
                int index = dgvBang.Rows.Add();
                dgvBang.Rows[index].Cells[0].Value = item.StudentID;
                dgvBang.Rows[index].Cells[1].Value = item.fullname;
                dgvBang.Rows[index].Cells[2].Value = item.Faculty.Facultyname;
                dgvBang.Rows[index].Cells[3].Value = item.AverageScore;
            }
        }
        private void btnADD_Click(object sender, EventArgs e)
        {
            
            Student student = new Student()
            {
                StudentID = txbMSSV.Text,
                fullname = txbName.Text,
                AverageScore = Convert.ToDouble(txbTotalGS.Text),
                FacultyID = ((Faculty)cmbFaculty.SelectedItem).FacultyID
            };
            contextDB.Students.Add(student);
            contextDB.SaveChanges();
            MessageBox.Show("Thêm thành công!");
            List<Student> liststudents = contextDB.Students.ToList();
            BindGrid(liststudents);
        }

        

        private void dgvBang_Click(object sender, EventArgs e)
        {
            
                if (dgvBang.SelectedRows.Count > 0)
                {
                    int index = dgvBang.SelectedRows[0].Index;
                    txbMSSV.Text = dgvBang.Rows[index].Cells[0].Value.ToString();
                    txbName.Text = dgvBang.Rows[index].Cells[1].Value.ToString();
                    txbTotalGS.Text = dgvBang.Rows[index].Cells[3].Value.ToString();
                    cmbFaculty.Text = dgvBang.Rows[index].Cells[2].Value.ToString();
                }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //sua du lieu vao database va bang
            Student student = contextDB.Students.FirstOrDefault(s => s.StudentID == txbMSSV.Text);
            if (student != null)
            {
                student.fullname = txbName.Text;
                student.AverageScore = Convert.ToDouble(txbTotalGS.Text);
                student.FacultyID = ((Faculty)cmbFaculty.SelectedItem).FacultyID;
                contextDB.SaveChanges();
                MessageBox.Show("Sửa thành công!");
                List<Student> liststudents = contextDB.Students.ToList();
                BindGrid(liststudents);
            }
            else
            {
                MessageBox.Show("Không tìm thấy sinh viên!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Student student = contextDB.Students.FirstOrDefault(s => s.StudentID == txbMSSV.Text);
            if(student != null)
            {
                contextDB.Students.Remove(student);
                contextDB.SaveChanges();
                MessageBox.Show("Xoa thanh cong");
                List<Student> liststudents = contextDB.Students.ToList();
                BindGrid(liststudents);
            }
            else
            {
                MessageBox.Show("Không tìm thấy sinh viên!");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Thoat?", "Thong Bao", MessageBoxButtons.OKCancel)==DialogResult.OK)
            this.Close();
        }


        private void saToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            this.Hide();
            f.ShowDialog();
        }

        private void dsaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

            Search f = new Search();
            this.Hide(); f.ShowDialog();
        }
    }
}
