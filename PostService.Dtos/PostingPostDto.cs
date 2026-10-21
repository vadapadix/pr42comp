using System.ComponentModel.DataAnnotations;
using PostService.CommonTypes;

namespace PostService.Dtos;

public class PostingPostDto
{
    [Required(ErrorMessage = "Поле From є обов'язковим.")]
    public string From { get; set; } = string.Empty;
    [Required(ErrorMessage = "Поле To є обов'язковим.")]
    public string To { get; set; } = string.Empty;
    [Required(ErrorMessage = "Поле Content є обов'язковим.")]
    public string Content { get; set; } = string.Empty;
    [EnumDataType(typeof(DeliveryType), ErrorMessage = "Виберіть тип доставки.")]
    public DeliveryType DeliveryType { get; set; }
    [Range(float.Epsilon, float.MaxValue, ErrorMessage = "Поле Weight має бути більше 0.")]
    public float Weight { get; set; }
    [Range(float.Epsilon, float.MaxValue, ErrorMessage = "Поле Width має бути більше 0.")]
    public float Width { get; set; }
    [Range(float.Epsilon, float.MaxValue, ErrorMessage = "Поле Height має бути більше 0.")]
    public float Height { get; set; }
    [Range(float.Epsilon, float.MaxValue, ErrorMessage = "Поле Depth має бути більше 0.")]
    public float Depth { get; set; }
    [Range(0, float.MaxValue, ErrorMessage = "Оголошена вартість має бути не меншою за 0.")]
    public float? Value { get; set; }
}
