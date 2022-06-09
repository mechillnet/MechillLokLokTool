namespace PhamNgocBau.Api.Model.Cast
{
    public class CastMappingModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CastId { get; set; }
        public string CastName { get; set; }
        public string CastLink { get; set; }
        public string CastAvatar { get; set; }
        public string CastAvatarThumb { get; set; }
        public int? CastAvatarId { get; set; }
        public string CdnAvatar { get; set; }
        public string CdnAvatarThumb { get; set; }
    }
}
