using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication6.Models
{
    public class ConvertImageToByte
    {
        public static byte[] ConvertImage(string path)
        {
            byte[] masByte = File.ReadAllBytes(path);
            return masByte;

        }
    }
}
