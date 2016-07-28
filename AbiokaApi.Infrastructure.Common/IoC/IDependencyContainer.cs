using System;
using System.Collections.Generic;

namespace AbiokaApi.Infrastructure.Common.IoC
{
    public interface IDependencyContainer : IDisposable
    {
        T Resolve<T>();
        IEnumerable<T> ResolveAll<T>();
        object Resolve(Type type);
        void Release(object instance);
        IDependencyContainer Register<T>(LifeStyle lifeStyle);
        IDependencyContainer Register(Type type, LifeStyle lifeStyle);
        IDependencyContainer RegisterWithAllInterfaces<T>();
        IDependencyContainer RegisterWithAllInterfaces(Type type);
        IDependencyContainer UsingFactoryMethod<T>(Func<T> func);
        IDependencyContainer Register<T1, T2>(LifeStyle lifeStyle = LifeStyle.Singleton);
    }
}
