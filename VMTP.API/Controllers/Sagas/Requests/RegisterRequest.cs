namespace VMTP.API.Controllers.Sagas.Requests;

public record RegisterRequest(string Login, string Password, string Code, string Device, string Ip);