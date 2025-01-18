namespace CashFlow.Communication.Responses;
public class ResponseShortExpenseJson
{
    public string Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}
