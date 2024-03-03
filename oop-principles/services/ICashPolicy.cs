using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_principles.services
{
    public interface ICachePolicy
    {
        TimeSpan? GetExpiration(string documentType);
    }
    public class DocumentCachePolicy : ICachePolicy
    {
        private Dictionary<string, TimeSpan?> _expirationTimes;

        public DocumentCachePolicy()
        {
            _expirationTimes = new Dictionary<string, TimeSpan?>
        {
            { "book", TimeSpan.FromMinutes(30) },
            { "localizedBook", TimeSpan.FromMinutes(30) },
            { "patent", TimeSpan.FromDays(1) },
            { "magazine", null },  // Null value for no expiration.
        };
        }

        public TimeSpan? GetExpiration(string documentType)
        {
            return _expirationTimes.TryGetValue(documentType, out var expiration) ? expiration : null;
        }
    }
}
