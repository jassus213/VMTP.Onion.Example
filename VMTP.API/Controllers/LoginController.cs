using Microsoft.AspNetCore.Mvc;
using VMTP.API.Controllers.Requests;
using VMTP.API.Sagas;
using VMTP.API.Sagas.Requests;

namespace VMTP.API.Controllers;

[Route("authentication/sign-in")]
public class LoginController : ControllerBase
{
    private readonly LoginSaga _loginSaga;

    public LoginController(LoginSaga loginSaga)
    {
        _loginSaga = loginSaga;
    }

    [HttpGet]
    public async Task<IActionResult> Login([FromBody] LoginHttpRequest request,
        [FromHeader(Name = "User-Agent")] string userAgent,
        CancellationToken cancellationToken)
    {
        var ip = HttpContext.Connection.RemoteIpAddress!.ToString();
        var result = await _loginSaga.LoginAsync(new LoginRequest(request.Login, request.Password, ip, userAgent),
            cancellationToken);

        return Ok(result);
    }
}