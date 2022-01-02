using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;

        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }

        public async Task<CastResponseModel> GetCastById(int id)
        {
            var cast = await _castRepository.GetById(id);
            if (cast != null)
            {
                var castModel = new CastResponseModel
                {
                    Id = id,
                    Name = cast.Name,
                    PosterUrl = cast.ProfilePath,
                    Character = cast.MoviesOfCast.Take(1).ToString(),
                };
                return castModel;
            }
            return null;
        }
    }
}
