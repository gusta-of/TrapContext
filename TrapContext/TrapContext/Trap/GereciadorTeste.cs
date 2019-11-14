using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TrapContext.Processo.Negocio;

namespace TrapContext.Trap
{
    public static class GereciadorTeste
    {
        private static Dictionary<string, ProcessoPadrao> _processosExtendidos = new Dictionary<string, ProcessoPadrao>();

        static GereciadorTeste()
        {
            var file = new FileInfo(@"..\\..\\");
            var path = $"{file.Directory.Parent.FullName}\\Extencoes\\bin\\Debug\\Extencoes.dll";
            var tipos = Assembly.LoadFrom(path).GetTypes();

            var tiposExtendidos = from tipo in tipos
                                  where tipo.IsDefined(typeof(ProcessoExtentdidoAttribute), false)
                                  select tipo;

            //if(!_processosExtendidos.TryGetValue())
        }

        public static bool MetodoEstaExtendido() 
        {
           
            return true;
        }
    }
}
