namespace PhamNgocBau.Api.Model.Movie.Product
{
    public class CategoryMappingModel
    {
        public int? Id { get; set; }
        public int? ProductId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryLink { get; set; }
    }
}
