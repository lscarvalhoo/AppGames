using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppCadastro.InputModel
{
    public class GameInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The game name must contain betwenn 3 and 100 characters")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The name Producer must contain betwenn 3 and 100 characters")]
        public string Producer { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "The price must be minimum 1 dolar and maximum 1000 dolars")]
        public double Price { get; set; }
    }
}
