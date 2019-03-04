namespace LineProductMes
{
    partial class Formme
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Formme));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ANN001 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ANW011 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ANN002 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ANN003 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ANN004 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ANN005 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ANN006 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ANN007 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ANN009 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RCC006 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.yearormoney = new DevExpress.XtraEditors.ComboBoxEdit();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.timeshow = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yearormoney.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeshow.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeshow.Properties)).BeginInit();
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
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1225, 454);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ANN001,
            this.ANW011,
            this.ANN002,
            this.ANN003,
            this.ANN004,
            this.ANN005,
            this.ANN006,
            this.ANN007,
            this.ANN009,
            this.RCC006});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            // 
            // ANN001
            // 
            this.ANN001.Caption = "报工单号";
            this.ANN001.FieldName = "ANN001";
            this.ANN001.Name = "ANN001";
            this.ANN001.Visible = true;
            this.ANN001.VisibleIndex = 0;
            this.ANN001.Width = 113;
            // 
            // ANW011
            // 
            this.ANW011.Caption = "生产车间";
            this.ANW011.FieldName = "ANW011";
            this.ANW011.Name = "ANW011";
            this.ANW011.Visible = true;
            this.ANW011.VisibleIndex = 1;
            this.ANW011.Width = 98;
            // 
            // ANN002
            // 
            this.ANN002.Caption = "来源单号";
            this.ANN002.FieldName = "ANN002";
            this.ANN002.Name = "ANN002";
            this.ANN002.Visible = true;
            this.ANN002.VisibleIndex = 2;
            this.ANN002.Width = 86;
            // 
            // ANN003
            // 
            this.ANN003.Caption = "品号";
            this.ANN003.FieldName = "ANN003";
            this.ANN003.Name = "ANN003";
            this.ANN003.Visible = true;
            this.ANN003.VisibleIndex = 3;
            this.ANN003.Width = 85;
            // 
            // ANN004
            // 
            this.ANN004.Caption = "品名";
            this.ANN004.FieldName = "ANN004";
            this.ANN004.Name = "ANN004";
            this.ANN004.Visible = true;
            this.ANN004.VisibleIndex = 4;
            this.ANN004.Width = 312;
            // 
            // ANN005
            // 
            this.ANN005.Caption = "规格";
            this.ANN005.FieldName = "ANN005";
            this.ANN005.Name = "ANN005";
            this.ANN005.Visible = true;
            this.ANN005.VisibleIndex = 5;
            this.ANN005.Width = 131;
            // 
            // ANN006
            // 
            this.ANN006.Caption = "单位";
            this.ANN006.FieldName = "ANN006";
            this.ANN006.Name = "ANN006";
            this.ANN006.Visible = true;
            this.ANN006.VisibleIndex = 6;
            this.ANN006.Width = 88;
            // 
            // ANN007
            // 
            this.ANN007.Caption = "工单数量";
            this.ANN007.FieldName = "ANN007";
            this.ANN007.Name = "ANN007";
            this.ANN007.Visible = true;
            this.ANN007.VisibleIndex = 7;
            this.ANN007.Width = 85;
            // 
            // ANN009
            // 
            this.ANN009.Caption = "完工数量";
            this.ANN009.FieldName = "ANN009";
            this.ANN009.Name = "ANN009";
            this.ANN009.Visible = true;
            this.ANN009.VisibleIndex = 8;
            this.ANN009.Width = 67;
            // 
            // RCC006
            // 
            this.RCC006.Caption = "入库数量";
            this.RCC006.DisplayFormat.FormatString = "0.####";
            this.RCC006.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.RCC006.FieldName = "RCC006";
            this.RCC006.Name = "RCC006";
            this.RCC006.Visible = true;
            this.RCC006.VisibleIndex = 9;
            this.RCC006.Width = 73;
            // 
            // yearormoney
            // 
            this.yearormoney.Location = new System.Drawing.Point(12, 20);
            this.yearormoney.Margin = new System.Windows.Forms.Padding(2);
            this.yearormoney.MenuManager = this.barManager1;
            this.yearormoney.Name = "yearormoney";
            this.yearormoney.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.yearormoney.Properties.Items.AddRange(new object[] {
            "年",
            "月",
            "日"});
            this.yearormoney.Size = new System.Drawing.Size(75, 20);
            this.yearormoney.TabIndex = 0;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 26);
            this.splitContainerControl1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.timeshow);
            this.splitContainerControl1.Panel1.Controls.Add(this.yearormoney);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1225, 517);
            this.splitContainerControl1.SplitterPosition = 51;
            this.splitContainerControl1.TabIndex = 5;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // timeshow
            // 
            this.timeshow.EditValue = null;
            this.timeshow.Location = new System.Drawing.Point(137, 20);
            this.timeshow.MenuManager = this.barManager1;
            this.timeshow.Name = "timeshow";
            this.timeshow.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeshow.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeshow.Size = new System.Drawing.Size(149, 20);
            this.timeshow.TabIndex = 2;
            // 
            // Formme
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1225, 543);
            this.Controls.Add(this.splitContainerControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Formme";
            this.Text = "入库明细比对表";
            this.Load += new System.EventHandler(this.Formme_Load);
            this.Controls.SetChildIndex(this.splitContainerControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yearormoney.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.timeshow.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeshow.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn ANN001;
        private DevExpress.XtraGrid.Columns.GridColumn ANW011;
        private DevExpress.XtraGrid.Columns.GridColumn ANN002;
        private DevExpress.XtraGrid.Columns.GridColumn ANN003;
        private DevExpress.XtraGrid.Columns.GridColumn ANN004;
        private DevExpress.XtraGrid.Columns.GridColumn ANN005;
        private DevExpress.XtraGrid.Columns.GridColumn ANN006;
        private DevExpress.XtraGrid.Columns.GridColumn ANN007;
        private DevExpress.XtraGrid.Columns.GridColumn ANN009;
        private DevExpress.XtraGrid.Columns.GridColumn RCC006;
        private DevExpress.XtraEditors.ComboBoxEdit yearormoney;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.DateEdit timeshow;
    }
}