namespace WL_Consultings_TestePratico.Models.DTOs.Paginacao
{
    public class PaginacaoResponse<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalItems { get; set; }
        public int NumeroPagina { get; set; }
        public int TotalPaginas { get; set; }
        public bool TemPaginaSequente { get; set; }
        public bool TemPaginaAnterior { get; set; }

    }
}
