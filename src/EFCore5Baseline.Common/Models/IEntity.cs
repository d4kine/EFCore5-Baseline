using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCore5Baseline.Common.Models
{
    public interface IEntity
    {
        object Id { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }

    public interface IEntity<T> : IEntity
    {
        new T Id { get; set; }
    }
}