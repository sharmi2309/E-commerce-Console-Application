using ecommerce;
using System;

namespace ecommerce
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("***************************");
                Console.WriteLine("ECOMMERCE MANAGEMENT SYSTEM");
                Console.WriteLine("***************************");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Signup");
                Console.WriteLine("3. Exit");
                Console.Write("\nEnter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        User.Login();
                        break;
                    case 2:
                        User.Signup();
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
