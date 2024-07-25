using Microsoft.AspNetCore.Mvc;
using PocketMoney.Interfaces;
using PocketMoney.Models.Accounting;

namespace PocketMoney.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController(IAccountService accountService) : ControllerBase
{
    [HttpGet("All")]
    public async Task<IActionResult> GetAllAccounts()
    {
        return Ok(await accountService.GetAllAccountsAsync());
    }

    [HttpGet("{accountId}")]
    public async Task<IActionResult> GetAccount(int accountId)
    {
        try
        {
            return Ok(await accountService.GetAccountAsync(accountId));
        }
        catch (ArgumentException ex)
        {
            return StatusCode(404, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] Account account)
    {
        try
        {
            return Ok(await accountService.CreateAccountAsync(account));
        }
        catch (ArgumentException ex)
        {
            return StatusCode(400, ex.Message);
        }
    }

    [HttpDelete("{accountId}")]
    public async Task<IActionResult> RemoveAccount(int accountId)
    {
        try
        {
            await accountService.DeleteAccountAsync(accountId);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return StatusCode(404, ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(400, ex.Message);
        }
    }
}