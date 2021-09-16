using System;
using Api.Models;

namespace Api.AccessControl
{
    public interface IParentAccessibleResource : IRootAccessibleResource
    {
        Budget Budget { get; }
    }
}
