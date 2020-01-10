using log4net;
using System.Text;

namespace WebApplication
{
    public static class ZKDemo
    {
        private static ILog log = log4net.LogManager.GetLogger(typeof(ZKDemo));
        public static Mango.NodisClient.ZooKeeperHelper zkhelper;
        public static void Init()
        { 
            log.Debug("连接zk");

            zkhelper = new Mango.NodisClient.ZooKeeperHelper("192.168.4.77:2181,192.168.4.78:2181,192.168.4.79:2181", "codis-mango", 60,
                (nodes) =>
                {
                    foreach (var item in nodes)
                    {
                        log.Warn($"新增节点：{item.Addr}");
                    }
                },
                (nodes) =>
                {
                    foreach (var item in nodes)
                    {
                        log.Warn($"删除节点：{item.Addr}");
                    }
                });
            StringBuilder pools = new StringBuilder();
            foreach (var item in zkhelper.pools)
            {
                pools.Append($"node:{item.Node}= host:{item.Addr},State:{item.State},Flag:{item.Flag}|");
            }
            log.Debug($"zk节点=>{pools.ToString()}");

            log.Debug("连接完成~");
        }
    }
}