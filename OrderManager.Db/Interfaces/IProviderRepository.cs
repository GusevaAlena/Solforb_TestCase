using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Db.Interfaces
{
    public interface IProviderRepository
    {
        Task<List<Provider>> GetAllAsync();
        Task<Provider> TryGetByIdAsync(int id);
    }
}
