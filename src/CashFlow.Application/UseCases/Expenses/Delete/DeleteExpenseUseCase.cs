
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.Exceptions;

namespace CashFlow.Application.UseCases.Expenses.Delete;
public class DeleteExpenseUseCase : IDeleteExpenseUseCase
{
    private readonly IExpensesDeleteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteExpenseUseCase(IExpensesDeleteOnlyRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(long id)
    {
        var result =  await _repository.Delete(id);

        if (!result)
        {
            throw new NotFoundException(ResourcesErrorsMessages.EXPENSE_NOT_FOUND);
        }

        await _unitOfWork.Commit();
    }
}
