using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoltDelivery.Model
{
    public class RestaurantMenu : Entity
    {

        public virtual List<Product> _menuArticle { get; }

        public RestaurantMenu()
        {
            _menuArticle = new List<Product>();
        }

        public RestaurantMenu(IEnumerable<Product> menuArticle)
        {
            _menuArticle = menuArticle.ToList();
        }


    }
}
