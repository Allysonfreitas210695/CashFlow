using AutoMapper;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Request;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.Exceptions;

namespace CashFlow.Application.UseCases.Expenses.Update;
public class UpdateExpenseUseCase : IUpdateExpenseUseCase
{
    private readonly  IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExpensesUpdateOnlyRepository _repository;
    public UpdateExpenseUseCase(IMapper mapper, IUnitOfWork unitOfWork, IExpensesUpdateOnlyRepository repository)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task Execute(long id, RequestExpenseJson request)
    {
        Validate(request);

        var _expense = await _repository.GetById(id);
        if (_expense == null) throw new NotFoundException(ResourcesErrorsMessages.EXPENSE_NOT_FOUND);

        _mapper.Map(request, _expense);

        _repository.Update(_expense);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestExpenseJson request)
    {
        var validator = new ExpenseValidator();
        
        var result = validator.Validate(request);
        if (!result.IsValid)
        {
            var errorsMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages);
        }
    }
}
