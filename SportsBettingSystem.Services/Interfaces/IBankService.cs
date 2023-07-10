using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBettingSystem.Services.Interfaces
{
	public interface IBankService
	{
		Task AddDepositAsync(string userId, decimal deposit);
	}
}
