using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

namespace TrapContext.Trap
{
    public class TrapContext : IContextProperty, IContributeObjectSink
    {
        internal static string PropName
        {
            get
            {
                return "TrapContext";
            }
        }

        public string Name
        {
            get { return (PropName); }
        }


        public void Freeze(Context newContext)
        {
        }

        public bool IsNewContextOK(Context newCtx)
        {
            return true;
        }

        public IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink)
        {
            return new MensagemDeInterceptacao(nextSink);
        }
    }
}
