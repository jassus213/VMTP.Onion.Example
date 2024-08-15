using Microsoft.EntityFrameworkCore;
using VMTP.Dal.Abstractions;

namespace VMTP.Code.Dal.Abstractions.Contexts;

public interface ICodeContext : ISavableContext
{
    DbSet<Domain.Entities.Code> Codes { get; }
}