using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace TrapContext.Trap
{
    public class ProcessoExtendido
    {
        public Type TypeExtendido { get; private set; }
        public ProcessoExtendido(Type type)
        {
            TypeExtendido = type;
        }

        public IMessage CrieExtencaoERetorne(IMethodCallMessage methodCallMessage)
        {
            object instancia = CrieInstancia(TypeExtendido);
            var methodInfo = TypeExtendido.GetMethod("Executa");
           return new RetornoMetodo(methodCallMessage, methodInfo.Invoke(instancia, methodCallMessage.Args));
        }

        private object CrieInstancia(Type type) => Activator.CreateInstance(type);
    }
}
