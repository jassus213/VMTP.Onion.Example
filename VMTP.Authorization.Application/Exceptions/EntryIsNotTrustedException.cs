using System.Net;

namespace VMTP.Authorization.Application.Exceptions;

public class EntryIsNotTrustedException : HttpRequestException
{
    public EntryIsNotTrustedException(string reasonPhrase = "Неизвестный вход") : base(reasonPhrase, null,
        HttpStatusCode.BadRequest)
    {
    }
}