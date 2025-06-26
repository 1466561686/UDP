using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace UDP.Cache
{
    class CustomCache
    {
        private static CustomCache m_CustomCache = null;
        private static ObjectCache cache = null;
        // 设置缓存策略
        private static CacheItemPolicy policy = null;


        public static CustomCache Instance
        {
            get
            {
                if (m_CustomCache == null)
                {
                    m_CustomCache = new CustomCache();
                }
                if (cache == null)
                {
                    cache = MemoryCache.Default;
                }
                if (policy == null)
                {
                    policy = new CacheItemPolicy
                    {
                        AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(50) // 绝对过期时间
                    };
                }
                return m_CustomCache;
            }
        }

        public void SetData(string key, List<DataModel> data)
        {
            CacheItem item = new CacheItem(key, data);
            cache.Set(item, policy);
        }

        public List<DataModel> GetData(String key)
        {
            List<DataModel> data = (List<DataModel>)cache.Get(key);
            return data;
        }
    }
}
