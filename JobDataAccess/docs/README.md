### Создание БД

```CREATE DATABASE job;```

### Создание схемы

```CREATE SCHEMA job;```


### Создание миграции

Перейти в папку .\TalentHub\JobService\DAL\JobDataAccess
Запустить
```shell dotnet ef migrations add <migration_name>```

### Удаление миграции

Запустить
```shell dotnet ef migrations remove```

### Скрипты для миграции

Запустить
```shell dotnet ef migrations script -i -o Migrations\Sql\up_InitialMigration.sql```


### TODO: написать про откат миграции

