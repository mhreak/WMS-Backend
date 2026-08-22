namespace WMS.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public string? ErrorCode { get; }

    public NotFoundException(string messageKey)
        : base(messageKey)
    {
    }

    public NotFoundException(string messageKey, string errorCode)
        : base(messageKey)
    {
        ErrorCode = errorCode;
    }
}