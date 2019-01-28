using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;
using StudentMgr;
using System . Data;

namespace LineProductMesBll . Dao
{
    public class SalaryByEveryOneDao
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataTable getTableView ( string dt )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT ANX001,ANX011,ANX002,ANX003,ANX004,ANX005,ANX006,ANX007,ANX008,ANX017 FROM MIKANX WHERE ANX001 LIKE '%{0}%'" ,dt );
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT ANV001,ANV007,ANV002,ANV003,ANV004,ANV005,ANV006,ANV013,ANV014,ANV010 FROM MIKANV WHERE ANV001 LIKE '%{0}%'" ,dt );
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT HAX001,HAX015,HAX002,HAX003,HAX004,HAX009,HAX010,HAX011,HAX012,HAX020 FROM MIKHAX WHERE HAX001 LIKE '%{0}%'" ,dt );
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT IJB001,IJB003,IJB002,IJB012,IJB013,IJB016,IJB017,IJB018,IJB019,IJB025 FROM MIKIJB WHERE IJB001 LIKE '%{0}%'" ,dt );
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT IJD001,IJD010,IJD002,IJD003,IJD004,NULL,NULL,IJD006,IJD007,IJD013 FROM MIKIJD WHERE IJD001 LIKE '%{0}%'" ,dt );
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT LEG001,LEG012,LEG002,LEG003,LEG004,LEG005,LEG006,LEG008,LEG009,LEG016 FROM MIKLEG WHERE LEG001 LIKE '%{0}%'" ,dt );
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT LGP001,LGP005,LGP002,LGP003,LGP004,LGP007,LGP008,LGP009,LGP010,LGP013 FROM MIKLGP WHERE LGP001 LIKE '%{0}%'" ,dt );
            strSql . Append ( " UNION ALL " );
            strSql . AppendFormat ( "SELECT PAP001,PPA010,PPA002,PPA003,PPA004,PPA005,PPA006,PPA007,PPA008,PPA014 FROM MIKPAP WHERE PAP001 LIKE '%{0}%'" ,dt );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

    }
}
