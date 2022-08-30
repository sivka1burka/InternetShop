using System;

namespace WebApplication6.Models
{
    public class Review
    {
        public int Id { get; set; }
        public User user { get; set; }
        public int Stars { get; set; }
        public string Description { get; set; }
        public DateTime dateOfWriting { get; set; }
        public int ProductId { get; set; }
    }
}
