using Microsoft.EntityFrameworkCore;
using OrderManager.Db.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Db.Repositories
{
    public class ProviderDbRepository : IProviderRepository
    {
        private readonly DatabaseContext databaseContext;
        public ProviderDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public async Task<List<Provider>> GetAllAsync()
        {
            return await databaseContext.Providers.ToListAsync();
        }
        public async Task<Provider> TryGetByIdAsync(int id)
        {
            return await databaseContext.Providers.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
