using VMTP.Code.Application.Models.Enums;

namespace VMTP.Code.Bal.Abstractions.Managers.Requests;

public record CreateCodeRequest(string Email, CodeType CodeType, Guid? AuthorizationId = null);