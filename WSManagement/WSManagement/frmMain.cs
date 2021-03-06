﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms; 
using DevExpress.UserSkins;
using DevExpress.XtraBars.Helpers;
using System.Threading;
using DevExpress.XtraTab;
using DevExpress.XtraEditors.Controls;
using DevExpress.Utils;
using DevExpress.Skins;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using System.Xml;
using BusinessLogicLayer;
using DataTransferObject;

namespace WSManagement
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmMain()
        {
            Thread t = new Thread(new ThreadStart(SplashScreen));
            t.Start(); 
            InitializeComponent();
            t.Abort(); 
        } 
        private void SplashScreen()
        {
             //load Form
            Application.Run(new frmWait());
        } 

        private void frmMain_Load(object sender, EventArgs e)
        {
            bsiFRInfo.Caption = "Server: " + SystemWS.ServerName + " - DataBase: " + SystemWS.DataBase + " - Tên Đăng Nhập: " + SystemWS.UserName; 
        }
        
        private void barAbout_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmAbout frm = new frmAbout();
            frm.ShowDialog();
        }

        private void tabMain_CloseButtonClick(object sender, EventArgs e)
        {
            int i = tabMain.SelectedTabPageIndex;
            if (i >= 0)
            {
                tabMain.TabPages.RemoveAt(i);
                tabMain.SelectedTabPageIndex = i - 1;
            }
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                tabMain_CloseButtonClick(null, null);
            }
        }
        private void ViewForm(Form frm)
        {
            try
            {
                XtraTabPage tab = new XtraTabPage();
                tab.Name = frm.Name;
                tab.Text = frm.Text;
                foreach (XtraTabPage tabTemp in tabMain.TabPages)
                {
                    if (tabTemp.Name == tab.Name)
                    {
                        tabMain.SelectedTabPage = tabTemp;
                        return;
                    }
                }
                tabMain.TabPages.Add(tab);
                tabMain.SelectedTabPage = tab;
                frm.TopLevel = false;
                frm.Parent = tab;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Dock = DockStyle.Fill;
                frm.Show();
            }
            catch { }
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
             
        }

        private void bbiEmploy_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmEmployees frm = new frmEmployees();
            ViewForm(frm);
        }

        private void bbiDepartment_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmDepartment frm = new frmDepartment();
            ViewForm(frm);
        } 
    }
}