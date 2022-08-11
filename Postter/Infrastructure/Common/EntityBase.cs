using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Postter.Infrastructure.Common;

public class EntityBase
{
    protected EntityBase()
    {
        DateAdded = DateTime.Now;
    }

    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Дата добавления
    /// </summary>
    public DateTime DateAdded { get; set; }
    
    /// <summary>
    /// Клонировать модель данных.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <remarks>
    /// Клонировние объектов БД с режимом lazy-load не клонирует вложенные объекты.
    /// При обращении к ним произойдет ошибка. Будьте внимательны.
    /// </remarks>
    public T Clone<T>() where T : EntityBase
    {
        return MemberwiseClone() as T;
    }
}