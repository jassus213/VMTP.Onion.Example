using System.Net;

namespace VMTP.Code.Application.Models.Exceptions;

public class CodeNotExistException : HttpRequestException
{
    public CodeNotExistException(string reasonPhrase = "Такого кода не существует") : base(reasonPhrase, null,
        HttpStatusCode.NotFound)
    {
    }
}