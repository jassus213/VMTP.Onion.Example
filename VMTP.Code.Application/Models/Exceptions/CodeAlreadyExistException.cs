using System.Net;

namespace VMTP.Code.Application.Models.Exceptions;

public class CodeAlreadyExistException : HttpRequestException
{
    public CodeAlreadyExistException(string reasonPhrase = "Код уже существует") : base(reasonPhrase, null,
        HttpStatusCode.BadRequest)
    {
    }
}