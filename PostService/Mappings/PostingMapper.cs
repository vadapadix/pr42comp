using PostService.CommonTypes;
using PostService.Dtos;
using PostService.Models;
namespace PostService.Mappings;

/// <summary>Перетворення між DTO та моделлю поштового відправлення.</summary>
public static class PostingMapper
{
    /// <summary>Перетворити DTO створення на модель.</summary>
    /// <param name="dto">Дані нового відправлення.</param>
    /// <returns>Модель нового відправлення.</returns>
    public static Posting ToPosting(PostingPostDto dto)
    {
        return new Posting
        {
            From = dto.From,
            To = dto.To,
            Content = dto.Content,
            DeliveryType = dto.DeliveryType,
            Weight = dto.Weight,
            Width = dto.Width,
            Height = dto.Height,
            Depth = dto.Depth,
            Value = dto.Value
        };
    }

    /// <summary>Перетворити DTO оновлення на модель.</summary>
    /// <param name="dto">Дані оновлення відправлення.</param>
    /// <returns>Модель із новими даними.</returns>
    public static Posting ToPosting(PostingPutDto dto)
    {
        return new Posting
        {
            Id = dto.Id,
            From = dto.From,
            To = dto.To,
            Content = dto.Content,
            DeliveryType = dto.DeliveryType,
            Weight = dto.Weight,
            Width = dto.Width,
            Height = dto.Height,
            Depth = dto.Depth,
            Value = dto.Value,
            Price = dto.Price
        };
    }

    /// <summary>Перетворити модель на DTO відповіді.</summary>
    /// <param name="posting">Модель збереженого відправлення.</param>
    /// <returns>Дані відправлення для відповіді API.</returns>
    public static PostingGetDto ToPostingGetDto(Posting posting)
    {
        return new PostingGetDto
        {
            Id = posting.Id,
            From = posting.From,
            To = posting.To,
            Content = posting.Content,
            DeliveryType = posting.DeliveryType,
            Weight = posting.Weight,
            Width = posting.Width,
            Height = posting.Height,
            Depth = posting.Depth,
            Value = posting.Value,
            Price = posting.Price,
            CreatedAt = posting.CreatedAt
        };
    }

    /// <summary>Перетворити модель на DTO відповіді через сумісний метод.</summary>
    /// <param name="posting">Модель збереженого відправлення.</param>
    /// <returns>Дані відправлення для відповіді API.</returns>
    public static PostingGetDto ToPostiongGetDto(Posting posting)
    {
        return ToPostingGetDto(posting);
    }
}
