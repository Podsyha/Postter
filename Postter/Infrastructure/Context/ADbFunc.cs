using Microsoft.EntityFrameworkCore.Storage;
using Postter.BusinessLogic.Assert;

namespace Postter.Infrastructure.Context
{
	/// <summary>
	/// Абстрактный класс описания функциональности взаимодействия с БД.
	/// </summary>
	public class ADbFunc : IRepository
	{
		public ADbFunc(AppDbContext dbContext, IAssert assert)
		{
			_dbContext = dbContext;
			_assert = assert;
		}
		

		protected readonly AppDbContext _dbContext;
		private readonly IAssert _assert;

		
		/// <summary>
		/// Получить транзакцию.
		/// </summary>
		/// <returns></returns>
		public IDbContextTransaction GetTransaction()
		{
			return _dbContext.Database.CurrentTransaction ?? _dbContext.Database.BeginTransaction();
		}

		/// <summary>
		/// Добавить данные в БД.
		/// </summary>
		/// <typeparam name="T">Тип данных.</typeparam>
		/// <param name="model">Модель данных.</param>
		public async Task AddModelToDb<T>(T model)
		{
			_assert.IsNull(model);

			await _dbContext.AddAsync(model);
		}
	}
}
