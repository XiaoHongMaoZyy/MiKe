﻿using System . Text;
using StudentMgr;
using System . Data;
using System;

namespace LineProductMesBll . Dao
{
    public class AssBoardDao
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable getTableView ( )
        {
            SqlHelper . StoreProcedure ( "CNSZZ" );
            return SqlHelper . ExecuteNoStoreTable ( null );
            //StringBuilder strSql = new StringBuilder ( );
            //strSql . Append ( "WITH CET AS(" );
            //strSql . Append ( "SELECT A.PRG004,PRG005,A.PRG001,DEA002,DEA057,SUM(PRF003) PRF003,SUM(A.PRG003) PRG003,ISNULL(D.ANW009,0) ANW009,SUM(E.PRG003) PRG3 FROM MIKPRG A INNER JOIN MIKPRF B ON A.PRG001=B.PRF001 AND A.PRG002=B.PRF002 INNER JOIN TPADEA C ON A.PRG001=C.DEA001 LEFT JOIN (SELECT ANW003,ANW012,SUM(ISNULL(ANW009,0)) ANW009 FROM MIKANW WHERE CONVERT(DATE,ANW015,112)=CONVERT(DATE,GETDATE(),112) GROUP BY ANW003,ANW012) D ON A.PRG001=D.ANW003 AND A.PRG004=D.ANW012 INNER JOIN (SELECT PRG003,PRG001,PRG004 FROM MIKPRG WHERE PRG002=(SELECT MIN(PRG002) PRG002 FROM MIKPRG WHERE PRG002>GETDATE())) E ON A.PRG001=E.PRG001 AND A.PRG004=E.PRG004 WHERE A.PRG002=CONVERT(DATE,GETDATE(),112) GROUP BY A.PRG001,PRG005,DEA002,DEA057,D.ANW009,A.PRG004) " );
            //strSql . Append ( ",CFT AS (" );
            //strSql . Append ( "SELECT PRG005,PRG001,DEA002,DEA057,PRF003,PRG003,A.ANW009,SUM(ISNULL(B.ANW009,0)) ANW09,PRG3 FROM CET A LEFT JOIN MIKANW B ON A.PRG001=B.ANW003 AND A.PRG004=B.ANW012 GROUP BY PRG005,PRG001,DEA002,DEA057,PRF003,PRG003,A.ANW009,PRG3) " );
            //strSql . Append ( "SELECT PRG005,PRG001,DEA002,DEA057,PRF003,PRG003,ANW009 ANW9,CASE WHEN PRG003=0 THEN 0 ELSE ANW009*1.0/PRG003*100 END ANW009,CASE WHEN PRF003=0 THEN 0 ELSE ANW09*1.0/PRF003*100 END ANW09,PRF003-ANW09 PRF,PRG3 FROM CFT" );

            //return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }



    }
}