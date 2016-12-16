using System;
using System.Reflection;

namespace AbiokaApi.Infrastructure.Common.IoC
{
    public interface IInvocationContext
    {
        object[] Arguments { get; }

        Type[] GenericArguments { get; }

        object InvocationTarget { get; }

        MethodInfo Method { get; }

        MethodInfo MethodInvocationTarget { get; }

        object Proxy { get; }

        object ReturnValue { get; set; }

        Type TargetType { get; }
    }

}
