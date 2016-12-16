using AbiokaApi.Infrastructure.Common.Helper;
using FluentValidation;
using FluentValidation.Results;

namespace AbiokaApi.ApplicationService.Validation
{
    public abstract class CustomValidator<T> : AbstractValidator<T>, ICustomValidator<T>, ICustomValidator
    {
        public ValidationResult Validate(object instance, ActionType actionType) => Validate((T)instance, actionType);

        public ValidationResult Validate(T instance, ActionType actionType) {
            var result = base.Validate(instance);
            if (!result.IsValid)
                return result;

            DataValidate(instance, actionType);
            return result;
        }

        protected virtual void DataValidate(T instance, ActionType actionType) {

        }
    }
}