using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    // T is template with generic contraint where T should be a entity class
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        List<T> GetAll();
        T Add(T entity);
        T Update(T entity);
        T Delete(int id);

    }
}
