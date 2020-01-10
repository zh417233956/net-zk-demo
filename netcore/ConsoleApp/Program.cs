using System;
using System.Text;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //加载log4net日志配置文件
            log4net.Config.XmlConfigurator.Configure(log4net.LogManager.CreateRepository("log4net-default-repository"), new System.IO.FileInfo("log4net.config"));
            var log = log4net.LogManager.GetLogger(typeof(Program));
            log.Info("启动...");
            log.Debug("连接zk");


            var zkhelper = new Mango.NodisClient.ZooKeeperHelper("192.168.4.77:2181,192.168.4.78:2181,192.168.4.79:2181", "codis-mango", 60,
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

            log.Info("完成...");
            Console.ReadKey();

        }
    }
}
