using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;
public interface IExpensesDeleteOnlyRepository
{
    Task Delete(long id);
}
