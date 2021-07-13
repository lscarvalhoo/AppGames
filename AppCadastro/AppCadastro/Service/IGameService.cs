using AppCadastro.InputModel;
using AppCadastro.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppCadastro.Service
{
    public interface IGameService
    {
        Task<List<GameViewModel>> Get(int page, int quantity); 
        Task<GameViewModel> Get(Guid id);
        Task<GameViewModel> Insert(GameInputModel game);
        Task Refresh(Guid id, GameInputModel game);
        Task Refresh(Guid id, double price);
        Task Remove(Guid id);


    }
}
