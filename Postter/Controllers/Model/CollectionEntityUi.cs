namespace Postter.Controllers.Model;

/// <summary>
/// Generic модель для коллекий выдачи на UI
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class CollectionEntityUi<T>
{
    /// <summary>
    /// Коллекция айтемов
    /// </summary>
    public ICollection<T> Items { get; set; }
    /// <summary>
    /// Общее кол-во айтемов
    /// </summary>
    public int TotalCount { get; set; }
    /// <summary>
    /// Общее кол-во доступных страниц
    /// </summary>
    public double TotalPages { get; set; }
}