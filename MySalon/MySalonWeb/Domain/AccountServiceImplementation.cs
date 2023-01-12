//using MySalonWeb.Models;

//namespace MySalonWeb.Domain
//{
//    public class AccountServiceImplementation : AccountService
//    {
//        private List<Account> accounts;

//        public AccountServiceImplementation()
//        {
//            accounts = new List<Account>
//            {
//                new Account
//                {
//                UserName = "admin",
//                Password = "admin",
//                FullName = "admin"
//                },
//                new Account
//                {
//                UserName = "admin1",
//                Password = "123",
//                FullName = "admin1"
//                }
//            };
//        }

//        public Account Login(string username, string password)
//        {
//            return accounts.SingleOrDefault(s => s.UserName == username && s.Password == password);
//        }
//    }
//}
