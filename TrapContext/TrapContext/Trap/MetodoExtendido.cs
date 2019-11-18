using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace TrapContext.Trap
{
    public class MetodoExtendido
    {
        private readonly Dictionary<string, InfoMetodoEstendido> _metodosExtendidos = new Dictionary<string, InfoMetodoEstendido>();
        private readonly Type _tipoProcesso;

        public MetodoExtendido(Type tipoExtendido)
        {
            _tipoProcesso = tipoExtendido;
        }

        public bool MetodoEstaExtendido(IMethodCallMessage methodCallMessage)
        {
            return _metodosExtendidos.ContainsKey(methodCallMessage.MethodBase.Name);
        }

        public void AddMetodo(MethodInfo methodInfo)
        {
            if (_tipoProcesso != methodInfo.ReflectedType)
            {
                throw new ArgumentException("methodInfo");
            }

            _metodosExtendidos.Add(methodInfo.Name, new InfoMetodoEstendido(methodInfo));
        }

        public IMethodReturnMessage ExecuteMetodoEObtenhaRetorno(IMethodCallMessage methodCallMessage)
        {
            var informacaoDeMetodo = _metodosExtendidos[methodCallMessage.MethodBase.Name];
            var valorRetorno = informacaoDeMetodo.InvoqueMetodo(methodCallMessage);
            return new RetornoMetodo(methodCallMessage, valorRetorno);
        }
    }
}
