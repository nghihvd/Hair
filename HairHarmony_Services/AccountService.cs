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
    }
}
