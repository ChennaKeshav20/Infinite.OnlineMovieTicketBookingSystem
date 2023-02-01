using Infinite.OnlineMovieTicketBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Data.SqlClient;


namespace Infinite.OnlineMovieTicketBookingSystem.Controllers
{
	public class MoviesController : Controller
	{
		private readonly IConfiguration _configuration;

		public MoviesController(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public async Task<IActionResult> Index()
		{
			List<MovieViewModel> movies = new();
			using (var client = new HttpClient())
			{
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
				var result = await client.GetAsync("Movies/GetAllMovies");
				if (result.IsSuccessStatusCode)
				{
					movies = await result.Content.ReadAsAsync<List<MovieViewModel>>();
				}
			}
			return View(movies);

		}
		[HttpGet("Create")]
		public async Task<IActionResult> Create()
		{
			//client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
			MovieViewModel viewModel = new MovieViewModel
			{
				Genres = await this.GetGenres(),
				shows = await this.GetShow(),
				screens = await this.GetScreen()
			};
			return View(viewModel);
		}
		[HttpPost("Create")]

		public async Task<IActionResult> Create(MovieViewModel movie)
		{
			if (ModelState.IsValid)
			{
				using (var client = new HttpClient())
				{
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    client.BaseAddress = new Uri(_configuration["ApiUrl:api"]);
					var result = await client.PostAsJsonAsync("Movies/Create", movie);
					if (result.StatusCode == System.Net.HttpStatusCode.Created)
					{
						return RedirectToAction("Index");

					}
				}
			}
			MovieViewModel viewModel = new MovieViewModel
			{
				Genres = await this.GetGenres(),
				shows = await this.GetShow(),     
				screens = await this.GetScreen()

			};
			return View(viewModel);
		}

		public async Task<IActionResult> Details(int id)
		{
			MovieDtoViewModel movie = null;
			using (var client = new HttpClient())
			{
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
				var result = await client.GetAsync($"Movies/GetMovieById/{id}");
				if (result.IsSuccessStatusCode)
				{
					movie = await result.Content.ReadAsAsync<MovieDtoViewModel>();
				}
			}
			return View(movie);
		}
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{ 
             
		
			if (ModelState.IsValid)
			{
                MovieViewModel movie = null;


                using (var client = new HttpClient())
				{
					client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
					client.BaseAddress = new Uri(_configuration["ApiUrl:api"]);
					var result = await client.GetAsync($"Movies/GetMovieById/{id}");
					if (result.IsSuccessStatusCode)
					{
						movie = await result.Content.ReadAsAsync<MovieViewModel>();
						movie.Genres = await this.GetGenres();
						movie.screens = await this.GetScreen();
						movie.shows = await this.GetShow();
						return View(movie);
					}
					else
					{
						ModelState.AddModelError("", "Movie doesn't exist");
					}
				}
			}
			
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Edit(int id, MovieViewModel movie)
		{

			if (ModelState.IsValid)
			{
				using (var client = new HttpClient())
				{
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                    client.BaseAddress = new Uri(_configuration["ApiUrl:api"]);
					var result = await client.PutAsJsonAsync($"Movies/UpdateMovie/{movie.Id}", movie);
					if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
					{
						return RedirectToAction("Index");
					}
					else
					{
						ModelState.AddModelError("", "Server Error. Please try again later");
					}
				}
			}
			MovieViewModel viewModel = new MovieViewModel
			{
				Genres = await this.GetGenres(),
				shows = await this.GetShow(),
				screens = await this.GetScreen()

			};
			return View(viewModel);
		}
		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			MovieViewModel movie = null;
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
				client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
				var result = await client.GetAsync($"Movies/GetMovieById/{id}");
				if (result.IsSuccessStatusCode)
				{
					movie = await result.Content.ReadAsAsync<MovieViewModel>();
				}
				else
				{
					ModelState.AddModelError("", "Server Error. Please try later");
				}
			}
			return View(movie);
		}
		[HttpPost]
		public async Task<IActionResult> Delete(MovieViewModel movie)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
				client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
				var result = await client.DeleteAsync($"Movies/Delete/{movie.Id}");
				if (result.IsSuccessStatusCode)
				{
					return RedirectToAction("Index");
				}
				else
				{
					ModelState.AddModelError("", "Server Error. Please try again later");
				}
			}
			return View();
		}
		[NonAction]
		public async Task<List<GenreViewModel>> GetGenres()
		{
			List<GenreViewModel> genres = new();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
				var result = await client.GetAsync("Movies/GetGenres");
				if (result.IsSuccessStatusCode)
				{
					genres = await result.Content.ReadAsAsync<List<GenreViewModel>>();
				}
			}
			return genres;
		}
		[NonAction]
		public async Task<List<ShowViewModel>> GetShow()
		{
			List<ShowViewModel> show = new();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
				var result = await client.GetAsync("Movies/GetShow");
				if (result.IsSuccessStatusCode)
				{
					show = await result.Content.ReadAsAsync<List<ShowViewModel>>();
				}
			}
			return show;
		}
		[NonAction]
		public async Task<List<ScreenViewModel>> GetScreen()
		{
			List<ScreenViewModel> screen = new();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
				var result = await client.GetAsync("Movies/GetScreen");
				if (result.IsSuccessStatusCode)
				{
					screen = await result.Content.ReadAsAsync<List<ScreenViewModel>>();
				}
			}
			return screen;
		}


        // RestinPease
        [HttpGet]
        public IActionResult CreateTicket()
        {
			//TicketViewModel = new TicketViewModel() { };
			return View() ;
        }
		[HttpPost]

		public async Task<IActionResult> CreateTicket(TicketViewModel ticket)
		{
			if (ModelState.IsValid)
			{
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(_configuration["ApiUrl:api"]);
					var result = await client.PostAsJsonAsync("Movies/CreateTicket", ticket);
					if (result.StatusCode == System.Net.HttpStatusCode.Created)
					{
						return RedirectToAction("Index");

					}
				}
			}

			return View();
		}

        [HttpGet]
        public  IActionResult CreatePayment()
        {
            
            return View();
        }
		[HttpPost("PaymentCreation")]

		public async Task<IActionResult> CreatePayment(PaymentViewModel payment)
		{
			if (ModelState.IsValid)
			{
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(_configuration["ApiUrl:api"]);
					var result = await client.PostAsJsonAsync("Movies/PaymentCreation", payment);
					if (result.StatusCode == System.Net.HttpStatusCode.Created)
					{
						return RedirectToAction("Index");

					}
				}
			}
			return View();
		}




		//trial


		public async Task<IActionResult> IndexUser()
		{
			List<MovieViewModel> movies = new();
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
				client.BaseAddress = new System.Uri(_configuration["ApiUrl:api"]);
				var result = await client.GetAsync("Movies/GetAllMovies");
				if (result.IsSuccessStatusCode)
				{
					movies = await result.Content.ReadAsAsync<List<MovieViewModel>>();
				}
			}
			return View(movies);

		}
	}
}



