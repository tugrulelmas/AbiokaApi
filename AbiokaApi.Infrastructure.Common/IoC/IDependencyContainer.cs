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
        IDependencyContainer RegisterWithFromInterface<T>();
        IDependencyContainer RegisterWithFromInterface(Type type);
        IDependencyContainer RegisterWithDefaultInterfaces<T1, T2>();
        IDependencyContainer RegisterWithDefaultInterfaces(Type type1, Type type2);
        IDependencyContainer UsingFactoryMethod<T>(Func<T> func);
        IDependencyContainer Register<T1, T2>(LifeStyle lifeStyle = LifeStyle.Singleton);
    }
}
