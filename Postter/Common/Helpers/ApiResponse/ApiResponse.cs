using Microsoft.VisualBasic.CompilerServices;

namespace Postter.Common.Helpers.ApiResponse;

/// <summary>
/// Вид всех возвращаемых ответов
/// </summary>
public sealed class ApiResponse
{
    public ApiResponse(object message, object error)
    {
        Message = message;
        Error = error;
    }
    /// <summary>
    /// Текст ответа
    /// </summary>
    public object Message { get; set; }
    /// <summary>
    /// Текст ошибки
    /// </summary>
    public object Error { get; set; }
}