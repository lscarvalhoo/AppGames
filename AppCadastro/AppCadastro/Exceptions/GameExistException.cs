using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppCadastro.Exceptions
{
    public class GameExistException : Exception
    {
        public GameExistException() : base ("This game already exist !")
        { }
    }
}
