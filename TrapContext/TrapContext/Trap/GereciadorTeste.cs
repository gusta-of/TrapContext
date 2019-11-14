using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using TrapContext.Processo.Negocio;

namespace TrapContext.Trap
{
    public class GereciadorTeste
    {
        private static Dictionary<int, Type> _processosExtendidos = new Dictionary<int, Type>();

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
                                  where tipo.IsDefined(typeof(ProcessoExtendidoAttribute), false)
                                  select tipo;

            foreach (var tipo in tiposExtendidos)
            {
                var atributosProcessoExtendido = tipo.GetCustomAttributes(typeof(ProcessoExtendidoAttribute), false).Cast<ProcessoExtendidoAttribute>();
                foreach (var attribute in atributosProcessoExtendido)
                {

                    if (_processosExtendidos.TryGetValue(attribute.Key, out var processo))
                    {
                        continue;
                    }
                    _processosExtendidos.Add(attribute.Key, tipo);
                }
            }
        }

        public static bool MetodoEstaExtendido()
        {
            return _processosExtendidos.ContainsKey(10);
        }

        public static IMessage CrieExtencaoERetorne(IMethodCallMessage methodSCallMessage)
        {
            var type = _processosExtendidos[10];
            return new ProcessoExtendido(type).CrieExtencaoERetorne(methodSCallMessage);
        }
    }
}
