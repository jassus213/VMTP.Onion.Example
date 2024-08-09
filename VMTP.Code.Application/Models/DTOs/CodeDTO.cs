using VMTP.Code.Application.Models.Enums;

namespace VMTP.Code.Application.Models.DTOs;

public class CodeDTO
{
    public Guid Id { get; init; }
    public string Email { get; init; } = string.Empty;
    public string Value { get; init; } = string.Empty;
    public DateTime ExpirationTime { get; init; }
    public CodeType CodeType { get; init; }
    public Guid? AuthorizationId { get; init; }
}