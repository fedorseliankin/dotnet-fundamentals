using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_principles.models
{
    public interface IDocument
    {
        string CardNumber { get; set; }
        string GetCardInfo();
    }
}
