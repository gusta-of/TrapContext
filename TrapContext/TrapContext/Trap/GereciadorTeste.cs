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
        private static Dictionary<string, dynamic> _processosExtendidos = new Dictionary<string, dynamic>();

        static GereciadorTeste()
        {
            var file = new FileInfo(@"..\\..\\");
            var path = String.Empty;
#if DEBUG
            path = $"{file.Directory.Parent.FullName}\\Extencoes\\bin\\Debug\\Extencoes.dll";
#else
            path = $"{file.Directory.Parent.FullName}\\Extencoes\\bin\\Release\\Extencoes.dll";
#endif

            var tipos = Assembly.LoadFrom(path).GetTypes();

            var tiposExtendidos = from tipo in tipos
                                  where tipo.IsDefined(typeof(ProcessoExtentdidoAttribute), false)
                                  select tipo;

            foreach (var tipo in tiposExtendidos)
            {
                if (_processosExtendidos.TryGetValue(tipo.Name, out var processo))
                {
                    continue;
                }

                _processosExtendidos.Add(tipo.Name, Activator.CreateInstance(tipo));
            }
        }

        public static bool MetodoEstaExtendido()
        {

            return true;
        }
    }
}
