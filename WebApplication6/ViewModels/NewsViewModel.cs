using Microsoft.AspNetCore.Http;
using System;

namespace WebApplication6.ViewModels
{
    public class NewsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public IFormFile Image { get; set; }
        public byte[] ImgByte { get; set; }
    }
}
