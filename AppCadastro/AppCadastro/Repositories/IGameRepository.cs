using AppCadastro.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppCadastro.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> Get(int page, int quantity);
        Task<Game> Get(Guid id);
        Task<List<Game>> Get(string name, string producer);
        Task Insert(Game game);
        Task Refresh(Game game);
        Task Remove(Guid id);
    }
}
