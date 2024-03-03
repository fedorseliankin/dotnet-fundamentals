using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace oop_principles.models
{
    public class LocalizedBook
        : IDocument
    {
        public string CardNumber { get; set; }
        public string Title { get; set; }

        // Напишите здесь значения уникальных свойств ...

        public string GetCardInfo()
        {
            return $"[LocalizedBook] Title: {Title}";
        }
    }
}
