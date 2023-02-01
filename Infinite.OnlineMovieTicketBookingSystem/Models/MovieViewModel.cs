using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infinite.OnlineMovieTicketBookingSystem.Models
{
	public class MovieViewModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Please provide movie name")]
		[Display(Name = "Movie Name")]
		public string MovieName { get; set; }
		[Required(ErrorMessage = "Please provide Production Name")]
		[Display(Name = "Movie Production")]
		public string ProductionName { get; set; }
		[Required(ErrorMessage = "Please provide Movie Description")]
		[Display(Name = "Movie Description")]
		public string MovieDescription { get; set; }
		[Required(ErrorMessage = "Please select the date ")]
		[Display(Name = "Release Date")]
		public DateTime ReleaseDate { get; set; }
		//public Genre Genre { get; set; }
		[Required(ErrorMessage = "Please Choose Genre Name ")]
		[Display(Name = "Genre Name")]
		public int? GenreGenreId { get; set; }
		//public Screen Screen { get; set; }
		[Required(ErrorMessage = "Please Choose Screen Name")]
		[Display(Name = "Screen Name")]
		public int? ScreenScreenId { get; set; }
		//	public Show Show { get; set; }
		[Required(ErrorMessage = "Please Choose Show Type ")]
		[Display(Name = "Show Time")]
		public int? ShowShowId { get; set; }
		public string GenreName { get; set; }
		public string ShowType { get; set; }
		public string ScreenName { get; set; }
		public List<GenreViewModel> Genres { get; set; }
		public List<ShowViewModel> shows { get; set; }
		public List<ScreenViewModel> screens { get; set; }

	}
}
