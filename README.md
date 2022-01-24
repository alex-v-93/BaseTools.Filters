# BaseTools.Filters
Набор библиотек **BaseTools.Filters** для предоставления фильтрации на уровне домена приложения.
# Для чего нужен?
Набор библиотек **BaseTools.Filters** помогает упростить реализацию фильтрации по полному набору полей модели в приложении, в основе архитектуры которого заложены такие подходы, как [CleanArchitecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html), Domain Driven Development, или Layer architecture. Абстракцию можно внедрять на Domain-уровне с последующим их использованием как различными слоями приложения, например бизнес-логики, так и внешними потребителями функционала. 
Во время использования позволяет потребителю построить полноценное выражение для поиска по модели в [ДНФ](https://en.wikipedia.org/wiki/Disjunctive_normal_form) и [КНФ](https://en.wikipedia.org/wiki/Conjunctive_normal_form). При этом менять контракт и добавлять дополнительных обработок не требуется - они будут заложены в базовой логике мапперов под определенную БД.
# Использование
Для начала, на уровне контракта сервиса доступа к объектам целевой модели (DAL) нужно добавить метод получения объектов по фильтру, например:
```
public interface IDocumentRepositoryDal
{
    Task<IReadOnlyCollection<DocumentModel>> GetByFilterAsync(IFilter filter);
}
```
Примеры использования можно посмотреть [здесь](https://github.com/alex-v-93/BaseTools.Filters/blob/main/csharp-src/BaseTools.Filters.Tests/UnitTest1.cs), либо в следующем примере:
```
var filter = new Filter<DocumentModel>();
filter.And(); // предикаты верхнего уровня должны объединяться через AND
filter.For(document => document.UserId) // предикаты свойств по умолчанию объединяются через OR, но можно явно указать объединение через AND
    .In(targetUsers);
var dtNow = DateTimeOffset.UtcNow;
filter.For(document => document.ExpiredDate)
    .LessThan(dtNow)
    .Equal(dtNow);
// Итоговое условия поиска: UserId in (targetUsers) AND (ExpiredDate < dtNow OR ExpiredDate == dtNow)
var expiredDocuments = await _documentRepository.GetByFilterAsync(filter);

```
Для реализации репозитория под целевую базу потребуется один из модулей **BaseTools.Filters.Mappers.*** (см. [RoadMap](README.md#RoadMap)) под подходящую БД, от разработчика потребуется лишь в некоторых случаях явно указать соответствие свойств-полей между доменной моделью и моделью БД (ORM). При этом со стороны бизнес-логики (Use cases level) создавать фильтры можно с помощью `Filter<T>`, где T - тип модели, по которой производится поиск (в примере это DocumentModel). Для сериализации/десериализации фильтра, в т.ч. транспорт, можно использовать **BaseTools.Filters.Dto** (см. [RoadMap](README.md#RoadMap)). Для проверки коректности можно будет использовать **BaseTools.Filter.Validation** (см. [RoadMap](README.md#RoadMap)).
# RoadMap
- [x] BaseTools.Filters
- [x] BaseTools.Filters.Tests (inprogress)
- [ ] BaseTools.Filters.Mappers.MongoDb
- [ ] BaseTools.Filters.Mappers.Dapper
- [ ] BaseTools.Filters.Mappers.EF
- [ ] BaseTools.Filters.Dto
- [ ] BaseTools.Filters.Validation
