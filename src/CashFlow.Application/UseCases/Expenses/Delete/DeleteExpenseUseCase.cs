
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.Exceptions;

namespace CashFlow.Application.UseCases.Expenses.Delete;
public class DeleteExpenseUseCase : IDeleteExpenseUseCase
{
    private readonly IExpensesReadOnlyRepository _expensesReadOnly;
    private readonly IExpensesDeleteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public DeleteExpenseUseCase(
        IExpensesReadOnlyRepository expensesReadOnly,
        IExpensesDeleteOnlyRepository repository, 
        IUnitOfWork unitOfWork, 
        ILoggedUser loggedUser)
    {
        _expensesReadOnly = expensesReadOnly;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }

    public async Task Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();

        var _expese =  await _expensesReadOnly.GetById(loggedUser, id);

        if (_expese is null)
            throw new NotFoundException(ResourcesErrorsMessages.EXPENSE_NOT_FOUND);

         await _repository.Delete(id);

        await _unitOfWork.Commit();
    }
}
