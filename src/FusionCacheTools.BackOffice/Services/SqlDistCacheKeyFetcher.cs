using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using FusionCacheTools.BackOffice.Models;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace FusionCacheTools.BackOffice.Services
{
    public class SqlDistCacheKeyFetcher : ICacheKeyFetcher
    {
        public static string CACHE_CONNECTIONSTRING_NAME = "cacheDb";
        public string Name => "SQL";

        private readonly IConfiguration _configuration;
        public SqlDistCacheKeyFetcher(IConfiguration configuration) {
            _configuration = configuration;
        }

        public IEnumerable<FusionCachedObject> GetCacheKeys()
        {
            string connectionString = _configuration.GetConnectionString(CACHE_CONNECTIONSTRING_NAME);
            List<SqlCacheItem> cacheItems = new List<SqlCacheItem>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM CustomCache";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Build the object up from the row
                            SqlCacheItem cacheItem = new SqlCacheItem();
                            cacheItem.Id = reader.GetString(0);
                            if (!reader.IsDBNull(1))
                                cacheItem.Value = reader.GetSqlBinary(1).Value;
                            if (!reader.IsDBNull(2))
                                cacheItem.ExpiresAtTime = reader.GetDateTimeOffset(2);
                            if (!reader.IsDBNull(3))
                                cacheItem.SlidingExpirationInSeconds = reader.GetInt64(3);
                            if (!reader.IsDBNull(4))
                                cacheItem.AbsoluteExpiration = reader.GetDateTimeOffset(4);

                            yield return new FusionCachedObject()
                            {
                                Key = cacheItem.CacheKey,
                                Expiration = cacheItem.AbsoluteExpiration.UtcDateTime,
                                CachedTypeName = Name
                            };
                        }
                    }
                }
            }
        }
    }
}
