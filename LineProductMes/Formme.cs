using DevExpress . Utils . Paint;
using DevExpress . XtraEditors;
using DevExpress . XtraGrid;
using DevExpress . XtraGrid . Columns;
using LineProductMes . ClassForMain;
using System;
using System . Collections . Generic;
using System . ComponentModel;
using System . Data;
using System . Drawing;
using System . Linq;
using System . Reflection;
using System . Text;
using System . Threading . Tasks;
using System . Windows . Forms;
using Utility;

namespace LineProductMes
{
    public partial class Formme : FormChild
    {
        LineProductMesBll.Bll.SalaryByEveryOneBll _bll = null;

        public Formme()
        {
            InitializeComponent();

            _bll = new LineProductMesBll.Bll.SalaryByEveryOneBll();
            //使表格单元格数据只读，
            gridView1.OptionsBehavior.Editable = false;
        
            GridViewMoHuSelect . SetFilter ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 } );
            GrivColumnStyle . setColumnStyle ( new DevExpress . XtraGrid . Views . Grid . GridView [ ] { gridView1 } );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            ToolBarContain . ToolbarsC ( barTool ,new DevExpress . XtraBars . BarItem [ ] { toolCanecl ,toolSave ,toolPrint ,toolCancellation ,toolExamin ,toolDelete ,toolEdit ,toolAdd } );
           
        }

        protected override int Query()
        {

            if (string.IsNullOrEmpty(yearormoney.Text))
            {
                XtraMessageBox.Show("请选择日期类型!");
                return 0;
            }
            if (string.IsNullOrEmpty(timeshow.Text))
            {
                XtraMessageBox.Show("请选择日期!");
                return 0;
            }
            //判断并给sqlwhere赋值
            string sqlwhere = string.Empty;
            if ("年".Equals(yearormoney.Text))
            {
                sqlwhere = Convert.ToDateTime(timeshow.Text).ToString("yyyy");
            }
            else if ("月".Equals(yearormoney.Text))
            {
                sqlwhere = Convert.ToDateTime(timeshow.Text).ToString("yyyyMM");
            }
            else if ("日".Equals(yearormoney.Text))
            {
                sqlwhere = Convert.ToDateTime(timeshow.Text).ToString("yyyyMMdd");
            }
            DataTable table = _bll.getInventoryView(sqlwhere);
            gridControl1.DataSource = table;
            return base.Query();
        }
        protected override int Export ( )
        {
            ViewExport . ExportToExcel ( this . Text ,gridControl1 );

            return base . Export ( );
        }

        decimal ann007=0M,ann009=0M,rcc006=0M;

        private void Formme_Load ( object sender ,EventArgs e )
        {
            toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
        }

        private void gridView1_RowStyle ( object sender ,DevExpress . XtraGrid . Views . Grid . RowStyleEventArgs e )
        {
            DataRow dt = gridView1 . GetDataRow ( e . RowHandle ) as DataRow;
            if ( dt == null )
                return;
            //工单数量
            ann007 = string . IsNullOrEmpty ( dt [ "ANN007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( dt [ "ANN007" ] );
            //完工数量
            ann009 = string . IsNullOrEmpty ( dt [ "ANN009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( dt [ "ANN009" ] );
            //入库数量
            rcc006 = string . IsNullOrEmpty ( dt [ "RCC006" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( dt [ "RCC006" ] );
            if ( ann009 > ann007 || ann009 < rcc006 || rcc006 == 0 )
                e . Appearance . BackColor = Color . Pink;

        }
    }
}
