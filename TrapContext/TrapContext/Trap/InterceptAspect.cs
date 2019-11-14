using System;
using System.Runtime.Remoting.Contexts;

namespace TrapContext.Trap
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InterceptAspect : Attribute, IContextAttribute
    {
        public void GetPropertiesForNewContext(System.Runtime.Remoting.Activation.IConstructionCallMessage msg)
        {
            msg.ContextProperties.Add(new TrapContext());
        }

        public bool IsContextOK(Context ctx, System.Runtime.Remoting.Activation.IConstructionCallMessage msg)
        {
            if (ctx.GetProperty("TrapContext") == null)
            {
                return false;
            }

            return true;
        }
    }
}
