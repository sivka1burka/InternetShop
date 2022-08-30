using System;

namespace WebApplication6.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public byte[] Image { get; set; }
    }
}
