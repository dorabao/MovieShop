using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CastRepository : Repository<Cast>, ICastRepository
    {
        public CastRepository(MovieShopDbContext dbContext) : base(dbContext)
        {

        }

        public async override Task<Cast> GetById(int id)
        {
            var cast = await _dbContext.Casts.FirstOrDefaultAsync(c => c.Id == id);
            if (cast == null) return null;
            return cast;
        }
    }
}
