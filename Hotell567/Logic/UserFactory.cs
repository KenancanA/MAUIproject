using Hotell567.Models;
using System.Diagnostics;

namespace Hotell567.Logic

{
    public class UserFactory
    {
        private const int minimumUsernameLength = 4;
        private const int minimumPasswordLength = 8;
        private const int minimumPhoneNumberLength = 5;

        public bool CheckIfUserCanBook()
        {
            User u = AppManager.currentUser;

            if (string.IsNullOrEmpty(u.username)) return false;
            if (string.IsNullOrEmpty(u.first_name)) return false;
            if (string.IsNullOrEmpty(u.last_name)) return false;
            if (string.IsNullOrEmpty(u.email)) return false;
            if (u.phone_number == 0) return false;
            if (string.IsNullOrEmpty(u.address_line_1)) return false;
            if (string.IsNullOrEmpty(u.address_line_2)) return false;
            if (string.IsNullOrEmpty(u.city)) return false;
            if (string.IsNullOrEmpty(u.state)) return false;
            if (u.postal_code == 0) return false;
            if (string.IsNullOrEmpty(u.country)) return false;

            return true;
        }


        public bool CheckIfCredentialsAreCorrect(string username, string password)
        {
            var user = AppManager.userDatabase.CheckDBForUser(username);

            if (user.username == username && user.password == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckIfUsernameExists(string username)
        {
            var user = AppManager.userDatabase.CheckDBForUser(username);

            if (user.username == username)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int CheckAuthorityLevel(string username)
        {
            User user = AppManager.userDatabase.CheckDBForUserType(username);
            return user.user_type;
        }

        public bool CheckIfAccountFieldsAreValid(string username, string password, string email)
        {
            if (IsValidUsername(username) == false)
            {
                return false;
            }
            if (IsValidEmail(email) == false)
            {
                return false;
            }
            if (IsValidPassword(password) == false)
            {
                return false;
            }

            return true;
        }

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return false;

            if (password.Length < minimumPasswordLength)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsValidUsername(string username)
        {
            if (string.IsNullOrEmpty(username)) return false;

            if (username.Length < minimumUsernameLength)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber)) return false;

            string cleanedNumber = new string(Array.FindAll(phoneNumber.ToCharArray(), (c => (char.IsDigit(c)))));

            Debug.WriteLine("Cleaned number is: " + cleanedNumber);

            if (cleanedNumber.Length < minimumPhoneNumberLength) return false;

            if (cleanedNumber.StartsWith("0")) return false;

            return true;
        }
    }
}
