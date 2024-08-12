namespace VMTP.API.Controllers.Requests;

public record RegisterHttpRequest(string Login, string Password, string Code);