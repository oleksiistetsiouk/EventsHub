using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace EventsHub.Mobile.Models
{
    public class Film
    {
        public int FilmId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string Cast { get; set; }
        public string Year { get; set; }
        public string PosterUrl { get; set; }
        public string TrailerUrl { get; set; }
        public List<Cinema> Cinemas { get; set; }
    }
}
