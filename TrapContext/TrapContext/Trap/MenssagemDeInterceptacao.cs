using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using TrapContext.Processo.Negocio;

namespace TrapContext.Trap
{
    public class MensagemDeInterceptacao : IMessageSink
    {
        private readonly IMessageSink _mNext;

        public MensagemDeInterceptacao(IMessageSink ims)
        {
            _mNext = ims;
        }

        public IMessageSink NextSink => _mNext;

        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            return _mNext.AsyncProcessMessage(msg, replySink);
        }

        public IMessage SyncProcessMessage(IMessage imCall)
        {
            IMethodCallMessage chamadaDeMetodoMessage = (IMethodCallMessage)imCall;
            var teste = chamadaDeMetodoMessage.MethodBase.ReflectedType.Name;

            if (GereciadorTeste.MetodoEstaExtendido())
            {
                return _mNext.SyncProcessMessage(imCall);
            }
            var obj = Activator.CreateInstance(typeof(ProcessoPadrao));
            return new RetornoMetodo(chamadaDeMetodoMessage, obj);
        }
    }
}
