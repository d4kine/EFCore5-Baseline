using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCore5Baseline.Common.Extensions
{
    public static class EntityExtensions
    {
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }

        public static bool IsEmpty(this IEnumerable<object> enumerable)
        {
            return !enumerable.Any();
        }

        public static bool IsNotEmpty(this IEnumerable<object> enumerable)
        {
            return enumerable.Any();
        }

        public static bool IsNullOrEmpty(this IEnumerable<object> enumerable)
        {
            return enumerable.IsNull() || enumerable.IsEmpty();
        }

        public static bool IsNotNullOrEmpty(this IEnumerable<object> enumerable)
        {
            return !enumerable.IsNullOrEmpty();
        }
    }
}