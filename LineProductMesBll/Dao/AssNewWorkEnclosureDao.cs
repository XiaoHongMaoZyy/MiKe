﻿using System;
using System . Collections . Generic;
using System . Text;
using StudentMgr;
using System . Data;
using System . Data . SqlClient;

namespace LineProductMesBll . Dao
{
    public class AssNewWorkEnclosureDao
    {
        /// <summary>
        /// 获取来源工单等信息
        /// </summary>
        /// <returns></returns>
        public DataTable getTablePInfo ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT RAA001,RAA015,DEA002,CONVERT(FLOAT,RAA018) RAA018,RAA008,ART003,ART004  FROM SGMRAA A INNER JOIN TPADEA B ON A.RAA015=B.DEA001 INNER JOIN MIKART C ON A.RAA015=C.ART001 WHERE DEA009 IN ('M','S') AND DEA076='0507' AND RAA020='N' AND ART003='附件'  AND RAA024='T'" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取单号
        /// </summary>
        /// <returns></returns>
        public string getCode ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT MAX(ANT001) ANT001 FROM MIKANT" );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                return UserInfoMation . sysTime . ToString ( "yyyyMMdd" ) + "001";
            else
            {
                string code = table . Rows [ 0 ] [ "ANT001" ] . ToString ( );
                if ( string . IsNullOrEmpty ( code ) )
                    return UserInfoMation . sysTime . ToString ( "yyyyMMdd" ) + "001";
                else
                {
                    if ( code . Substring ( 0 ,8 ) . Equals ( UserInfoMation . sysTime . ToString ( "yyyyMMdd" ) ) )
                        return ( Convert . ToInt64 ( code ) + 1 ) . ToString ( );
                    else
                        return UserInfoMation . sysTime . ToString ( "yyyyMMdd" ) + "001";
                }
            }
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable getTableViewOne ( string strWhere )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT idx,ANU001,ANU002,ANU003,ANU004,ANU005,ANU006,CONVERT(FLOAT,ANU007) ANU007,ANU008,ANU009,ANU010,ANU011,0 U4 FROM MIKANU" );
            strSql . AppendFormat ( " WHERE " + strWhere );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable getTableViewTwo ( string strWhere )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT idx,ANV001,ANV002,ANV003,ANV004,ANV005,ANV006,ANV007,0.000000 U1,ANV008,ANV009,ANV010 FROM MIKANV " );
            strSql . AppendFormat ( "WHERE {0}" ,strWhere );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public bool Delete ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "DELETE FROM MIKANT WHERE ANT001='{0}'" ,oddNum );

            return SqlHelper . ExecuteNonQueryBool ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tableViewOne"></param>
        /// <param name="tableViewTwo"></param>
        /// <returns></returns>
        public bool Save ( LineProductMesEntityu . AssNewWorkEnclosureHeaderEntity model ,DataTable tableViewOne ,DataTable tableViewTwo )
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            StringBuilder strSql = new StringBuilder ( );
            model . ANT001 = getCode ( );
            AddHeader ( SQLString ,strSql ,model );
            LineProductMesBll . UserInfoMation . oddNum = model . ANT001;

            LineProductMesEntityu . AssNewWorkEnclosureBodyOneEntity modelOne = new LineProductMesEntityu . AssNewWorkEnclosureBodyOneEntity ( );
            foreach ( DataRow row in tableViewOne . Rows )
            {
                modelOne . ANU001 = model . ANT001;
                modelOne . ANU002 = row [ "ANU002" ] . ToString ( );
                modelOne . ANU003 = row [ "ANU003" ] . ToString ( );
                modelOne . ANU004 = row [ "ANU004" ] . ToString ( );
                modelOne . ANU005 = row [ "ANU005" ] . ToString ( );
                modelOne . ANU006 = row [ "ANU006" ] . ToString ( );
                modelOne . ANU007 = string . IsNullOrEmpty ( row [ "ANU007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANU007" ] . ToString ( ) );
                modelOne . ANU008 = row [ "ANU008" ] . ToString ( );
                modelOne . ANU009 = row [ "ANU009" ] . ToString ( );
                modelOne . ANU010 = string . IsNullOrEmpty ( row [ "ANU010" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "ANU010" ] . ToString ( ) );
                modelOne . ANU011 = string . IsNullOrEmpty ( row [ "ANU011" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "ANU011" ] . ToString ( ) );
                AddBodyOne ( SQLString ,strSql ,modelOne );
            }

            LineProductMesEntityu . AssNewWorkEnclosureBodyTwoEntity modelTwo = new LineProductMesEntityu . AssNewWorkEnclosureBodyTwoEntity ( );
            foreach ( DataRow row in tableViewTwo . Rows )
            {
                modelTwo . ANV001 = model . ANT001;
                modelTwo . ANV002 = row [ "ANV002" ] . ToString ( );
                modelTwo . ANV003 = row [ "ANV003" ] . ToString ( );
                modelTwo . ANV004 = row [ "ANV004" ] . ToString ( );
                if ( row [ "ANV005" ] == null || row [ "ANV005" ] . ToString ( ) == string . Empty )
                    modelTwo . ANV005 = null;
                else
                    modelTwo . ANV005 = Convert . ToDateTime ( row [ "ANV005" ] );
                if ( row [ "ANV006" ] == null || row [ "ANV006" ] . ToString ( ) == string . Empty )
                    modelTwo . ANV006 = null;
                else
                    modelTwo . ANV006 = Convert . ToDateTime ( row [ "ANV006" ] );
                modelTwo . ANV007 = row [ "ANV007" ] . ToString ( );
                modelTwo . ANV008 = row [ "ANV008" ] . ToString ( );
                modelTwo . ANV009 = string . IsNullOrEmpty ( row [ "ANV009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANV009" ] . ToString ( ) );
                modelTwo . ANV010 = string . IsNullOrEmpty ( row [ "ANV010" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANV010" ] . ToString ( ) );
                ADDBodyTwo ( SQLString ,strSql ,modelTwo );
            }

            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tableViewOne"></param>
        /// <param name="tableViewTwo"></param>
        /// <param name="idxOne"></param>
        /// <param name="idxTwo"></param>
        /// <returns></returns>
        public bool Edit ( LineProductMesEntityu . AssNewWorkEnclosureHeaderEntity model ,DataTable tableViewOne ,DataTable tableViewTwo ,List<string> idxOne ,List<string> idxTwo )
        {
            Dictionary<object ,object> SQLString = new Dictionary<object ,object> ( );
            StringBuilder strSql = new StringBuilder ( );
            EditHeader ( SQLString ,strSql ,model );

            LineProductMesEntityu . AssNewWorkEnclosureBodyOneEntity modelOne = new LineProductMesEntityu . AssNewWorkEnclosureBodyOneEntity ( );
            foreach ( DataRow row in tableViewOne . Rows )
            {
                modelOne . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                modelOne . ANU001 = model . ANT001;
                modelOne . ANU002 = row [ "ANU002" ] . ToString ( );
                modelOne . ANU003 = row [ "ANU003" ] . ToString ( );
                modelOne . ANU004 = row [ "ANU004" ] . ToString ( );
                modelOne . ANU005 = row [ "ANU005" ] . ToString ( );
                modelOne . ANU006 = row [ "ANU006" ] . ToString ( );
                modelOne . ANU007 = string . IsNullOrEmpty ( row [ "ANU007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANU007" ] . ToString ( ) );
                modelOne . ANU008 = row [ "ANU008" ] . ToString ( );
                modelOne . ANU009 = row [ "ANU009" ] . ToString ( );
                modelOne . ANU010 = string . IsNullOrEmpty ( row [ "ANU010" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "ANU010" ] . ToString ( ) );
                modelOne . ANU011 = string . IsNullOrEmpty ( row [ "ANU011" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "ANU011" ] . ToString ( ) );
                if ( modelOne . idx > 0 )
                    EditBodyOne ( SQLString ,strSql ,modelOne );
                else
                    AddBodyOne ( SQLString ,strSql ,modelOne );
            }
            LineProductMesEntityu . AssNewWorkEnclosureBodyTwoEntity modelTwo = new LineProductMesEntityu . AssNewWorkEnclosureBodyTwoEntity ( );
            foreach ( DataRow row in tableViewTwo . Rows )
            {
                modelTwo . idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                modelTwo . ANV001 = model . ANT001;
                modelTwo . ANV002 = row [ "ANV002" ] . ToString ( );
                modelTwo . ANV003 = row [ "ANV003" ] . ToString ( );
                modelTwo . ANV004 = row [ "ANV004" ] . ToString ( );
                if ( row [ "ANV005" ] == null || row [ "ANV005" ] . ToString ( ) == string . Empty )
                    modelTwo . ANV005 = null;
                else
                    modelTwo . ANV005 = Convert . ToDateTime ( row [ "ANV005" ] );
                if ( row [ "ANV006" ] == null || row [ "ANV006" ] . ToString ( ) == string . Empty )
                    modelTwo . ANV006 = null;
                else
                    modelTwo . ANV006 = Convert . ToDateTime ( row [ "ANV006" ] );
                modelTwo . ANV007 = row [ "ANV007" ] . ToString ( );
                modelTwo . ANV008 = row [ "ANV008" ] . ToString ( );
                modelTwo . ANV009 = string . IsNullOrEmpty ( row [ "ANV009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANV009" ] . ToString ( ) );
                modelTwo . ANV010 = string . IsNullOrEmpty ( row [ "ANV010" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "ANV010" ] . ToString ( ) );
                if ( modelTwo . idx > 0 )
                    EditBodyTwo ( SQLString ,strSql ,modelTwo );
                else
                    ADDBodyTwo ( SQLString ,strSql ,modelTwo );
            }

            foreach ( string s in idxOne )
            {
                DeleteBodyOne ( SQLString ,strSql ,s );
            }

            foreach ( string s in idxTwo )
            {
                DeleteBodyTwo ( SQLString ,strSql ,s);
            }

            return SqlHelper . ExecuteSqlTranDic ( SQLString );
        }

        void AddHeader ( Dictionary<object ,object> SQLString ,StringBuilder strSql ,LineProductMesEntityu . AssNewWorkEnclosureHeaderEntity model )
        {
            strSql = new StringBuilder ( );
            strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKANT(" );
            strSql . Append ( "ANT001,ANT002,ANT003,ANT004,ANT005,ANT008,ANT009,ANT010)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@ANT001,@ANT002,@ANT003,@ANT004,@ANT005,@ANT008,@ANT009,@ANT010)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@ANT001", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANT002", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANT003", SqlDbType.NVarChar,50),
                    new SqlParameter("@ANT004", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANT005", SqlDbType.NVarChar,50),
                    new SqlParameter("@ANT008", SqlDbType.Date,3),
                    new SqlParameter("@ANT009", SqlDbType.Decimal),
                    new SqlParameter("@ANT010", SqlDbType.Decimal)
            };
            parameters [ 0 ] . Value = model . ANT001;
            parameters [ 1 ] . Value = model . ANT002;
            parameters [ 2 ] . Value = model . ANT003;
            parameters [ 3 ] . Value = model . ANT004;
            parameters [ 4 ] . Value = model . ANT005;
            parameters [ 5 ] . Value = model . ANT008;
            parameters [ 6 ] . Value = model . ANT009;
            parameters [ 7 ] . Value = model . ANT010;
            SQLString . Add ( strSql ,parameters );
        }
        void AddBodyOne ( Dictionary<object ,object> SQLString ,StringBuilder strSql ,LineProductMesEntityu . AssNewWorkEnclosureBodyOneEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKANU(" );
            strSql . Append ( "ANU001,ANU002,ANU003,ANU004,ANU005,ANU006,ANU007,ANU008,ANU009,ANU010,ANU011)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@ANU001,@ANU002,@ANU003,@ANU004,@ANU005,@ANU006,@ANU007,@ANU008,@ANU009,@ANU010,@ANU011)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@ANU001", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANU002", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANU003", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANU004", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANU005", SqlDbType.NVarChar,50),
                    new SqlParameter("@ANU006", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANU007", SqlDbType.Decimal,9),
                    new SqlParameter("@ANU008", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANU009", SqlDbType.NVarChar,50),
                    new SqlParameter("@ANU010", SqlDbType.Int,4),
                    new SqlParameter("@ANU011", SqlDbType.Int,4)};
            parameters [ 0 ] . Value = model . ANU001;
            parameters [ 1 ] . Value = model . ANU002;
            parameters [ 2 ] . Value = model . ANU003;
            parameters [ 3 ] . Value = model . ANU004;
            parameters [ 4 ] . Value = model . ANU005;
            parameters [ 5 ] . Value = model . ANU006;
            parameters [ 6 ] . Value = model . ANU007;
            parameters [ 7 ] . Value = model . ANU008;
            parameters [ 8 ] . Value = model . ANU009;
            parameters [ 9 ] . Value = model . ANU010;
            parameters [ 10 ] . Value = model . ANU011;
            SQLString . Add ( strSql ,parameters );
        }
        void ADDBodyTwo ( Dictionary<object ,object> SQLString ,StringBuilder strSql ,LineProductMesEntityu . AssNewWorkEnclosureBodyTwoEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "insert into MIKANV(" );
            strSql . Append ( "ANV001,ANV002,ANV003,ANV004,ANV005,ANV006,ANV007,ANV008,ANV009,ANV010)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@ANV001,@ANV002,@ANV003,@ANV004,@ANV005,@ANV006,@ANV007,@ANV008,@ANV009,@ANV010)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@ANV001", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANV002", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANV003", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANV004", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANV005", SqlDbType.DateTime),
                    new SqlParameter("@ANV006", SqlDbType.DateTime),
                    new SqlParameter("@ANV007", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANV008", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANV009", SqlDbType.Decimal),
                    new SqlParameter("@ANV010", SqlDbType.Decimal)
            };
            parameters [ 0 ] . Value = model . ANV001;
            parameters [ 1 ] . Value = model . ANV002;
            parameters [ 2 ] . Value = model . ANV003;
            parameters [ 3 ] . Value = model . ANV004;
            parameters [ 4 ] . Value = model . ANV005;
            parameters [ 5 ] . Value = model . ANV006;
            parameters [ 6 ] . Value = model . ANV007;
            parameters [ 7 ] . Value = model . ANV008;
            parameters [ 8 ] . Value = model . ANV009;
            parameters [ 9 ] . Value = model . ANV010;
            SQLString . Add ( strSql ,parameters );
        }

        void EditHeader ( Dictionary<object ,object> SQLString ,StringBuilder strSql ,LineProductMesEntityu . AssNewWorkEnclosureHeaderEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "update MIKANT set " );
            strSql . Append ( "ANT002=@ANT002," );
            strSql . Append ( "ANT003=@ANT003," );
            strSql . Append ( "ANT004=@ANT004," );
            strSql . Append ( "ANT005=@ANT005," );
            strSql . Append ( "ANT006=@ANT006," );
            strSql . Append ( "ANT007=@ANT007," );
            strSql . Append ( "ANT009=@ANT009," );
            strSql . Append ( "ANT010=@ANT010 " );
            strSql . Append ( " where ANT001=@ANT001" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@ANT002", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANT003", SqlDbType.NVarChar,50),
                    new SqlParameter("@ANT004", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANT005", SqlDbType.NVarChar,50),
                    new SqlParameter("@ANT006", SqlDbType.Bit,1),
                    new SqlParameter("@ANT007", SqlDbType.Bit,1),
                    new SqlParameter("@ANT001", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANT009", SqlDbType.Decimal),
                    new SqlParameter("@ANT010", SqlDbType.Decimal)
            };
            parameters [ 0 ] . Value = model . ANT002;
            parameters [ 1 ] . Value = model . ANT003;
            parameters [ 2 ] . Value = model . ANT004;
            parameters [ 3 ] . Value = model . ANT005;
            parameters [ 4 ] . Value = model . ANT006;
            parameters [ 5 ] . Value = model . ANT007;
            parameters [ 6 ] . Value = model . ANT001;
            parameters [ 7 ] . Value = model . ANT009;
            parameters [ 8 ] . Value = model . ANT010;
            SQLString . Add ( strSql ,parameters );
        }
        void EditBodyOne ( Dictionary<object ,object> SQLString ,StringBuilder strSql ,LineProductMesEntityu . AssNewWorkEnclosureBodyOneEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "update MIKANU set " );
            strSql . Append ( "ANU002=@ANU002," );
            strSql . Append ( "ANU003=@ANU003," );
            strSql . Append ( "ANU004=@ANU004," );
            strSql . Append ( "ANU005=@ANU005," );
            strSql . Append ( "ANU006=@ANU006," );
            strSql . Append ( "ANU007=@ANU007," );
            strSql . Append ( "ANU008=@ANU008," );
            strSql . Append ( "ANU009=@ANU009," );
            strSql . Append ( "ANU010=@ANU010," );
            strSql . Append ( "ANU011=@ANU011" );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@ANU003", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANU004", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANU005", SqlDbType.NVarChar,50),
                    new SqlParameter("@ANU006", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANU007", SqlDbType.Decimal,9),
                    new SqlParameter("@ANU008", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANU009", SqlDbType.NVarChar,50),
                    new SqlParameter("@ANU010", SqlDbType.Int,4),
                    new SqlParameter("@idx", SqlDbType.Int,4),
                    new SqlParameter("@ANU002", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANU011", SqlDbType.Int,4)
            };
            parameters [ 0 ] . Value = model . ANU003;
            parameters [ 1 ] . Value = model . ANU004;
            parameters [ 2 ] . Value = model . ANU005;
            parameters [ 3 ] . Value = model . ANU006;
            parameters [ 4 ] . Value = model . ANU007;
            parameters [ 5 ] . Value = model . ANU008;
            parameters [ 6 ] . Value = model . ANU009;
            parameters [ 7 ] . Value = model . ANU010;
            parameters [ 8 ] . Value = model . idx;
            parameters [ 9 ] . Value = model . ANU002;
            parameters [ 10 ] . Value = model . ANU011;
            SQLString . Add ( strSql ,parameters );
        }
        void EditBodyTwo ( Dictionary<object ,object> SQLString ,StringBuilder strSql ,LineProductMesEntityu . AssNewWorkEnclosureBodyTwoEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "update MIKANV set " );
            strSql . Append ( "ANV002=@ANV002," );
            strSql . Append ( "ANV003=@ANV003," );
            strSql . Append ( "ANV004=@ANV004," );
            strSql . Append ( "ANV005=@ANV005," );
            strSql . Append ( "ANV006=@ANV006," );
            strSql . Append ( "ANV007=@ANV007," );
            strSql . Append ( "ANV008=@ANV008," );
            strSql . Append ( "ANV009=@ANV009," );
            strSql . Append ( "ANV010=@ANV010" );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@ANV003", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANV004", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANV005", SqlDbType.DateTime),
                    new SqlParameter("@ANV006", SqlDbType.DateTime),
                    new SqlParameter("@idx", SqlDbType.Int,4),
                    new SqlParameter("@ANV002", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANV007", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANV008", SqlDbType.NVarChar,20),
                    new SqlParameter("@ANV009", SqlDbType.Decimal,9),
                    new SqlParameter("@ANV010", SqlDbType.Decimal,9)
            };
            parameters [ 0 ] . Value = model . ANV003;
            parameters [ 1 ] . Value = model . ANV004;
            parameters [ 2 ] . Value = model . ANV005;
            parameters [ 3 ] . Value = model . ANV006;
            parameters [ 4 ] . Value = model . idx;
            parameters [ 5 ] . Value = model . ANV002;
            parameters [ 6 ] . Value = model . ANV007;
            parameters [ 7 ] . Value = model . ANV008;
            parameters [ 8 ] . Value = model . ANV009;
            parameters [ 9 ] . Value = model . ANV010;
            SQLString . Add ( strSql ,parameters );
        }

        void DeleteBodyOne ( Dictionary<object ,object> SQLString ,StringBuilder strSql ,string idx )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "delete from MIKANU " );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@idx", SqlDbType.Int,4)
            };
            parameters [ 0 ] . Value = idx;
            SQLString . Add ( strSql ,parameters );
        }
        void DeleteBodyTwo ( Dictionary<object ,object> SQLString ,StringBuilder strSql ,string idx )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "delete from MIKANV " );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@idx", SqlDbType.Int,4)
            };
            parameters [ 0 ] . Value = idx;
            SQLString . Add ( strSql ,parameters );
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Examine ( LineProductMesEntityu . AssNewWorkEnclosureHeaderEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "UPDATE MIKANT SET ANT006=@ANT006 WHERE ANT001=@ANT001" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@ANT006",SqlDbType.Bit),
                new SqlParameter("@ANT001",SqlDbType.NVarChar,20)
            };
            parameter [ 0 ] . Value = model . ANT006;
            parameter [ 1 ] . Value = model . ANT001;

            return SqlHelper . ExecuteNonQueryResult ( strSql . ToString ( ) ,parameter );
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Cancella ( LineProductMesEntityu . AssNewWorkEnclosureHeaderEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "UPDATE MIKANT SET ANT007=@ANT007 WHERE ANT001=@ANT001" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@ANT007",SqlDbType.Bit),
                new SqlParameter("@ANT001",SqlDbType.NVarChar,20)
            };
            parameter [ 0 ] . Value = model . ANT007;
            parameter [ 1 ] . Value = model . ANT001;

            return SqlHelper . ExecuteNonQueryResult ( strSql . ToString ( ) ,parameter );
        }

        /// <summary>
        /// 获取组装报工单的单号
        /// </summary>
        /// <returns></returns>
        public DataTable getTableOrder ( string orderNum ,string proNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT ANW001,ANW012,ANW009,ANW013,ANW002,ANW003 FROM MIKANW WHERE ANW021=0 AND ANW002!='' AND ANW002='{0}' AND ANW003='{1}'",orderNum ,proNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <returns></returns>
        public DataTable getDepart ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT DAA001,DAA002 FROM TPADAA WHERE DAA001 LIKE '05%' ORDER BY DAA001" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <returns></returns>
        public DataTable getPersonInfo ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT EMP001,EMP002,EMP007,EMP005,DAA002 FROM MIKEMP A INNER JOIN TPADAA B ON A.EMP005=B.DAA001 WHERE EMP003='05' AND EMP034=0 AND EMP037=1" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public LineProductMesEntityu . AssNewWorkEnclosureHeaderEntity getModel ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "select ANT001,ANT002,ANT003,ANT004,ANT005,ANT006,ANT007,ANT008,ANT009,ANT010 from MIKANT " );
            strSql . Append ( " where ANT001=@ANT001" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@ANT001", SqlDbType.NVarChar,20)
            };
            parameters [ 0 ] . Value = oddNum;

            DataTable ds = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) ,parameters );
            if ( ds . Rows . Count > 0 )
            {
                return getModel ( ds . Rows [ 0 ] );
            }
            else
            {
                return null;
            }
        }

        public LineProductMesEntityu . AssNewWorkEnclosureHeaderEntity getModel ( DataRow row )
        {
            LineProductMesEntityu . AssNewWorkEnclosureHeaderEntity model = new LineProductMesEntityu . AssNewWorkEnclosureHeaderEntity ( );
            if ( row != null )
            {
                if ( row [ "ANT001" ] != null )
                {
                    model . ANT001 = row [ "ANT001" ] . ToString ( );
                }
                if ( row [ "ANT002" ] != null )
                {
                    model . ANT002 = row [ "ANT002" ] . ToString ( );
                }
                if ( row [ "ANT003" ] != null )
                {
                    model . ANT003 = row [ "ANT003" ] . ToString ( );
                }
                if ( row [ "ANT004" ] != null )
                {
                    model . ANT004 = row [ "ANT004" ] . ToString ( );
                }
                if ( row [ "ANT005" ] != null )
                {
                    model . ANT005 = row [ "ANT005" ] . ToString ( );
                }
                if ( row [ "ANT006" ] != null && row [ "ANT006" ] . ToString ( ) != "" )
                {
                    if ( ( row [ "ANT006" ] . ToString ( ) == "1" ) || ( row [ "ANT006" ] . ToString ( ) . ToLower ( ) == "true" ) )
                    {
                        model . ANT006 = true;
                    }
                    else
                    {
                        model . ANT006 = false;
                    }
                }
                if ( row [ "ANT007" ] != null && row [ "ANT007" ] . ToString ( ) != "" )
                {
                    if ( ( row [ "ANT007" ] . ToString ( ) == "1" ) || ( row [ "ANT007" ] . ToString ( ) . ToLower ( ) == "true" ) )
                    {
                        model . ANT007 = true;
                    }
                    else
                    {
                        model . ANT007 = false;
                    }
                }
                if ( row [ "ANT008" ] != null && row [ "ANT008" ] . ToString ( ) != "" )
                {
                    model . ANT008 = DateTime . Parse ( row [ "ANT008" ] . ToString ( ) );
                }
                if ( row [ "ANT009" ] != null && row [ "ANT009" ] . ToString ( ) != "" )
                {
                    model . ANT009 = decimal . Parse ( row [ "ANT009" ] . ToString ( ) );
                }
                if ( row [ "ANT010" ] != null && row [ "ANT010" ] . ToString ( ) != "" )
                {
                    model . ANT010 = decimal . Parse ( row [ "ANT010" ] . ToString ( ) );
                }
            }
            return model;
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable getTableColumn ( string strWhere )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT ANT001,ANU003,ANU004,ANU005,ANT006,ANT007,ANT008,ANU010,ANU011 FROM MIKANT A INNER JOIN MIKANU B ON A.ANT001=B.ANU001 WHERE {0}" ,strWhere );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取打印列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTablePrintOne ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT ANT001 ANW001,ANT003 ANW011,ANT005 ANW013,ANT008 ANW022,GETDATE() dat FROM MIKANT WHERE ANT001='{0}'" ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取打印列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTablePrintTwo ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            //strSql . AppendFormat ( "SELECT DISTINCT ANU003 ANW002,ANU004 ANW003,ANU005 ANW004,DEA057 ANW005,DEA003 ANW007,CONVERT(FLOAT,RAA018) ANW006,ANU010 ANW009,DEA008 FROM MIKANU A LEFT JOIN TPADEA B ON A.ANU004=B.DEA001 LEFT JOIN SGMRAA C ON A.ANU003=C.RAA001 WHERE ANU001='{0}'" ,oddNum );
            strSql . AppendFormat ( "SELECT DISTINCT ANU003 ANW002,ANU004 ANW003,ANU005 ANW004,DEA057 ANW005,DEA003 ANW007,ANU011 ANW006,ANU010 ANW009,DDA003 DEA008 FROM MIKANU A LEFT JOIN TPADEA B ON A.ANU004=B.DEA001 INNER JOIN TPADDA D ON B.DEA008=D.DDA001 WHERE ANU001='{0}'" ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取未完工数量
        /// </summary>
        /// <param name="orderNum"></param>
        /// <param name="proNum"></param>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTableOtherSur ( string orderNum ,string proNum,string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT ANU003,ANU004,ANU011-SUM(ANU010) ANU FROM MIKANU WHERE ANU001!='{0}' AND ANU003='{1}' AND ANU004='{2}' GROUP BY ANU003,ANU004,ANU011" ,oddNum ,orderNum ,proNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

    }
}
