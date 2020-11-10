using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace LSPWebAPI.APP_Context
{
    public static class Cache
    {

        /// <summary>
        /// 读取缓存数据
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <returns></returns>
        public static object GetCache(string cacheKey)
        {
            var objCache = HttpRuntime.Cache.Get(cacheKey);
            return objCache;
        }

        /// <summary>
        /// 设置缓存数据
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <param name="content">值</param>
        public static void SetCache(string cacheKey, object content)
        {
            var objCache = HttpRuntime.Cache;
            objCache.Insert(cacheKey, content);
        }

        /// <summary>
        /// 设置缓存数据并且设置默认过期时间
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="content"></param>
        /// <param name="timeOut"></param>
        public static void SetCache(string cacheKey, object content, int timeOut = 3600)
        {
            try
            {
                if (content == null)
                {
                    return;
                }
                var objCache = HttpRuntime.Cache;
                //设置绝对过期时间
                //绝对时间过期。DateTime.Now.AddSeconds(10)表示缓存在3600秒后过期，TimeSpan.Zero表示不使用平滑过期策略。
                objCache.Insert(cacheKey, content, null, DateTime.Now.AddSeconds(timeOut), TimeSpan.Zero, CacheItemPriority.High, null);
                //相对过期
                //DateTime.MaxValue表示不使用绝对时间过期策略，TimeSpan.FromSeconds(10)表示缓存连续10秒没有访问就过期。
                //objCache.Insert(cacheKey, objObject, null, DateTime.MaxValue, timeout, CacheItemPriority.NotRemovable, null); 
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 移除指定缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        public static void RemoveAllCache(string cacheKey)
        {
            var objCache = HttpRuntime.Cache;
            objCache.Remove(cacheKey);
        }

        /// <summary>
        /// 删除全部缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            var objCache = HttpRuntime.Cache;
            var cacheEnum = objCache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                objCache.Remove(cacheEnum.Key.ToString());
            }
        }

    }
}
