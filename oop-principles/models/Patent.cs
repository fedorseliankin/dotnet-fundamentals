using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace oop_principles.models
{
    public class Patent : IDocument
    {
        public string CardNumber { get; set; }
        public string Title { get; set; }
        public string[] Authors { get; set; }
        public string DatePublished { get; set; }
        public string ExpirationDate { get; set; }
        public string UniqueId { get; set; }

        public string GetCardInfo()
        {
            return $"[Patent] Title: {Title},\nAuthors: {string.Join(", ", Authors)},  \n" +
                $" \nDate Published: {DatePublished},  \nExpiration Date: {ExpirationDate}, " +
                $" \nUnique Id: {UniqueId}";
        }
    }
}
