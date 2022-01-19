using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants57Blocks.Infrastructure.ProviderCache
{
    public class MemoryCacheProvider
    {
        static MemoryCacheProvider instance;
        private  MemoryCache _memoryCacheCache { get; set; }
        private MemoryCacheEntryOptions _memoryCacheEntryOptions { get; set; }
        protected MemoryCacheProvider()
        {
            _memoryCacheCache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = 1024,
            });

            _memoryCacheEntryOptions = new MemoryCacheEntryOptions()
                 .SetSize(1)
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(20));
        }

        public static MemoryCacheProvider Instance()
        {
            // Uses lazy initialization.
            // Note: this is not thread safe.
            if (instance == null)
            {
                instance = new MemoryCacheProvider();
            }
            return instance;
        }


        /// <summary>
        /// se encarga de crear el cache 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="informactio"></param>
        public string  SetCahe(string key, object informactio) 
        {
            if (!_memoryCacheCache.TryGetValue(key, out _))// Look for cache key.
            {
                _memoryCacheCache.Set(key, informactio, _memoryCacheEntryOptions);
            }
            return key;
        }

        /// <summary>
        /// Consulta la informacion almacenada en el Cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetCache(string key) 
        {
            return  _memoryCacheCache.Get(key);
        }
    }
}
