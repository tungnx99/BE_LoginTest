using Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class BaseEntity : IObjectState
    {
        [Key, Required]
        public Guid Id { get; set; }
        public ObjectState ObjectState { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
