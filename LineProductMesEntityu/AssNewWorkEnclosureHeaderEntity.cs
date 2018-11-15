﻿using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace LineProductMesEntityu
{
    public class AssNewWorkEnclosureHeaderEntity
    {
        private int _idx;
        private string _ant001;
        private string _ant002;
        private string _ant003;
        private string _ant004;
        private string _ant005;
        private bool _ant006;
        private bool _ant007;
        private DateTime? _ant008;
        private decimal? _ant009;
        private decimal? _ant010;

        /// <summary>
        /// 
        /// </summary>
        public int idx
        {
            set
            {
                _idx = value;
            }
            get
            {
                return _idx;
            }
        }
        /// <summary>
        /// 单号
        /// </summary>
        public string ANT001
        {
            set
            {
                _ant001 = value;
            }
            get
            {
                return _ant001;
            }
        }
        /// <summary>
        /// 部门编号
        /// </summary>
        public string ANT002
        {
            set
            {
                _ant002 = value;
            }
            get
            {
                return _ant002;
            }
        }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string ANT003
        {
            set
            {
                _ant003 = value;
            }
            get
            {
                return _ant003;
            }
        }
        /// <summary>
        /// 班组编号
        /// </summary>
        public string ANT004
        {
            set
            {
                _ant004 = value;
            }
            get
            {
                return _ant004;
            }
        }
        /// <summary>
        /// 班组名称
        /// </summary>
        public string ANT005
        {
            set
            {
                _ant005 = value;
            }
            get
            {
                return _ant005;
            }
        }
        /// <summary>
        /// 审核
        /// </summary>
        public bool ANT006
        {
            set
            {
                _ant006 = value;
            }
            get
            {
                return _ant006;
            }
        }
        /// <summary>
        /// 注销
        /// </summary>
        public bool ANT007
        {
            set
            {
                _ant007 = value;
            }
            get
            {
                return _ant007;
            }
        }
        /// <summary>
        /// 制单日期
        /// </summary>
        public DateTime? ANT008
        {
            set
            {
                _ant008 = value;
            }
            get
            {
                return _ant008;
            }
        }
        /// <summary>
        /// 午休
        /// </summary>
        public decimal? ANT009
        {
            get
            {
                return _ant009;
            }

            set
            {
                _ant009 = value;
            }
        }
        /// <summary>
        /// 晚休
        /// </summary>
        public decimal? ANT010
        {
            get
            {
                return _ant010;
            }

            set
            {
                _ant010 = value;
            }
        }

    }
}
