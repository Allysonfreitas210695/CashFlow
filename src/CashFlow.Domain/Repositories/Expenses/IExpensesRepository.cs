using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;
public interface IExpensesRepository
{
    Task<Expense?> GetById(long id);
    Task<List<Expense>> GetAll();
    Task Add(Expense expense);
}
