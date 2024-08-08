using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace VMTP.Dal.Abstractions;

/// <summary>
/// Интерфейс, нужный для абстрайкций контекста, дающий возможность обновлять данные
/// </summary>
public interface IUpdatableContext
{
    /// <summary>
    /// Метод, который позволяет обновить данные в определенном set'е, указав какие данные на что нужно поменять
    /// </summary>
    /// <param name="set">Set, где происходит поиск</param>
    /// <param name="setPropertyCalls">То какие и на какие данные нужно поменять, используя Func</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    /// <returns>Количество обновленных записей в бд</returns>
    Task<int> ExecuteUpdateAsync<TEntity>(IQueryable<TEntity> set,
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls,
        CancellationToken cancellationToken = default) where TEntity : class;
}