﻿using System;
namespace MyFirstAPI.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public int UserId { get; set; }

    }
}