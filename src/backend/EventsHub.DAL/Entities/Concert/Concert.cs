﻿using System;

namespace EventsHub.DAL.Entities.Concert
{
    public class Concert
    {
        public int ConcertId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Price { get; set; }
        public string PosterUrl { get; set; }
        public string DirectLink { get; set; }
        public string Place { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
