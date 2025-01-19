
using System.Net;

namespace CashFlow.Exception.Exceptions;
public class NotFoundException : CashFlowException
{
    private string ErrorMessage { get; set; }
    public NotFoundException(string message) : base(string.Empty)
    {
        ErrorMessage = message;
    }

    public override List<string> GetErrors => [ErrorMessage];

    public override int GetStatusError() => (int)HttpStatusCode.NotFound;
}
