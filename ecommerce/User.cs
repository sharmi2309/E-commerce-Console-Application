using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ecommerce
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        static string usersFile = @"C:\Users\10decoders\Downloads\ecommerce\ecommerce\users.json";

        public static void Login()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            Console.Write("Enter role (admin/user): ");
            string role = Console.ReadLine().ToLower();

            List<User> users = LoadUsers();

            if (users.Count == 0)
            {
                Console.WriteLine("No users loaded.");
                return;
            }

            User user = users.Find(u => u.Name.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                                        u.Password.Equals(password, StringComparison.Ordinal) &&
                                        u.Role.Equals(role, StringComparison.OrdinalIgnoreCase));

            if (user != null)
            {
               
                Console.WriteLine("\n*****Login successful!*****");
                

                if (user.Role == "admin")
                {
                    AdminMenu();
                }
                else if (user.Role == "user")
                {
                    UserMenu();
                }
            }
            else
            {
                Console.WriteLine("\nInvalid username, password,role/Please Sign Up" );
            }
        }

        public static void Signup()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            Console.Write("Enter role (admin/user): ");
            string role = Console.ReadLine();
            List<User> users = LoadUsers();
            if (users.Exists(u => u.Name.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("\nUsername already exists. Please choose a different username.");
                return;
            }
            int newId = users.Count + 1;
            User newUser = new User { Id = newId, Name = username, Password = password, Role = role };
            users.Add(newUser);
            SaveUsers(users);
            Console.WriteLine("\n*****Signup successful!*****\n");                
        }

        static void AdminMenu()
        {
            while (true)
            {
                Console.WriteLine("\n*******************");
                Console.WriteLine("Admin Menu:");
                Console.WriteLine("*******************");
                Console.WriteLine("1. View Items");
                Console.WriteLine("2. Add Item");
                Console.WriteLine("3. Update Item");
                Console.WriteLine("4. Remove Item");
                Console.WriteLine("5. Logout");

                Console.Write("\nEnter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Item.ViewItems();
                        break;
                    case 2:
                        Item.AddItems();
                        break;
                    case 3:
                        Item.UpdateItem();
                        break;
                    case 4:
                        Item.RemoveItem();
                        break;
                    case 5:
                        Console.WriteLine("\nLogging out...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void UserMenu()
        {
            while (true)
            {
                Console.WriteLine("\n*******************");
                Console.WriteLine("User Menu:");
                Console.WriteLine("*******************");
                Console.WriteLine("1. View Items");
                Console.WriteLine("2. Add Item to Cart");
                Console.WriteLine("3. Remove Item from Cart");
                Console.WriteLine("4. View Cart");
                Console.WriteLine("5. Place Order");
                Console.WriteLine("6. Logout");

                Console.Write("\nEnter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Item.ViewItems();
                        break;
                    case 2:
                        Order.AddItemsToCart();
                        break;
                    case 3:
                        Order.RemoveItemFromCart();
                        break;
                    case 4:
                        Order.ViewCart();
                        break;
                    case 5:
                        Order.PlaceOrder();
                        break;
                    case 6:
                        Console.WriteLine("\nLogging out...");
                        return;
                    default:
                        Console.WriteLine("\nInvalid choice. Please try again.");
                        break;
                }
            }
        }

        static List<User> LoadUsers()
        {
            if (File.Exists(usersFile))
            {
                string jsonData = File.ReadAllText(usersFile);
                return JsonConvert.DeserializeObject<List<User>>(jsonData);
            }
            else
            {
                return new List<User>();
            }
        }

        static void SaveUsers(List<User> users)
        {
            string jsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(usersFile, jsonData);
        }
    }
}
