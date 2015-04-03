using Castle.DynamicProxy;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AopWithDp.Interceptors
{
    /// <summary>
    /// wraps model using DynamicProxy
    /// </summary>
    public class VmFactory
    {
        /// <summary>
        /// additional ifaces we'd like to add to base model type
        /// </summary>
        private static readonly Type[] ExtendedTypes = new[] 
            {
                typeof (INotifyPropertyChanged),
                typeof (IDataErrorInfo),
                typeof (ISupportsUndo)
            };


        private readonly ProxyGenerator _generator;
        private readonly ProxyGenerationOptions _generatorOptions;
        
        public VmFactory()
        {
            _generatorOptions = new ProxyGenerationOptions(new ProxyGenerationHook());
            _generator = new ProxyGenerator();
        }


        public IFormModel CreateViewModel(IFormModel model, IValidator<IFormModel> validator)
        {
            var interceptors = new IInterceptor[] 
            {
                new ValidationInterceptor<IFormModel>(validator),
                new UndoInterceptor(),
                new NotificationInterceptor()
            };

            return _generator.CreateInterfaceProxyWithTarget(typeof(IFormModel), 
                ExtendedTypes, 
                model, 
                _generatorOptions, 
                interceptors) as IFormModel;

        }
    }
}
