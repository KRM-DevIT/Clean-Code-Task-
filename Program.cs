using System;

// 1- Meaningful Names

namespace UserRegisteration
{
    public static class Rules
    {
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
    public class UserValidator
    {
        public bool ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;
            if (name.Length > Rules.MaxNameLen) return false;
            return true;
        }

        public bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            if (!email.Contains("@")) return false;
            return true;
        }

        public bool ValidatePhoneNumber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            if (phone.Length < 7) return false;
            return true;
        }

        public bool ValidateAge(int age)
        {
            if (age < Rules.MinAge) return false;
            if (age > Rules.MaxAge) return false;
            return true;
        }

        public bool ValidatePassword(string password, string confirm)
        {
            if (password.Length < Rules.MinPasswordLen) return false;
            if (password != confirm) return false;
            return true;
        }
    }

    // 2- Single Responsibility Principle
    class Program
    {
        static void Main(string[] args)
        {
            var validator = new UserValidator();
            var user = new User();

            Console.WriteLine("Please fill in the following form to complete registration.\n");

            user.Name = ReadAndValidate("Name", validator.ValidateName, "Invalid name");
            user.Email = ReadAndValidate("Email", validator.ValidateEmail, "Invalid email");
            user.Phone = ReadAndValidate("Phone", validator.ValidatePhoneNumber, "Invalid phone");
            user.Age = ReadAndValidateAge("Age", validator.ValidateAge, "Invalid age");
            user.Password = ReadAndValidatePassword(validator);

            Console.WriteLine("\nRegistration completed successfully!");
        }

        // 2- Single Responsibility Principle
        static int ReadAndValidateAge(string field, Func<int, bool> validate, string error)
        {
            while (true)
            {
                Console.Write($"{field}: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int age) && validate(age))
                    return age;

                Console.WriteLine(error);
            }
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
        static string ReadAndValidatePassword(UserValidator validator)
        {
            while (true)
            {
                Console.Write("Password: ");
                string pass = Console.ReadLine();

                Console.Write("Confirm Password: ");
                string confirm = Console.ReadLine();

                if (validator.ValidatePassword(pass, confirm))
                    return pass;

                Console.WriteLine("Invalid password or passwords do not match");
            }
        }
    }
}