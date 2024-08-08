using System.Net;

namespace VMTP.Authorization.Application.Exceptions;

public class WrongPasswordException : HttpRequestException
{
    public WrongPasswordException(string reasonPhrase = "Не верный пароль.") : base(reasonPhrase, null,
        HttpStatusCode.BadRequest)
    {
    }
}