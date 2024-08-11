using System.Net;

namespace VMTP.Code.Application.Models.Exceptions;

public class CodeInvalidException : HttpRequestException
{
    public CodeInvalidException(string reasonPhrase = "Код невалидный") : base(reasonPhrase, null,
        HttpStatusCode.BadRequest)
    {
    }
}