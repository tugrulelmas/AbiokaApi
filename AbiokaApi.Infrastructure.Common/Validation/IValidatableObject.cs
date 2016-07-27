namespace AbiokaApi.Infrastructure.Common.Validation
{
    public interface IValidatableObject
    {
        /// <summary>
        /// Validates the specified action type.
        /// </summary>
        /// <param name="actionType">Type of the action.</param>
        /// <returns></returns>
        ValidationResult Validate(ActionType actionType);
    }
}
