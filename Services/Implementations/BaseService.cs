using Services.ExceptionsAndErrors;

namespace Services.Implementations
{
    public abstract class BaseService
    {
        // Common check methods
        protected static void IsEntityExist(object entity, int entityId, string entityName)
        {
            if (entity != null) return;
            var errorMessage = string.Format(entityName + " with id = {0} not found", entityId);
            throw new ActionArgumentException(errorMessage) { ErrorType = DataCheckingErrors.EntityNotFound };
        }
    }
}