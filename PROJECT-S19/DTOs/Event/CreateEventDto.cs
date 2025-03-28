﻿using System.ComponentModel.DataAnnotations;

namespace PROJECT_S19.DTOs.Event
{
    public class CreateEventDto
    {
        [Required]
        [StringLength(50)]
        public required string Title { get; set; }
        [Required]
        public required DateTime Date { get; set; }
        [Required]
        [StringLength(50)]
        public required string Location { get; set; }
        [Required]
        public int ArtistId { get; set; }
    }
}
