using System;

namespace Shop.Areas.Admin.Models
{
    public class TypeProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static explicit operator TypeProduct(Product v)
        {
            throw new NotImplementedException();
        }
    }
}