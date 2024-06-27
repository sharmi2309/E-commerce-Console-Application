using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ecommerce
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        static string itemsFile = @"C:\Users\10decoders\Downloads\ecommerce\ecommerce\items.json";
        public static void ViewItems()
        {
            List<Item> items = LoadItems();
            Console.WriteLine("--\t----\t\t-----------\t\t------------------\t--------");
            Console.WriteLine("ID\tName\t\tDescription\t\t   Price          \tQuantity");
            Console.WriteLine("--\t----\t\t-----------\t\t------------------\t--------");

            foreach (var item in items)
            {
                Console.WriteLine($"{item.Id}\t{item.Name.PadRight(15)}\t{item.Description.PadRight(25)}\t{item.Price.ToString("00.00").PadRight(5)}\t{item.Quantity.ToString().PadLeft(15)}");
            }
        }


        public static void AddItems()
        {

            List<Item> items = LoadItems();
            while (true)
            {
               
                Console.Write("Enter item name: ");
                string name = Console.ReadLine();
                Console.Write("Enter item description: ");
                string description = Console.ReadLine();
                Console.Write("Enter item price: ");
                double price = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter item quantity: ");
                int quantity = Convert.ToInt32(Console.ReadLine());

                int newid = items.Count + 1;
                Item newItem = new Item { Id = newid, Name = name, Description = description, Price = price, Quantity = quantity };
                items.Add(newItem);

                Console.WriteLine("\nItem added successfully!");

                Console.Write("Do you want to add another item? (yes/no): ");
                string response = Console.ReadLine().ToLower();
                if (response != "yes")
                {
                    break;
                }
            }
            SaveItems(items);
        }

        public static void UpdateItem()
        {
            Console.Write("Enter item ID to update: ");
            int id = Convert.ToInt32(Console.ReadLine());

            List<Item> items = LoadItems();
            Item itemToUpdate = items.Find(i => i.Id == id);
            if (itemToUpdate != null)
            {
                Console.Write("Enter new name: ");
                itemToUpdate.Name = Console.ReadLine();
                Console.Write("Enter new description: ");
                itemToUpdate.Description = Console.ReadLine();
                Console.Write("Enter new price: ");
                itemToUpdate.Price = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter new quantity: ");
                itemToUpdate.Quantity = Convert.ToInt32(Console.ReadLine());
                SaveItems(items);
                Console.WriteLine("Item updated successfully!");
            }
            else
            {
                Console.WriteLine("Item not found.");
            }
        }

        public static void RemoveItem()
        {
            Console.Write("Enter item ID to remove: ");
            int id = Convert.ToInt32(Console.ReadLine());

            List<Item> items = LoadItems();
            Item itemToRemove = items.Find(i => i.Id == id);
            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
                SaveItems(items);
                Console.WriteLine("Item removed successfully!");
                
            }
            else
            {
                Console.WriteLine("Item not found.");
            }
        }


        public static List<Item> LoadItems()
        {
            if (File.Exists(itemsFile))
            {
                string jsonData = File.ReadAllText(itemsFile);
                return JsonConvert.DeserializeObject<List<Item>>(jsonData);
            }
            else
            {
                return new List<Item>();
            }
        }

        public static void SaveItems(List<Item> items)
        {
            string jsonData = JsonConvert.SerializeObject(items, Formatting.Indented);
            File.WriteAllText(itemsFile, jsonData);
        }
    }
}
