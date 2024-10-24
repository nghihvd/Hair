using HairHarmony_BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HairHarmony_DAOs
{
    public class AccountDAO
    {
        private HairContext dbContext;
        private static AccountDAO instance = null;

        public static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountDAO();
                }
                return instance;
            }
        }

        public AccountDAO()
        {
            dbContext = new HairContext();
        }

        public List<Account> GetAccountList()
        {
            return dbContext.Accounts.ToList();
        }
        public Account SearchAccount(String accountID)
        {
            return dbContext.Accounts.SingleOrDefault(m => m.AccountId.Equals(accountID));
        }

        public bool RegisAccount(Account account)
        {
            bool result = false;
            Account search = SearchAccount(account.AccountId);
            if (search == null)
            {
                dbContext.Accounts.Add(account);
                dbContext.SaveChanges();
                result = true;
            }
            return result;
        }
        public List<Account> getStylistAcc()
        {

            return dbContext.Accounts.Where(a => a.RoleId == 2).ToList();
        }
        public bool UpdateSylistAcc(Account account)
        {

            bool isSuccess = false;
            Account stylist = SearchAccount(account.AccountId);
            if (stylist != null)
            {
                stylist.Level = account.Level;
                dbContext.Update(stylist);
                dbContext.SaveChanges();
                isSuccess = true;
            }
            return isSuccess;
        }

        public bool EnableStylist(string accountID)
        {
            bool isSuccess = false;
            Account stylist = SearchAccount(accountID);
            if (stylist != null)
            {
                stylist.RoleId = 3;
                dbContext.Update(stylist);
                dbContext.SaveChanges();
                isSuccess = true;
            }
            return isSuccess;
        }

        public List<Account> getManagerAcc()
        {
            return dbContext.Accounts.Where(a => a.RoleId == 1).ToList();
        }

        public bool UpdateManagerAcc(Account account)
        {

            bool isSuccess = false;
            Account manager = SearchAccount(account.AccountId);
            if (manager != null)
            {
                manager.Name = account.Name;
                manager.Email = account.Email;
                manager.Phone = account.Phone;
                manager.Password = account.Password;

                dbContext.Update(manager);
                dbContext.SaveChanges();
                isSuccess = true;
            }
            return isSuccess;
        }
        public void UpdateAccount(Account account)
        {
            var existingAccount = dbContext.Accounts.FirstOrDefault(a => a.AccountId == account.AccountId);

            if (existingAccount != null)
            {
                existingAccount.Name = account.Name;
                existingAccount.Email = account.Email;
                existingAccount.Phone = account.Phone;

                if (!string.IsNullOrEmpty(account.Password))
                {
                    existingAccount.Password = account.Password;
                }

                dbContext.SaveChanges();
            }
        }
    }
}
