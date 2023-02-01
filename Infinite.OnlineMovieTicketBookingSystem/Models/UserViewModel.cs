using System.Net.Sockets;

namespace Infinite.OnlineMovieTicketBookingSystem.Models
{
	public class UserViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public int MobileNo { get; set; }
		//public Ticket Ticket { get; set; }
		public int TicketTicketId { get; set; }
	}
}
