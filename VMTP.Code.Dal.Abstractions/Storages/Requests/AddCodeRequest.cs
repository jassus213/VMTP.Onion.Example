using VMTP.Code.Application.Models.Enums;

namespace VMTP.Code.Dal.Abstractions.Storages.Requests;

public record AddCodeRequest(string Email, CodeType CodeType, string Value, DateTimeOffset AbsoluteExpiration);