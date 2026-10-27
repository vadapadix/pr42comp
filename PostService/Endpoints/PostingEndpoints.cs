using PostService.BusinessLogic;
using PostService.Dtos;
using PostService.Mappings;

namespace PostService.Endpoints;

/// <summary>Обробники REST API поштових відправлень.</summary>
public static class PostingEndpoints
{
    /// <summary>Отримати інформацію про API.</summary>
    /// <remarks>Повертає текстове повідомлення про призначення сервісу.</remarks>
    /// <returns>Повідомлення про API.</returns>
    /// <response code="200">Сервіс доступний.</response>
    public static string GetInfo() => "Це API поштового клієнта";

    /// <summary>Отримати список відправлень.</summary>
    /// <remarks>Повертає всі збережені відправлення або порожній масив, якщо записів немає.</remarks>
    /// <param name="postingService">Сервіс бізнес-логіки з контейнера залежностей.</param>
    /// <returns>Список відправлень.</returns>
    /// <response code="200">Список успішно отримано.</response>
    public static IResult GetList(IPostingService postingService)
    {
        var postings = postingService.GetAll();
        var resultDtos = new List<PostingGetDto>();
        foreach (var posting in postings)
        {
            resultDtos.Add(PostingMapper.ToPostingGetDto(posting));
        }
        return Results.Ok(resultDtos);
    }

    /// <summary>Отримати відправлення за ідентифікатором.</summary>
    /// <remarks>Шукає запис за значенням id у маршруті.</remarks>
    /// <param name="id">Ідентифікатор відправлення в базі даних.</param>
    /// <param name="postingService">Сервіс бізнес-логіки з контейнера залежностей.</param>
    /// <returns>Знайдене відправлення або відповідь про його відсутність.</returns>
    /// <response code="200">Відправлення знайдено.</response>
    /// <response code="400">Ідентифікатор не є цілим числом.</response>
    /// <response code="404">Відправлення не існує.</response>
    public static IResult GetById(int id, IPostingService postingService)
    {
        var posting = postingService.Find(id);
        return posting is null ? Results.NotFound() : Results.Ok(PostingMapper.ToPostingGetDto(posting));
    }

    /// <summary>Створити поштове відправлення.</summary>
    /// <remarks>Приймає JSON. Сервер призначає ідентифікатор, дату створення та розраховує вартість доставки. Адреса нового запису повертається в заголовку Location.</remarks>
    /// <param name="postDto">Дані нового відправлення у тілі JSON-запиту.</param>
    /// <param name="postingService">Сервіс бізнес-логіки з контейнера залежностей.</param>
    /// <returns>Створене відправлення.</returns>
    /// <response code="201">Відправлення створено.</response>
    /// <response code="400">JSON має неправильний формат або типи значень.</response>
    public static IResult Create(PostingPostDto postDto, IPostingService postingService)
    {
        var saved = postingService.Create(PostingMapper.ToPosting(postDto));
        var resultDto = PostingMapper.ToPostingGetDto(saved);
        return Results.Created($"/postings/{resultDto.Id}", resultDto);
    }

    /// <summary>Оновити поштове відправлення.</summary>
    /// <remarks>Ідентифікатори в маршруті й тілі мають збігатися. Дата створення зберігається; вартість доставки береться з поля price запиту.</remarks>
    /// <param name="id">Ідентифікатор у маршруті; має збігатися з полем id у JSON.</param>
    /// <param name="putDto">Нові дані відправлення у тілі JSON-запиту.</param>
    /// <param name="postingService">Сервіс бізнес-логіки з контейнера залежностей.</param>
    /// <returns>Оновлене відправлення або повідомлення про помилку.</returns>
    /// <response code="200">Відправлення оновлено.</response>
    /// <response code="400">Ідентифікатори не збігаються або запит має неправильний формат.</response>
    /// <response code="404">Відправлення не існує.</response>
    public static IResult Update(int id, PostingPutDto putDto, IPostingService postingService)
    {
        if (id != putDto.Id)
        {
            return Results.BadRequest("Id в маршруті та в тілі запиту не збігаються");
        }
        var updated = postingService.Update(PostingMapper.ToPosting(putDto));
        return updated is null ? Results.NotFound() : Results.Ok(PostingMapper.ToPostingGetDto(updated));
    }

    /// <summary>Видалити поштове відправлення.</summary>
    /// <remarks>Видаляє запис за ідентифікатором. Успішна відповідь не містить тіла.</remarks>
    /// <param name="id">Ідентифікатор відправлення, яке потрібно видалити.</param>
    /// <param name="postingService">Сервіс бізнес-логіки з контейнера залежностей.</param>
    /// <returns>Результат видалення.</returns>
    /// <response code="204">Відправлення видалено.</response>
    /// <response code="400">Ідентифікатор не є цілим числом.</response>
    /// <response code="404">Відправлення не існує.</response>
    public static IResult Delete(int id, IPostingService postingService)
    {
        return postingService.Delete(id) == 0 ? Results.NotFound() : Results.NoContent();
    }
}
