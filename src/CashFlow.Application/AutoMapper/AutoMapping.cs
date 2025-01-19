using AutoMapper;
using CashFlow.Communication.Request;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToReponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestExpenseJson, Expense>();
        CreateMap<RequestRegisterUserJson, User>()
                        .ForMember(dest => dest.Password, config => config.Ignore());
    }

    private void EntityToReponse()
    {
        CreateMap<Expense, ResponseExpenseJson>();
        CreateMap<Expense, ResponseRegisteredExpenseJson>();
        CreateMap<Expense, ResponseShortExpenseJson>();
    }
}
