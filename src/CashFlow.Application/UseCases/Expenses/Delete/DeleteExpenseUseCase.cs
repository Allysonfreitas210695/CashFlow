
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.Exceptions;

namespace CashFlow.Application.UseCases.Expenses.Delete;
public class DeleteExpenseUseCase : IDeleteExpenseUseCase
{
    private readonly IExpensesReadOnlyRepository _expensesReadOnlyRepository;
    private readonly IExpensesDeleteOnlyRepository _expensesDeleteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteExpenseUseCase(
        IExpensesReadOnlyRepository expensesReadOnlyRepository, 
        IExpensesDeleteOnlyRepository expensesDeleteOnlyRepository,
        IUnitOfWork unitOfWork)
    {
        _expensesReadOnlyRepository = expensesReadOnlyRepository;
        _expensesDeleteOnlyRepository = expensesDeleteOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(long id)
    {
        var result =  await _expensesDeleteOnlyRepository.Delete(id);

        if (!result)
        {
            throw new NotFoundException(ResourcesErrorsMessages.EXPENSE_NOT_FOUND);
        }

        await _unitOfWork.Commit();
    }
}
