using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.Exceptions;

namespace CashFlow.Application.UseCases.Expenses.GetExpenseById;
public class GetExpenseByIdUseCase : IGetExpenseByIdUseCase
{
    private readonly IExpensesReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    public GetExpenseByIdUseCase(IExpensesReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseExpenseJson> Execute(long id)
    {
        var result = await _repository.GetById(id);

        if (result is null)
        {
            throw new NotFoundException(ResourcesErrorsMessages.EXPENSE_NOT_FOUND);
        }

        return _mapper.Map<ResponseExpenseJson>(result);
    }
}
