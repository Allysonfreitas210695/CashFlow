using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.GetAllExpense;

public interface IGetAllExpenseUseCase
{
   Task<ResponseExpensesJson> Execute();
}
