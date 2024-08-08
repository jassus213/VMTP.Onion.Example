namespace VMTP.Dal.Abstractions;

/// <summary>
/// Интерфейс, нужный для абстрайкций контекста, позволяющий сохранять изменения в базе данных
/// </summary>
public interface ISavableContext
{
    /// <summary>
    /// Идентичен методу EF Core, что позволяет явно не реализовывать
    /// </summary>
    /// <see href="http://learn.microsoft.com/ru-ru/dotnet/api/system.data.entity.dbcontext.savechangesasync?view=entity-framework-6.2.0">Ссылка на метод EF</see>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает количество сохраненных записей из бд</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}