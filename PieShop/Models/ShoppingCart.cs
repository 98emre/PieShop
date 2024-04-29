
using Microsoft.EntityFrameworkCore;
using PieShop.Data;

namespace PieShop.Models
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly PieShopDbContext _pieShopDbContext;


        private ShoppingCart(PieShopDbContext pieShopDbContext)
        {
            _pieShopDbContext = pieShopDbContext;
        }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

            PieShopDbContext context = services.GetService<PieShopDbContext>() ?? throw new Exception("Error initializing");

            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();
            session?.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Pie pie)
        {
            var shoppinCartItem = _pieShopDbContext.ShoppingCartItems
                .SingleOrDefault(s => s.Pie.PieId == pie.PieId && s.ShopingCartId == ShoppingCartId);
        
            if(shoppinCartItem == null)
            {
                shoppinCartItem = new ShoppingCartItem
                {
                    ShopingCartId = ShoppingCartId,
                    Pie = pie,
                    Amount = 1
                };

                _pieShopDbContext.Add(shoppinCartItem);
            }

            else
            {
                shoppinCartItem.Amount++;
            }

            _pieShopDbContext.SaveChanges();
        }

        public void ClearCart()
        {
            var cartItem = _pieShopDbContext.ShoppingCartItems
                .Where(cart => cart.ShopingCartId == ShoppingCartId);

            _pieShopDbContext.Remove(cartItem);
            _pieShopDbContext.SaveChanges();
        }


        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            if (ShoppingCartItems == null)
            {
                ShoppingCartItems = _pieShopDbContext.ShoppingCartItems
                                      .Where(c => c.ShopingCartId == ShoppingCartId)
                                      .Include(s => s.Pie)
                                      .ToList();
            }
            return ShoppingCartItems;

        }

        public decimal GetShoppingCartTotal()
        {
            var total = _pieShopDbContext.ShoppingCartItems
                .Where(c => c.ShopingCartId == ShoppingCartId)
                .Select(c => c.Pie.Price * c.Amount).Sum();

            return total;
        }

        public int RemoveFromCart(Pie pie)
        {
           var shoppingCartItem = _pieShopDbContext.ShoppingCartItems
                .SingleOrDefault(s => s.Pie.PieId == pie.PieId && s.ShopingCartId == ShoppingCartId);

            var localAmount = 0;

            if(shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }

                else
                {
                    _pieShopDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _pieShopDbContext.SaveChanges();

            return localAmount;
        }
    }
}
