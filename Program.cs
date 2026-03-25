using System;
using System.Text.RegularExpressions;

// 1- Meaningful Names

namespace UserRegisteration
{

    //constants for validation rules
    public static class Rules
    {
        public const int MinNameLen = 3;
        public const int MaxNameLen = 50;
        public const int MinAge = 18;
        public const int MaxAge = 100;
        public const int MinPasswordLen = 6;
    }

    // 1- Meaningful Names
    // 2- Single Responsibility Principle
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }
    }

    // 2- Single Responsibility Principle
    public static class UserValidator
    {
        public static bool ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;
            if (name.Length < Rules.MinNameLen || name.Length > Rules.MaxNameLen) return false;
            return true;
        }

        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            //regular expression for basic email validation
            string emailPattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }

        public static bool ValidatePhoneNumber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            // check for characters other than digits
            foreach (char c in phone)
            {
                if (!char.IsDigit(c)) return false;
            }
            if (phone.Length < 7) return false;
            return true;
        }

        public static bool ValidateAge(string age)
        {
            // check if age is a valid integer
            if (!int.TryParse(age, out int ageValue)) return false;

            if (ageValue < Rules.MinAge) return false;
            if (ageValue > Rules.MaxAge) return false;
            return true;
        }


        public static bool ValidatePassword(string password, string confirmPassword)
        {
            if (password.Length < Rules.MinPasswordLen) return false;
            if (password != confirmPassword) return false;
            return true;
        }
    }

    // 2- Single Responsibility Principle
    class Program
    {
        static void Main(string[] args)
        {
            var user = new User();

            Console.WriteLine("Please fill in the following form to complete registration.\n");

            user.Name = ReadAndValidate("Name", UserValidator.ValidateName, "Invalid name");
            user.Email = ReadAndValidate("Email", UserValidator.ValidateEmail, "Invalid email");
            user.Phone = ReadAndValidate("Phone", UserValidator.ValidatePhoneNumber, "Invalid phone");
            user.Age = int.Parse(ReadAndValidate("Age", UserValidator.ValidateAge, "Invalid age"));
            user.Password = ReadAndValidatePassword(UserValidator.ValidatePassword);
            Console.WriteLine("\nRegistration completed successfully!");
        }

        // 2- Single Responsibility Principle
        static string ReadAndValidate(string field, Func<string, bool> validate, string error)
        {
            while (true)
            {
                Console.Write($"{field}: ");
                string input = Console.ReadLine();

                if (validate(input))
                    return input;

                Console.WriteLine(error);
            }
        }

        // 2- Single Responsibility Principle
        static string ReadAndValidatePassword(Func<string, string, bool> validate)
        {
            while (true)
            {
                Console.Write("Password: ");
                string pass = Console.ReadLine();

                Console.Write("Confirm Password: ");
                string confirm = Console.ReadLine();

                if (validate(pass, confirm))
                    return pass;

                Console.WriteLine("Invalid password or passwords do not match");
            }
        }
    }
}