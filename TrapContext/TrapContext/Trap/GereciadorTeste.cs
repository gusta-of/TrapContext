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
    public static class GereciadorTeste
    {
        private static Dictionary<int, dynamic> _processosExtendidos = new Dictionary<int, dynamic>();

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
                    _processosExtendidos.Add(attribute.Key, (ProcessoPadrao)Activator.CreateInstance(tipo));
                }
            }
        }

        public static bool MetodoEstaExtendido()
        {
            return _processosExtendidos[10] != null;
        }

        public static object InvoqueMetodo(IMethodCallMessage methodCallMessage)
        {
            var instancia = _processosExtendidos[10];
            var type = (Type)instancia.GetType();
            var methodInfo = type.GetMethod("Executa");
            return methodInfo.Invoke(instancia, methodCallMessage.Args);
        }
    }
}
