using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;
public interface IExpensesDeleteOnlyRepository
{
    Task<bool> Delete(long id);
}
