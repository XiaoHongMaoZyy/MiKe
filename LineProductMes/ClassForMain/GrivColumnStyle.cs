using DevExpress . XtraGrid . Views . Grid;

namespace LineProductMes . ClassForMain
{
    public static class GrivColumnStyle
    {
        /// <summary>
        /// 设置gridview的样式
        /// </summary>
        /// <param name="grids"></param>
        public static void setColumnStyle ( GridView [ ] grids )
        {
            foreach ( GridView gv in grids )
            {
                foreach ( DevExpress . XtraGrid . Columns . GridColumn co in gv . Columns )
                {
                    co . OptionsFilter . FilterPopupMode = DevExpress . XtraGrid . Columns . FilterPopupMode . CheckedList;
                    co . AppearanceCell . Font = new System . Drawing . Font ( "微软雅黑" ,10F  );
                    co . AppearanceCell . Options . UseFont = true;
                    co . AppearanceCell . Options . UseTextOptions = true;
                    co . AppearanceCell . TextOptions . HAlignment = DevExpress . Utils . HorzAlignment . Center;
                    co . AppearanceCell . TextOptions . WordWrap = DevExpress . Utils . WordWrap . Wrap;
                    co . AppearanceHeader . Font = new System . Drawing . Font ( "微软雅黑" ,10F );
                    co . AppearanceHeader . Options . UseFont = true;
                    co . AppearanceHeader . Options . UseTextOptions = true;
                    co . AppearanceHeader . TextOptions . HAlignment = DevExpress . Utils . HorzAlignment . Center;
                    co . AppearanceHeader . TextOptions . WordWrap = DevExpress . Utils . WordWrap . Wrap;
                }
            }
        }
        
        /// <summary>
        /// 设置tree的样式
        /// </summary>
        /// <param name="tree"></param>
        public static void setColumnStyle ( DevExpress . XtraTreeList . TreeList tree )
        {
            foreach ( DevExpress .XtraTreeList.Columns.TreeListColumn co in tree . Columns )
            {
                co . OptionsFilter . FilterPopupMode = DevExpress . XtraTreeList . FilterPopupMode . CheckedList;
                co . AppearanceCell . Font = new System . Drawing . Font ( "微软雅黑" ,10F );
                co . AppearanceCell . Options . UseFont = true;
                co . AppearanceCell . Options . UseTextOptions = true;
                co . AppearanceCell . TextOptions . HAlignment = DevExpress . Utils . HorzAlignment . Center;
                co . AppearanceCell . TextOptions . WordWrap = DevExpress . Utils . WordWrap . Wrap;
                co . AppearanceHeader . Font = new System . Drawing . Font ( "微软雅黑" ,10F );
                co . AppearanceHeader . Options . UseFont = true;
                co . AppearanceHeader . Options . UseTextOptions = true;
                co . AppearanceHeader . TextOptions . HAlignment = DevExpress . Utils . HorzAlignment . Center;
                co . AppearanceHeader . TextOptions . WordWrap = DevExpress . Utils . WordWrap . Wrap;
            }
        }

        /// <summary>
        /// 设置行字体色
        /// </summary>
        /// <param name="grids"></param>
        public static void setColumnFontStyle ( GridView [ ] grids )
        {
            foreach ( GridView gv in grids )
            {
                foreach ( DevExpress . XtraGrid . Columns . GridColumn co in gv . Columns )
                {
                    co . AppearanceCell . ForeColor = System . Drawing . Color . White;
                    co . AppearanceCell . Options . UseForeColor = true;
                }
            }
        }

        /// <summary>
        /// 设置行背景色
        /// </summary>
        /// <param name="grids"></param>
        public static void setColumnAppHeaderBack ( GridView [ ] grids )
        {
            foreach ( GridView gv in grids )
            {
                foreach ( DevExpress . XtraGrid . Columns . GridColumn co in gv . Columns )
                {
                    if ( co . OptionsColumn . AllowEdit )
                    {
                        co . AppearanceCell . BackColor = System . Drawing . Color . Pink;
                    }
                }
            }
        }

    }
}
