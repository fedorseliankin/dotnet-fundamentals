using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_principles.models
{
    public class Book
        : IDocument
    {
        public string CardNumber { get; set; }
        public string Title { get; set; }

        public string GetCardInfo()
        {
            return $"[Book] Title: {Title}";
        }
    }
}
