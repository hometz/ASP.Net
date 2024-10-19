using Microsoft.AspNetCore.Mvc;

namespace Web_253501_Homets.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        // Пример данных о корзине
        public IViewComponentResult Invoke()
        {
            var cartInfo = new CartViewModel
            {
                TotalItems = 3,
                TotalPrice = 1599.99m
            };
            return View(cartInfo);
        }
    }

    public class CartViewModel
    {
        public int TotalItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
