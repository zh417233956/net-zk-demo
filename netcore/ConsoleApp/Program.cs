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

            //WebApplication.ZKDemo.Init();

            WebApplication.RedisAndCodisDemo.Init(1);
            WebApplication.RedisAndCodisDemo.GetRedisValue();

            log.Info("完成...");
            Console.ReadKey();

        }
    }
}