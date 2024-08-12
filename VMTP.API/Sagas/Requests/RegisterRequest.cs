namespace VMTP.API.Sagas.Requests;

public record RegisterRequest(string Login, string Password, string Code, string Device, string Ip);