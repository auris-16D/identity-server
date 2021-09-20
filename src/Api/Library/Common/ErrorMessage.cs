using System;
namespace Api.Library.Common
{
    public static class ErrorMessage
    {
        public static string IsRequired(this string objName)
        {
            return $"{objName} is required";
        }

        public static class Fields
        {
            public const string PrincipleId = "principleId";
        }
    }
}
