using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBettingSystem.Web.ViewModels.Account
{
    public class AccountViewModel
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;

        public DateTime CreatedOn { get; set; }
        public decimal WalletBalance { get; set; }
    }
}
