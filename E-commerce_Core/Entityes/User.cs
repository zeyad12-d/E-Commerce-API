using Microsoft.AspNetCore.Identity;

namespace E_commerce_Core.Entityes
{
    public class User : IdentityUser
    {
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

    }
}
