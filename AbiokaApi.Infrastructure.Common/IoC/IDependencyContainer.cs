using System;

namespace AbiokaApi.Infrastructure.Common.IoC
{
    public interface IDependencyContainer
    {
        T Resolve<T>();
        object Resolve(Type type);
        void Release(object instance);
        IDependencyContainer Register<T>(string name);
        IDependencyContainer RegisterWithAllInterfaces<T>();
        IDependencyContainer RegisterWithAllInterfaces(Type type);
        IDependencyContainer UsingFactoryMethod<T>(Func<T> func);
        IDependencyContainer Register<T1, T2>();
        IDependencyContainer RegisterPerWebRequest<T1, T2>();
        void Dispose();
    }
}
