using System;

namespace FoltDelivery.Model
{
    public class Logo : Entity
    {
        public String Image { get; set; }

        public Logo() { }

        public Logo(Guid id, String image)
        {
            Id = id;
            Image = image;
        }
    }
}
