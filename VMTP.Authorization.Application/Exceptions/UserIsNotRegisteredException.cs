using System.Net;

namespace VMTP.Authorization.Application.Exceptions;

public class UserIsNotRegisteredException : HttpRequestException
{
    public UserIsNotRegisteredException(string reasonPhrase = "Пользователя с таким логином не существует") : base(
        reasonPhrase, null, HttpStatusCode.BadRequest)
    {
    }
}