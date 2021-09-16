using System;
namespace Api.AccessControl
{
    public interface IRootAccessibleResource
    {
        long BudgetId { get; }
        bool IsOwnedBy(Guid principleId);
    }
}
