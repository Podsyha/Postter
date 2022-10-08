namespace Postter.Common.Attribute;

public interface ICustomAuthorizeAttribute
{
    void AddToken(Guid authorId, string jwt);

    void RemoveToken(Guid authorId);
}