namespace WMS.Application.Common.Exceptions;

public class BadRequestException : Exception
{
    public string? ErrorCode { get; }
    public object[] Args { get; }

    public BadRequestException(string messageKey)
        : base(messageKey)
    {
        Args = Array.Empty<object>();
    }

    public BadRequestException(string messageKey, string errorCode, params object[] args)
        : base(messageKey)
    {
        ErrorCode = errorCode;
        Args = args ?? Array.Empty<object>();
    }
}