namespace Services.Results
{
    public enum ErrorType { Ok, OkNoContent, EntityNotFound, ImpossibleOperation }
    
    public class ServiceResult<T>
    {
        public ErrorType ErrorType { get; set; }
        public string ErrorMessage { get; set; }
        public T DtoEntity { get; set; }

        #region Constructors
        public ServiceResult() { }
        public ServiceResult(T dtoEntity, ErrorType errorType = ErrorType.Ok, string errorMessage = null)
        {
            ErrorType = errorType;
            ErrorMessage = errorMessage;
            DtoEntity = dtoEntity;
        }
        #endregion
    }
}