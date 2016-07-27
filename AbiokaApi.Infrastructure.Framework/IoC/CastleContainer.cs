using AbiokaApi.Infrastructure.Common.IoC;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;

namespace AbiokaApi.Infrastructure.Framework.IoC
{
    public class CastleContainer : IDependencyContainer
    {
        public CastleContainer() {
            container = new WindsorContainer();
        }

        /// <summary>
        /// Resolve the target type with necessary dependencies.
        /// </summary>
        public object Resolve(Type targetType) {
            if (container.Kernel.HasComponent(targetType))
            {
                return container.Resolve(targetType);
            }
            return null;
        }

        /// <summary>
        /// Resolves all registered instances for a specific service type.
        /// </summary>
        public IList<object> ResolveAll(Type serviceType) {
            if (container.Kernel.HasComponent(serviceType))
            {
                return new List<object>((object[])container.ResolveAll(serviceType));
            }
            return null;
        }

        public readonly IWindsorContainer container;

        public T Resolve<T>() {
            return container.Resolve<T>();
        }

        public IDependencyContainer Register<T>(string name) {
            container.Register(Component.For(typeof(T)).Named(name).LifeStyle.Transient);
            return this;
        }

        public IDependencyContainer RegisterWithAllInterfaces<T>() {
            return RegisterWithAllInterfaces(typeof(T));
        }

        public IDependencyContainer RegisterWithAllInterfaces(Type type) {
            container.Register(Classes.FromThisAssembly().BasedOn(type).WithServiceAllInterfaces().Configure(c => c.LifestyleSingleton()));
            return this;
        }

        public IDependencyContainer Register<T1, T2>() {
            container.Register(Component.For(typeof(T1)).ImplementedBy(typeof(T2)).LifeStyle.Singleton);
            return this;
        }

        public IDependencyContainer RegisterPerWebRequest<T1, T2>() {
            container.Register(Component.For(typeof(T1)).ImplementedBy(typeof(T2)).LifeStyle.PerWebRequest);
            return this;
        }

        public void Release(object instance) {
            container.Release(instance);
        }

        public void Dispose() {
            container.Dispose();
        }

        public IDependencyContainer UsingFactoryMethod<T>(Func<T> func) {
            container.Register(Component.For(typeof(T)).UsingFactoryMethod(func).LifeStyle.Singleton);
            return this;
        }
    }
}
