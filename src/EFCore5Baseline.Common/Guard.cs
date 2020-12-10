using System.Text;
using System.Linq;
using System.Collections.Generic;
using System;
using EFCore5Baseline.Common.Exceptions;
using EFCore5Baseline.Common.Extensions;
using EFCore5Baseline.Common.Models;

namespace EFCore5Baseline.Common
{
    public static class Guard
    {
        public static void IsNotNull(object obj)
        {
            if (obj.IsNull())
            {
                throw new ArgumentNullException($"Given object {nameof(obj)} is null");
            }
        }

        public static void IsNotNullOrEmpty(IEntity entity)
        {
            if (entity.IsNull())
            {
                throw new EntityNotFoundException($"Given entity {entity} is null");
            }
        }

        public static void IsNotNullOrEmpty(IEnumerable<object> enumerable)
        {
            if (enumerable.IsNullOrEmpty())
            {
                throw new ArgumentNullException($"Given enumerable {nameof(enumerable)} is null or empty");
            }
        }

        public static void IsNotNullOrEmpty(IEnumerable<IEntity> entityEnumerable)
        {
            if (entityEnumerable.IsNotNullOrEmpty())
            {
                throw new EntityNotFoundException($"Given enumerable of entities {entityEnumerable} is null or empty");
            }
        }
    }
}