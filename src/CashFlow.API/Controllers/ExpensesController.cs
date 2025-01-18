using CashFlow.Application.UseCases.Expenses.Delete;
using CashFlow.Application.UseCases.Expenses.GetAllExpense;
using CashFlow.Application.UseCases.Expenses.GetExpenseById;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Communication.Request;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredExpenseJson),StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterExpenseUseCase useCase, 
        [FromBody] RequestExpenseJson request
    )
    {
            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseExpensesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllExpense([FromServices] IGetAllExpenseUseCase useCase)
    {
        var response = await useCase.Execute();

        if(response.Expenses.Count != 0)
               return Ok(response);

       return NoContent();
    }

    [HttpGet]
    [Route("{Id}")]
    [ProducesResponseType(typeof(ResponseExpenseJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromServices] IGetExpenseByIdUseCase useCase, [FromRoute] long Id)
    {
        var response = await useCase.Execute(Id);

        return Created(string.Empty, response);
    }

    [HttpDelete]
    [Route("{Id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeleteExpenseUseCase useCase, [FromRoute] long Id)
    {
        await useCase.Execute(Id);

        return NoContent();
    }

    [HttpPut]
    [Route("{Id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromServices] IUpdateExpenseUseCase useCase, [FromRoute] long Id, [FromBody] RequestExpenseJson request)
    {
        await useCase.Execute(Id, request);

        return NoContent();
    }
}