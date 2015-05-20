using Services.ExceptionsAndErrors;

namespace Services.Implementations
{
    public abstract class BaseService
    {
        #region Check methods
        protected static void IsEntityExist<T>(object entity, T entityId, string entityName)
        {
            if (entity != null) return;
            var errorMessage = string.Format(entityName + " with id = {0} not found", entityId);
            throw new ActionArgumentException(errorMessage) { ErrorType = DataCheckingErrors.EntityNotFound };
        }
        protected static void IsDtoNotNull(object dto)
        {
            if (dto != null) return;
            throw new ActionArgumentException("Empty request body") { ErrorType = DataCheckingErrors.EmptyRequestBody };
        }
        #endregion // Check methods
    }
}