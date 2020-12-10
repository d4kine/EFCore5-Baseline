using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCore5Baseline.Common.Models
{
    public class User : Entity<Guid>
    {
        public string Email { get; set; }
    }
}