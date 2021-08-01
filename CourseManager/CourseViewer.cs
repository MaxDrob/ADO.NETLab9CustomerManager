﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity;

namespace CourseManager
{
    public partial class CourseViewer : Form
    {
        private SchoolEntities schoolContext;
        public CourseViewer()
        {
            InitializeComponent();
        }

        private void CourseViewer_Load(object sender, EventArgs e)
        {
           
            schoolContext = new SchoolEntities();
            schoolContext.Department.Load();

            var departmentQuery = from d in schoolContext.Department.Include("Courses")
                                  orderby d.Name
                                  select d;

            try
            {
                this.departmentList.DisplayMember = "Name";
                this.departmentList.DataSource = schoolContext.Department.Local;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void departmentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
                Department department = this.departmentList.SelectedItem as Department;

                courseGridView.DataSource = department.Course.ToList();
                courseGridView.Columns["Department"].Visible = false;
                courseGridView.Columns["StudentGrades"].Visible = false;
                courseGridView.Columns["OnlineCourse"].Visible = false;
                courseGridView.Columns["OnsiteCourse"].Visible = false;
                courseGridView.Columns["People"].Visible = false;
                courseGridView.Columns["DepartmentId"].Visible = false;
                courseGridView.AllowUserToDeleteRows = false;
                courseGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void saveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                schoolContext.SaveChanges();
                MessageBox.Show("Your changes have been saved");
                this.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void closeForm_Click(object sender, EventArgs e)
        {
            Close();
            schoolContext.Dispose();
        }

        private void saveChanges_Click_1(object sender, EventArgs e)
        {
            try
            {
                schoolContext.SaveChanges();
                MessageBox.Show("Changes saved to the database.");
                this.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void closeForm_Click_1(object sender, EventArgs e)
        {
            this.Close();
            schoolContext.Dispose();
        }
    }
}
