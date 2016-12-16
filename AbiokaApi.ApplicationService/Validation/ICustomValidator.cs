using AbiokaApi.Infrastructure.Common.Helper;
using FluentValidation;
using FluentValidation.Results;

namespace AbiokaApi.ApplicationService.Validation
{
    public interface ICustomValidator
    {
        ValidationResult Validate(object instance, ActionType actionType);
    }

    public interface ICustomValidator<in T>
    {
        ValidationResult Validate(T instance, ActionType actionType);
    }
}
