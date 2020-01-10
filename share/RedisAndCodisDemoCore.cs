namespace WebApplication
{
    public static class RedisAndCodisDemo
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(RedisAndCodisDemo));

#if NETFRAMEWORK
       /// <summary>
        /// 初始化连接
        /// </summary>
        /// <param name="type">0为Codis基于zk连接,1为普通redis</param>
        public static void Init(int type = 0)
        {
            if (type == 0)
            {
                //初始化ZK连接信息
                Mango.CodisHA.RedisPoolBuilder.Init("192.168.4.79:2181", "codis-mango");

                log.Debug($"通过ZK连接Codis=>192.168.4.79:2181, codis - mango");
            }
            else
            {
                //初始化redis连接信息
                Mango.CodisHA.RedisPoolBuilder.InitRedis("192.168.4.79:6378");
                log.Debug($"通过直接连接Redis=>192.168.4.79:6378");
            }
        }
        /// <summary>
        /// 获取redis连接，实现redis读/写/删
        /// </summary>
        public static void GetRedisValue()
        {
            //获取redis连接数据库
            string value = "null";
            using (var redisClient = Mango.CodisHA.RedisPoolBuilder.GetClient())
            {
                redisClient.Db = 5;
                log.Debug($"选择db为{5}");
                redisClient.Set<string>("codisproxytest", "zhh");
                log.Debug($"进行一次写入=>key:codisproxytest,value:zhh");
                value = redisClient.Get<string>("codisproxytest");
                log.Debug($"进行一次读取=>key:codisproxytest,value:{value}");
                redisClient.Remove("codisproxytest");
                log.Debug($"进行一次删除=>key:codisproxytest");
            }
        }
        
#else
        /// <summary>
        /// 初始化连接
        /// </summary>
        /// <param name="type">0为Codis基于zk连接,1为普通redis</param>
        public static void Init(int type = 0)
        {
            if (type == 0)
            {
                //初始化ZK连接信息
                Mango.CodisHA.RedisPoolBuilder.Init("192.168.4.79:2181", "codis-mango");

                log.Debug($"通过ZK连接Codis=>192.168.4.79:2181, codis - mango");
            }
            else
            {
                //初始化redis连接信息
                Mango.CodisHA.RedisPoolBuilder.InitRedis("192.168.4.79:6378");
                log.Debug($"通过直接连接Redis=>192.168.4.79:6378");
            }
        }
        /// <summary>
        /// 获取redis连接，实现redis读/写/删
        /// </summary>
        public static void GetRedisValue()
        {
            //获取redis连接数据库
            var redisClient = Mango.CodisHA.RedisPoolBuilder.GetDatabase(5);
            log.Debug($"选择db为{5}");
            redisClient.StringSet("codisproxytest", "zhh");
            log.Debug($"进行一次写入=>key:codisproxytest,value:zhh");
            var value = redisClient.StringGet("codisproxytest");
            log.Debug($"进行一次读取=>key:codisproxytest,value:{value}");
            redisClient.KeyDelete("codisproxytest");
            log.Debug($"进行一次删除=>key:codisproxytest");
        }
#endif


#if NETFRAMEWORK
        
//#elif NETSTANDARD
        
#else

#endif
    }
}