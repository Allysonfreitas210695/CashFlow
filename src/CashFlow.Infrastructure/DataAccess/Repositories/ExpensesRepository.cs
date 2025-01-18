using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;
internal class ExpensesRepository : IExpensesWriteOnlyRepository, IExpensesReadOnlyRepository, IExpensesDeleteOnlyRepository, IExpensesUpdateOnlyRepository
{
    private readonly CashFlowDbContext _dbContext;
    public ExpensesRepository(CashFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Expense expense)
    {
        await _dbContext.Expenses.AddAsync(expense);
    }

    public async Task<bool> Delete(long id)
    {
        var result = await _dbContext.Expenses.FirstOrDefaultAsync(x => x.Id == id);
        if (result == null) return false;

        _dbContext.Expenses.Remove(result);

        return true;
    }

    public async Task<List<Expense>> GetAll()
    {
        return await _dbContext.Expenses.AsNoTracking().ToListAsync();
    }

    async Task<Expense?> IExpensesReadOnlyRepository.GetById(long id)
    {
        return await _dbContext.Expenses.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
    }

    async Task<Expense?> IExpensesUpdateOnlyRepository.GetById(long id)
    {
        return await _dbContext.Expenses.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public void Update(Expense expense)
    {
        _dbContext.Expenses.Update(expense);
    }

    
}
