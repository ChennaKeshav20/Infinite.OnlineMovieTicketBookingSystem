using System.Net.Sockets;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Infinite.OnlineMovieTicketBookingSystem.Models
{
	public class PaymentViewModel
	{
        [Required]
        [Display(Name = "PaymentId")]
        public int PaymentId { get; set; }
        [Required]
        [Display(Name = "Credit/Debit Card Number")]
        [DataType(DataType.CreditCard)]
        public string CreditCardNumber { get; set; }
        [Required]
        [Display(Name = "Expiration Month and Year")]
        public string MonthAndYear { get; set; }
        [Required]
        [Display(Name = "CVV")]
        public int Cvv { get; set; }
        [Required]
        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
        [Required]
        [Display(Name = "TicketId")]
        public int TicketId { get; set; }
        [Required]
        [Display(Name = "Number of Seats")]
        public string NoOfSeats { get; set; }
        public int Price { get; set; }


  //      public int Amount
		//{
		//	get
		//	{
		//		return Convert.ToInt32(NoOfSeats) * Price;
		//	}
		//}
	}
}
