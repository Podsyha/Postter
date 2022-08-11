using Postter.Common.Exceptions;

namespace Postter.Common.Assert;

public interface IAssert
{
    /// <summary>
    /// NullReferenceException если объект null
    /// </summary>
    /// <param name="value">Объект</param>
    /// <exception cref="NullReferenceException"></exception>
    public void IsNull<T>(T value);

    /// <summary>
    /// RequestLogicException если true
    /// </summary>
    /// <param name="boolean">boolean значение</param>
    /// <param name="errorMessage">Текст ошибки</param>
    /// <exception cref="RequestLogicException"></exception>
    public void ThrowIfTrue(bool boolean, string errorMessage);

    /// <summary>
    /// RequestLogicException если false
    /// </summary>
    /// <param name="boolean">boolean значение</param>
    /// <param name="errorMessage">Текст ошибки</param>
    /// <exception cref="RequestLogicException"></exception>
    public void ThrowIfFalse(bool boolean, string errorMessage);
}