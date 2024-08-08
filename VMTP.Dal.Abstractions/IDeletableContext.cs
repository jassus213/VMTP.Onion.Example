namespace VMTP.Dal.Abstractions;

/// <summary>
/// Интерфейс, нужный для абстрайкций контекста, дающий возможность удалять
/// </summary>
public interface IDeletableContext
{
    /// <summary>
    /// Выполняет операцию удаления для указанного набора объектов
    /// </summary>
    /// <param name="set">Set Объектов, которые нужно удалить</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <typeparam name="TEntity">TEntity Class</typeparam>
    /// <returns>Возвращает количество удаленных записей из бд</returns>
    Task<int> ExecuteDeleteAsync<TEntity>(IQueryable<TEntity> set,
        CancellationToken cancellationToken = default) where TEntity : class;
}