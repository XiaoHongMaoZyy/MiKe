using System;
using System . Collections . Generic;
using System . Data;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesBll . Bll
{
    public class SalaryByEveryOneBll
    {
        private readonly Dao.SalaryByEveryOneDao dal;

        public SalaryByEveryOneBll ( )
        {
            dal = new Dao . SalaryByEveryOneDao ( );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataTable getTableView ( string dt )
        {
            return dal . getTableView ( dt );
        }


    }
}
