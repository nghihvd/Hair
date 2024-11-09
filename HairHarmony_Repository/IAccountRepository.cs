using HairHarmony_BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairHarmony_Repository
{
    public interface IAccountRepository
    {
        public Account getAccountByID(String id);
        public List<Account> getAllAccounts();

        public bool RegisAccount(Account account);

        public bool UpdateSylistAcc(Account account);
        public bool EnableStylist(string accountID);

        public List<Account> getStylistAcc();

        public void UpdateAccount(Account account);

        public bool UpdateManagerAcc(Account account);

        public List<Account> GetStylists();

    }
}
