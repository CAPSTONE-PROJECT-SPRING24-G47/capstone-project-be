﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace capstone_project_be.Application.DTOs.RestaurantComments
{
    public class UpdateRestaurantCommentDTO
    {
        public required int UserId { get; set; }
        public required int RestaurantId { get; set; }
        public required float Stars { get; set; }
        public string? CommentContent { get; set; }

        public string? DeletePhotos { get; set; }
        [NotMapped]
        public IEnumerable<IFormFile>? Photos { get; set; }
    }
}
