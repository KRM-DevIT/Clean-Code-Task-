using System;


// 1- Meaningful Names
// 2- Single Responsibility Principle

namespace UserRegisteration
{
    public static class Rules
    {
        public const int MaxNameLen = 50;
        public const int MinAge = 18;
        public const int MaxAge = 100;
        public const int MinPasswordLen = 6;
    }

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

    class Program
    {
        static void Main(string[] args)
        {
            var validator = new UserValidator();

            Console.WriteLine("Please fill in the following form to complete registration.\n");

            string name = ReadAndValidate("Name", validator.ValidateName, "Invalid name");
            string email = ReadAndValidate("Email", validator.ValidateEmail, "Invalid email");
            string phone = ReadAndValidate("Phone", validator.ValidatePhoneNumber, "Invalid phone");
            int age = ReadAndValidateAge("Age", validator.ValidateAge, "Invalid age");
            string pass = ReadAndValidatePassword(validator);

            Console.WriteLine("\nRegistration completed successfully!");
        }

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