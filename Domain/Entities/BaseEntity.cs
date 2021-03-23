using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key, Required]
        public string Id { get; set; }
    }
}
