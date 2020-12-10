using System.Text;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore5Baseline.Common.Models
{
    public abstract class Entity<T> : IEntity<T>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }

        object IEntity.Id
        {
            get => Id;
            set => Id = (T)value;
        }

        [JsonIgnore]
        [DataType(DataType.Date)]
        public DateTime? CreatedAt { get; set; }

        [JsonIgnore]
        [DataType(DataType.Date)]
        public DateTime? ModifiedAt { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}