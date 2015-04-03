using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopWithDp.Interceptors
{
    public class NotificationInterceptor: IInterceptor
    {
        private event PropertyChangedEventHandler PropertyChanged;

        public void Intercept(IInvocation invocation)
        {
            var name = invocation.Method.Name;

            if (name == "add_PropertyChanged")
            {
                PropertyChanged += (PropertyChangedEventHandler)invocation.Arguments[0];
                return;
            }

            if (name == "remove_PropertyChanged")
            {
                PropertyChanged -= (PropertyChangedEventHandler)invocation.Arguments[0];
                return;
            }

            if (name.StartsWith("set_"))
            {
                var propName = name.Substring(4);

                invocation.Proceed();

                // old and new values should be fetched here and comparison made before calling event
                RaisePropertychanged(invocation.Proxy, propName);

                return;
            }

            invocation.Proceed();
        }

        private void RaisePropertychanged(object vm, string property)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler.Invoke(vm, new PropertyChangedEventArgs(property));
        }
    }
}
