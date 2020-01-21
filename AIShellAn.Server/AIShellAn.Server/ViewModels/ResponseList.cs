using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIShellAn.Server.ViewModels
{
    public class ListModel<T>
    {
        public List<T> list { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int count { get; set; }
        public int page { get; set; }
        public int size { get; set; }
    }
}
