using System;
using System.Collections.Generic;
using Npgsql;
using shoe_store.Models;

namespace shoe_store.Data
{
    public static class ProductRepository
    {
        private const string SelectAllSql = @"
            SELECT 
                p.product_id, 
                p.article, 
                p.product_name, 
                c.category_name, 
                p.description,
                m.manufacturer_name, 
                s.supplier_name, 
                p.price, 
                u.unit_name,
                p.stock_quantity, 
                p.discount, 
                p.photo_path
            FROM product p
            JOIN category c ON c.category_id = p.category_id
            JOIN manufacturer m ON m.manufacturer_id = p.manufacturer_id
            JOIN supplier s ON s.supplier_id = p.supplier_id
            JOIN unit u ON u.unit_id = p.unit_id
            ORDER BY p.product_id";

        public static List<Product> GetAll()
        {
            var products = new List<Product>();

            // Используем using для автоматического закрытия соединения
            using (var connection = Database.CreateConnection())
            {
                connection.Open();

                using (var command = new NpgsqlCommand(SelectAllSql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var product = new Product
                            {
                                ProductId = reader.GetInt32(0),
                                Article = reader.GetString(1),
                                ProductName = reader.GetString(2),
                                CategoryName = reader.GetString(3),
                                // Проверка на NULL для строковых полей
                                Description = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                                ManufacturerName = reader.GetString(5),
                                SupplierName = reader.GetString(6),
                                Price = reader.GetDecimal(7),
                                UnitName = reader.GetString(8),
                                StockQuantity = reader.GetInt32(9),
                                Discount = reader.GetInt16(10),
                                // Проверка на NULL для пути к фото
                                PhotoPath = reader.IsDBNull(11) ? string.Empty : reader.GetString(11)
                            };

                            products.Add(product);
                        }
                    }
                }
            }

            return products;
        }
    }
}