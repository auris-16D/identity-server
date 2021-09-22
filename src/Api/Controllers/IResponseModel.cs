using System;
using Api.AccessControl;

namespace Api.Controllers
{
    public interface IResponseModel
    {
        IResponseModel FromModel(IAccessibleResource accessibleResource);
    }
}
