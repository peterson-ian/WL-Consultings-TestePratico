using System.ComponentModel.DataAnnotations;
using WL_Consultings_TestePratico.Models.Exceptions;

namespace WL_Consultings_TestePratico.Models.DTOs.Paginacao
{
    public class PaginacaoParametros
    {
        private int _maximoTamanhoPagina = 50;
        private int _tamanhoPagina = 10;

        [Range(1, int.MaxValue, ErrorMessage = "O número da página deve ser maior ou igual a 1.")]
        public int NumeroPagina { get; set; } = 1;

        [Range(1, 50, ErrorMessage = "O tamanho da página deve estar entre 1 e 50.")]
        public int TamanhoPagina
        {
            get => _tamanhoPagina;
            set => _tamanhoPagina = value > _maximoTamanhoPagina ? _maximoTamanhoPagina : value;
        }
    }
}
