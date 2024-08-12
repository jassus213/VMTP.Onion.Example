namespace VMTP.API.Sagas.Requests;

public record LoginRequest(string Login, string Password, string Ip, string Device);