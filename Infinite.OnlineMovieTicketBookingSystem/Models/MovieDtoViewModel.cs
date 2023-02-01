using System;

namespace Infinite.OnlineMovieTicketBookingSystem.Models
{
	public class MovieDtoViewModel
	{
		public int Id { get; set; }
		public string MovieName { get; set; }
		public string ProductionName { get; set; }
        public string MovieDescription { get; set; }

        public DateTime ReleaseDate { get; set; }
		public int GenreGenreId { get; set; }
		public string GenreName { get; set; }
		public int ShowShowId { get; set; }
		public string ShowType { get; set; }
        public int ScreenScreenId { get; set; }
        public string ScreenName { get; set; }

    }
}
