﻿using System;
using Api.Controllers;

namespace Api.AccessControl
{
    public interface IAccessibleResource
    {
        long BudgetId { get; }
        bool IsOwnedBy(Guid principleId);
        bool IsParentOwnedBy(Guid principleId);
    }
}
