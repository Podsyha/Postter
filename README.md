# Укажите путь к БД
В файле <code>appsettings.json</code> укуажите данные для подключения к БД

# Выполните команду обновления БД из миграций
```
dotnet ef database update --project Postter\Postter.csproj --startup-project Postter\Postter.csproj --context Postter.Infrastructure.Context.AppDbContext --configuration Debug 20221008102827_Initial
```