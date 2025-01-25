using System.Net;

namespace CashFlow.Exception.Exceptions;
public class InvalidLoginException : CashFlowException
{
    public InvalidLoginException() : base(ResourcesErrorsMessages.EMAIL_OR_PASSWORD_INVALID)
    {

    }
    public override List<string> GetErrors => [Message];

    public override int GetStatusError() => (int)HttpStatusCode.Unauthorized;
}
