using Microsoft.AspNetCore.Mvc;
using VMTP.API.Controllers.Requests;
using VMTP.API.Sagas;
using VMTP.API.Sagas.Requests;

namespace VMTP.API.Controllers;

[Route("authentication/sign-up")]
public class RegisterController : ControllerBase
{
    private readonly RegisterSaga _registerSaga;

    public RegisterController(RegisterSaga registerSaga)
    {
        _registerSaga = registerSaga;
    }

    [HttpPost]
    [Route("challenge/{email}")]
    public async Task<IActionResult> RegisterChallengeAsync(string email, CancellationToken cancellationToken)
    {
        await _registerSaga.RegisterChallengeAsync(email, cancellationToken);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterHttpRequest request,
        [FromHeader(Name = "User-Agent")] string userAgent,
        CancellationToken cancellationToken)
    {
        var ip = HttpContext.Connection.RemoteIpAddress!.ToString();
        var result = await _registerSaga.RegisterAsync(
            new RegisterRequest(request.Login, request.Password, request.Code, userAgent, ip), cancellationToken);

        return Ok(result);
    }
}