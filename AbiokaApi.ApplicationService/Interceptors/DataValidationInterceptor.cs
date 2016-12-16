using AbiokaApi.ApplicationService.Validation;
using AbiokaApi.Infrastructure.Common.Exceptions;
using AbiokaApi.Infrastructure.Common.Helper;
using AbiokaApi.Infrastructure.Common.IoC;

namespace AbiokaApi.ApplicationService.Interceptors
{
    internal class DataValidationInterceptor : IServiceInterceptor
    {
        private readonly ICurrentContext currentContext;

        public DataValidationInterceptor(ICurrentContext currentContext) {
            this.currentContext = currentContext;
        }

        public int Order => 1;

        public void BeforeProceed(IInvocationContext context) {
            foreach (var item in context.Arguments) {
                if (item == null)
                    continue;

                var itemType = item.GetType();
                if (itemType.IsPrimitive || itemType.IsArray)
                    continue;

                var type = typeof(ICustomValidator<>).MakeGenericType(item.GetType());
                var validator = (ICustomValidator)DependencyContainer.Container.Resolve(type);
                if (validator != null) {
                    var result = validator.Validate(item, currentContext.Current.ActionType);
                    if (!result.IsValid) {
                        foreach (var errorItem in result.Errors) {
                            throw new DenialException(errorItem.ErrorMessage, errorItem.PropertyName);
                            // TODO: throw exception for all error messages
                        }
                    }
                }
            }
        }
    }
}
