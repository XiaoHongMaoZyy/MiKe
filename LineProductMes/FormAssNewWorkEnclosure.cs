﻿using DevExpress . Utils . Paint;
using DevExpress . XtraEditors;
using LineProductMes . ClassForMain;
using System;
using System . Collections . Generic;
using System . ComponentModel;
using System . Data;
using System . Linq;
using System . Reflection;
using System . Windows . Forms;
using Utility;

namespace LineProductMes
{
    public partial class FormAssNewWorkEnclosure :FormChild
    {
        LineProductMesEntityu.AssNewWorkEnclosureHeaderEntity _header=null;
        LineProductMesEntityu.AssNewWorkEnclosureBodyOneEntity _bodyOne=null;
        LineProductMesEntityu.AssNewWorkEnclosureBodyTwoEntity _bodyTwo=null;
        LineProductMesBll.Bll.AssNewWorkEnclosureBll _bll=null;
        DataTable tableWork,tableViewOne,tableViewTwo,tablePInfo,tableDepart,tablePersonInfo,tableEInfo,tablePrintOne,tablePrintTwo,tableUser,tableOtherSur;
        DataRow row;
        string state=string.Empty,strWhere=string.Empty,focuseName=string.Empty;
        bool result=false;
        int selectIdx;
        List<string> idxOne;
        List<string> idxTwo;
        DateTime dt;

        public FormAssNewWorkEnclosure ( )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . AssNewWorkEnclosureBll ( );
            _header = new LineProductMesEntityu . AssNewWorkEnclosureHeaderEntity ( );
            _bodyOne = new LineProductMesEntityu . AssNewWorkEnclosureBodyOneEntity ( );
            _bodyTwo = new LineProductMesEntityu . AssNewWorkEnclosureBodyTwoEntity ( );
            idxOne = new List<string> ( );
            idxTwo = new List<string> ( );

            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 ,gridView2 ,View1 ,View2 ,View3 ,View4 ,View5 ,View6 } );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 ,gridView2 ,View1 ,View2 ,View3 ,View4 ,View5 ,View6 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            Edit5 . VistaEditTime = Edit6 . VistaEditTime = DevExpress . Utils . DefaultBoolean . True;

            layoutControlItem7 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;

            controlUnEnable ( );
            controlClear ( );
            wait . Hide ( );

            dt = LineProductMesBll . UserInfoMation . sysTime;
            InitData ( );
        }

        #region Main
        protected override int Query ( )
        {
            ChildForm . AssNewWorkEnclosureQuery from = new ChildForm . AssNewWorkEnclosureQuery ( );
            if ( from . ShowDialog ( ) == DialogResult . OK )
            {
                _header . ANT001 = from . getOddNum;

                _header = _bll . getModel ( _header . ANT001 );
                if ( _header == null )
                    return 0;

                setValue ( );

                tableViewOne = _bll . getTableViewOne ( " ANU001='" + _header . ANT001 + "'" );
                gridControl1 . DataSource = tableViewOne;
                tableViewTwo = _bll . getTableViewTwo ( " ANV001='" + _header . ANT001 + "'" );
                gridControl2 . DataSource = tableViewTwo;

                //calcuSumTime ( );
                addTotalTime ( );
                calcuSumPrice ( );
                calcuSumNum ( );
                //calcuPrice ( );

                editOtherSur ( string . Empty ,string . Empty );

                QueryTool ( );
            }

            return base . Query ( );
        }
        protected override int Add ( )
        {
            controlClear ( );
            controlEnable ( );
            txtANT001 . Text = _bll . getCode ( );
            addTool ( );
            state = "add";

            tableViewOne = _bll . getTableViewOne ( "1=2" );
            gridControl1 . DataSource = tableViewOne;
            tableViewTwo = _bll . getTableViewTwo ( "1=2" );
            gridControl2 . DataSource = tableViewTwo;

            txtANT003 . EditValue = "0507";
            txtANT008 . Text = LineProductMesBll . UserInfoMation . sysTime . ToString ( "yyyy-MM-dd" );

            layoutControlItem7 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;

            return base . Add ( );
        }
        protected override int Edit ( )
        {
            controlEnable ( );
            state = "edit";
            editTool ( );

            return base . Edit ( );
        }
        protected override int Delete ( )
        {
            if ( string . IsNullOrEmpty ( txtANT001 . Text ) )
            {
                XtraMessageBox . Show ( "请选择需要删除的内容" );
                return 0;
            }
            result = _bll . Delete ( txtANT001 . Text );
            if ( result )
            {
                XtraMessageBox . Show ( "成功删除" );
                deleteTool ( );
                controlClear ( );
            }
            else
                XtraMessageBox . Show ( "删除失败" );


            return base . Delete ( );
        }
        protected override int Save ( )
        {
            if ( getValeu ( ) == false )
            {
                 ClassForMain.FormClosingState.formClost = false;
                return 0;
            }

            wait . Show ( );
            backgroundWorker1 . RunWorkerAsync ( );

            return base . Save ( );
        }
        protected override int Cancel ( )
        {
            cancelTool ( state );
            if ( state . Equals ( "add" ) )
                controlClear ( );
            controlUnEnable ( );

            return base . Cancel ( );
        }
        protected override int Examine ( )
        {
            if ( string . IsNullOrEmpty ( txtANT001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return 0;
            }
            _header . ANT001 = txtANT001 . Text;
            state = toolExamin . Caption;
            if ( state . Equals ( "审核" ) )
                _header . ANT006 = true;
            else
                _header . ANT006 = false;
            result = _bll . Examine ( _header );
            if ( result )
            {
                XtraMessageBox . Show ( state + "成功" );
                examineTool ( state );
                if ( state . Equals ( "审核" ) )
                {
                    layoutControlItem7 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                    Graph . grPicS ( pictureEdit1 ,"审 核" );
                }
                else
                {
                    layoutControlItem7 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
                    Graph . grPicS ( pictureEdit1 ,"反" );
                }
            }
            else
                XtraMessageBox . Show ( state + "失败" );

            return base . Examine ( );
        }
        protected override int Cancellation ( )
        {
            if ( string . IsNullOrEmpty ( txtANT001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return 0;
            }
            _header . ANT001 = txtANT001 . Text;
            state = toolCancellation . Caption;
            if ( state . Equals ( "注销" ) )
                _header . ANT007 = true;
            else
                _header . ANT007 = false;
            result = _bll . Cancella ( _header  );
            if ( result )
            {
                XtraMessageBox . Show ( state + "成功" );
                cancelltionTool ( state );
                if ( state . Equals ( "注销" ) )
                {
                    layoutControlItem7 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                    Graph . grPicS ( pictureEdit1 ,"注 销" );
                }
                else
                {
                    layoutControlItem7 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
                    Graph . grPicS ( pictureEdit1 ,"反" );
                }
            }
            else
                XtraMessageBox . Show ( state + "失败" );

            return base . Cancellation ( );
        }
        protected override int Print ( )
        {
            printOrExport ( );
            Print ( new DataTable [ ] { tablePrintOne ,tablePrintTwo } ,"入库单.frx" );

            return base . Print ( );
        }
        protected override int Export ( )
        {
            printOrExport ( );
            Export ( new DataTable [ ] { tablePrintOne ,tablePrintTwo } ,"入库单.frx" );

            return base . Export ( );
        }
        #endregion

        #region Event
        private void gridView1_CustomDrawRowIndicator ( object sender ,DevExpress . XtraGrid . Views . Grid . RowIndicatorCustomDrawEventArgs e )
        {
            if ( e . Info . IsRowIndicator && e . RowHandle >= 0 )
            {
                e . Info . DisplayText = ( e . RowHandle + 1 ) . ToString ( );
            }
        }
        private void gridView2_CustomDrawRowIndicator ( object sender ,DevExpress . XtraGrid . Views . Grid . RowIndicatorCustomDrawEventArgs e )
        {
            if ( e . Info . IsRowIndicator && e . RowHandle >= 0 )
            {
                e . Info . DisplayText = ( e . RowHandle + 1 ) . ToString ( );
            }
        }
        private void backgroundWorker1_DoWork ( object sender ,DoWorkEventArgs e )
        {
            if ( state . Equals ( "add" ) )
                result = _bll . Save ( _header ,tableViewOne ,tableViewTwo );
            else
                result = _bll . Edit ( _header ,tableViewOne ,tableViewTwo ,idxOne ,idxTwo );
        }
        private void backgroundWorker1_RunWorkerCompleted ( object sender ,RunWorkerCompletedEventArgs e )
        {
            if ( e . Error == null )
            {
                wait . Hide ( );
                if ( result )
                {
                    XtraMessageBox . Show ( "成功保存" );
                     ClassForMain.FormClosingState.formClost = true;
                    controlUnEnable ( );
                    if ( state . Equals ( "add" ) )
                        _header . ANT001 = txtANT001 . Text = LineProductMesBll . UserInfoMation . oddNum;
                    else
                        _header . ANT001 = txtANT001 . Text;

                    setValue ( );

                    tableViewOne = _bll . getTableViewOne ( " ANU001='" + _header . ANT001 + "'" );
                    gridControl1 . DataSource = tableViewOne;
                    tableViewTwo = _bll . getTableViewTwo ( " ANV001='" + _header . ANT001 + "'" );
                    gridControl2 . DataSource = tableViewTwo;

                    addTotalTime ( );
                    calcuSumPrice ( );
                    calcuPrice ( );
                    calcuSumNum ( );

                    editOtherSur ( string . Empty ,string . Empty );

                    saveTool ( );
                }
                else
                {
                    XtraMessageBox . Show ( "保存失败" );
                     ClassForMain.FormClosingState.formClost = false;
                }
            }
        }
        private void gridControl1_KeyPress ( object sender ,System . Windows . Forms . KeyPressEventArgs e )
        {
            if ( toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always && e . KeyChar == ( char ) Keys . Enter )
            {
                row = gridView1 . GetFocusedDataRow ( );
                if ( row == null )
                    return;
                _bodyOne . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                if ( _bodyOne . idx > 0 && !idxOne . Contains ( _bodyOne . idx . ToString ( ) ) )
                    idxOne . Add ( _bodyOne . idx . ToString ( ) );
                tableViewOne . Rows . Remove ( row );
                gridControl1 . RefreshDataSource ( );
            }
        }
        private void gridControl2_KeyPress ( object sender ,KeyPressEventArgs e )
        {
            if ( toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always && e . KeyChar == ( char ) Keys . Enter )
            {
                row = gridView2 . GetFocusedDataRow ( );
                if ( row == null )
                    return;
                _bodyTwo . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                if ( _bodyTwo . idx > 0 && !idxTwo . Contains ( _bodyTwo . idx . ToString ( ) ) )
                    idxTwo . Add ( _bodyTwo . idx . ToString ( ) );
                tableViewTwo . Rows . Remove ( row );
                gridControl2 . RefreshDataSource ( );
            }
        }
        private void txtANT003_EditValueChanged ( object sender ,EventArgs e )
        {
            if ( txtANT003 . EditValue == null || txtANT003 . EditValue . ToString ( ) == string . Empty )
                return;
            tableUser = _bll . getDepart ( txtANT003 . EditValue . ToString ( ) );
            txtANT005 . Properties . DataSource = tableUser;
            txtANT005 . Properties . DisplayMember = "DAA002";
            txtANT005 . Properties . ValueMember = "DAA001";

            txtANT005 . EditValue = "050708";
        }
        private void gridView1_CellValueChanged ( object sender ,DevExpress . XtraGrid . Views . Base . CellValueChangedEventArgs e )
        {
            row = gridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            if ( e . Column . FieldName == "ANU010" )
            {
                calcuSumNum ( );
                calcuSumPrice ( );
            }
            else if ( e . Column . FieldName == "ANU007" )
            {
                calcuSumPrice ( );
            }
            //else if ( e . Column . FieldName == "ANU008" || e . Column . FieldName == "ANU009" )
            //{
            //    addPerson ( );
            //}
        }
        private void Edit7_EditValueChanged ( object sender ,EventArgs e )
        {
            DevExpress . XtraEditors . BaseEdit edit = gridView1 . ActiveEditor;
            switch ( gridView1 . FocusedColumn . FieldName )
            {
                case "ANU008":
                if ( edit . EditValue == null )
                    return;
                row = tableDepart . Select ( "DAA001='" + edit . EditValue . ToString ( ) + "'" ) [ 0 ];
                if ( row == null )
                    return;
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "ANU009" ] ,row [ "DAA002" ] . ToString ( ) );
                break;
            }
        }
        private void Edit3_EditValueChanged ( object sender ,EventArgs e )
        {
            DevExpress . XtraEditors . BaseEdit edit = gridView2 . ActiveEditor;
            switch ( gridView2 . FocusedColumn . FieldName )
            {
                case "ANV002":
                if ( edit . EditValue == null )
                    return;
                row = tablePersonInfo . Select ( "EMP001='" + edit . EditValue . ToString ( ) + "'" ) [ 0 ];
                if ( row == null )
                    return;
                _bodyTwo . ANV003 = row [ "EMP002" ] . ToString ( );
                _bodyTwo . ANV004 = row [ "EMP007" ] . ToString ( );
                _bodyTwo . ANV008 = row [ "DAA002" ] . ToString ( );
                gridView2 . SetFocusedRowCellValue ( gridView2 . Columns [ "ANV003" ] ,_bodyTwo . ANV003 );
                gridView2 . SetFocusedRowCellValue ( gridView2 . Columns [ "ANV004" ] ,_bodyTwo . ANV004 );
                gridView2 . SetFocusedRowCellValue ( gridView2 . Columns [ "ANV007" ] ,"在职" );
                gridView2 . SetFocusedRowCellValue ( gridView2 . Columns [ "ANV008" ] ,_bodyTwo . ANV008 );
                break;
            }
        }
        private void Edit2_EditValueChanged ( object sender ,EventArgs e )
        {
            DevExpress . XtraEditors . BaseEdit edit = gridView1 . ActiveEditor;
            switch ( gridView1 . FocusedColumn . FieldName )
            {
                case "ANU002":
                if ( edit . EditValue == null )
                    return;
                if ( tableEInfo . Select ( "ANW001='" + edit . EditValue . ToString ( ) + "'" ) . Length < 1 )
                    return;
                row = tableEInfo . Select ( "ANW001='" + edit . EditValue . ToString ( ) + "'" ) [ 0 ];
                if ( row == null )
                    return;
                _bodyOne . ANU008 = row [ "ANW012" ] . ToString ( );
                _bodyOne . ANU009 = row [ "ANW013" ] . ToString ( );
                _bodyOne . ANU006 = row [ "ANW009" ] . ToString ( );
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "ANU008" ] ,_bodyOne . ANU008 );
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "ANU009" ] ,_bodyOne . ANU009 );
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "ANU010" ] ,_bodyOne . ANU006 );
                break;
            }
        }
        private void gridView2_CellValueChanged ( object sender ,DevExpress . XtraGrid . Views . Base . CellValueChangedEventArgs e )
        {
            selectIdx = gridView2 . FocusedRowHandle;
            row = gridView2 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            if ( e . Column . FieldName == "ANV005" || e . Column . FieldName == "ANV006" )
            {
                if ( e . Column . FieldName == "ANV005" && workShopTime . startTimeFJ ( row ,e . Value ) == false )
                {
                    row [ "ANV005" ] = DBNull . Value;
                }
                if ( e . Column . FieldName == "ANV006" && workShopTime . endTimeFJ ( row ,e . Value ) == false )
                {
                    row [ "ANV006" ] = DBNull . Value;
                }
                addRow ( e . Column . FieldName ,e . RowHandle ,e . Value );
                calcuSumTime ( );
            }
            else if ( e . Column . FieldName == "ANV002" || e . Column . FieldName == "ANV003" || e . Column . FieldName == "ANV004" ||  e . Column . FieldName == "ANV008" )
            {
                if ( row [ "ANV005" ] == null || row [ "ANV005" ] . ToString ( ) == string . Empty )
                {
                    row [ "ANV005" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                }
                if ( row [ "ANV006" ] == null || row [ "ANV006" ] . ToString ( ) == string . Empty )
                {
                    row [ "ANV006" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
                }
                if ( row [ "ANV007" ] == null || row [ "ANV007" ] . ToString ( ) == string . Empty )
                {
                    row [ "ANV007" ] = "在职";
                }
                calcuSumTime ( );
            }
        }
        private void Edit1_EditValueChanged ( object sender ,EventArgs e )
        {
            BaseEdit edit = gridView1 . ActiveEditor;
            switch ( gridView1 . FocusedColumn . FieldName )
            {
                case "ANU003":
                if ( edit . EditValue == null )
                    return;
                row = tablePInfo . Select ( "RAA001='" + edit . EditValue + "'" ) [ 0 ];
                if ( row == null )
                    return;
                if ( row [ "RAA008" ] == null || row [ "RAA008" ] . ToString ( ) == string . Empty )
                {
                    gridView1 . SetFocusedRowCellValue ( "ANU003" ,null );
                    return;
                }
                _bodyOne . ANU004 = row [ "RAA015" ] . ToString ( );
                _bodyOne . ANU005 = row [ "DEA002" ] . ToString ( );
                _bodyOne . ANU006 = row [ "ART003" ] . ToString ( );
                _bodyOne . ANU007 = string . IsNullOrEmpty ( row [ "ART004" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ART004" ] . ToString ( ) );
                gridView1 . SetFocusedRowCellValue ( "ANU004" ,_bodyOne . ANU004 );
                gridView1 . SetFocusedRowCellValue ( "ANU005" ,_bodyOne . ANU005 );
                gridView1 . SetFocusedRowCellValue ( "ANU006" ,_bodyOne . ANU006 );
                gridView1 . SetFocusedRowCellValue ( "ANU007" ,_bodyOne . ANU007 );
                break;
            }
        }
        private void txtu0_TextChanged ( object sender ,EventArgs e )
        {
            calcuPrice ( );
        }
        private void txtu2_TextChanged ( object sender ,EventArgs e )
        {
            calcuPrice ( );
        }
        private void txtANT005_EditValueChanged ( object sender ,EventArgs e )
        {
            if ( txtANT003 . EditValue == null || txtANT003 . EditValue . ToString ( ) == string . Empty )
                return;
            if ( txtANT005 . EditValue == null || txtANT005 . EditValue . ToString ( ) == string . Empty )
                return;

            gridView2 . CloseEditor ( );
            gridView2 . UpdateCurrentRow ( );

            if ( tableViewTwo != null )
                tableViewTwo . Rows . Clear ( );

            DateTime dtNow = LineProductMesBll . UserInfoMation . sysTime;
            DataRow [ ] rows = tablePersonInfo . Select ( "EMP005='" + txtANT005 . EditValue + "'" );
            DataRow row;
            if ( tableViewTwo == null )
                return;
            if ( tableViewTwo . Rows . Count < 1 )
            {
                foreach ( DataRow ro in rows )
                {
                    if ( ro != null )
                    {
                        row = tableViewTwo . NewRow ( );
                        row [ "ANV002" ] = ro [ "EMP001" ];
                        row [ "ANV003" ] = ro [ "EMP002" ];
                        row [ "ANV004" ] = ro [ "EMP007" ];
                        row [ "ANV008" ] = ro [ "DAA002" ];
                        row [ "ANV005" ] = dtNow . ToString ( "yyyy-MM-dd 08:00" );
                        row [ "ANV006" ] = dtNow . ToString ( "yyyy-MM-dd 17:00" );
                        row [ "ANV007" ] = "在职";
                        tableViewTwo . Rows . Add ( row );
                        gridControl2 . Refresh ( );
                    }
                }
            }
            else
            {
                foreach ( DataRow ro in rows )
                {
                    if ( tableViewTwo . Select ( "ANV002='" + ro [ "EMP001" ] . ToString ( ) + "'" ) . Length < 1 )
                    {
                        row = tableViewTwo . NewRow ( );
                        row [ "ANV002" ] = ro [ "EMP001" ] . ToString ( );
                        row [ "ANV003" ] = ro [ "EMP002" ] . ToString ( );
                        row [ "ANV004" ] = ro [ "EMP007" ] . ToString ( );
                        row [ "ANV008" ] = ro [ "DAA002" ];
                        row [ "ANV005" ] = dtNow . ToString ( "yyyy-MM-dd 08:00" );
                        row [ "ANV006" ] = dtNow . ToString ( "yyyy-MM-dd 17:00" );
                        row [ "ANV007" ] = "在职";
                        tableViewTwo . Rows . Add ( row );
                        gridControl2 . Refresh ( );
                    }
                }
            }
            calcuSumTime ( );
        }
        protected override void OnClosing ( CancelEventArgs e )
        {
            if ( toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always )
            {
                if ( txtANT003 . Text == string . Empty || tableViewOne == null || tableViewOne . Rows . Count < 1 )
                    return;
                if ( XtraMessageBox . Show ( "是否保存?" ,"提示" ,MessageBoxButtons . OKCancel ) == DialogResult . OK )
                {
                    Save ( );
                    if (  ClassForMain.FormClosingState.formClost == false )
                        e . Cancel = true;
                }
            }

            base . OnClosing ( e );
        }
        private void txtANT009_TextChanged ( object sender ,EventArgs e )
        {
            calcuSumTime ( );
        }
        private void txtANT010_TextChanged ( object sender ,EventArgs e )
        {
            calcuSumTime ( );
        }
        private void btnEdit_ButtonClick ( object sender ,DevExpress . XtraEditors . Controls . ButtonPressedEventArgs e )
        {
            DataRow row = gridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            tableEInfo = _bll . getTableOrder ( row [ "ANU003" ] . ToString ( ) ,row [ "ANU004" ] . ToString ( ) );
            ChildForm . FormAssNewList from = new ChildForm . FormAssNewList ( tableEInfo );
            if ( from . ShowDialog ( ) == DialogResult . OK )
            {
                DataRow ro = from . getDataRow;
                if ( ro == null )
                    return;
                row [ "ANU002" ] = ro [ "ANW001" ];
                row [ "ANU008" ] = ro [ "ANW012" ];
                row [ "ANU009" ] = ro [ "ANW013" ];
                row [ "ANU011" ] = ro [ "ANW009" ];

                editOtherSur ( row [ "ANU003" ] . ToString ( ) ,row [ "ANU004" ] . ToString ( ) );
            }
        }
        private void gridView1_RowCellStyle ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellStyleEventArgs e )
        {
            if ( e . Column . FieldName == "U4" )
            {
                e . Appearance . BackColor = System . Drawing . Color . LightSteelBlue;
            }
        }
        private void contextMenuStrip1_ItemClicked ( object sender ,ToolStripItemClickedEventArgs e )
        {
            CopyUtils . copyResult ( gridView1 ,focuseName );
        }
        private void contextMenuStrip2_ItemClicked ( object sender ,ToolStripItemClickedEventArgs e )
        {
            CopyUtils . copyResult ( gridView2 ,focuseName );
        }
        private void gridView1_RowCellClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellClickEventArgs e )
        {
            focuseName = e . Column . FieldName;
        }
        private void gridView2_RowCellClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellClickEventArgs e )
        {
            focuseName = e . Column . FieldName;
        }
        #endregion

        #region Method
        void controlUnEnable ( )
        {
            txtANT003 . ReadOnly = txtANT005 . ReadOnly = txtANT009 . ReadOnly = txtANT010 . ReadOnly = true;
            gridView1 . OptionsBehavior . Editable = gridView2 . OptionsBehavior . Editable = false;
        }
        void controlEnable ( )
        {
            txtANT003 . ReadOnly = txtANT005 . ReadOnly = txtANT009 . ReadOnly = txtANT010 . ReadOnly = false;
            gridView1 . OptionsBehavior . Editable = gridView2 . OptionsBehavior . Editable = true;
        }
        void controlClear ( )
        {
            txtANT001 . Text = txtANT003 . Text = txtANT005 . Text = txtu0 . Text = txtu1 . Text = txtu2 . Text = txtANT009 . Text = txtANT010 . Text = string . Empty;
            gridControl1 . DataSource = null;
            gridControl2 . DataSource = null;
        }
        void InitData ( )
        {
            tableWork = _bll . getDepart ( );

            txtANT003 . Properties . DataSource = tableWork;
            txtANT003 . Properties . DisplayMember = "DAA002";
            txtANT003 . Properties . ValueMember = "DAA001";

            tablePInfo = _bll . getTablePInfo ( );
            Edit1 . DataSource = tablePInfo;
            Edit1 . DisplayMember = "RAA001";
            Edit1 . ValueMember = "RAA001";

            //tableEInfo = _bll . getTableOrder ( );
            //Edit2 . DataSource = tableEInfo;
            //Edit2 . DisplayMember = "ANW001";
            //Edit2 . ValueMember = "ANW001";

            tablePersonInfo= _bll . getPersonInfo ( );
            Edit3 . DataSource = tablePersonInfo;
            Edit3 . DisplayMember = "EMP001";
            Edit3 . ValueMember = "EMP001";

            tableDepart = _bll . getDepart ( "0507" );
            Edit7 . DataSource = tableDepart;
            Edit7 . DisplayMember = "DAA001";
            Edit7 . ValueMember = "DAA001";
        }
        bool getValeu ( )
        {
            result = true;
            if ( string . IsNullOrEmpty ( txtANT003 . Text ) )
            {
                XtraMessageBox . Show ( "请选择部门" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtANT005 . Text ) )
            {
                XtraMessageBox . Show ( "请选择班组" );
                return false;
            }
            gridView1 . CloseEditor ( );
            gridView1 . UpdateCurrentRow ( );
            if ( tableViewOne == null || tableViewOne . Rows . Count < 1 )
                return false;

            gridView2 . CloseEditor ( );
            gridView2 . UpdateCurrentRow ( );
            if ( tableViewTwo == null || tableViewTwo . Rows . Count < 1 )
                return false;

            _header . ANT001 = txtANT001 . Text;
            _header . ANT002 = txtANT003 . EditValue . ToString ( );
            _header . ANT003 = txtANT003 . Text;
            _header . ANT004 = txtANT005 . EditValue . ToString ( );
            _header . ANT005 = txtANT005 . Text;
            _header . ANT008 = Convert . ToDateTime ( txtANT008 . Text );
            _header . ANT009 = string . IsNullOrEmpty ( txtANT009 . Text ) == true ? 0 : Convert . ToDecimal ( txtANT009 . Text );
            _header . ANT010 = string . IsNullOrEmpty ( txtANT010 . Text ) == true ? 0 : Convert . ToDecimal ( txtANT010 . Text );
            gridView1 . ClearColumnErrors ( );
            for ( int i = 0 ; i < gridView1 . RowCount ; i++ )
            {
                row = gridView1 . GetDataRow ( i );
                if ( row != null )
                {
                    row . ClearErrors ( );
                    _bodyOne . ANU002 = row [ "ANU002" ] . ToString ( );
                    if ( string . IsNullOrEmpty ( _bodyOne . ANU002 ) )
                    {
                        row . SetColumnError ( "ANU002" ,"不可为空" );
                        result = false;
                        break;
                    }
                    _bodyOne . ANU003 = row [ "ANU003" ] . ToString ( );
                    if ( string . IsNullOrEmpty ( _bodyOne . ANU003 ) )
                    {
                        row . SetColumnError ( "ANU003" ,"不可为空" );
                        result = false;
                        break;
                    }
                    _bodyOne . ANU010 = string . IsNullOrEmpty ( row [ "ANU010" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "ANU010" ] );
                    _bodyOne . ANU011 = string . IsNullOrEmpty ( row [ "U4" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "U4" ] );
                    if ( _bodyOne . ANU010 > _bodyOne . ANU011 )
                    {
                        row . SetColumnError ( "ANU010" ,"数量多于未完工数量" );
                        result = false;
                        break;
                    }
                }
            }
            if ( result == false )
                return result;

            gridView2 . ClearColumnErrors ( );
            for ( int i = 0 ; i < gridView2 . RowCount ; i++ )
            {
                row = gridView2 . GetDataRow ( i );
                if ( row != null )
                {
                    row . ClearErrors ( );
                    _bodyTwo . ANV002 = row [ "ANV002" ] . ToString ( );
                    if ( string . IsNullOrEmpty ( _bodyTwo . ANV002 ) )
                    {
                        row . SetColumnError ( "ANV002" ,"不可为空" );
                        result = false;
                        break;
                    }
                }
            }
            if ( result == false )
                return result;

            var query = from p in tableViewOne . AsEnumerable ( )
                        group p by new
                        {
                            p1 = p . Field<string> ( "ANU002" ) ,
                            p2 = p . Field<string> ( "ANU003" )
                        } into m
                        select new
                        {
                            anu002 = m . Key . p1 ,
                            anu003 = m . Key . p2 ,
                            count = m . Count ( )
                        };

            if ( query != null )
            {
                foreach ( var x in query )
                {
                    if ( x . count > 1 )
                    {
                        XtraMessageBox . Show ( "来源工单:" + x . anu003 + "\n\r来源组装报工单:" + x . anu002 + "\n\r重复,请核实" ,"提示" );
                        result = false;
                        break;
                    }
                }
            }

            if ( result == false )
                return result;

            var quer = from t in tableViewTwo . AsEnumerable ( )
                       group t by new
                       {
                           t1 = t . Field<string> ( "ANV002" )
                       } into m
                       select new
                       {
                           anv002 = m . Key . t1 ,
                           count = m . Count ( )
                       };

            if ( quer != null )
            {
                foreach ( var x in quer )
                {
                    if ( x . count > 1 )
                    {
                        XtraMessageBox . Show ( "工号:" + x . anv002 + "\n\r重复,请核实" ,"提示" );
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }
        void setValue ( )
        {
            txtANT001 . Text = _header . ANT001;
            txtANT003 . EditValue = _header . ANT002;
            txtANT005 . EditValue = _header . ANT004;
            txtANT009 . Text = Convert . ToDecimal ( _header . ANT009 ) . ToString ( "0.#" );
            txtANT010 . Text = Convert . ToDecimal ( _header . ANT010 ) . ToString ( "0.#" );
            txtANT008 . Text = Convert . ToDateTime ( _header . ANT008 ) . ToString ( "yyyy-MM-dd" );
            Graph . grPicS ( pictureEdit1 ,"反" );
            layoutControlItem7 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
            if ( _header . ANT006 )
            {
                layoutControlItem7 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                Graph . grPicS ( pictureEdit1 ,"审 核" );
                examineTool ( "审核" );
            }
            else
                examineTool ( "反审核" );
            if ( _header . ANT007 )
            {
                layoutControlItem7 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                Graph . grPicS ( pictureEdit1 ,"注 销" );
                cancelltionTool ( "注销" );
            }
            else
                cancelltionTool ( "反注销" );
           
            //addPerson ( );
        }
        void calcuSumNum ( )
        {
            if ( tableViewOne . Compute ( "SUM(ANU010)" ,null ) == DBNull.Value )
                return;
            int sunNum = Convert . ToInt32 ( tableViewOne . Compute ( "SUM(ANU010)" ,null ) );
            txtu1 . Text = sunNum . ToString ( );
        }
        void calcuSumPrice ( )
        {
            int numCount = 0;
            decimal priceCount = 0M, priceSum = 0M;

            gridView1 . CloseEditor ( );
            gridView1 . UpdateCurrentRow ( );
            foreach ( DataRow row in tableViewOne . Rows )
            {
                numCount = string . IsNullOrEmpty ( row [ "ANU010" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "ANU010" ] . ToString ( ) );
                priceCount = string . IsNullOrEmpty ( row [ "ANU007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANU007" ] . ToString ( ) );
                priceSum += numCount * priceCount;
            }
            txtu2 . Text = priceSum . ToString ( "0.#" );
        }
        void calcuSumTime ( )
        {
            gridView2 . CloseEditor ( );
            gridView2 . UpdateCurrentRow ( );

            if ( tableViewTwo == null || tableViewTwo . Rows . Count < 1 )
                return;
            DateTime dtOne, dtTwo;
            decimal sunNum = 0;

            decimal ant009 = txtANT009 . Text == string . Empty ? 0 : Convert . ToDecimal ( txtANT009 . Text ) * 60;
            decimal ant010 = txtANT010 . Text == string . Empty ? 0 : Convert . ToDecimal ( txtANT010 . Text ) * 60;

            foreach ( DataRow row in tableViewTwo . Rows )
            {
                //ANV007
                if ( string . IsNullOrEmpty ( row [ "ANV007" ] . ToString ( ) ) || row [ "ANV007" ] . ToString ( ) . Equals ( "离职" ) || row [ "ANV007" ] . ToString ( ) . Equals ( "未上班" ) )
                {
                    row [ "ANV009" ] = 0;
                    row [ "ANV010" ] = 0;
                    continue;
                }

                if ( !string . IsNullOrEmpty ( row [ "ANV005" ] . ToString ( ) ) && !string . IsNullOrEmpty ( row [ "ANV006" ] . ToString ( ) ) )
                {
                    dtOne = Convert . ToDateTime ( row [ "ANV005" ] . ToString ( ) );
                    dtTwo = Convert . ToDateTime ( row [ "ANV006" ] . ToString ( ) );
                    sunNum = ( dtTwo - dtOne ) . Hours + ( dtTwo - dtOne ) . Minutes * Convert . ToDecimal ( 1.0 ) / 60;
                    if ( dtOne . Hour <= 11 && dtTwo . Hour >= 12 )
                    {
                        sunNum = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( ant009 ) ) * Convert . ToDecimal ( 1.0 ) / 60;
                        if ( dtTwo . Hour >= 17 && dtTwo . Minute >= 30 )
                            sunNum = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( ant009 ) - Convert . ToDecimal ( ant010 ) ) * Convert . ToDecimal ( 1.0 ) / 60;
                    }
                    else if ( dtTwo . Hour >= 17 && dtTwo . Minute >= 30 )
                        sunNum = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( ant010 ) ) * Convert . ToDecimal ( 1.0 ) / 60;
                    row [ "ANV009" ] = Math . Round ( sunNum ,1 ,MidpointRounding . AwayFromZero );
                }
                else
                    row [ "ANV009" ] = 0;

                row [ "ANV010" ] = Math . Round ( string . IsNullOrEmpty ( row [ "ANV009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANV009" ] ) * ( string . IsNullOrEmpty ( row [ "U1" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "U1" ] ) ) ,1 ,MidpointRounding . AwayFromZero );
            }
            addTotalTime ( );
        }
        void addTotalTime ( )
        {
            txtu0 . Text = Math . Round ( ANV009 . SummaryItem . SummaryValue == null ? 0 : Convert . ToDecimal ( ANV009 . SummaryItem . SummaryValue ) ,1 ,MidpointRounding . AwayFromZero ) . ToString ( "0.#" );
        }
        void calcuPrice ( )
        {
            if ( string . IsNullOrEmpty ( txtu0 . Text ) )
                return;
            if ( string . IsNullOrEmpty ( txtu2 . Text ) )
                return;
            decimal price = Convert . ToDecimal ( txtu0 . Text ) == 0 ? 0 : Convert . ToDecimal ( txtu2 . Text ) / Convert . ToDecimal ( txtu0 . Text );
            for ( int i = 0 ; i < gridView2.RowCount ; i++ )
            {
                row = gridView2 . GetDataRow ( i );
                if ( row != null )
                {
                    row [ "U1" ] = price . ToString ( "0.##" );
                    row [ "ANV010" ] = Math . Round ( Math . Round ( price ,2 ,MidpointRounding . AwayFromZero ) * ( string . IsNullOrEmpty ( row [ "ANV009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANV009" ] ) ) ,1 ,MidpointRounding . AwayFromZero );
                }
            }
            gridControl2 . RefreshDataSource ( );
        }
        void addPerson ( )
        {
            DataRow [ ] rowes = tablePersonInfo . Select ( "EMP005='" + row [ "ANU008" ] + "'" );
            if ( rowes == null )
                return;
            DataRow rows;
            DateTime dtNow = LineProductMesBll . UserInfoMation . sysTime;
            if ( tableViewTwo == null || tableViewTwo . Rows . Count < 1 )
            {
                foreach ( DataRow ro in rowes )
                {
                    rows = tableViewTwo . NewRow ( );
                    rows [ "ANV002" ] = ro [ "EMP001" ] . ToString ( );
                    rows [ "ANV003" ] = ro [ "EMP002" ] . ToString ( );
                    rows [ "ANV004" ] = ro [ "EMP007" ] . ToString ( );
                    rows [ "ANV008" ] = ro [ "DAA002" ] . ToString ( );
                    rows [ "ANV005" ] = dtNow . ToString ( "yyyy-MM-dd 08:00" );
                    rows [ "ANV006" ] = dtNow . ToString ( "yyyy-MM-dd 17:00" );
                    rows [ "ANV007" ] = "在职";
                    tableViewTwo . Rows . Add ( rows );
                }
            }
            else
            {
                foreach ( DataRow ro in rowes )
                {
                    if ( tableViewTwo . Select ( "ANV002='" + ro [ "EMP001" ] + "'" ) . Length < 1 )
                    {
                        rows = tableViewTwo . NewRow ( );
                        rows [ "ANV002" ] = ro [ "EMP001" ] . ToString ( );
                        rows [ "ANV003" ] = ro [ "EMP002" ] . ToString ( );
                        rows [ "ANV004" ] = ro [ "EMP007" ] . ToString ( );
                        rows [ "ANV008" ] = ro [ "DAA002" ] . ToString ( );
                        rows [ "ANV005" ] = dtNow . ToString ( "yyyy-MM-dd 08:00" );
                        rows [ "ANV006" ] = dtNow . ToString ( "yyyy-MM-dd 17:00" );
                        rows [ "ANV007" ] = "在职";
                        tableViewTwo . Rows . Add ( rows );
                    }
                }
            }
            gridControl2 . RefreshDataSource ( );
            calcuSumTime ( );
        }
        void printOrExport ( )
        { 
            tablePrintOne = _bll . getTablePrintOne ( txtANT001 . Text );
            tablePrintOne . TableName = "TableOne";
            tablePrintTwo = _bll . getTablePrintTwo ( txtANT001 . Text );
            tablePrintTwo . TableName = "TableTwo";
        }
        void addRow ( string column ,int selectIdx ,object value )
        {
            if ( selectIdx < tableViewTwo . Rows . Count - 1 )
            {
                DataRow row, ro;
                for ( int i = selectIdx ; i < tableViewTwo . Rows . Count ; i++ )
                {
                    row = tableViewTwo . Rows [ i ];
                    if ( i + 1 == tableViewTwo . Rows . Count )
                        return;
                    if ( column . Equals ( "ANV005" ) )
                    {
                        if ( workShopTime . startTimeFJ ( row ,row [ "ANV005" ] ) )
                        {
                            ro = tableViewTwo . Rows [ i + 1 ];
                            if ( ro [ "ANV005" ] == null || ro [ "ANV005" ] . ToString ( ) == string . Empty )
                            {
                                ro . BeginEdit ( );
                                ro [ "ANV005" ] = /*row [ "ANV005" ];*/value;
                                ro . EndEdit ( );
                            }
                        }
                    }
                    if ( column . Equals ( "ANV006" ) )
                    {
                        if ( workShopTime . endTimeFJ ( row ,row [ "ANV006" ] ) )
                        {
                            ro = tableViewTwo . Rows [ i + 1 ];
                            if ( ro [ "ANV006" ] == null || ro [ "ANV006" ] . ToString ( ) == string . Empty )
                            {
                                ro . BeginEdit ( );
                                ro [ "ANV006" ] =/* row [ "ANV006" ];*/value;
                                ro . EndEdit ( );
                            }
                        }
                    }
                }
            }
            gridControl1 . RefreshDataSource ( );
        }
        void editOtherSur ( string orderNum ,string proNum )
        {
            _bodyOne . ANU001 = txtANT001 . Text;
            _bodyOne . ANU003 = orderNum;
            _bodyOne . ANU004 = proNum;

            if ( _bodyOne . ANU003 == string . Empty || _bodyOne . ANU004 == string . Empty )
            {
                if ( tableViewOne != null && tableViewOne . Rows . Count > 0 )
                {
                    foreach (DataRow row in tableViewOne.Rows )
                    {
                        _bodyOne . ANU003 = row [ "ANU003" ] . ToString ( );
                        _bodyOne . ANU004 = row [ "ANU004" ] . ToString ( );
                        tableOtherSur = _bll . getTableOtherSur ( _bodyOne . ANU003 ,_bodyOne . ANU004 ,_bodyOne . ANU001 );
                        if ( tableOtherSur != null && tableOtherSur . Rows . Count > 0 )
                        {
                            row [ "U4" ] = tableOtherSur . Rows [ 0 ] [ "ANU" ];
                        }
                        else
                        {
                            row [ "U4" ] = row [ "ANU011" ];
                        }
                    }
                }
            }
            else
            {
                tableOtherSur = _bll . getTableOtherSur ( _bodyOne . ANU003 ,_bodyOne . ANU004 ,_bodyOne . ANU001 );
                if ( tableViewOne != null && tableViewOne . Rows . Count > 0 )
                {
                    DataRow [ ] rows = tableViewOne . Select ( "ANU003='" + _bodyOne . ANU003 + "' AND ANU004='" + _bodyOne . ANU004 + "'" );
                    if ( tableOtherSur != null && tableOtherSur . Rows . Count > 0 )
                    {
                        foreach ( DataRow row in rows )
                        {
                            row [ "U4" ] = tableOtherSur . Rows [ 0 ] [ "ANU" ];
                        }
                    }
                    else
                    {
                        foreach ( DataRow row in rows )
                        {
                            row [ "U4" ] = row [ "ANU011" ];
                        }
                    }
                }
            }
        }
        #endregion

    }
}