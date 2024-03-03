using Newtonsoft.Json;
using oop_principles.models;

using System.Runtime.Caching;

namespace oop_principles.services
{
    public sealed class FileDocumentStorageService : IDocumentStorageService
    {
        private readonly MemoryCache _cache;
        private readonly ICachePolicy _cachePolicy;

        public FileDocumentStorageService(ICachePolicy cachePolicy)
        {
            _cache = new MemoryCache("DocumentCache");
            _cachePolicy = cachePolicy;
        }

        IEnumerable<IDocument> IDocumentStorageService.GetDocumentByNumber(string CardNumber)
        {
            var filePaths = Directory.EnumerateFiles("data", $"*_{CardNumber}.json");

            foreach (var filePath in filePaths)
            {
                var cacheItem = _cache.GetCacheItem(filePath);
                if (cacheItem != null)
                {
                    yield return (IDocument)cacheItem.Value;
                }

                var type = new FileInfo(filePath).Name.Split('_')[0];
                IDocument doc = type switch
                {
                    "book" => JsonConvert.DeserializeObject<Book>(File.ReadAllText(filePath)),
                    "localizedBook" => JsonConvert.DeserializeObject<LocalizedBook>(File.ReadAllText(filePath)),
                    "patent" => JsonConvert.DeserializeObject<Patent>(File.ReadAllText(filePath)),
                    "magazine" => JsonConvert.DeserializeObject<Magazine>(File.ReadAllText(filePath)),
                    _ => throw new Exception("unknown file format"),
                };

                var policy = new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTimeOffset.UtcNow.Add(
                        _cachePolicy.GetExpiration(type) ?? TimeSpan.FromDays(1))
                };
                _cache.Add(filePath, doc, policy);
                yield return doc;
            }

            yield return null;
        }
    }
}
