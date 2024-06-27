using System;
using System.Collections.Generic;
using System.IO;
using ecommerce;
using Newtonsoft.Json;

namespace ecommerce
{
    public class Order
    {
        public string Username { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }

        static string ordersFile = @"C:\Users\10decoders\Downloads\ecommerce\ecommerce\orders.json";

        public static void AddItemsToCart()
        {
            List<Item> items = Item.LoadItems();
            List<Order> orders = LoadOrders();

            while (true)
            {
                Console.Write("Enter item ID to add to cart: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter quantity to add to cart: ");
                int quantityToAdd = Convert.ToInt32(Console.ReadLine());

                Item itemToAdd = items.Find(i => i.Id == id);
                if (itemToAdd != null)
                {
                    if (itemToAdd.Quantity < quantityToAdd)
                    {
                        Console.WriteLine($"Out of stock or limited stock only. Available quantity: {itemToAdd.Quantity}");
                    }
                    else
                    {
                        itemToAdd.Quantity -= quantityToAdd;
                        orders.Add(new Order { Username = "current_user", ItemId = itemToAdd.Id, ItemName = itemToAdd.Name, Quantity = quantityToAdd });
                        Item.SaveItems(items);
                        SaveOrders(orders);
                        Console.WriteLine("Item added to cart successfully!");
                    }
                }
                else
                {
                    Console.WriteLine("Item not found.");
                }

                Console.Write("Do you want to add another item to the cart? (yes/no): ");
                string response = Console.ReadLine().ToLower();
                if (response != "yes")
                {
                    break;
                }
            }
        }

        public static void RemoveItemFromCart()
        {
            Console.Write("Enter item ID to remove from cart: ");
            int id = Convert.ToInt32(Console.ReadLine());

            List<Order> orders = LoadOrders();
            Order orderToRemove = orders.Find(o => o.ItemId == id);
            if (orderToRemove != null)
            {
                List<Item> items = Item.LoadItems();
                Item item = items.Find(i => i.Id == id);
                if (item != null)
                {
                    item.Quantity += orderToRemove.Quantity;
                    Item.SaveItems(items);
                }

                orders.Remove(orderToRemove);
                SaveOrders(orders);
                Console.WriteLine("\nItem removed from cart successfully!");
            }
            else
            {
                Console.WriteLine("\nItem not found in cart.");
            }
        }

        public static void ViewCart()
        {
            List<Order> orders = LoadOrders();
            Console.WriteLine("--\t-------\t\t-----------\t");
            Console.WriteLine("ID\t Name  \t\t Quantity  \t");
            Console.WriteLine("--\t-------\t\t-----------\t");
            foreach (var order in orders)
            {
                Console.WriteLine($"{order.ItemId}\t{order.ItemName.PadRight(15)}\t{order.Quantity.ToString().PadRight(25)}");
            }
        }
        public static void PlaceOrder()
        {
            List<Order> orders = LoadOrders();
            if (orders.Count == 0)
            {
                Console.WriteLine("Your cart is empty. Please add items to your cart first.");
                return;
            }

            SaveOrders(orders);
            orders.Clear();
            Console.WriteLine("\n******Order placed successfully!******");

        }

        public static List<Order> LoadOrders()
        {


            if (File.Exists(ordersFile))
            {
                string jsonData = File.ReadAllText(ordersFile);
                return JsonConvert.DeserializeObject<List<Order>>(jsonData);
            }
            else
            {
                return new List<Order>();
            }
        }

        public static void SaveOrders(List<Order> orders)
        {
            string jsonData = JsonConvert.SerializeObject(orders, Formatting.Indented);
            File.WriteAllText(ordersFile, jsonData);
        }



    }
}
