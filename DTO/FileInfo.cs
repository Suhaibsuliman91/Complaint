using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class FileInfo
    {
        public string? ContentType { get; set; }
        public byte[]? Bytes { get; set; }
        public IFormFile? File { get; set; }
        public string? FileName { get; set; }
        public string? Extension { get; set; }
        public string? FullFileName { get; set; }
    }
}
