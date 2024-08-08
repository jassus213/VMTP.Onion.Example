using Microsoft.EntityFrameworkCore.Storage;

namespace VMTP.Dal.Abstractions;

/// <summary>
/// Интерфейс, нужный для абстрайкций контекста, дающий возможность создавать транзакции
/// </summary>
/// <see href="https://learn.microsoft.com/ru-ru/dotnet/api/system.data.common.dbconnection.begintransactionasync?view=net-8.0">Ссылка на метод EF</see>
public interface ITransactionContext
{
    /// <summary>
    /// Начинает транзакцию
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает запущенную транзакцию</returns>
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}