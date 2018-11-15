﻿using System;
using System . ComponentModel;
using System . Data;
using System . Threading . Tasks;
using DevExpress . XtraEditors;
using LineProductMes . ClassForMain;
using Utility;
using DevExpress . Utils . Paint;
using System . Reflection;
using System . Linq;
using System . Windows . Forms;
using System . Collections . Generic;

namespace LineProductMes
{
    public partial class FormHardWareWork :FormChild
    {
        LineProductMesEntityu.HardWareWorkHeaderEntity _header=null;
        LineProductMesEntityu.HardWareWorkBodyEntity _body=null;
        LineProductMesBll.Bll.HardWareWorkBll _bll=null;
        DataTable tableView,tablePInfo,tableWork,tableUser,tableArt,tablePrintOne,tablePrintTwo,tableOtherSur;
        DataRow row;
        string strWhere="1=1",state=string.Empty,sign=string.Empty,focuseName=string.Empty;
        bool result=false;
        List<string> idxList=new List<string>();

        public FormHardWareWork ( )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . HardWareWorkBll ( );
            _header = new LineProductMesEntityu . HardWareWorkHeaderEntity ( );
            _body = new LineProductMesEntityu . HardWareWorkBodyEntity ( );

            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { bandedGridView1 ,View1 ,View2 ,View3 ,View4 ,View5} );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { bandedGridView1 ,View1 ,View2 ,View3 ,View4,View5 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            dt1 . VistaEditTime = dt2 . VistaEditTime = dt3 . VistaEditTime = dt4 . VistaEditTime = DevExpress . Utils . DefaultBoolean . True;

            wait . Hide ( );
            controlUnEnable ( );
            controlClear ( );

            InitData ( );
            getData ( );

        }

        #region Main
        protected override int Query ( )
        {
            ChildForm . FormHardWareWorkQuery from = new ChildForm . FormHardWareWorkQuery ( );
            from . StartPosition = FormStartPosition . CenterScreen;
            if ( from . ShowDialog ( ) == DialogResult . OK )
            {
                _header . HAW001 = from . getStr;

                sign = "clear";

                strWhere = "1=1";
                strWhere += " AND HAX001='" + _header . HAW001 + "'";

                _header = _bll . getModel ( _header . HAW001 );
                if ( _header == null )
                    return 0;
                setValue ( );

                tableView = _bll . getTableView ( strWhere );
                gridControl1 . DataSource = tableView;

                calcuTsumTime ( );

                tableOtherSur = _bll . getTableOtherSur ( txtHAW002 . Text ,txtHAW003 . Text ,txtHAW001 . Text );
                editOtherSur ( );

                QueryTool ( );
            }

            return base . Query ( );
        }
        protected override int Add ( )
        {
            controlClear ( );
            controlEnable ( );
            txtHAW001 . Text = _bll . getCode ( );
            sign = string . Empty;
            strWhere = "1=2";
            tableView = _bll . getTableView ( strWhere );
            gridControl1 . DataSource = tableView;

            txtHAW011 . Text = "计件";
            txtHAW013 . EditValue = "0501";
            txtHAW010 . Text = LineProductMesBll . UserInfoMation . sysTime . ToString ( "yyyy-MM-dd" );
            state = "add";

            addTool ( );

            return base . Add ( );
        }
        protected override int Edit ( )
        {
            sign = string . Empty;
            controlEnable ( );
            editTool ( );
            state = "edit";
            return base . Edit ( );
        }
        protected override int Delete ( )
        {
            if ( string . IsNullOrEmpty ( txtHAW001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return 0;
            }
            result = _bll . Delete ( txtHAW001 . Text );
            if ( result )
            {
                XtraMessageBox . Show ( "成功删除" );
                controlUnEnable ( );
                controlClear ( );
            }
            else
                XtraMessageBox . Show ( "删除失败" );

            return base . Delete ( );
        }
        protected override int Save ( )
        {
            if ( getValue ( ) == false )
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
            controlUnEnable ( );
            if ( state . Equals ( "add" ) )
                controlClear ( );
            cancelTool ( state );

            return base . Cancel ( );
        }
        protected override int Examine ( )
        {
            if ( string . IsNullOrEmpty ( txtHAW001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return 0;
            }
            state = toolExamin . Caption;
            if ( state . Equals ( "审核" ) )
                _header . HAW018 = true;
            else
                _header . HAW018 = false;
            result = _bll . Exanmie ( txtHAW001 . Text ,_header . HAW018 );
            if ( result )
            {
                XtraMessageBox . Show ( state + "成功" );
                examineTool ( state );
                if ( state . Equals ( "审核" ) )
                {
                    layoutControlItem19 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                    Graph . grPicZ ( pictureEdit1 ,"审 核" );
                }
                else
                {
                    layoutControlItem19 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
                    Graph . grPicZ ( pictureEdit1 ,"反" );
                }
            }
            else
                XtraMessageBox . Show ( state + "失败" );

            return base . Examine ( );
        }
        protected override int Cancellation ( )
        {
            if ( string . IsNullOrEmpty ( txtHAW001 . Text ) )
            {
                XtraMessageBox . Show ( "单号不可为空" );
                return 0;
            }
            state = toolCancellation . Caption;
            if ( state . Equals ( "注销" ) )
                _header . HAW019 = true;
            else
                _header . HAW019 = false;
            result = _bll . CancelTion ( txtHAW001 . Text ,_header . HAW019 );
            if ( result )
            {
                XtraMessageBox . Show ( state + "成功" );
                cancelltionTool ( state );
                if ( state . Equals ( "注销" ) )
                {
                    layoutControlItem19 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                    Graph . grPicZ ( pictureEdit1 ,"注 销" );
                }
                else
                {
                    layoutControlItem19 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
                    Graph . grPicZ ( pictureEdit1 ,"反" );
                }
            }
            else
                XtraMessageBox . Show ( state + "失败" );

            return base . Cancellation ( );
        }
        protected override int Print ( )
        {
            if ( string . IsNullOrEmpty ( txtHAW009 . Text ) || Convert . ToInt32 ( txtHAW009 . Text ) <= 0 )
                return 0;

            if ( printOrExport ( ) == false )
                return 0;
            Print ( new DataTable [ ] { tablePrintOne ,tablePrintTwo } ,"入库单.frx" );

            return base . Print ( );
        }
        protected override int Export ( )
        {
            if ( string . IsNullOrEmpty ( txtHAW009 . Text ) || Convert . ToInt32 ( txtHAW009 . Text ) <= 0 )
                return 0;

            if ( printOrExport ( ) == false )
                return 0;
            Export ( new DataTable [ ] { tablePrintOne ,tablePrintTwo } ,"入库单.frx" );

            return base . Export ( );
        }
        #endregion

        #region Evnet
        private void bandedGridView1_CustomDrawRowIndicator ( object sender ,DevExpress . XtraGrid . Views . Grid . RowIndicatorCustomDrawEventArgs e )
        {
            if ( e . Info . IsRowIndicator && e . RowHandle >= 0 )
            {
                e . Info . DisplayText = ( e . RowHandle + 1 ) . ToString ( );
            }
        }
        private void txtHAW013_EditValueChanged ( object sender ,EventArgs e )
        {
            if ( txtHAW013 . EditValue == null || txtHAW013 . EditValue . ToString ( ) == string . Empty )
                return;
            txtHAW015 . Properties . DataSource = _bll . getDepart ( txtHAW013 . EditValue . ToString ( ) );
            txtHAW015 . Properties . DisplayMember = "DAA002";
            txtHAW015 . Properties . ValueMember = "DAA001";

            txtHAW015 . EditValue = "0501";
        }
        private void txtHAW003_TextChanged ( object sender ,EventArgs e )
        {
            if ( string . IsNullOrEmpty ( txtHAW003 . Text ) )
                return;
            if ( txtHAW003 . Text != string . Empty )
            {
                tableArt = _bll . getArtInfo ( txtHAW003 . Text );
                Edit2 . DataSource = tableArt;
                Edit2 . DisplayMember = "ART011";
                Edit2 . ValueMember = "ART011";
            }
            if ( tableView == null )
                return;
            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );
            if ( tableArt == null || tableArt . Rows . Count < 1 )
                return;
            if ( tableArt . Select ( "ART001='" + txtHAW003 . Text + "'" ) . Length < 1 )
                return;
            DataRow [ ] rows = tableArt . Select ( "ART001='" + txtHAW003 . Text + "'" );
            if ( rows == null )
                return;
            DateTime dt = LineProductMesBll . UserInfoMation . sysTime;
            if ( tableView == null || tableView . Rows . Count < 1 )
            {
                foreach ( DataRow r in rows )
                {
                    row = tableView . NewRow ( );
                    row [ "HAX016" ] = r [ "ART011" ];
                    row [ "HAX005" ] = r [ "ART002" ];
                    row [ "HAX006" ] = r [ "ART003" ];
                    row [ "HAX007" ] = r [ "ART004" ];
                    row [ "HAX015" ] = "在职";
                    tableView . Rows . Add ( row );
                }
            }
            else
            {
                foreach ( DataRow r in rows )
                {
                    if ( tableView . Select ( "HAX016='" + r [ "ART011" ] . ToString ( ) + "'" ) . Length < 1 )
                    {
                        row = null;
                        row = tableView . NewRow ( );
                        row [ "HAX016" ] = r [ "ART011" ];
                        row [ "HAX005" ] = r [ "ART002" ];
                        row [ "HAX006" ] = r [ "ART003" ];
                        row [ "HAX007" ] = r [ "ART004" ];
                        row [ "HAX015" ] = "在职";
                        tableView . Rows . Add ( row );
                    }
                }
            }
            tableOtherSur = _bll . getTableOtherSur ( txtHAW002 . Text ,txtHAW003 . Text ,txtHAW001 . Text );
            editOtherSur ( );
            gridControl1 . RefreshDataSource ( );
        }
        private void txtHAW011_SelectedValueChanged ( object sender ,EventArgs e )
        {
            if ( txtHAW011 . Text == string . Empty )
                return;
            //startT ( );
            //endT ( );
        }
        private void txtHAW002_EditValueChanged ( object sender ,EventArgs e )
        {
            row = View1 . GetFocusedDataRow ( );
            if ( sign . Equals ( "clear" ) )
                row = null;
            if ( row == null )
                return;
            if ( row [ "RAA008" ] . ToString ( ) == string . Empty )
            {
               txtHAW002.Text= txtHAW003 . Text = txtHAW004 . Text = txtHAW005 . Text = txtHAW006 . Text = txtHAW007 . Text = txtHAW008 . Text = txtu0 . Text = string . Empty;
                return;
            }
            _header . HAW003 = row [ "RAA015" ] . ToString ( );
            _header . HAW004 = row [ "DEA002" ] . ToString ( );
            _header . HAW005 = row [ "DEA057" ] . ToString ( );
            _header . HAW006 = row [ "DEA003" ] . ToString ( );
            _header . HAW007 = string . IsNullOrEmpty ( row [ "RAA018" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "RAA018" ] . ToString ( ) );
            _header . HAW008 = string . IsNullOrEmpty ( row [ "DEA050" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "DEA050" ] . ToString ( ) );

            txtHAW003 . Text = _header . HAW003;
            txtHAW004 . Text = _header . HAW004;
            txtHAW005 . Text = _header . HAW005;
            txtHAW006 . Text = _header . HAW006;
            txtHAW007 . Text = _header . HAW007 . ToString ( );
            txtHAW008 . Text = _header . HAW008 . ToString ( );

            if ( txtHAW003 . Text != string . Empty )
            {
                tableArt = _bll . getArtInfo ( txtHAW003 . Text );
                Edit2 . DataSource = tableArt;
                Edit2 . DisplayMember = "ART011";
                Edit2 . ValueMember = "ART011";
            }
            txtHAW003 . Text = string . Empty;
            txtHAW003 . Text = _header . HAW003;
        }
        private void txtHAW015_EditValueChanged ( object sender ,EventArgs e )
        {
            return;
            if ( txtHAW013 . EditValue == null || txtHAW013 . EditValue . ToString ( ) == string . Empty )
                return;
            if ( txtHAW015 . EditValue == null || txtHAW015 . EditValue . ToString ( ) == string . Empty )
                return;
            if ( tableView == null )
                return;
            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );
            DataRow [ ] rowes = tableUser . Select ( "EMP005='" + txtHAW015 . EditValue . ToString ( ) + "'" );
            DataRow rows;
            DateTime dt = LineProductMesBll . UserInfoMation . sysTime;
            if ( tableView == null || tableView . Rows . Count < 1 )
            {
                foreach ( DataRow row in rowes )
                {
                    if ( row != null )
                    {
                        rows = tableView . NewRow ( );
                        rows [ "HAX002" ] = row [ "EMP001" ] . ToString ( );
                        rows [ "HAX003" ] = row [ "EMP002" ] . ToString ( );
                        rows [ "HAX004" ] = row [ "EMP007" ] . ToString ( );
                        rows [ "HAX015" ] = "在职";
                        //rows [ "HAX009" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                        //rows [ "HAX010" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
                        tableView . Rows . Add ( rows );
                        gridControl1 . Refresh ( );
                    }
                }
            }
            else
            {
                foreach ( DataRow row in rowes )
                {
                    if ( tableView . Select ( "HAX002='" + row [ "EMP001" ] . ToString ( ) + "'" ) . Length < 1 )
                    {
                        rows = tableView . NewRow ( );
                        rows [ "HAX002" ] = row [ "EMP001" ] . ToString ( );
                        rows [ "HAX003" ] = row [ "EMP002" ] . ToString ( );
                        rows [ "HAX004" ] = row [ "EMP007" ] . ToString ( );
                        rows [ "HAX015" ] = "在职";
                        rows [ "HAX009" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                        rows [ "HAX010" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
                        tableView . Rows . Add ( rows );
                        gridControl1 . Refresh ( );
                    }
                }
            }
            tableOtherSur = _bll . getTableOtherSur ( txtHAW002 . Text ,txtHAW003 . Text ,txtHAW001 . Text );
            editOtherSur ( );
        }
        private void txtHAW007_TextChanged ( object sender ,EventArgs e )
        {
            calcuPrice ( );
        }
        private void txtHAW008_TextChanged ( object sender ,EventArgs e )
        {
            calcuPrice ( );
        }
        private void bandedGridView1_CellValueChanged ( object sender ,DevExpress . XtraGrid . Views . Base . CellValueChangedEventArgs e )
        {
            row = bandedGridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            if ( e . Column . FieldName == "HAX015" )
            {
                _body . HAX015 = row [ "HAX015" ] . ToString ( );
                if ( _body . HAX015 == string . Empty || _body . HAX015 . Equals ( "离职" ) || _body . HAX015 . Equals ( "未上班" ) )
                {
                    row [ "HAX008" ] = DBNull . Value;
                    row [ "HAX009" ] = DBNull . Value;
                    row [ "HAX010" ] = DBNull . Value;
                    row [ "HAX011" ] = DBNull . Value;
                    row [ "HAX012" ] = DBNull . Value;
                    row [ "HAX013" ] = DBNull . Value;
                    row [ "HAX014" ] = DBNull . Value;
                }
            }
            else if ( e . Column . FieldName == "HAX002" )
            {
                if ( row [ "HAX015" ] == null || row [ "HAX015" ] . ToString ( ) == string . Empty )
                {
                    row [ "HAX015" ] = "在职";
                }
            }
            else if ( e . Column . FieldName == "HAX016" )
            {
                if ( row [ "HAX015" ] == null || row [ "HAX015" ] . ToString ( ) == string . Empty )
                {
                    row [ "HAX015" ] = "在职";
                }
            }
            else if ( e . Column . FieldName == "HAX008" )
            {
                DateTime dt = LineProductMesBll . UserInfoMation . sysTime;
                if ( txtHAW011 . Text . Equals ( "计件" ) )
                {
                    if ( row [ "HAX009" ] == null || row [ "HAX009" ] . ToString ( ) == string . Empty )
                    {
                        row [ "HAX009" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                    }
                    if ( row [ "HAX010" ] == null || row [ "HAX010" ] . ToString ( ) == string . Empty )
                    {
                        row [ "HAX010" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
                    }
                }
                else if ( txtHAW011 . Text . Equals ( "计时" ) )
                {
                    if ( row [ "HAX011" ] == null || row [ "HAX011" ] . ToString ( ) == string . Empty )
                    {
                        row [ "HAX011" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                    }
                    if ( row [ "HAX012" ] == null || row [ "HAX012" ] . ToString ( ) == string . Empty )
                    {
                        row [ "HAX012" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
                    }
                }
                if ( row [ "HAX015" ] == null || row [ "HAX015" ] . ToString ( ) == string . Empty )
                {
                    row [ "HAX015" ] = "在职";
                }
                if ( tableArt != null && tableArt . Rows . Count > 0 )
                {
                    DataRow [ ] rs = tableArt . Select ( "ART011='" + row [ "HAX016" ] + "'" );
                    if ( rs . Length > 0 && "是".Equals(rs [0][ "ART010" ].ToString()) )
                    {
                        bandedGridView1 . CloseEditor ( );
                        bandedGridView1 . UpdateCurrentRow ( );
                        txtHAW009 . Text = tableView . Compute ( "SUM(HAX008)" ,"HAX016='" + row [ "HAX016" ] + "'" ) . ToString ( );
                    }
                }
                calcuTsumTime ( );
            }
            else if ( e . Column . FieldName == "HAX009" )
            {
                if ( workShopTime . startTime ( row ,e . Value ,"HAX010" ,"HAX011" ,"HAX012" ) == false )
                {
                    row [ "HAX009" ] = DBNull . Value;
                }
                addRows ( "HAX009" ,e . RowHandle ,e . Value );
            }
            else if ( e . Column . FieldName == "HAX010" )
            {
                if ( workShopTime . endTime ( row ,e . Value ,"HAX009" ,"HAX011" ,"HAX012" ) == false )
                {
                    row [ "HAX010" ] = DBNull . Value;
                }
                addRows ( "HAX010" ,e . RowHandle ,e . Value );
            }
            else if ( e . Column . FieldName == "HAX011" )
            {
                if ( workShopTime . startCenTime ( row ,e . Value ,"HAX010" ,"HAX012" ,"HAX009" ) == false )
                {
                    row [ "HAX011" ] = DBNull . Value;
                }
                addRows ( "HAX011" ,e . RowHandle ,e . Value );
            }
            else if ( e . Column . FieldName == "HAX012" )
            {
                if ( workShopTime . endCenTime ( row ,e . Value ,"HAX011" ,"HAX009" ,"HAX010" ) == false )
                {
                    row [ "HAX012" ] = DBNull . Value;
                }
                addRows ( "HAX012" ,e . RowHandle ,e . Value );
            }
            else if ( e . Column . FieldName == "HAX013" )
            {
                DateTime dt = LineProductMesBll . UserInfoMation . sysTime;
                if ( txtHAW011 . Text . Equals ( "计件" ) )
                {
                    if ( row [ "HAX009" ] == null || row [ "HAX009" ] . ToString ( ) == string . Empty )
                    {
                        row [ "HAX009" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                    }
                    if ( row [ "HAX010" ] == null || row [ "HAX010" ] . ToString ( ) == string . Empty )
                    {
                        row [ "HAX010" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
                    }
                }
                else if ( txtHAW011 . Text . Equals ( "计时" ) )
                {
                    if ( row [ "HAX011" ] == null || row [ "HAX011" ] . ToString ( ) == string . Empty )
                    {
                        row [ "HAX011" ] = dt . ToString ( "yyyy-MM-dd 08:00" );
                    }
                    if ( row [ "HAX012" ] == null || row [ "HAX012" ] . ToString ( ) == string . Empty )
                    {
                        row [ "HAX012" ] = dt . ToString ( "yyyy-MM-dd 17:00" );
                    }
                }
                if ( row [ "HAX015" ] == null || row [ "HAX015" ] . ToString ( ) == string . Empty )
                {
                    row [ "HAX015" ] = "在职";
                }
                calcuTsumTime ( );
            }
            else if ( e . Column . FieldName == "HAX005" || e . Column . FieldName == "HAX006" || e . Column . FieldName == "HAX007" )
            {
                editOtherSur ( );
            }
        }
        private void Edit1_EditValueChanged ( object sender ,EventArgs e )
        {
            BaseEdit edit = bandedGridView1 . ActiveEditor;
            switch ( bandedGridView1 . FocusedColumn . FieldName )
            {
                case "HAX002":
                if ( edit . EditValue == null )
                    return;
                row = tableUser . Select ( "EMP001='" + edit . EditValue + "'" ) [ 0 ];
                if ( row == null )
                    return;
                _body . HAX003 = row [ "EMP002" ] . ToString ( );
                _body . HAX004 = row [ "EMP007" ] . ToString ( );
                _body . HAX017 = row [ "DAA002" ] . ToString ( );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "HAX003" ] ,_body . HAX003 );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "HAX004" ] ,_body . HAX004 );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "HAX017" ] ,_body . HAX017 );
                break;
            }
        }
        private void Edit2_EditValueChanged ( object sender ,EventArgs e )
        {
            BaseEdit edit = bandedGridView1 . ActiveEditor;
            switch ( bandedGridView1 . FocusedColumn . FieldName )
            {
                case "HAX016":
                if ( edit . EditValue == null || edit . EditValue . ToString ( ) == string . Empty )
                    return;
                row = tableArt . Select ( "ART011='" + edit . EditValue + "'" ) [ 0 ];
                //if ( tableView != null && tableView . Rows . Count > 0 )
                //{
                //    if ( tableView . Select ( "HAX016='" + edit . EditValue + "'" ) . Length > 0 )
                //    {
                //        edit . EditValue = null;
                //        return;
                //    }
                //}
                if ( row == null )
                    return;
                _body . HAX005 = row [ "ART002" ] . ToString ( );
                _body . HAX006 = row [ "ART003" ] . ToString ( );
                _body . HAX007 = string . IsNullOrEmpty ( row [ "ART004" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ART004" ] . ToString ( ) );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "HAX005" ] ,_body . HAX005 );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "HAX006" ] ,_body . HAX006 );
                bandedGridView1 . SetFocusedRowCellValue ( bandedGridView1 . Columns [ "HAX007" ] ,_body . HAX007 );
                break;
            }
        }
        private void gridControl1_KeyPress ( object sender ,System . Windows . Forms . KeyPressEventArgs e )
        {
            row = bandedGridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            if ( e . KeyChar == ( char ) Keys . Enter && toolSave.Visibility == DevExpress.XtraBars.BarItemVisibility.Always)
            {
                if ( XtraMessageBox . Show ( "确认删除?" ,"删除" ,MessageBoxButtons . OKCancel ) != DialogResult . OK )
                    return;
                _body . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                if ( !idxList . Contains ( _body . idx . ToString ( ) ) )
                    idxList . Add ( _body . idx . ToString ( ) );
                tableView . Rows . Remove ( row );
                gridControl1 . RefreshDataSource ( );
            }
        }
        private void backgroundWorker1_DoWork ( object sender ,DoWorkEventArgs e )
        {
            if ( state . Equals ( "add" ) )
                result = _bll . Save ( _header ,tableView );
            else if ( state . Equals ( "edit" ) )
                result = _bll . Edit ( _header ,tableView ,idxList );
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
                        _header . HAW001 = txtHAW001 . Text = LineProductMesBll . UserInfoMation . oddNum;

                    strWhere = "1=1";
                    strWhere += " AND HAX001='" + _header . HAW001 + "'";
                    tableView = _bll . getTableView ( strWhere );
                    gridControl1 . DataSource = tableView;
                    setValue ( );
                    saveTool ( );

                    tableOtherSur = _bll . getTableOtherSur ( txtHAW002 . Text ,txtHAW003 . Text ,txtHAW001 . Text );
                    editOtherSur ( );
                }
                else
                {
                     ClassForMain.FormClosingState.formClost = false;
                    XtraMessageBox . Show ( "保存失败" );
                }
            }
        }
        protected override void OnClosing ( CancelEventArgs e )
        {
            if ( toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always )
            {
                if ( txtHAW002 . Text == string . Empty || tableView == null || tableView . Rows . Count < 1 )
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
        private void txtHAW020_TextChanged ( object sender ,EventArgs e )
        {
            calcuTsumTime ( );
        }
        private void txtHAW021_TextChanged ( object sender ,EventArgs e )
        {
            calcuTsumTime ( );
        }
        private void txtHAW009_TextChanged ( object sender ,EventArgs e )
        {
            calcuPrice ( );
        }
        private void bandedGridView1_RowCellStyle ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellStyleEventArgs e )
        {
            if ( e . Column . FieldName == "U4" )
            {
                e . Appearance . BackColor = System . Drawing . Color . LightSteelBlue;
            }
        }
        private void txtHAW001_TextChanged ( object sender ,EventArgs e )
        {
            //tableOtherSur = _bll . getTableOtherSur ( txtHAW002 . Text ,txtHAW003 . Text ,txtHAW001 . Text );
            //editOtherSur ( );
        }
        private void bandedGridView1_CustomColumnSort ( object sender ,DevExpress . XtraGrid . Views . Base . CustomColumnSortEventArgs e )
        {
        }
        private void contextMenuStrip1_ItemClicked ( object sender ,ToolStripItemClickedEventArgs e )
        {
            CopyUtils . copyResult ( bandedGridView1 ,focuseName );
        }
        private void bandedGridView1_RowCellClick ( object sender ,DevExpress . XtraGrid . Views . Grid . RowCellClickEventArgs e )
        {
            focuseName = e . Column . FieldName; 
        }
        #endregion

        #region Method
        void controlUnEnable ( )
        {
            txtHAW001 . ReadOnly = txtHAW002 . ReadOnly = txtHAW003 . ReadOnly = txtHAW004 . ReadOnly = txtHAW005 . ReadOnly = txtHAW006 . ReadOnly = txtHAW007 . ReadOnly = txtHAW008 . ReadOnly = txtHAW010 . ReadOnly = txtHAW011 . ReadOnly = txtHAW013 . ReadOnly = txtHAW015 . ReadOnly = txtHAW016 . ReadOnly = /*txtHAW017 . Enabled =*/ txtu0 . ReadOnly = txtHAW020.ReadOnly=txtHAW021.ReadOnly= true;
            bandedGridView1 . OptionsBehavior . Editable = false;
        }
        void controlEnable ( )
        {
            txtHAW002 . ReadOnly =  txtHAW011 . ReadOnly = txtHAW013 . ReadOnly = txtHAW015 . ReadOnly = txtHAW016 . ReadOnly /*= txtHAW017 . ReadOnly*/ =  txtHAW020 . ReadOnly = txtHAW021 . ReadOnly = false;
            bandedGridView1 . OptionsBehavior . Editable = true;
        }
        void controlClear ( )
        {
            sign = "clear";
            txtHAW001 . Text = txtHAW002 . Text = txtHAW003 . Text = txtHAW004 . Text = txtHAW005 . Text = txtHAW006 . Text = txtHAW007 . Text = txtHAW008 . Text = txtHAW009 . Text = txtHAW010 . Text = txtHAW011 . Text = txtHAW013 . Text = txtHAW015 . Text = txtHAW016 . Text = /*txtHAW017 . Text =*/ txtu0 . Text = txtHAW020 . Text = txtHAW021 . Text = string . Empty;
            gridControl1 . DataSource = null;
            layoutControlItem19 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
        }
        void InitData ( )
        {
            tableWork = _bll . getDepart ( );

            txtHAW013 . Properties . DataSource = tableWork;
            txtHAW013 . Properties . DisplayMember = "DAA002";
            txtHAW013 . Properties . ValueMember = "DAA001";
        }
        void getData ( )
        {
            tablePInfo = _bll . getTablePInfo ( );
            txtHAW002 . Properties . DataSource = tablePInfo;
            txtHAW002 . Properties . DisplayMember = "RAA001";
            txtHAW002 . Properties . ValueMember = "RAA001";

            tableUser = _bll . getUserInfo ( );
            Edit1 . DataSource = tableUser;
            Edit1 . DisplayMember = "EMP001";
            Edit1 . ValueMember = "EMP001";
        }
        void calcuPrice ( )
        {
            txtu0 . Text = ( string . IsNullOrEmpty ( txtHAW009 . Text ) == true ? 0 : Convert . ToInt32 ( txtHAW009 . Text ) * ( string . IsNullOrEmpty ( txtHAW008 . Text ) == true ? 0 : Convert . ToDecimal ( txtHAW008 . Text ) ) ) . ToString ( "0.######" );
        }
        bool getValue ( )
        {
            result = true;

            if ( string . IsNullOrEmpty ( txtHAW002 . Text ) )
            {
                XtraMessageBox . Show ( "请选择来源单号" );
                return false;
            }
            _header . HAW002 = txtHAW002 . Text;
            _header . HAW003 = txtHAW003 . Text;
            _header . HAW004 = txtHAW004 . Text;
            _header . HAW005 = txtHAW005 . Text;
            _header . HAW006 = txtHAW006 . Text;
            _header . HAW007 = string . IsNullOrEmpty ( txtHAW007 . Text ) == true ? 0 : Convert . ToInt32 ( txtHAW007 . Text );
            _header . HAW008 = string . IsNullOrEmpty ( txtHAW008 . Text ) == true ? 0 : Convert . ToDecimal ( txtHAW008 . Text );
            //if ( string . IsNullOrEmpty ( txtHAW009 . Text ) )
            //{
            //    XtraMessageBox . Show ( "请填写完工数量" );
            //    return false;
            //}
            int outResult = 0;
            if (!string . IsNullOrEmpty ( txtHAW009 . Text ) && int . TryParse ( txtHAW009 . Text ,out outResult ) == false )
            {
                XtraMessageBox . Show ( "完工数量请填写数字" );
                return false;
            }
            _header . HAW009 = outResult;
            _header . HAW010 = Convert . ToDateTime ( txtHAW010 . Text );
            if ( string . IsNullOrEmpty ( txtHAW011 . Text ) )
            {
                XtraMessageBox . Show ( "请选择工资类型" );
                return false;
            }
            _header . HAW011 = txtHAW011 . Text;
            if ( string . IsNullOrEmpty ( txtHAW013 . Text ) )
            {
                XtraMessageBox . Show ( "请选择生产车间" );
                return false;
            }
            _header . HAW012 = txtHAW013 . EditValue . ToString ( );
            _header . HAW013 = txtHAW013 . Text;
            if ( string . IsNullOrEmpty ( txtHAW015 . Text ) )
            {
                XtraMessageBox . Show ( "请选择班组" );
                return false;
            }
            _header . HAW014 = txtHAW015 . EditValue . ToString ( );
            _header . HAW015 = txtHAW015 . Text;
            _header . HAW016 = txtHAW016 . Text;
            _header . HAW017 = /*txtHAW017 . Text;*/string . Empty;
            _header . HAW018 = false;
            _header . HAW019 = false;
            _header . HAW020 = string . IsNullOrEmpty ( txtHAW020 . Text ) == true ? 0 : Convert . ToDecimal ( txtHAW020 . Text );
            _header . HAW021 = string . IsNullOrEmpty ( txtHAW021 . Text ) == true ? 0 : Convert . ToDecimal ( txtHAW021 . Text );


            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );

            if ( tableView == null || tableView . Rows . Count < 1 )
                return false;


            foreach ( DataRow r in tableView.Rows )
            {
                if ( r [ "HAX015" ] == null || r [ "HAX015" ] . ToString ( ) == string . Empty )
                {
                    XtraMessageBox . Show ( "工作状态不可为空" );
                    result = false;
                    break;
                }
                if ( r [ "HAX005" ] == null || r [ "HAX005" ] . ToString ( ) == string . Empty )
                {
                    XtraMessageBox . Show ( "工序编号不可为空" );
                    result = false;
                    break;
                }
                if ( r [ "HAX002" ] == null || r [ "HAX002" ] . ToString ( ) == string . Empty )
                {
                    XtraMessageBox . Show ( "工号不可为空" );
                    result = false;
                    break;
                }
            }

            if ( result == false )
                return false;

            var query = from p in tableView . AsEnumerable ( )
                        group p by new
                        {
                            p1 = p . Field<string> ( "HAX002" ),
                            p2 = p . Field<string> ( "HAX016" )
                        } into m
                        select new
                        {
                            hax002 = m . Key . p1 ,
                            hax016 = m . Key . p1 ,
                            count = m . Count ( )
                        };
            if ( query != null )
            {
                foreach ( var x in query )
                {
                    if ( x . count > 1 )
                    {
                        XtraMessageBox . Show ( "工号:" + x . hax002 + "\n\r序号:" + x . hax016 + "\n\r重复,请核实" );
                        result = false;
                        break;
                    }
                }
            }

            if ( result == false )
                return false;

            //var quer = from k in tableView . AsEnumerable ( )
            //           group k by new
            //           {
            //               p1 = k . Field<string> ( "HAX002" ) ,
            //               p2 = k . Field<string> ( "HAX016" )
            //           } into m
            //           let sum = m . Sum ( t => t . Field<int?> ( "HAX008" ) == null ? 0 : Convert . ToInt32 ( t . Field<int?> ( "HAX008" ) ) )
            //           select new
            //           {
            //               hax002 = m . Key . p1 ,
            //               hax016 = m . Key . p2 ,
            //               sum = sum
            //           };
            //if ( quer != null )
            //{
            //    foreach ( var x in quer )
            //    {
            //        if ( x . sum > _header . HAW007 )
            //        {
            //            XtraMessageBox . Show ( "工号:" + x . hax002 + "\n\r序号:" + x . hax016 + "\n\r的生产数量多于工单数量" ,"提示" );
            //            result = false;
            //            break;
            //        }
            //    }
            //}

            //if ( result == false )
            //    return false;

            //if ( _bll . ExistsNum ( _header ) == false )
            //{
            //    XtraMessageBox . Show ( "完工数量之和多余工单数量,请核实" );
            //    return false;
            //}

            var que = from k in tableView . AsEnumerable ( )
                      group k by new
                      {
                          p2 = k . Field<string> ( "HAX016" ) ,
                          p3 = k . Field<int> ( "U4" )
                      } into m
                      let sum = m . Sum ( t => t . Field<int?> ( "HAX008" ) == null ? 0 : Convert . ToInt32 ( t . Field<int?> ( "HAX008" ) ) )
                      select new
                      {
                          hax016 = m . Key . p2 ,
                          u4 = m . Key . p3 ,
                          sum = sum
                      };
            if ( que != null )
            {
                foreach ( var x in que )
                {
                    if ( x . sum > x . u4 )
                    {
                        XtraMessageBox . Show ( "序号:" + x . hax016 + "\n\r的生产数量多于工单数量" ,"提示" );
                        result = false;
                        break;
                    }
                    //else
                    //{
                    //    if ( _bll . ExistsNums ( _header ,x . hax016 ,x . sum ) == false )
                    //    {
                    //        XtraMessageBox . Show ( "序号:" + x . hax016 + "完工数量之和多余工单数量,请核实" );
                    //        result = false;
                    //        break;
                    //    }
                    //}
                }
            }

            if ( result == false )
                return false;

            var qu = from k in tableView . AsEnumerable ( )
                     group k by new
                     {
                         k1 = k . Field<string> ( "HAX016" )
                     } into m
                     let sum = m . Sum ( t => t . Field<int?> ( "HAX008" ) == null ? 0 : t . Field<int> ( "HAX008" ) )
                     select new
                     {
                         hax016 = m . Key . k1 ,
                         sum = sum
                     };

            var quSur = from k in tableView . AsEnumerable ( )
                        group k by new
                        {
                            k1 = k . Field<string> ( "HAX016" ) ,
                            k2 = k . Field<int?> ( "U4" ) == null ? 0 : k . Field<int> ( "U4" )
                        } into m
                        select new
                        {
                            hax016 = m . Key . k1 ,
                            u4 = m . Key . k2
                        };

            if ( qu != null )
            {
                int res = 0, total = string . IsNullOrEmpty ( txtHAW007 . Text ) == true ? 0 : Convert . ToInt32 ( txtHAW007 . Text );
                for ( int i = 0 ; i < qu . ToArray() . Length ; i++ )
                {
                    _body . HAX005 = qu . ToArray ( ) [ i ] . hax016;
                    _body . idx = qu . ToArray ( ) [ i ] . sum;
                    if ( i >= 1 && i < qu . ToArray ( ) . Length - 1 )
                    {
                        _body . HAX006 = qu . ToArray ( ) [ i + 1 ] . hax016;
                        _body . HAX008 = qu . ToArray ( ) [ i + 1 ] . sum;
                        if ( !_body . HAX005 . Equals ( _body . HAX006 ) )
                        {
                            if ( quSur != null )
                            {
                                res = quSur . ToList ( ) . Find ( ( t ) =>
                                {
                                    return t . hax016 . Equals ( _body . HAX005 );
                                } ) . u4;
                                _body . idx = _body . idx + total - res;
                                res = quSur . ToList ( ) . Find ( ( t ) =>
                                {
                                    return t . hax016 . Equals ( _body . HAX006 );
                                } ) . u4;
                                _body . HAX008 = _body . HAX008 + total - res;
                                if ( _body . idx < _body . HAX008 )
                                {
                                    MessageBox . Show ( "工序序号:" + _body . HAX006 + "的完工数量多于" + _body . HAX005 + "的完工数量,请核实" ,"提示" );
                                    result = false;
                                    break;
                                }
                            }
                            else
                            {
                                if ( _body . idx < _body . HAX008 )
                                {
                                    MessageBox . Show ( "工序序号:" + _body . HAX006 + "的完工数量多于" + _body . HAX005 + "的完工数量,请核实" ,"提示" );
                                    result = false;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }
        void setValue ( )
        {
            txtHAW001 . Text = _header . HAW001;
            txtHAW002 . Text = _header . HAW002;
            txtHAW003 . Text = _header . HAW003;
            txtHAW004 . Text = _header . HAW004;
            txtHAW005 . Text = _header . HAW005;
            txtHAW006 . Text = _header . HAW006;
            txtHAW007 . Text = _header . HAW007 . ToString ( );
            txtHAW008 . Text = _header . HAW008 . ToString ( );
            txtHAW009 . Text = _header . HAW009 . ToString ( );
            txtHAW010 . Text = Convert . ToDateTime ( _header . HAW010 ) . ToString ( "yyyy-MM-dd" );
            txtHAW011 . Text = _header . HAW011;
            txtHAW013 . EditValue = _header . HAW012;
            txtHAW015 . EditValue = _header . HAW014;
            txtHAW016 . Text = _header . HAW016;
            //txtHAW017 . Text = _header . HAW017;
            txtHAW020 . Text = Convert . ToDecimal ( _header . HAW020 ) . ToString ( "0.#" );
            txtHAW021 . Text = Convert . ToDecimal ( _header . HAW021 ) . ToString ( "0.#" );
            layoutControlItem19 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Never;
            Graph . grPic ( pictureEdit1 ,"反" );
            if ( _header . HAW018 )
            {
                layoutControlItem19 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                Graph . grPicZ ( pictureEdit1 ,"审 核" );
                examineTool ( "审核" );
            }
            else
            {
                examineTool ( "反审核" );
            }
            if ( _header . HAW019 )
            {
                layoutControlItem19 . Visibility = DevExpress . XtraLayout . Utils . LayoutVisibility . Always;
                Graph . grPicZ ( pictureEdit1 ,"注 销" );
                cancelltionTool ( "注销" );
            }
            else
            {
                cancelltionTool ( "反注销" );
            }
        }
        bool printOrExport ( )
        {
            tablePrintOne = _bll . getTablePrintOne ( txtHAW001 . Text );
            tablePrintOne . TableName = "TableOne";
            tablePrintTwo = _bll . getTablePrintTwo ( txtHAW001 . Text );
            tablePrintTwo . TableName = "TableTwo";

            if ( tablePrintTwo == null || tablePrintTwo . Rows . Count < 1 )
                return false;

            return true;
        }
        void addRows ( string column,int selectIdx ,object value )
        {
            if ( selectIdx < tableView . Rows . Count - 1 )
            {
                DataRow row, ro;
                for ( int i = selectIdx ; i < tableView . Rows . Count ; i++ )
                {
                    row = tableView . Rows [ i ];
                    if ( i + 1 != tableView . Rows . Count )
                    {
                        if ( txtHAW011 . Text . Equals ( "计件" ) )
                        {
                            if ( column . Equals ( "HAX009" ) )
                            {
                                if ( workShopTime . startTime ( row ,row [ "HAX009" ] ,"HAX010" ,"HAX011" ,"HAX012" ) )
                                {
                                    ro = tableView . Rows [ i + 1 ];
                                    if ( ro [ "HAX009" ] == null || ro [ "HAX009" ] . ToString ( ) == string . Empty )
                                    {
                                        ro . BeginEdit ( );
                                        ro [ "HAX009" ] = /*row [ "HAX009" ];*/value;
                                        ro . EndEdit ( );
                                    }
                                }
                            }
                            else if ( column . Equals ( "HAX010" ) )
                            {
                                if ( workShopTime . endTime ( row ,row [ "HAX010" ] ,"HAX009" ,"HAX011" ,"HAX012" ) )
                                {
                                    ro = tableView . Rows [ i + 1 ];
                                    if ( ro [ "HAX010" ] == null || ro [ "HAX010" ] . ToString ( ) == string . Empty )
                                    {
                                        ro . BeginEdit ( );
                                        ro [ "HAX010" ] = /*row [ "HAX010" ];*/value;
                                        ro . EndEdit ( );
                                    }
                                }
                            }
                        }
                        else
                        {
                            if ( column . Equals ( "HAX011" ) )
                            {
                                if ( workShopTime . startCenTime ( row ,row [ "HAX011" ] ,"HAX010" ,"HAX012" ,"HAX009" ) )
                                {
                                    ro = tableView . Rows [ i + 1 ];
                                    if ( ro [ "HAX011" ] == null || ro [ "HAX011" ] . ToString ( ) == string . Empty )
                                    {
                                        ro . BeginEdit ( );
                                        ro [ "HAX011" ] = /*row [ "HAX011" ];*/value;
                                        ro . EndEdit ( );
                                    }
                                }
                            }
                            else if ( column . Equals ( "HAX012" ) )
                            {
                                if ( workShopTime . endCenTime ( row ,row [ "HAX012" ] ,"HAX011" ,"HAX009" ,"HAX010" ) )
                                {
                                    ro = tableView . Rows [ i + 1 ];
                                    if ( ro [ "HAX012" ] == null || ro [ "HAX012" ] . ToString ( ) == string . Empty )
                                    {
                                        ro . BeginEdit ( );
                                        ro [ "HAX012" ] = /*row [ "HAX012" ];*/value;
                                        ro . EndEdit ( );
                                    }
                                }
                            }
                        }
                    }
                }
            }
            gridControl1 . RefreshDataSource ( );
            calcuTsumTime ( );
        }
        void startT ( )
        {
            result = true;
            string startTime = LineProductMesBll . UserInfoMation . sysTime . ToString ( "yyyy-MM-dd 08:00" );
            if ( string . IsNullOrEmpty ( startTime ) )
                return;
            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );
            if ( tableView == null || tableView . Rows . Count < 1 )
                return;
            for ( int i = 0 ; i < tableView . Rows . Count ; i++ )
            {
                row = tableView . Rows [ i ];
                if ( row != null )
                {
                    if ( txtHAW011 . Text . Equals ( "计件" ) )
                    {
                        row [ "HAX011" ] = DBNull . Value;
                        row [ "HAX012" ] = DBNull . Value;
                        if ( workShopTime . startTime ( row ,row["HAX009"] ,"HAX010" ,"HAX011" ,"HAX012" ) == false )
                        {
                            row [ "HAX009" ] = DBNull . Value;
                            result = false;
                        }
                        if ( result )
                        {
                            if ( workShopTime . startTime ( row ,row [ "HAX009" ] ,"HAX010" ,"HAX011" ,"HAX012" ) == false )
                            {
                                row [ "HAX009" ] = DBNull . Value;
                                result = false;
                            }
                            else
                            {
                                row . BeginEdit ( );
                                row [ "HAX009" ] = startTime;
                                row . EndEdit ( );
                            }
                        }
                    }
                    else if ( txtHAW011 . Text . Equals ( "计时" ) )
                    {
                        row [ "HAX009" ] = DBNull . Value;
                        row [ "HAX010" ] = DBNull . Value;
                        if ( workShopTime . startCenTime ( row ,row["HAX011"] ,"HAX010" ,"HAX012" ,"HAX009" ) == false )
                        {
                            row [ "HAX011" ] = DBNull . Value;
                            result = false;
                        }
                        if ( result )
                        {
                            if ( workShopTime . startCenTime ( row ,row [ "HAX011" ] ,"HAX010" ,"HAX012" ,"HAX009" ) == false )
                            {
                                row [ "HAX011" ] = DBNull . Value;
                                result = false;
                            }
                            else
                            {
                                row . BeginEdit ( );
                                row [ "HAX011" ] = startTime;
                                row . EndEdit ( );
                            }
                        }
                    }
                }
            }
            gridControl1 . RefreshDataSource ( );
            calcuTsumTime ( );
        }
        void endT ( )
        {
            result = true;
            string endTime =  LineProductMesBll . UserInfoMation . sysTime . ToString ( "yyyy-MM-dd 17:00" );
            if ( string . IsNullOrEmpty ( endTime ) )
                return;
            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );
            if ( tableView == null || tableView . Rows . Count < 1 )
                return;
            for ( int i = 0 ; i < tableView . Rows . Count ; i++ )
            {
                row = tableView . Rows [ i ];
                if ( row != null )
                {
                    if ( txtHAW011 . Text . Equals ( "计件" ) )
                    {
                        row [ "HAX011" ] = DBNull . Value;
                        row [ "HAX012" ] = DBNull . Value;
                        if ( workShopTime . endTime ( row ,row["HAX010"] ,"HAX009" ,"HAX011" ,"HAX012" ) == false )
                        {
                            row [ "HAX010" ] = DBNull . Value;
                            result = false;
                        }
                        if ( result )
                        {
                            if ( workShopTime . endTime ( row ,row [ "HAX010" ] ,"HAX009" ,"HAX011" ,"HAX012" ) == false )
                            {
                                row [ "HAX010" ] = DBNull . Value;
                                result = false;
                            }
                            if ( result )
                            {
                                row . BeginEdit ( );
                                row [ "HAX010" ] = endTime;
                                row . EndEdit ( );
                            }
                        }
                    }
                    else if ( txtHAW011 . Text . Equals ( "计时" ) )
                    {
                        row [ "HAX009" ] = DBNull . Value;
                        row [ "HAX010" ] = DBNull . Value;
                        if ( workShopTime . endCenTime ( row ,row["HAX012"] ,"HAX011" ,"HAX009" ,"HAX010" ) == false )
                        {
                            row [ "HAX012" ] = DBNull . Value;
                            result = false;
                        }
                        if ( result )
                        {
                            if ( workShopTime . endCenTime ( row ,row [ "HAX012" ] ,"HAX011" ,"HAX009" ,"HAX010" ) == false )
                            {
                                row [ "HAX012" ] = DBNull . Value;
                                result = false;
                            }
                            if ( result )
                            {
                                row . BeginEdit ( );
                                row [ "HAX012" ] = endTime;
                                row . EndEdit ( );
                            }
                        }
                    }
                }
            }
            calcuTsumTime ( );
        }
        void calcuTsumTime ( )
        {
            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );

            if ( tableView == null || tableView . Rows . Count < 1 )
                return;
            decimal haw020 = txtHAW020 . Text == string . Empty ? 0 : Convert . ToDecimal ( txtHAW020 . Text ) * 60;
            decimal haw021 = txtHAW021 . Text == string . Empty ? 0 : Convert . ToDecimal ( txtHAW021 . Text ) * 60;

            DateTime dtOne, dtTwo;
            decimal u0 = 0M;
            foreach ( DataRow row in tableView . Rows )
            {
                if ( string . IsNullOrEmpty ( row [ "HAX015" ] . ToString ( ) ) || row [ "HAX015" ] . ToString ( ) . Equals ( "离职" ) || row [ "HAX015" ] . ToString ( ) . Equals ( "未上班" ) )
                {
                    row [ "HAX018" ] = 0;
                    row [ "HAX019" ] = 0;
                    row [ "HAX020" ] = 0;
                    continue;
                }

                if ( !string . IsNullOrEmpty ( row [ "HAX009" ] . ToString ( ) ) && !string . IsNullOrEmpty ( row [ "HAX010" ] . ToString ( ) ) )
                {
                    dtOne = Convert . ToDateTime ( row [ "HAX009" ] );
                    dtTwo = Convert . ToDateTime ( row [ "HAX010" ] );
                    //判断开始上班时间和中午休息时间、下午下班时间
                    u0 = ( dtTwo - dtOne ) . Hours + ( dtTwo - dtOne ) . Minutes * Convert . ToDecimal ( 1.0 ) / 60;

                    if ( dtOne . Hour <= 11 && dtTwo . Hour >= 12 )
                    {
                        u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( haw020 ) ) * Convert . ToDecimal ( 1.0 ) / 60;
                        if ( dtTwo . Hour >= 17 && dtTwo . Minute >= 30 )
                            u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( haw020 ) - Convert . ToDecimal ( haw021 ) ) * Convert . ToDecimal ( 1.0 ) / 60;
                    }
                    else if ( dtTwo . Hour >= 17 && dtTwo . Minute >= 30 )
                        u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( haw021 ) ) * Convert . ToDecimal ( 1.0 ) / 60;

                    row [ "HAX018" ] = Math . Round ( u0 ,1 ,MidpointRounding . AwayFromZero );
                }
                else
                    row [ "HAX018" ] = 0;

                u0 = 0M;
                if ( !string . IsNullOrEmpty ( row [ "HAX011" ] . ToString ( ) ) && !string . IsNullOrEmpty ( row [ "HAX012" ] . ToString ( ) ) )
                {
                    dtOne = Convert . ToDateTime ( row [ "HAX011" ] );
                    dtTwo = Convert . ToDateTime ( row [ "HAX012" ] );
                    //判断开始上班时间和中午休息时间、下午下班时间
                    u0 = ( dtTwo - dtOne ) . Hours + ( dtTwo - dtOne ) . Minutes * Convert . ToDecimal ( 1.0 ) / 60;
                    if ( dtOne . Hour <= 11 && dtTwo . Hour >= 12 )
                    {
                        u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( haw020 ) ) * Convert . ToDecimal ( 1.0 ) / 60;
                        if ( dtTwo . Hour >= 17 && dtTwo . Minute >= 30 )
                            u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( haw020 ) - Convert . ToDecimal ( haw021 ) ) * Convert . ToDecimal ( 1.0 ) / 60;
                    }
                    else if ( dtTwo . Hour >= 17 && dtTwo . Minute >= 30 )
                        u0 = ( dtTwo - dtOne ) . Hours + ( ( dtTwo - dtOne ) . Minutes - Convert . ToDecimal ( haw021 ) ) * Convert . ToDecimal ( 1.0 ) / 60;

                    row [ "HAX019" ] = Math . Round ( u0 ,1 ,MidpointRounding . AwayFromZero );
                }
                else
                    row [ "HAX019" ] = 0;
            }
            addSalaryAll ( );
        }
        void addSalaryAll ( )
        {
            foreach ( DataRow row in tableView . Rows )
            {
                if ( row == null )
                    continue;
                if ( string . IsNullOrEmpty ( row [ "HAX015" ] . ToString ( ) ) || row [ "HAX015" ] . ToString ( ) . Equals ( "离职" ) || row [ "HAX015" ] . ToString ( ) . Equals ( "未上班" ) )
                {
                    row [ "HAX018" ] = 0;
                    row [ "HAX019" ] = 0;
                    row [ "HAX020" ] = 0;
                    continue;
                }
                row [ "HAX020" ] = Math . Round ( ( string . IsNullOrEmpty ( row [ "HAX007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "HAX007" ] ) * ( string . IsNullOrEmpty ( row [ "HAX008" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "HAX008" ] ) ) ) ,1 ,MidpointRounding . AwayFromZero ) + Math . Round ( ( string . IsNullOrEmpty ( row [ "HAX013" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "HAX013" ] ) * ( string . IsNullOrEmpty ( row [ "HAX019" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "HAX019" ] ) ) ) ,1 ,MidpointRounding . AwayFromZero );
            }
        }
        void editOtherSur ( )
        {
            bandedGridView1 . CloseEditor ( );
            bandedGridView1 . UpdateCurrentRow ( );

            if ( tableView == null || tableView . Rows . Count < 1 )
                return;
            DataRow [ ] rows;
            foreach ( DataRow row in tableView . Rows )
            {
                rows = tableOtherSur . Select ( "HAX016='" + row [ "HAX016" ] + "'" );
                if ( rows . Length > 0 )
                {
                    row [ "U4" ] = rows [ 0 ] [ "HAX008" ];
                }
                else
                {
                    row [ "U4" ] = string . IsNullOrEmpty ( txtHAW007 . Text ) == true ? 0 : Convert . ToInt32 ( txtHAW007 . Text );
                }
            }
        }
        #endregion

    }
}




