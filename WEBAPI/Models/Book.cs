using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public int PublishYear { get; set; }
        public int ParperCount { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
