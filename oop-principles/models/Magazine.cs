using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_principles.models
{
    public class Magazine : IDocument
    {
        public string CardNumber { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string ReleaseNumber { get; set; }
        public string PublishDate { get; set; }

        public string GetCardInfo()
        {
            return $"[Magazine] Title: {Title}, Publisher: {Publisher}, " +
                $"Release Number: {ReleaseNumber}, Publish Date: {PublishDate}";
        }
    }
}
