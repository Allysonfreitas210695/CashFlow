using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Exception.Exceptions;
using CashFlow.Exception;
using CommonTestUltilities.Cryptography;
using CommonTestUltilities.Mapper;
using CommonTestUltilities.Repositories;
using CommonTestUltilities.Request;
using CommonTestUltilities.Token;
using FluentAssertions;

namespace UseCases.Test.Users.Register;
public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();

        result.Name.Should().Be(request.Name);

        result.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        request.Name = string.Empty;

        var useCase = CreateUseCase();

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors.Count == 1 && ex.GetErrors.Contains(ResourcesErrorsMessages.NAME_EMPTY));
    }

    [Fact]
    public async Task Error_Email_Already_Exist()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var useCase = CreateUseCase(request.Email);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors.Count == 1 && ex.GetErrors.Contains(ResourcesErrorsMessages.EMAIL_ALREADY_REGISTERED));
    }

    private RegisterUserUseCase CreateUseCase(string? email = null)
    {
        var mapper = MapperBuilder.Build();

        var unitOfWork = UnitOfWorkBuilder.Build();

        var writeRepository = UserWriteOnlyRepositoryBuilder.Build();

        var passwordEncripter = new PasswordEncrypterBuilder().Build();

        var tokenGenerator = JwtTokenGeneratorBuilder.Build();

        var readRepository = new UserReadOnlyRepositoryBuilder();

        if (string.IsNullOrWhiteSpace(email) == false)
            readRepository.ExistActiveUserWithEmail(email);

        return new RegisterUserUseCase(mapper, passwordEncripter, readRepository.Build(), writeRepository, tokenGenerator, unitOfWork);
    }
}
