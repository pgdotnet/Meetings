using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopWithDp.Interceptors
{
    public class ProxyGenerationHook: IProxyGenerationHook
    {
        public void MethodsInspected()
        {
            // do nothing
        }

        public void NonProxyableMemberNotification(Type type, System.Reflection.MemberInfo memberInfo)
        {
            // do nothing
        }

        /// <summary>
        /// we want to intercept in two cases:
        /// * property setter was called (required by undo and notify property changed)
        /// * call to additional iface was made
        /// </summary>
        /// <param name="type"></param>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public bool ShouldInterceptMethod(Type type, System.Reflection.MethodInfo methodInfo)
        {
            if (type == typeof(INotifyPropertyChanged))
                return true;
            if (type == typeof(IDataErrorInfo))
                return true;
            if (type == typeof(ISupportsUndo))
                return true;
            if (methodInfo.Name.StartsWith("set_"))
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return typeof(ProxyGenerationHook).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj != null && typeof(ProxyGenerationHook) == obj.GetType();
        }
    }
}
