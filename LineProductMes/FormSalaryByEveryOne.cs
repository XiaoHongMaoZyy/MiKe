using System;
using System . Collections . Generic;
using System . ComponentModel;
using System . Data;
using System . Drawing;
using System . Text;
using System . Linq;
using System . Threading . Tasks;
using System . Windows . Forms;
using DevExpress . XtraEditors;
using Utility;
using LineProductMes . ClassForMain;
using System . Reflection;
using DevExpress . Utils . Paint;

namespace LineProductMes
{
    public partial class FormSalaryByEveryOne :FormChild
    {
        LineProductMesBll.Bll.SalaryByEveryOneBll _bll=null;

        public FormSalaryByEveryOne ( )
        {
            InitializeComponent ( );

            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 } );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            ToolBarContain . ToolbarsC ( barTool ,new DevExpress . XtraBars . BarItem [ ] { toolCanecl ,toolSave ,toolPrint ,toolCancellation ,toolExamin ,toolDelete ,toolEdit ,toolAdd } );

            _bll = new LineProductMesBll . Bll . SalaryByEveryOneBll ( );
            toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
        }

        protected override int Query ( )
        {
            if ( string . IsNullOrEmpty ( tYear . Text ) )
            {
                XtraMessageBox . Show ( "请选择年月日" );return 0;
            }
            if ( string . IsNullOrEmpty ( dtTime . Text ) )
            {
                XtraMessageBox . Show ( "请选择日期" );
                return 0;
            }

            string strWhere = string . Empty;
            if ( tYear . Text . Equals ( "年" ) )
                strWhere = Convert . ToDateTime ( dtTime . Text ) . ToString ( "yyyy" );
            else if ( tYear . Text . Equals ( "月" ) )
                strWhere = Convert . ToDateTime ( dtTime . Text ) . ToString ( "yyyyMM" );
            else if ( tYear . Text . Equals ( "日" ) )
                strWhere = Convert . ToDateTime ( dtTime . Text ) . ToString ( "yyyyMMdd" );

            DataTable table = _bll . getTableView ( strWhere );
            gridControl1 . DataSource = table;

            return base . Query ( );
        }
        protected override int Export ( )
        {
            ViewExport . ExportToExcel ( this . Text ,gridControl1 );

            return base . Export ( );
        }

    }
}