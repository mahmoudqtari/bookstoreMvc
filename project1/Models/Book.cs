﻿using System.ComponentModel.DataAnnotations;

namespace project1.Models
{
    public class Book
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string Title { get; set; } = null!;

        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public string Publisher { get; set; } = null!;
        public DateTime PublishDate { get; set; }
        public string? imageUrl {  get; set; }
        public string Description { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
        public List<BookCategory> Categories { get; set; } = new List<BookCategory>();

    }
}
