using AutoMapper;
using CashFlow.Communication.Request;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.Exceptions;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase : IRegisterExpenseUseCase
{
    private readonly IExpensesWriteOnlyRepository _expensesWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterExpenseUseCase(IExpensesWriteOnlyRepository expensesWriteOnlyRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _expensesWriteOnlyRepository = expensesWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseRegisteredExpenseJson> Execute(RequestExpenseJson request)
    {
        Validate(request);

        var entity = _mapper.Map<Expense>(request);

        await _expensesWriteOnlyRepository.Add(entity);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisteredExpenseJson>(entity);
    }

    private void Validate(RequestExpenseJson request)
    {
        var validator = new ExpenseValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var listErrors = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorOnValidationException(listErrors);
        }
    }
}