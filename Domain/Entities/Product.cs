using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Image { get; set; }

        void aaa()
        {
            var dog = new Dog();
            dog.Name = "dsada";
        }
    }

    class Animal
    {
        public string Name { get; set; }
    }

    class Dog : Animal
    {

    }
}
