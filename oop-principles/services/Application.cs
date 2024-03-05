using oop_principles.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_principles.services
{
    public sealed class Application
    {
        private readonly IDocumentStorageService _storageService;

        public Application(IDocumentStorageService storageService) => _storageService = storageService;

        public void Run()
        {
            Console.Write("Enter Card Number: ");
            var number = Console.ReadLine();

            Console.WriteLine($"Search result for card number {number}: ");
            var docs = _storageService.GetDocumentByNumber(number).Where(doc => doc is not null);
            foreach (var document in docs)
            {
                Console.Write(document.GetCardInfo());
            }
        }
    }
}
