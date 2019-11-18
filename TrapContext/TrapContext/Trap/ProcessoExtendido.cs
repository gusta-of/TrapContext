using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace TrapContext.Trap
{
    public class ProcessoExtendido
    {
        private Dictionary<string, MetodoExtendido> _metodosExtendidos = new Dictionary<string, MetodoExtendido>();
        public void AddProcesso(Type type)
        {
            MetodoExtendido metodosExtendidosDoProcesso;

            if (!_metodosExtendidos.TryGetValue(type.Name, out metodosExtendidosDoProcesso))
            {
                metodosExtendidosDoProcesso = new MetodoExtendido(type);
                _metodosExtendidos.Add(type.Name, metodosExtendidosDoProcesso);
            }

            foreach (MethodInfo informacaoMetodo in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                var teste = informacaoMetodo.Name;
                metodosExtendidosDoProcesso.AddMetodo(informacaoMetodo);
            }
        }

        public bool MetodoEstaExtendido(IMethodCallMessage methodCallMessage)
        {
            MetodoExtendido metodosExtendidosDoProcesso;

            if (_metodosExtendidos.TryGetValue(methodCallMessage.MethodBase.ReflectedType.Name, out metodosExtendidosDoProcesso))
            {
                return metodosExtendidosDoProcesso.MetodoEstaExtendido(methodCallMessage);
            }
            return false;
        }

        public IMessage CrieExtencaoERetorne(IMethodCallMessage methodCallMessage)
        {
            var processoExtendido = _metodosExtendidos[methodCallMessage.MethodBase.ReflectedType.Name];
            return processoExtendido.ExecuteMetodoEObtenhaRetorno(methodCallMessage);
        }
    }
}
