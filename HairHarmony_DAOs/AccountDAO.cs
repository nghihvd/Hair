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
                stylist.Salary = account.Salary;
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
            // account là thong tin mới 
            //manager chứa thong tin cũ 
            Account manager = SearchAccount(account.AccountId);
            if (manager != null)
            {
                if (!string.IsNullOrEmpty(account.Name) && !account.Name.ToString().Equals(manager.Name.ToString()))
                {
                    manager.Name = account.Name.ToString();
                }
                if (!string.IsNullOrEmpty(account.Email) && !account.Email.ToString().Equals(manager.Email.ToString())) 
                {
                    manager.Email = account.Email.ToString();
                }
                if (!string.IsNullOrEmpty(account.Phone) && !account.Phone.ToString().Equals(manager.Phone.ToString())) 
                {
                    manager.Phone = account.Phone.ToString();
                }
                if (!string.IsNullOrEmpty(account.Password) && !account.Password.ToString().Equals(manager.Password.ToString()))
                {
                    manager.Password = account.Password.ToString();
                }
                                
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

        public List<Account> GetStylists()
        {
                return dbContext.Accounts.Where(a => a.RoleId == 2).ToList();
            
        }

        public void UpdateAccountLoyaltyPoints(Account account)
        {
            try
            {
                dbContext.Accounts.Update(account);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating loyalty points.", ex);
            }
        }
    }
}
