using NLog;
using NLog.Config;
using NLog.Targets;
using NLogTest;

//配置文件
#if DEBUG
LogManager.Configuration = new XmlLoggingConfiguration("nlog.Debug.config");
#else
        LogManager.Configuration = new XmlLoggingConfiguration("nlog.Release.config");
#endif

var logger = LogManager.GetCurrentClassLogger();

logger.Info("hello");

Person p1 = new("zz");
p1.Walk();

//代码动态设置配置
static void Configure()
{
    var config = new LoggingConfiguration();

    // 控制台目标
    var consoleTarget = new ConsoleTarget("console")
    {
        Layout = "${longdate}|${level:uppercase=true}|${logger}|${message} ${exception:format=toString}"
    };
    config.AddTarget(consoleTarget);

    // 文件目标
    var fileTarget = new FileTarget("file")
    {
        FileName = "${basedir}/logs/${shortdate}.log",
        Layout = "${longdate}|${level:uppercase=true}|${logger}|${message} ${exception:format=tostring}",
        ArchiveEvery = FileArchivePeriod.Day,
        MaxArchiveFiles = 30,
        Encoding = System.Text.Encoding.UTF8
    };
    config.AddTarget(fileTarget);

    // 设置规则
    config.AddRule(LogLevel.Info, LogLevel.Fatal, consoleTarget);
    config.AddRule(LogLevel.Debug, LogLevel.Fatal, fileTarget);

    // 应用配置
    LogManager.Configuration = config;
}