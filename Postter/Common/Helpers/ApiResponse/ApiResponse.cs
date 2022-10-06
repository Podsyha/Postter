using Microsoft.VisualBasic.CompilerServices;

namespace Postter.Common.Helpers.ApiResponse;

/// <summary>
/// Вид всех возвращаемых ответов
/// </summary>
public sealed class ApiResponse
{
    public ApiResponse(object message, object error)
    {
        this.message = message;
        this.error = error;
    }
    /// <summary>
    /// Текст ответа
    /// </summary>
    public object message { get; set; }
    /// <summary>
    /// Текст ошибки
    /// </summary>
    public object error { get; set; }
}