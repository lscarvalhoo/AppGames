using AppCadastro.Entities;
using AppCadastro.Exceptions;
using AppCadastro.InputModel;
using AppCadastro.Repositories;
using AppCadastro.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppCadastro.Service
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<List<GameViewModel>> Get(int page, int quantity)
        {
            var games = await _gameRepository.Get(page, quantity);

            return games.Select(game => new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            }).ToList();
        }

        public async Task<GameViewModel> Get(Guid id)
        {
            var game = await _gameRepository.Get(id);
            if (game == null)
                return null;

            return new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            };
        }

        public async Task<GameViewModel> Insert(GameInputModel game)
        {
            var entitieGame = await _gameRepository.Get(game.Name, game.Producer);

            if (entitieGame.Count > 0)
                throw new GameExistException();

            var gameInsert = new Game
            {
                Id = Guid.NewGuid(),
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            };

            await _gameRepository.Insert(gameInsert);

            return new GameViewModel
            {
                Id = gameInsert.Id,
                Name = game.Name,
                Producer = gameInsert.Producer,
                Price = game.Price
            };
        }

        public async Task Refresh(Guid id, GameInputModel game)
        {
            var entitieGame = await _gameRepository.Get(id);

            if(entitieGame == null)
                throw new GameNotExistException();

            entitieGame.Name = game.Name;
            entitieGame.Producer = game.Producer;
            entitieGame.Price = game.Price;

            await _gameRepository.Refresh(entitieGame);

        }

        public async Task Refresh(Guid id, double price)
        {
            var entitieGame = await _gameRepository.Get(id);

            if (entitieGame == null)
                throw new GameNotExistException();

            entitieGame.Price = price;

            await _gameRepository.Refresh(entitieGame);
        }

        public async Task Remove(Guid id)
        {
            var game = await _gameRepository.Get(id);

            if (game == null)
                return;

            await _gameRepository.Remove(id);
        }

        public void Dispose()
        {
            _gameRepository?.Dispose();
        }
    }
}
