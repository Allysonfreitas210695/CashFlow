
using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetAllExpense;
public class GetAllExpenseUseCase : IGetAllExpenseUseCase
{
    private readonly IExpensesReadOnlyRepository _expensesReadOnlyRepository;
    private readonly IMapper _mapper;
    public GetAllExpenseUseCase(IExpensesReadOnlyRepository expensesReadOnlyRepository, IMapper mapper)
    {
        _expensesReadOnlyRepository = expensesReadOnlyRepository;
        _mapper = mapper;
    }

    public async Task<ResponseExpensesJson> Execute()
    {
        var result = await _expensesReadOnlyRepository.GetAll();

        return new ResponseExpensesJson
        {
            Expenses = _mapper.Map<List<ResponseShortExpenseJson>>(result)
        };
    }
}
