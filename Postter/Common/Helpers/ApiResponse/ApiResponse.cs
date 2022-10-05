namespace Postter.Common.Helpers.ApiResponse;

/// <summary>
/// Вид всех возвращаемых ответов
/// </summary>
public sealed class ApiResponse
{
    /// <summary>
    /// Текст ответа
    /// </summary>
    public object Message { get; set; }
    /// <summary>
    /// Текст ошибки
    /// </summary>
    public object Error { get; set; }
}