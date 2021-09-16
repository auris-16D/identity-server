using System;
namespace Api.AccessControl
{
    public interface IChildAccessibleResource : IParentAccessibleResource
    {
        bool IsParentOwnedBy(Guid principleId);
    }
}
