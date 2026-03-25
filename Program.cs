using System;
using System.Text.RegularExpressions;

namespace UserRegistration // ✅ Consistent naming: corrected spelling
{
    // ✅ Consistent formatting: PascalCase for constants
    public static class Rules
    {
        public const int MinNameLength = 3;
        public const int MaxNameLength = 50;
        public const int MinAge = 18;
        public const int MaxAge = 100;
        public const int MinPasswordLength = 6;
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
            if (name.Length < Rules.MinNameLength || name.Length > Rules.MaxNameLength) return false;
            return true;
        }

        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            // ✅ Consistent formatting: regex pattern clearly named
            string emailPattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }

        public static bool ValidatePhoneNumber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;

            foreach (char c in phone)
            {
                if (!char.IsDigit(c)) return false;
            }

            if (phone.Length < 7) return false;
            return true;
        }

        public static bool ValidateAge(string age)
        {
            // ✅ Graceful error handling: TryParse avoids exceptions
            if (!int.TryParse(age, out int ageValue)) return false;

            if (ageValue < Rules.MinAge) return false;
            if (ageValue > Rules.MaxAge) return false;
            return true;
        }

        public static bool ValidatePassword(string password, string confirmPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword)) return false;
            if (password.Length < Rules.MinPasswordLength) return false;
            if (password != confirmPassword) return false;
            return true;
        }
    }

    // 2- Single Responsibility Principle
    class Program
    {
        static void Main(string[] args)
        {
            try // ✅ Graceful error handling: catch unexpected runtime errors
            {
                var user = new User();

                Console.WriteLine("Please fill in the following form to complete registration.\n");

                user.Name = ReadAndValidate("Name", UserValidator.ValidateName, "Invalid name");
                user.Email = ReadAndValidate("Email", UserValidator.ValidateEmail, "Invalid email");
                user.Phone = ReadAndValidate("Phone", UserValidator.ValidatePhoneNumber, "Invalid phone");

                // ✅ Graceful error handling: avoid direct int.Parse
                string ageInput = ReadAndValidate("Age", UserValidator.ValidateAge, "Invalid age");
                user.Age = int.TryParse(ageInput, out int ageValue) ? ageValue : 0;

                user.Password = ReadAndValidatePassword(UserValidator.ValidatePassword);

                Console.WriteLine("\nRegistration completed successfully!");
            }
            catch (Exception ex) // ✅ Graceful error handling
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

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
