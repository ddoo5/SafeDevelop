using SD.Models.Responses.Failures;

namespace SD
{
    public interface IValidationService<TEntity> where TEntity : class
    {
        IReadOnlyList<IOperationFailure> ValidateEntity(TEntity item);
    }
}