using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;

namespace TrapContext.Trap
{
    public class InfoMetodoEstendido
    {
        private readonly MethodInfo _informacaoDoMetodo;
        private readonly bool _utilizaCache;

        public InfoMetodoEstendido(MethodInfo informacaoDoMetodo)
        {
            _informacaoDoMetodo = informacaoDoMetodo;
            var atributoProcessoEstendido = (ProcessoExtendidoAttribute)_informacaoDoMetodo.DeclaringType.GetCustomAttributes(typeof(ProcessoExtendidoAttribute)).First();
            if (atributoProcessoEstendido.UtilizaCache)
            {
                _utilizaCache = true;
            }
        }

        public object InvoqueMetodo(IMethodCallMessage methodCallMessage)
        {
            object instancia = null;
            if (_utilizaCache)
            {
                var chaveContexto = methodCallMessage.MethodBase.ToString();
                instancia = methodCallMessage.LogicalCallContext.GetData(chaveContexto);
                if (instancia == null)
                {
                    instancia = CrieInstancia();
                    methodCallMessage.LogicalCallContext.SetData(chaveContexto, instancia);
                }
            }
            else
            {
                instancia = CrieInstancia();
            }

            return _informacaoDoMetodo.Invoke(instancia, methodCallMessage.Args);
        }

        private object CrieInstancia()
        {
            return Activator.CreateInstance(_informacaoDoMetodo.DeclaringType);
        }
    }
}