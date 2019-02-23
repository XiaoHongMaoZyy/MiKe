using System;
using System . Collections . Generic;
using LineProductMes . ClassForMain;
using Utility;
using System . Reflection;
using DevExpress . Utils . Paint;
using DevExpress . XtraTreeList . Nodes . Operations;
using DevExpress . XtraTreeList . Columns;
using DevExpress . XtraTreeList . Nodes;
using System . Windows . Forms;

namespace LineProductMes
{
    public partial class FormSemiProductPlan :FormChild
    {
        LineProductMesBll.Bll.SemiProductPlanBll _bll=null;
        LineProductMesEntityu.SemiProductPlanEntity model=null;
        
        public FormSemiProductPlan ( )
        {
            InitializeComponent ( );

            _bll = new LineProductMesBll . Bll . SemiProductPlanBll ( );
            model = new LineProductMesEntityu . SemiProductPlanEntity ( );

            ToolBarContain . ToolbarsC ( barTool ,new DevExpress . XtraBars . BarItem [ ] { toolCanecl ,toolSave  ,toolPrint ,toolCancellation ,toolExamin ,toolDelete ,toolEdit } );
            GrivColumnStyle . setColumnStyle ( treeList1 );
            FieldInfo fi = typeof ( XPaint ) . GetField ( "graphics" ,BindingFlags . Static | BindingFlags . NonPublic );
            fi . SetValue ( null ,new DrawXPaint ( ) );

            toolAdd . Caption = "展开";
        }

        #region Main
        protected override int Query ( )
        {
            treeList1 . KeyFieldName = "SEP007";
            treeList1 . ParentFieldName = "SEP008";
            List<LineProductMesEntityu . SemiProductPlanEntity> modelList = _bll . getListModel ( );
            treeList1 . DataSource = modelList;

            toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;

            return base . Query ( );
        }
        protected override int ExportBase ( )
        {
            ViewExport . ExportToExcel ( this . Text ,this . treeList1 );

            return base . ExportBase ( );
        }
        protected override int Add ( )
        {
            if ( toolAdd . Caption . Equals ( "展开" ) )
            {
                treeList1 . ExpandAll ( );
                toolAdd . Caption = "折叠";
            }
            else
            {
                toolAdd . Caption = "展开";
                foreach ( TreeListNode node in treeList1 . Nodes )
                {
                    treeList1 . Nodes . TreeList . FindNodeByID ( node . Id ) . Expanded = false;
                }
            }

            return base . Add ( );
        }
        #endregion

        #region Event
        private void treeList1_ColumnFilterChanged ( object sender ,EventArgs e )
        {
            //if ( treeList1 . ActiveEditor != null )
            //{
            //    string newKey = treeList1 . ActiveEditor . EditValue . ToString ( );
            //    FilterNodeOperation operation = new FilterNodeOperation ( ( !System . String . IsNullOrEmpty ( newKey ) ) ? newKey : "" );
            //    treeList1 . NodesIterator . DoOperation ( operation );
            //}

        }
        #endregion

        private void FormSemiProductPlan_Load ( object sender ,EventArgs e )
        {
            treeList1 . OptionsBehavior . EnableFiltering = true;
            treeList1 . OptionsView . ShowAutoFilterRow = true;
            treeList1 . OptionsFilter . FilterMode = DevExpress . XtraTreeList . FilterMode . Smart;
        }

    }
}