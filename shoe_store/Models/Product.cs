using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Windows.Media;
using shoe_store.Helpers;

namespace shoe_store.Models
{public class Product
        {
            private const int BigDiscountThreshold = 15;
            private const int LowStockThreshold = 3;
            private ImageSource _photo;
            public int ProductId { get; set; }
            public string Article { get; set; } = string.Empty;
            public string ProductName { get; set; } = string.Empty;
            public string CategoryName { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string ManufacturerName { get; set; } = string.Empty;
            public string SupplierName { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public string UnitName { get; set; } = string.Empty;
            public int StockQuantity { get; set; }
            public int Discount { get; set; }
            public string PhotoPath { get; set; }
            public bool HasDiscount => Discount > 0;
            public bool IsBigDiscount => Discount > BigDiscountThreshold;
            // Товара нет на складе
            public bool IsOutOfStock => StockQuantity <= 0;
            // Остаток от 1 до 3 включительно (Приложение 3)
            public bool IsLowStock => StockQuantity > 0 && StockQuantity <= LowStockThreshold;
        public decimal FinalPrice => Math.Round(Price * (100 - Discount) / 100m, 2);
        // Картинка создаётся один раз при первом обращении
        public ImageSource Photo
        {
            get
            {
                if (_photo == null)
                {
                    _photo = ImageLoader.Load(PhotoPath);
                }
                return _photo;
            }
        }
    }
}