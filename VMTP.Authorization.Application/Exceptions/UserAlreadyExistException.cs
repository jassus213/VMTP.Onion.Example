using System.Net;

namespace VMTP.Authorization.Application.Exceptions;

public class UserAlreadyExistException : HttpRequestException
{
    public UserAlreadyExistException(string reasonPhrase = "Такой пользователь уже зарегестрирован") : base(
        reasonPhrase, null, HttpStatusCode.BadRequest)
    {
    }
}