using System;
using System.Collections.Generic;

namespace MovieDatabase.Models
{
    public enum Genre {comedy, romance, action, thriller, family, horror, western, scifi, war  }
    public class Movie
    {
        // declare the movie properties and a constructor to initialise a blank movie
        public string Title { get; set; }
        public int Duration { get; set; }
        public int Year { get; set; }
        public int Budget { get; set; }
        public string Director { get; set; }
        public string URL { get; set; }
        public List<string> Actors;
        public List<Genre> Genres;
        public int Rating { get; set; }


        public Movie()
        {
            Title = "";
            Duration = 0;
            Year = 0;
            Budget = 0;
            Director = "";
            Rating = 0;
            URL = "";
            Actors = new List<string>();
            Genres = new List<Genre>();
        }



    }

}
