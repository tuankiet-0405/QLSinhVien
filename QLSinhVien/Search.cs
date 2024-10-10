using QLSinhVien.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSinhVien
{
    public partial class Search : Form
    {
        StudentContextDB contextDB = new StudentContextDB();
        public Search()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string studentID = txtMSSV.Text;
            string fullname = txtName.Text;
            string facultyID = "";
            if (cmbKhoa.SelectedItem != null)
            {
                facultyID = ((Faculty)cmbKhoa.SelectedItem).FacultyID;
            }
            List<Student> listStudent = contextDB.Students.Where(s => (s.StudentID.Contains(studentID) || s.fullname.Contains(fullname)) && (s.FacultyID == facultyID || facultyID == "")).ToList();
            BindGrid(listStudent);
            txtTotal.Text = listStudent.Count.ToString();
        }
        private void BindGrid(List<Student> listStudent)
        {
            dgvBang3.Rows.Clear();
            foreach (var item in listStudent)
            {
                int index = dgvBang3.Rows.Add();
                dgvBang3.Rows[index].Cells[0].Value = item.StudentID;
                dgvBang3.Rows[index].Cells[1].Value = item.fullname;
                dgvBang3.Rows[index].Cells[2].Value = item.Faculty.Facultyname;
                dgvBang3.Rows[index].Cells[3].Value = item.AverageScore;
            }
        }
        private void FillFalcultyCombobox()
        {
            List<Faculty> listFalcultys = contextDB.Faculties.ToList();
            this.cmbKhoa.DataSource = listFalcultys;
            this.cmbKhoa.DisplayMember = "FacultyName";
            this.cmbKhoa.ValueMember = "FacultyID";
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            Student student = contextDB.Students.FirstOrDefault(s => s.StudentID == txtMSSV.Text);
            if (student != null) 
            {
                contextDB.Students.Remove(student);
                contextDB.SaveChanges();
                MessageBox.Show("Xoa thanh cong");
                List<Student> liststudents = contextDB.Students.ToList();
                BindGrid(liststudents);
            }
        }

        private void Search_Load(object sender, EventArgs e)
        {
            FillFalcultyCombobox();
        }
    }
}
