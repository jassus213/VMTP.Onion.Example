using VMTP.Code.Application.Models.Enums;

namespace VMTP.Code.Bal.Abstractions.Providers.Requests;

public record ValidateCodeOrThrowRequest(string Email, string Value, CodeType CodeType);