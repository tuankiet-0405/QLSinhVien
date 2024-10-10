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
using System.Xml.Linq;

namespace QLSinhVien
{
    public partial class Form2 : Form
    {
        StudentContextDB contextDB = new StudentContextDB();
        public Form2()
        {
            InitializeComponent();
        }
        private void BindGrid(List<Faculty> listfaculry)
        {
            dgvBang2.Rows.Clear();
            foreach (var item in listfaculry)
            {
                int index = dgvBang2.Rows.Add();
                dgvBang2.Rows[index].Cells[0].Value = item.FacultyID;
                dgvBang2.Rows[index].Cells[1].Value = item.Facultyname;
                dgvBang2.Rows[index].Cells[2].Value = item.TotalProfessor;
            }
        }
        private void btnADD_Click(object sender, EventArgs e)
        {
            //them du lieu vao bang va database
            Faculty faculty = new Faculty()
            {
                FacultyID = txtMaKhoa.Text,
                Facultyname = txtTenKhoa.Text,
                TotalProfessor = Convert.ToInt32(txtTongGS.Text),
            };
            contextDB.Faculties.Add(faculty);
            contextDB.SaveChanges();
            BindGrid(contextDB.Faculties.ToList());
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            StudentContextDB context = new StudentContextDB();
            List<Faculty> listfaculties = context.Faculties.ToList();
            BindGrid(listfaculties);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            Faculty faculty = contextDB.Faculties.FirstOrDefault(f => f.FacultyID == txtMaKhoa.Text);
            if (faculty != null)
            {
                faculty.Facultyname = txtTenKhoa.Text;
                faculty.TotalProfessor = Convert.ToInt32(txtTongGS.Text);
                contextDB.SaveChanges();
                BindGrid(contextDB.Faculties.ToList());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //xoa du lieu bang va database

            Faculty db = contextDB.Faculties.FirstOrDefault(s => s.FacultyID == txtMaKhoa.Text);
            if (db != null)
            {
                contextDB.Faculties.Remove(db);
                contextDB.SaveChanges();
                MessageBox.Show("Xoa thanh cong");
                List<Faculty> list= contextDB.Faculties.ToList();
                BindGrid(list);
            }
            else
            {
                MessageBox.Show("Không tìm thấy sinh viên!");
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.ShowDialog();
            
        }

        private void dgvBang2_Click(object sender, EventArgs e)
        {
            if (dgvBang2.SelectedRows.Count > 0)
            {
                int index = dgvBang2.SelectedRows[0].Index;
                txtMaKhoa.Text = dgvBang2.Rows[index].Cells[0].Value.ToString();
                txtTenKhoa.Text = dgvBang2.Rows[index].Cells[1].Value.ToString();
                txtTongGS.Text = dgvBang2.Rows[index].Cells[2].Value.ToString();
            }
        }
    }
}
