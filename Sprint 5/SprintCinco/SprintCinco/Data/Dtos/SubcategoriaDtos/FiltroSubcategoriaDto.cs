namespace IEcommerceAPI.Data.Dtos.SubcategoriaDtos
{
    public class FiltroSubcategoriaDto
    {
        public string Nome { get; set; }
        public string Ordem { get; set; }
        public int PaginaAtual { get; set; }
        public int PorPagina { get; set; }
        public bool? Status { get; set; } = null;
    }
}
