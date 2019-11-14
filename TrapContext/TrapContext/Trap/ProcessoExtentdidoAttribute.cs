using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrapContext.Trap
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ProcessoExtendidoAttribute : Attribute
    {
        public int Key { get; set; }
        public bool UtilizaCache { get; set; }
        public ProcessoExtendidoAttribute(int key)
            : this(key, false)
        {
        }

        public ProcessoExtendidoAttribute(int key, bool utilizaCache)
        {
            Key = key;
           UtilizaCache = utilizaCache;
        }
    }
}
