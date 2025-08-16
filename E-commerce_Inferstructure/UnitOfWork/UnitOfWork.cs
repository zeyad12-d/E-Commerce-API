using E_commerce_Core.Entityes;
using E_commerce_Core.Interfaces;
using E_commerce_Inferstructure.Data;
using E_commerce_Inferstructure.Repositry;

namespace E_commerce_Core.UnitOfWork
{
    public class UnitOfWork
    {
        public readonly ApplicationDBcontext _dbcontext;
        IRepository<Product> _products;//1
        IRepository<Category> _categories;//2
        IRepository<Order> _orders;//4
        IRepository<OrderItem> _orderItems;//5
        IRepository<Address> _address;
        IRepository<Review> _reviews;
        IRepository<ShoppingCart> _shoppingCarts;
        IRepository<CartItem> _cartItems;
        IRepository<Payment> _payments;
        

        public UnitOfWork(ApplicationDBcontext dBcontext)
        {
            _dbcontext = dBcontext;
        }

        public IRepository<Product> ProductRepo
        {
            get
            {
                if (_products == null)
                {
                    _products = new Repository<Product>(_dbcontext);
                }
                return _products; 
            }
        }
        public IRepository<Category> CategoryRepo
        {
            get
            {
                if (_categories == null)
                {
                    _categories = new Repository<Category>(_dbcontext);
                }
                return _categories;
            }
        }
   
        public IRepository<Order> OrderRepo
        {
            get
            {
                if (_orders == null)
                {
                    _orders = new Repository<Order>(_dbcontext);
                }
                return _orders;
            }
        }
        public IRepository<OrderItem> OrderItemRepo
        {
            get
            {
                if (_orderItems == null)
                {
                    _orderItems = new Repository<OrderItem>(_dbcontext);
                }
                return _orderItems;
            }
        }
        public IRepository<Address> AddressRepo
        {
            get
            {
                if (_address == null)
                {
                    _address = new Repository<Address>(_dbcontext);
                }
                return _address;
            }
        }
        public IRepository<Review> ReviewRepo
        {
            get
            {
                if (_reviews == null)
                {
                    _reviews = new Repository<Review>(_dbcontext);
                }
                return _reviews;
            }
        }
        public IRepository<ShoppingCart> ShoppingCartRepo
        {
            get
            {
                if (_shoppingCarts == null)
                {
                    _shoppingCarts = new Repository<ShoppingCart>(_dbcontext);
                }
                return _shoppingCarts;
            }
        }
        public IRepository<CartItem> CartItemRepo
        {
            get
            {
                if (_cartItems == null)
                {
                    _cartItems = new Repository<CartItem>(_dbcontext);
                }
                return _cartItems;
            }
        }
        public IRepository<Payment> PaymentRepo
        {
            get
            {
                if (_payments == null)
                {
                    _payments = new Repository<Payment>(_dbcontext);
                }
                return _payments;
            }
        }


        public async Task<int> SaveChangesAsync()
        {
            return  await _dbcontext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbcontext.Dispose();
        }
    }
   

}
