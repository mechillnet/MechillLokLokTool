using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Domain.Common.Movie
{
    public class MovieAttribute
    {
        public MovieAttribute()
        {
            MovieStatusAvailabels = new List<MovieAttributeStatus>();
            MovieStatusAvailabels.Add(new MovieAttributeStatus { Id = 1, Name="Đang Cập Nhật"});
            MovieStatusAvailabels.Add(new MovieAttributeStatus { Id = 2, Name = "Trailer" });
            MovieStatusAvailabels.Add(new MovieAttributeStatus { Id = 3, Name = "Sắp Chiếu" });
            MovieStatusAvailabels.Add(new MovieAttributeStatus { Id = 4, Name = "Đang Chiếu" });
            MovieStatusAvailabels.Add(new MovieAttributeStatus { Id = 5, Name = "Hoàn Tất" });

            MovieTypeAvailabels = new List<MovieAttributeType>();
            MovieTypeAvailabels.Add(new MovieAttributeType { Id = 1, Name = "Phim Lẻ" });
            MovieTypeAvailabels.Add(new MovieAttributeType { Id = 2, Name = "Phim Bộ" });

            //MovieSupplier = new List<MovieSupplier>();
            //MovieSupplier.Add(new MovieSupplier { Id = 1, Name = "Phim 77",IsFrame=false });
            //MovieSupplier.Add(new MovieSupplier { Id = 2, Name = "Dailymotion", IsFrame = true });
            //MovieSupplier.Add(new MovieSupplier { Id = 3, Name = "Facebook", IsFrame = false });
        }
        public List<MovieAttributeType> MovieTypeAvailabels { get; set; }
        public List<MovieAttributeStatus> MovieStatusAvailabels { get; set; }
        //public List<MovieSupplier> MovieSupplier { get; set; }
    }
    public class MovieAttributeType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class MovieAttributeStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    //public class MovieSupplier
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public bool IsFrame { get; set; }
    //}
}
