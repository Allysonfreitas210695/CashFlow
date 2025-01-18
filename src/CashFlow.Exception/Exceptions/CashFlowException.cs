using System.Net;

namespace CashFlow.Exception.Exceptions;

public abstract class CashFlowException : SystemException
{
    protected CashFlowException(string message) : base(message)
    {
        
    }

    public abstract List<string> GetErrors { get; }
    public abstract HttpStatusCode GetStatusError();
}