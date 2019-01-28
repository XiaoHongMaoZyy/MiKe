﻿namespace LineProductMes
{
    partial class FormSalaryByEveryOne
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose ( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components . Dispose ( );
            }
            base . Dispose ( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ( )
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSalaryByEveryOne));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ANX001 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ANX011 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ANX002 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ANX003 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ANX004 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ANX005 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.ANX006 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ANX007 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ANX008 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ANX017 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.dtTime = new DevExpress.XtraEditors.DateEdit();
            this.tYear = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tYear.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // toolExport
            // 
            this.toolExport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("toolExport.ImageOptions.Image")));
            this.toolExport.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("toolExport.ImageOptions.LargeImage")));
            this.toolExport.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // toolPrint
            // 
            this.toolPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("toolPrint.ImageOptions.Image")));
            this.toolPrint.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("toolPrint.ImageOptions.LargeImage")));
            this.toolPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemDateEdit1});
            this.gridControl1.Size = new System.Drawing.Size(1145, 331);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ANX001,
            this.ANX011,
            this.ANX002,
            this.ANX003,
            this.ANX004,
            this.ANX005,
            this.ANX006,
            this.ANX007,
            this.ANX008,
            this.ANX017});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // ANX001
            // 
            this.ANX001.Caption = "单号";
            this.ANX001.FieldName = "ANX001";
            this.ANX001.Name = "ANX001";
            this.ANX001.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "ANX001", "{0}")});
            this.ANX001.Visible = true;
            this.ANX001.VisibleIndex = 0;
            // 
            // ANX011
            // 
            this.ANX011.Caption = "工作状态";
            this.ANX011.FieldName = "ANX011";
            this.ANX011.Name = "ANX011";
            this.ANX011.Visible = true;
            this.ANX011.VisibleIndex = 1;
            // 
            // ANX002
            // 
            this.ANX002.Caption = "工号";
            this.ANX002.FieldName = "ANX002";
            this.ANX002.Name = "ANX002";
            this.ANX002.Visible = true;
            this.ANX002.VisibleIndex = 2;
            // 
            // ANX003
            // 
            this.ANX003.Caption = "姓名";
            this.ANX003.FieldName = "ANX003";
            this.ANX003.Name = "ANX003";
            this.ANX003.Visible = true;
            this.ANX003.VisibleIndex = 3;
            // 
            // ANX004
            // 
            this.ANX004.Caption = "岗位";
            this.ANX004.FieldName = "ANX004";
            this.ANX004.Name = "ANX004";
            this.ANX004.Visible = true;
            this.ANX004.VisibleIndex = 4;
            // 
            // ANX005
            // 
            this.ANX005.Caption = "计件开始";
            this.ANX005.ColumnEdit = this.repositoryItemDateEdit1;
            this.ANX005.FieldName = "ANX005";
            this.ANX005.Name = "ANX005";
            this.ANX005.Visible = true;
            this.ANX005.VisibleIndex = 5;
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.repositoryItemDateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit1.EditFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.repositoryItemDateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit1.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            // 
            // ANX006
            // 
            this.ANX006.Caption = "计件结束";
            this.ANX006.ColumnEdit = this.repositoryItemDateEdit1;
            this.ANX006.FieldName = "ANX006";
            this.ANX006.Name = "ANX006";
            this.ANX006.Visible = true;
            this.ANX006.VisibleIndex = 6;
            // 
            // ANX007
            // 
            this.ANX007.Caption = "计时开始";
            this.ANX007.ColumnEdit = this.repositoryItemDateEdit1;
            this.ANX007.FieldName = "ANX007";
            this.ANX007.Name = "ANX007";
            this.ANX007.Visible = true;
            this.ANX007.VisibleIndex = 7;
            // 
            // ANX008
            // 
            this.ANX008.Caption = "计时结束";
            this.ANX008.ColumnEdit = this.repositoryItemDateEdit1;
            this.ANX008.FieldName = "ANX008";
            this.ANX008.Name = "ANX008";
            this.ANX008.Visible = true;
            this.ANX008.VisibleIndex = 8;
            // 
            // ANX017
            // 
            this.ANX017.Caption = "工资";
            this.ANX017.DisplayFormat.FormatString = "0.##";
            this.ANX017.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.ANX017.FieldName = "ANX017";
            this.ANX017.Name = "ANX017";
            this.ANX017.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ANX017", "{0:0.######}")});
            this.ANX017.Visible = true;
            this.ANX017.VisibleIndex = 9;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 24);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.dtTime);
            this.splitContainerControl1.Panel1.Controls.Add(this.tYear);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1145, 374);
            this.splitContainerControl1.SplitterPosition = 38;
            this.splitContainerControl1.TabIndex = 5;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // dtTime
            // 
            this.dtTime.EditValue = null;
            this.dtTime.Location = new System.Drawing.Point(118, 11);
            this.dtTime.MenuManager = this.barManager1;
            this.dtTime.Name = "dtTime";
            this.dtTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTime.Size = new System.Drawing.Size(149, 20);
            this.dtTime.TabIndex = 1;
            // 
            // tYear
            // 
            this.tYear.Location = new System.Drawing.Point(12, 11);
            this.tYear.MenuManager = this.barManager1;
            this.tYear.Name = "tYear";
            this.tYear.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tYear.Properties.Items.AddRange(new object[] {
            "",
            "年",
            "月",
            "日"});
            this.tYear.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.tYear.Size = new System.Drawing.Size(100, 20);
            this.tYear.TabIndex = 0;
            // 
            // FormSalaryByEveryOne
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1145, 398);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "FormSalaryByEveryOne";
            this.Text = "个人工资明细表";
            this.Controls.SetChildIndex(this.splitContainerControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tYear.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress . XtraGrid . GridControl gridControl1;
        private DevExpress . XtraGrid . Views . Grid . GridView gridView1;
        private DevExpress . XtraGrid . Columns . GridColumn ANX001;
        private DevExpress . XtraGrid . Columns . GridColumn ANX011;
        private DevExpress . XtraGrid . Columns . GridColumn ANX002;
        private DevExpress . XtraGrid . Columns . GridColumn ANX003;
        private DevExpress . XtraGrid . Columns . GridColumn ANX004;
        private DevExpress . XtraGrid . Columns . GridColumn ANX005;
        private DevExpress . XtraGrid . Columns . GridColumn ANX006;
        private DevExpress . XtraGrid . Columns . GridColumn ANX007;
        private DevExpress . XtraGrid . Columns . GridColumn ANX008;
        private DevExpress . XtraGrid . Columns . GridColumn ANX017;
        private DevExpress . XtraEditors . SplitContainerControl splitContainerControl1;
        private DevExpress . XtraEditors . DateEdit dtTime;
        private DevExpress . XtraEditors . ComboBoxEdit tYear;
        private DevExpress . XtraEditors . Repository . RepositoryItemDateEdit repositoryItemDateEdit1;
    }
}