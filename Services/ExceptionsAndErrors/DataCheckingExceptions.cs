using System;

namespace Services.ExceptionsAndErrors
{
    public class ActionArgumentException : ArgumentException
    {
        public DataCheckingErrors ErrorType { get; set; }
        
        public ActionArgumentException() { }
        public ActionArgumentException(string errorMessage) : base(errorMessage) { }
    }

    public class SameThemeExistsException : Exception
    {
        public SameThemeExistsException() { }
        public SameThemeExistsException(string errorMessage) : base(errorMessage) { }
    }

    public class AccessDeniedException : Exception
    {
        public AccessDeniedException() { }
        public AccessDeniedException(string errorMessage) : base(errorMessage) { }
    }

    public class MessageQuotedException : Exception
    {
        public MessageQuotedException() { }
        public MessageQuotedException(string errorMessage) : base(errorMessage) { }
    }
}