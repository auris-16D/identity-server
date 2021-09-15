using System;
namespace Api.Library.Extensions
{
    public static class StringExtensions
    {
        public static Guid ToGuid(this string stringGuid)
        {
            return Guid.Parse(stringGuid);
        }
    }
}
