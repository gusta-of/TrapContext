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
        private static Dictionary<int, ProcessoExtendido> _processosExtendidos;

        static GereciadorTeste()
        {
            lock (new object())
            {
                if(_processosExtendidos != null)
                {
                    return;
                }

                _processosExtendidos = new Dictionary<int, ProcessoExtendido>();

                var file = new FileInfo(@"..\\..\\..\\");
                var path = String.Empty;
#if DEBUG
                path = $"{file.Directory.Parent.FullName}\\Binarios\\Extencoes.dll";
#else
            path = $"{file.Directory.Parent.FullName}\\Binarios\\Extencoes.dll";
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
                        if (!_processosExtendidos.TryGetValue(attribute.Key, out var processo))
                        {
                            processo = new ProcessoExtendido();
                            _processosExtendidos.Add(attribute.Key, processo);

                        }

                        processo.AddProcesso(tipo);
                    }
                }
            }
        }

        public static bool MetodoEstaExtendido(IMethodCallMessage methodSCallMessage)
        {
            return _processosExtendidos.TryGetValue(10, out var processoExtendido) && 
                processoExtendido.MetodoEstaExtendido(methodSCallMessage);
        }

        public static IMessage CrieExtencaoERetorne(IMethodCallMessage methodSCallMessage)
        {
            var processoExtendido = _processosExtendidos[10];
            return processoExtendido.CrieExtencaoERetorne(methodSCallMessage);
        }
    }
}
