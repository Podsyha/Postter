using Microsoft.EntityFrameworkCore.Storage;

namespace Postter.Infrastructure.Context
{
	/// <summary>
	/// Репозиторий данных.
	/// </summary>
	public interface IRepository
	{
		/// <summary>
		/// Добавить данные в БД.
		/// </summary>
		/// <typeparam name="T">Тип данных.</typeparam>
		/// <param name="model">Модель данных.</param>
		Task AddModelToDb<T>(T model);

		/// <summary>
		/// Получить транзакцию.
		/// </summary>
		/// <returns></returns>
		IDbContextTransaction GetTransaction();
	}
}
