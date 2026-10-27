using PostService.CommonTypes;

namespace PostService.Dtos;

/// <summary>
/// Дані для оновлення поштового відправлення.
/// </summary>
public class PostingPutDto
{
    /// <summary>
    /// Ідентифікатор поштового відправлення.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Відправник поштового відправлення.
    /// </summary>
    public string From { get; set; } = string.Empty;
    /// <summary>
    /// Одержувач поштового відправлення.
    /// </summary>
    public string To { get; set; } = string.Empty;
    /// <summary>
    /// Опис вмісту відправлення.
    /// </summary>
    public string Content { get; set; } = string.Empty;
    /// <summary>
    /// Тип доставки: 0 — відділення, 1 — кур'єр, 2 — експрес-кур'єр.
    /// </summary>
    public DeliveryType DeliveryType { get; set; }
    /// <summary>
    /// Вага відправлення в кілограмах.
    /// </summary>
    public float Weight { get; set; }
    /// <summary>
    /// Ширина відправлення в сантиметрах.
    /// </summary>
    public float Width { get; set; }
    /// <summary>
    /// Висота відправлення в сантиметрах.
    /// </summary>
    public float Height { get; set; }
    /// <summary>
    /// Довжина відправлення в сантиметрах.
    /// </summary>
    public float Depth { get; set; }
    /// <summary>
    /// Оголошена вартість у гривнях. null означає, що вартість не вказано.
    /// </summary>
    public float? Value { get; set; }
    /// <summary>
    /// Вартість доставки у гривнях.
    /// </summary>
    public float Price { get; set; }
}
