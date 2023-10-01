using NPoco;
using System.Text.RegularExpressions;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace FusionCacheTools.BackOffice.Models
{
    [PrimaryKey("Id", AutoIncrement = false)]
    [ExplicitColumns]
    public class SqlCacheItem
    {
        [PrimaryKeyColumn(AutoIncrement = false)]
        [Column("Id")]
        [Length(449)]
        public string Id { get; set; }

        [Column("Value")]
        public byte[] Value { get; set; }

        [Column("ExpiresAtTime")]
        public DateTimeOffset ExpiresAtTime { get; set; }

        [Column("SlidingExpirationInSeconds")]
        public long SlidingExpirationInSeconds { get; set; }

        [Column("AbsoluteExpiration")]
        public DateTimeOffset AbsoluteExpiration { get; set; }

        public string CacheKey { get { return GetNormalisedCacheKey(Id); } }

        /// <summary>
        /// Removes trailing versioning from the cache key that is automatically added
        /// by distributed cache (maybe just SQL cache?) when it adds it.
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        private static string GetNormalisedCacheKey(string cacheKey)
        {
            // The cache keys are in the format v1:somekeyhere
            // where v1: is prepended automatically and shouldn't
            // be included when looking up the cache
            string pattern = @"^v\d+:(.*)";
            Match match = Regex.Match(cacheKey, pattern);
            // If we have it, take the string after the version. If not, just return the key
            if (match.Success)
                cacheKey = match.Groups[1].Value;

            return cacheKey;
        }
    }
}
