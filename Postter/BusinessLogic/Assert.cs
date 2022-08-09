using Postter.BusinessLogic.Exceptions;

namespace Postter.BusinessLogic;

public class Assert
{
    /// <summary>
    /// NullReferenceException если объект null
    /// </summary>
    /// <param name="obj">Объект</param>
    /// <exception cref="NullReferenceException"></exception>
    public void IsNull(object? obj)
    {
        if (obj == null)
            throw new NullReferenceException("Object is null");
    }

    /// <summary>
    /// RequestLogicException если true
    /// </summary>
    /// <param name="boolean">boolean значение</param>
    /// <param name="errorMessage">Текст ошибки</param>
    /// <exception cref="RequestLogicException"></exception>
    public void ThrowIfTrue(bool boolean, string errorMessage)
    {
        if (boolean)
            throw new RequestLogicException(errorMessage);
    }
    
    /// <summary>
    /// RequestLogicException если false
    /// </summary>
    /// <param name="boolean">boolean значение</param>
    /// <param name="errorMessage">Текст ошибки</param>
    /// <exception cref="RequestLogicException"></exception>
    public void ThrowIfFalse(bool boolean, string errorMessage)
    {
        if (!boolean)
            throw new RequestLogicException(errorMessage);
    }
}