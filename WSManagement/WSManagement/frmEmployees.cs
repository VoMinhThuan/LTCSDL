﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BusinessLogicLayer;
using DataTransferObject;
namespace WSManagement
{
    public partial class frmEmployees : DevExpress.XtraEditors.XtraForm
    {
        public frmEmployees()
        {
            InitializeComponent();
        }

        EmployeeLogic logicEmploy;
        EmployeeData infoEmploy;
        private int TypeConmand = 0;
        DataTable dt;
        private void frmEmployees_Load(object sender, EventArgs e)
        {
            try
            {
                logicEmploy = new EmployeeLogic();
                infoEmploy = new EmployeeData();             
                gridControl1.DataSource = logicEmploy.GetAllEmployee();
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[1].Caption = "Mã Nhân Viên";
                gridView1.Columns[2].Visible = true;
                gridView1.Columns[2].Caption = "Tên Nhân Viên";
                gridView1.OptionsView.ShowAutoFilterRow = true;
                gridView1.OptionsView.ColumnAutoWidth = false;
                gridView1.BestFitColumns();
                //txtNo_.DataBindings.Add("Text", elogic.GetAllEmployee(), "No_");
                //txtFirstName.DataBindings.Add("Text", elogic.GetAllEmployee(), "FirstName");
            }
            catch (Exception ex)
            {
                Library.Message("Lỗi: " + ex.Message, "Thông báo");
            }
            DefaultSetting(true);
        }


        private void gridView1_Click(object sender, EventArgs e)
        {

            //txtNo_.DataBindings.Add("Text", elogic.GetAllEmployee(), "No_");
            //txtFirstName.DataBindings.Add("Text", logicEmploy.GetAllEmployee(), "FirstName");
        }

        private void gridControl1_Click(object sender, EventArgs e)
        { 
            //txtNo_.DataBindings.Add("Text", elogic.GetAllEmployee(), "No_");
            //txtFirstName.DataBindings.Add("Text", logicEmploy.GetAllEmployee(), "FirstName");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                logicEmploy = new EmployeeLogic();
                infoEmploy.No_ = txtNo_.Text;
                if (logicEmploy.DeleteEmployee(infoEmploy)==1)
                {
                    Library.Message("Đã xóa nhân viên thành công", "Thông Báo - " + this.Text);
                }
                else 
                    Library.Message("Không thể xóa nhân viên: " + infoEmploy.FullName + ".\n Vui lòng kiểm tra lại dữ liệu", "Thông Báo - " + this.Text);
            }
            catch (Exception ex)
            {
                Library.Message("Lỗi: " + ex.Message + " \n" + ex.ToString(), "Thông Báo - " + this.Text);
            }
            finally
            {
                gridControl1.DataSource = logicEmploy.GetAllEmployee();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "&Thêm")
            {
                btnAdd.Text = "&Hủy Bỏ";
                ResetControl();
                DefaultSetting(false);
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnSave.Enabled = true;
                TypeConmand = 1;
            }
            else if (btnAdd.Text == "&Hủy Bỏ")
            {
                TypeConmand = 0;
                btnAdd.Text = "&Thêm";
                DefaultSetting(true);
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnSave.Enabled = false;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "&Sửa")
            {
                TypeConmand = 2;
                btnEdit.Text = "&Hủy Bỏ";
                btnAdd.Enabled = false;
                gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
                btnDelete.Enabled = false;
                DefaultSetting(false);

            }
            else if (btnEdit.Text == "&Hủy Bỏ")
            {
                TypeConmand = 0;
                btnEdit.Text = "&Sửa";
                DefaultSetting(true);
                btnAdd.Enabled = true;
                btnDelete.Enabled = true;
                DefaultSetting(true);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int kq = 0;
            try
            {
                if (TypeConmand == 1)
                {
                    //insert to database
                    FillControlToDT();
                    kq = logicEmploy.InsertEmployee(infoEmploy);
                }
                else if (TypeConmand == 2)
                { 
                    //update data 
                    FillControlToDT(); 
                    kq = logicEmploy.UpdateEmployee(infoEmploy);
                }
            }
            catch (System.Data.SqlClient.SqlException  ex)
            {
                Library.Message("Lỗi: " + ex.Message + " \n " + ex.ToString(), "Thông Báo - " + this.Text); 
            }

            if (kq==1)
            {
                gridControl1.DataSource = logicEmploy.GetAllEmployee();
                TypeConmand = 0;
                btnEdit.Text = "&Sửa";
                btnAdd.Text = "&Thêm";
                btnAdd.Enabled = true;
                btnDelete.Enabled = true;
                DefaultSetting(true); 
                Library.Message("Đã cập nhật dữ liệu của nhân viên.", "Cảnh Báo");
            }
            else 
            {
                Library.Message("Không thể cập nhật dữ liệu.\nVui lòng kiểm tra lại thông tin", "Cảnh Báo");
                return; 
            }     
        }
        public void FillControlToDT()
        {
            infoEmploy = new EmployeeData();
            infoEmploy.No_ = txtNo_.Text.Trim();
            infoEmploy.No_2 = "";
            infoEmploy.FirstName = txtFirstName.Text.Trim();
            infoEmploy.LastName = txtLastName.Text.Trim();
            infoEmploy.FullName = txtFirstName.Text.Trim() + " " + txtLastName.Text.Trim();
            if (radNam.Checked)
            {
                infoEmploy.Gender="Nam";
            }
            if (radNu.Checked)
            {
                infoEmploy.Gender="Nữ";
            } 
          //
            infoEmploy.BirthDate=Convert.ToDateTime(deNgaysinh.EditValue);
            infoEmploy.PlaceOfBirth=txtNoiCap.Text.Trim();
            infoEmploy.Address=txtAddress.Text.Trim();
            infoEmploy.JobTitle= lookUpChucVu.EditValue.ToString();
            infoEmploy.Department=lookUpPhongBan.EditValue.ToString();
            infoEmploy.IDCard=txtCMND.Text.Trim();
            infoEmploy.DateOfIssue=Convert.ToDateTime(deNgayCap.EditValue);
            infoEmploy.PlaceOfIssue=txtNoiCap.Text.Trim();
            infoEmploy.Phone=txtDienThoai.Text.Trim();
            infoEmploy.Email=txtEmail.Text.Trim();
            if (radCogiadinh.Checked)
            {
                infoEmploy.MaritalStatus = "Có Gia Đình";
            }
            if (raddocthan.Checked)
            {
                infoEmploy.MaritalStatus = "Độc thân";
            } 
                infoEmploy.CreateDate=Convert.ToDateTime(deNgayVaoLam.EditValue);
            infoEmploy.Login = frmLogin.Login;
            infoEmploy.MaritalStatus=""; 
            infoEmploy.HireDate=DateTime.Now;
            infoEmploy.Status = 1;
            infoEmploy.Login = "";
        }
        private void BindingData()
        {
            if (gridView1.FocusedRowHandle<0) 
            {
                return;
            }  
            txtNo_.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "No_").ToString();
            txtFirstName.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "FirstName").ToString();
            txtLastName.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "LastName").ToString();
            if (gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Gender").ToString() == "Nam")
            {
                radNam.Checked = true;
            }
            else
                radNu.Checked = true;

            deNgaysinh.EditValue = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BirthDate").ToString();
            txtNoisinh.Text = Library.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PlaceOfBith"));

            //lookUpQuoctich.EditValue = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Quoctich").ToString();
            //lookUpTonGiao.EditValue = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Tongiao").ToString();
            //lookUpDanToc.EditValue = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Dantoc").ToString();
            if (Library.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MarialStatus")) == "Độc Thân")
            {
                raddocthan.Checked = true;
            }
            else if (Library.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MarialStatus")) == "Có Gia Đình")
            {
                radCogiadinh.Checked = true;
            }
            txtDienThoai.Text = Library.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Phone"));
            txtEmail.Text = Library.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Email"));
            txtAddress.Text = Library.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Address"));
            deNgayVaoLam.EditValue = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CreateDate").ToString();
            txtCMND.Text = Library.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CMND"));
            deNgayCap.EditValue = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DateOfIssue").ToString();
            txtNoiCap.Text = Library.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PlaceOfIssue"));
            lookUpPhongBan.EditValue = Library.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Department"));
            lookUpChucVu.EditValue= Library.IsDBNull(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "JobTitle"));            
        }
        private void LoadData()
        {
            //LoadPhongBAn 
            DepartmentLogic department=new DepartmentLogic();
            Library.LoadDataToLookup(lookUpPhongBan, department.GetData(), "Name", "No_");
        }
        private void DefaultSetting(bool flag)
        { 
            btnSave.Enabled = !flag;
            foreach (Control item in this.layoutControl1.Controls)
            {     
                if (item.GetType() == typeof(TextEdit)||item.GetType() == typeof(DateEdit)||item.GetType() == typeof(LookUpEdit))
                //if (item.GetType() == typeof(TextEdit))
                    ((TextEdit)item).Properties.ReadOnly = flag; 
            }
        }
        private void ResetControl()
        {
            txtNo_.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtNoisinh.Text = "";
            txtDienThoai.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            txtCMND.Text = "";
            txtNoiCap.Text = "";
            deNgaysinh.EditValue = DateTime.Now.ToShortDateString();
            deNgayVaoLam.EditValue = DateTime.Now.ToShortDateString();
            deNgayCap.EditValue = DateTime.Now.ToShortDateString();
            lookUpChucVu.Text = "";
            lookUpDanToc.Text = "";
            lookUpTonGiao.Text = "";
            lookUpChucVu.Text = "";
            lookUpPhongBan.Text = "";
        } 
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            BindingData();            
        }

        private void bnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
