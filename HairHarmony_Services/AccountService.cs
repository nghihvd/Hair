using HairHarmony_BusinessObject;
using HairHarmony_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairHarmony_Services
{
    public class AccountService : IAccountService
    {
        private IAccountRepository accountRepo;

        public AccountService()
        {
            accountRepo = new AccountRepository();
        }

        public bool EnableStylist(string accountID)
        {
            return accountRepo.EnableStylist(accountID);  
        }

        public Account getAccountByID(string id)
        {
            return accountRepo.getAccountByID(id);
        }


        public List<Account> getAllAccounts()
        {
            return accountRepo.getAllAccounts();
        }

        public bool RegisAccount(Account account)
        {
            return accountRepo.RegisAccount(account);
        }

        public bool UpdateSylistAcc(Account account)
        {
            return accountRepo.UpdateSylistAcc(account);
        }
        public List<Account> getStylistAcc()
        {
            return accountRepo.getStylistAcc();
        }
        public void UpdateAccount(Account account)
        {
            accountRepo.UpdateAccount(account);
        }
    }
}
