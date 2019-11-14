using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace TrapContext.Trap
{
    public class RetornoMetodo : ReturnMessage
    {
        private object _valorRetorno;
        public RetornoMetodo(Exception excecao, IMethodCallMessage mensagemCall)
            : base(excecao, mensagemCall)
        {

        }

        public RetornoMetodo(IMethodCallMessage mensagemCall, object valorRetorno)
            : base(null, new object[] { }, 0, mensagemCall.LogicalCallContext, mensagemCall)
        {
            _valorRetorno = valorRetorno;
        }

        public override object ReturnValue => _valorRetorno;
    }
}
