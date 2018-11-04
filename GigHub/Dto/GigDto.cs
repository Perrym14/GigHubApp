﻿using System;

namespace GigHub.Dto
{
    public class GigDto
    {
        public int Id { get; set; }
        public bool IsCanceled { get; set; }
        public UserDto Artist { get; set; }
        public string Venue { get; set; }
        public DateTime DateTime { get; set; }
        public GenreDto Genre { get; set; }
    }
}