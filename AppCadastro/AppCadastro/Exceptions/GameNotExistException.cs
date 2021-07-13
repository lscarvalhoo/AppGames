using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppCadastro.Exceptions
{
    public class GameNotExistException : Exception
    {
        public GameNotExistException() : base("This game not exist !")
        { }
    }
}
