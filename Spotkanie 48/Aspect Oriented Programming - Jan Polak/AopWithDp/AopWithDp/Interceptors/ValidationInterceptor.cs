using Castle.DynamicProxy;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopWithDp.Interceptors
{
    public class ValidationInterceptor<T>: IInterceptor
    {
        IValidator<T> _validator;

        public ValidationInterceptor(IValidator<T> validator)
        {
            _validator = validator;
        }


        public void Intercept(IInvocation invocation)
        {
            if (invocation.Method.Name == "get_Item")
            {
                // just ignore type safety, obviously this shouldn't not crash in BAU scenario
                invocation.ReturnValue = Validate((T)invocation.Proxy, (string)invocation.Arguments[0]);
                return;
            }

            if (invocation.Method.Name == "get_Error")
            {
                invocation.ReturnValue = Validate((T)invocation.Proxy, null);
                return;
            }

            invocation.Proceed();
        }

        
        private string Validate(T model, string property)
        {
            var result = property != null 
                ? _validator.Validate(model, property) 
                : _validator.Validate(model);
            if (result.IsValid)
                return null;
            return string.Join(" \n", result.Errors.Select(e => e.ErrorMessage));
        }
    }
}
