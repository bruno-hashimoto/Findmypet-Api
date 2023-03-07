using System;
namespace FindMyPet.Models
{
	public class Feed
	{
        public int UserId { get; set; }

        public string? Image { get; set; }

        public string? Description { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public DateTime Date { get; set; }
    }
}

