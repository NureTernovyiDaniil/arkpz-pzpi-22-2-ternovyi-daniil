﻿using System.Text.Json.Serialization;

namespace ChefMate_backend.Models
{
    public class MenuItemDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsAvailable { get; set; }
        public Guid MenuId { get; set; }
    }
}
