我将为您提供NLog的完整配置和使用示例，包括基础配置、依赖注入和实际应用场景。

## 1. 安装NuGet包

```bash
# 基础包
dotnet add package NLog
dotnet add package NLog.Extensions.Logging

# 如果使用配置文件方式
dotnet add package NLog.Config

# ASP.NET Core项目推荐
dotnet add package NLog.Web.AspNetCore
```

## 2. 配置文件方式

### 方式A：使用 nlog.config 文件

**nlog.config** (放在项目根目录)

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
      autoReload="true"
      throwExceptions="false"
      internalLogLevel="Off" 
      internalLogFile="c:\temp\nlog-internal.log">

  <!-- 目标配置 -->
  <targets>
    <!-- 输出到控制台 -->
    <target name="console" xsi:type="Console" 
            layout="${longdate}|${level:uppercase=true}|${logger}|${message} ${exception:format=toString}" />

    <!-- 输出到文件（按日期滚动） -->
    <target name="file" xsi:type="File"
            fileName="${basedir}/logs/${shortdate}.log"
            layout="${longdate}|${level:uppercase=true}|${logger}|${message} ${exception:format=tostring}"
            archiveEvery="Day"
            archiveNumbering="Rolling"
            maxArchiveFiles="30"
            encoding="utf-8" />

    <!-- 输出到文件（按大小滚动） -->
    <target name="fileBySize" xsi:type="File"
            fileName="${basedir}/logs/log.txt"
            layout="${longdate}|${level:uppercase=true}|${logger}|${message} ${exception:format=tostring}"
            archiveAboveSize="10485760"  <!-- 10MB -->
            archiveEvery="Day"
            maxArchiveFiles="10"
            encoding="utf-8" />
  </targets>

  <!-- 路由规则 -->
  <rules>
    <!-- 所有日志写入文件和控制台 -->
    <logger name="*" minlevel="Info" writeTo="console,file" />
    
    <!-- 特定命名空间的日志级别 -->
    <logger name="MyApp.*" minlevel="Debug" writeTo="file" />
    <logger name="Microsoft.*" maxlevel="Warn" writeTo="file" />
  </rules>
</nlog>
```

**设置配置文件属性**：复制到输出目录 - 始终复制

```xml
<ItemGroup>
  <Content Include="nlog.config" CopyToOutputDirectory="PreserveNewest" />
</ItemGroup>
```

## 3. 代码配置方式（不使用配置文件）

```csharp
using NLog;
using NLog.Config;
using NLog.Targets;

public static class NLogConfig
{
    public static void Configure()
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
            ArchiveEvery = FileTargetArchiveAttributes.Day,
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
}
```

## 4. 在应用程序中初始化

### Console/桌面应用

```csharp
using NLog;
using NLog.Extensions.Logging;

class Program
{
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
    
    static void Main(string[] args)
    {
        try
        {
            // 方式1：自动加载 nlog.config
            // 无需额外代码，NLog会自动查找配置文件
            
            // 方式2：手动加载配置
            // var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "nlog.config");
            // LogManager.Configuration = new XmlLoggingConfiguration(configPath);
            
            // 方式3：代码配置
            // NLogConfig.Configure();
            
            Logger.Info("应用程序启动");
            
            // 业务代码...
            
            Logger.Info("应用程序结束");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "应用程序发生异常");
            throw;
        }
        finally
        {
            // 确保日志被写入
            LogManager.Shutdown();
        }
    }
}
```

### [ASP.NET](https://ASP.NET) Core (Web应用)

**Program.cs** (.NET 6+)

```csharp
using NLog;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// 移除默认日志提供程序，添加NLog
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Host.UseNLog(); // 自动加载 nlog.config

var app = builder.Build();

// 获取日志记录器
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("应用启动");

app.Run();
```

**Startup.cs** (.NET Core 3.1)

```csharp
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
    {
        logger.LogInformation("应用配置中...");
        
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        
        logger.LogInformation("应用配置完成");
    }
}
```

## 5. 实际使用示例

### 在类中使用

```csharp
using Microsoft.Extensions.Logging;

public class UserService
{
    private readonly ILogger<UserService> _logger;
    
    public UserService(ILogger<UserService> logger)
    {
        _logger = logger;
    }
    
    public async Task<User> GetUserAsync(int id)
    {
        try
        {
            _logger.LogInformation("获取用户信息，ID: {UserId}", id);
            
            // 业务逻辑
            var user = await GetUserFromDatabase(id);
            
            if (user == null)
            {
                _logger.LogWarning("用户不存在，ID: {UserId}", id);
                return null;
            }
            
            _logger.LogDebug("用户信息: {@User}", user);
            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取用户失败，ID: {UserId}", id);
            throw;
        }
    }
}
```

### 使用NLog原生API

```csharp
using NLog;

public class OrderService
{
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
    
    public void ProcessOrder(Order order)
    {
        Logger.Trace("开始处理订单");
        Logger.Debug("订单详情: {@Order}", order);
        
        try
        {
            // 业务逻辑
            Logger.Info($"订单 {order.Id} 处理成功");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "订单处理失败");
            throw;
        }
        finally
        {
            Logger.Trace("结束处理订单");
        }
    }
}
```

## 6. 高级配置示例

```xml
<nlog>
  <extensions>
    <!-- 添加额外的目标 -->
    <add assembly="NLog.Targets.ElasticSearch" />
  </extensions>

  <targets>
    <!-- 异步日志 -->
    <target name="asyncFile" xsi:type="AsyncWrapper" queueLimit="1000" overflowAction="Discard">
      <target xsi:type="File" fileName="${basedir}/logs/${shortdate}.log" />
    </target>
    
    <!-- 按日志级别分文件 -->
    <target name="fileByLevel" xsi:type="File">
      <fileName>${basedir}/logs/${level}.log</fileName>
      <layout>${longdate}|${callsite}|${message} ${exception}</layout>
    </target>
    
    <!-- JSON格式 -->
    <target name="jsonFile" xsi:type="File">
      <fileName>${basedir}/logs/json.log</fileName>
      <layout xsi:type="JsonLayout">
        <attribute name="time" layout="${longdate}" />
        <attribute name="level" layout="${level}" />
        <attribute name="logger" layout="${logger}" />
        <attribute name="message" layout="${message}" />
        <attribute name="exception" layout="${exception:format=toString}" />
      </layout>
    </target>
  </targets>
  
  <rules>
    <logger name="*" minlevel="Info" writeTo="asyncFile" />
    <logger name="*" minlevel="Trace" writeTo="fileByLevel" />
    <logger name="MyApp.*" minlevel="Debug" writeTo="jsonFile" />
  </rules>
</nlog>
```

## 7. 日志级别说明

```csharp
// 从低到高
_logger.LogTrace("跟踪信息");     // 最详细
_logger.LogDebug("调试信息");     // 开发调试
_logger.LogInformation("普通信息"); // 一般信息
_logger.LogWarning("警告信息");    // 警告
_logger.LogError("错误信息");      // 错误
_logger.LogCritical("严重错误");   // 严重错误
```

## 8. 最佳实践

1. **使用结构化日志**：使用 `{PropertyName}` 占位符
2. **避免字符串拼接**：`_logger.LogInformation("User {Id}", id)` 优于 `$"User {id}"`
3. **合理设置日志级别**：生产环境设为 Info 或 Warn
4. **使用异步日志**：提高性能
5. **定期清理日志文件**：配置 maxArchiveFiles

这样您就可以开始使用NLog了！根据项目类型选择合适的配置方式即可。

---

我来详细说明如何设置和使用 `nlog.config` 文件。

## 1. 创建 nlog.config 文件

### 步骤1：在项目中创建文件

在项目根目录右键 → 添加 → 新建项 → XML文件，命名为 `nlog.config`

```text
项目目录/
├── nlog.config
├── Program.cs
├── ...
```

### 步骤2：基础配置模板

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
      autoReload="true"
      throwExceptions="false"
      internalLogLevel="Warn"
      internalLogFile="${basedir}/logs/nlog-internal.log">
  
  <!-- 目标：定义日志输出到哪里 -->
  <targets>
    <!-- 控制台输出 -->
    <target name="console" xsi:type="Console" 
            layout="${longdate}|${level:uppercase=true}|${logger}|${message} ${exception:format=toString}" />
    
    <!-- 文件输出 -->
    <target name="file" xsi:type="File"
            fileName="${basedir}/logs/${shortdate}.log"
            layout="${longdate}|${level:uppercase=true}|${logger}|${message} ${exception:format=toString}" />
  </targets>
  
  <!-- 规则：定义哪些日志记录到哪些目标 -->
  <rules>
    <logger name="*" minlevel="Info" writeTo="console,file" />
  </rules>
</nlog>
```

## 2. 配置文件属性设置

**重要**：确保配置文件在编译时被复制到输出目录。

### 方法A：使用项目文件 (.csproj)

```xml
<ItemGroup>
  <Content Include="nlog.config">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </Content>
</ItemGroup>
```

### 方法B：在Visual Studio中设置

1. 右键点击 `nlog.config` 文件
2. 选择"属性"
3. 设置：
   - **复制到输出目录**：`如果较新则复制` 或 `始终复制`
   - **生成操作**：`内容`

![文件属性设置示意]

### 方法C：使用链接（多项目共享）

```xml
<ItemGroup>
  <None Include="..\Shared\nlog.config" Link="nlog.config">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </None>
</ItemGroup>
```

## 3. 加载配置文件的多种方式

### 方式1：自动加载（推荐）

NLog会自动从以下位置查找配置文件：

- 应用程序目录
- 标准配置位置

```csharp
// 无需任何代码，NLog会自动加载
using NLog;

public class Program
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    
    static void Main()
    {
        Logger.Info("应用程序启动"); // 自动使用 nlog.config
    }
}
```

### 方式2：显式加载

```csharp
using NLog;
using NLog.Config;

public class Program
{
    static void Main()
    {
        // 从当前目录加载
        LogManager.Configuration = new XmlLoggingConfiguration("nlog.config");
        
        // 或者使用绝对路径
        var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "nlog.config");
        LogManager.Configuration = new XmlLoggingConfiguration(configPath);
        
        // 业务代码...
    }
}
```

### 方式3：在[ASP.NET](https://ASP.NET) Core中加载

```csharp
// Program.cs (NET 6+)
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// 方式A：自动加载
builder.Host.UseNLog();

// 方式B：指定配置文件
builder.Host.UseNLog("nlog.config");

// 方式C：使用环境变量
var configPath = Environment.GetEnvironmentVariable("NLOG_CONFIG") ?? "nlog.config";
builder.Host.UseNLog(configPath);
```

## 4. 详细配置说明

### 完整的 nlog.config 示例

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
      autoReload="true"      <!-- 配置文件修改时自动重载 -->
      throwExceptions="false" <!-- 不抛出NLog异常 -->
      internalLogLevel="Warn" <!-- NLog内部日志级别 -->
      internalLogFile="${basedir}/logs/nlog-internal.log"> <!-- NLog内部日志文件 -->

  <!-- 变量定义 -->
  <variable name="logDirectory" value="${basedir}/logs" />
  <variable name="logLayout" value="${longdate}|${level:uppercase=true}|${logger}|${message} ${exception:format=toString}" />

  <!-- 扩展 -->
  <extensions>
    <!-- 添加额外的目标，如数据库、邮件等 -->
    <add assembly="NLog.Targets.ElasticSearch" />
  </extensions>

  <!-- 目标配置 -->
  <targets>
    <!-- 1. 控制台目标 -->
    <target name="console" xsi:type="Console"
            layout="${logLayout}" />
    
    <!-- 2. 文件目标 - 按日期滚动 -->
    <target name="fileByDate" xsi:type="File"
            fileName="${logDirectory}/${shortdate}.log"
            layout="${logLayout}"
            archiveEvery="Day"
            archiveNumbering="Rolling"
            maxArchiveFiles="30"
            encoding="utf-8" />
    
    <!-- 3. 文件目标 - 按大小滚动 -->
    <target name="fileBySize" xsi:type="File"
            fileName="${logDirectory}/current.log"
            layout="${logLayout}"
            archiveAboveSize="10485760" <!-- 10MB -->
            maxArchiveFiles="10"
            encoding="utf-8" />
    
    <!-- 4. 异步日志目标 -->
    <target name="asyncFile" xsi:type="AsyncWrapper"
            queueLimit="5000"
            overflowAction="Discard">
      <target xsi:type="File"
              fileName="${logDirectory}/async-${shortdate}.log"
              layout="${logLayout}" />
    </target>
    
    <!-- 5. 按级别分文件 -->
    <target name="fileByLevel" xsi:type="File">
      <fileName>${logDirectory}/${level}.log</fileName>
      <layout>${longdate}|${callsite}|${message} ${exception}</layout>
    </target>
    
    <!-- 6. JSON格式输出 -->
    <target name="jsonFile" xsi:type="File">
      <fileName>${logDirectory}/json-${shortdate}.log</fileName>
      <layout xsi:type="JsonLayout">
        <attribute name="timestamp" layout="${longdate}" />
        <attribute name="level" layout="${level}" />
        <attribute name="logger" layout="${logger}" />
        <attribute name="message" layout="${message}" />
        <attribute name="exception" layout="${exception:format=toString}" />
        <attribute name="properties" encode="false">
          <layout xsi:type="JsonLayout" includeAllProperties="true" />
        </attribute>
      </layout>
    </target>
    
    <!-- 7. 数据库目标 -->
    <target name="database" xsi:type="Database">
      <connectionString>Server=.;Database=LogDB;Integrated Security=true</connectionString>
      <commandText>
        INSERT INTO Logs (LogDate, Level, Logger, Message, Exception) 
        VALUES (@logDate, @level, @logger, @message, @exception)
      </commandText>
      <parameters>
        <parameter name="@logDate" layout="${longdate}" />
        <parameter name="@level" layout="${level}" />
        <parameter name="@logger" layout="${logger}" />
        <parameter name="@message" layout="${message}" />
        <parameter name="@exception" layout="${exception:format=toString}" />
      </parameters>
    </target>
  </targets>

  <!-- 路由规则 -->
  <rules>
    <!-- 所有日志写入文件和数据库 -->
    <logger name="*" minlevel="Info" writeTo="fileByDate,database" />
    
    <!-- 特定命名空间写入不同文件 -->
    <logger name="Microsoft.*" maxlevel="Warn" writeTo="fileByLevel" />
    <logger name="System.*" maxlevel="Warn" writeTo="fileByLevel" />
    
    <!-- 应用程序日志更详细 -->
    <logger name="MyApp.*" minlevel="Debug" writeTo="asyncFile" />
    
    <!-- 控制台只显示重要信息 -->
    <logger name="*" minlevel="Warn" writeTo="console" />
    
    <!-- JSON日志只记录业务操作 -->
    <logger name="MyApp.Services.*" minlevel="Info" writeTo="jsonFile" />
  </rules>
</nlog>
```

## 5. 环境特定配置

### 为不同环境创建不同配置

```text
项目目录/
├── nlog.config              (默认)
├── nlog.Debug.config        (调试)
├── nlog.Release.config      (发布)
├── nlog.Production.config   (生产)
```

### 在代码中选择配置

```csharp
public class Program
{
    static void Main(string[] args)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
        var configFile = $"nlog.{env}.config";
        
        if (File.Exists(configFile))
        {
            LogManager.Configuration = new XmlLoggingConfiguration(configFile);
        }
        else
        {
            LogManager.Configuration = new XmlLoggingConfiguration("nlog.config");
        }
        
        // 或者使用条件编译
#if DEBUG
        LogManager.Configuration = new XmlLoggingConfiguration("nlog.Debug.config");
#else
        LogManager.Configuration = new XmlLoggingConfiguration("nlog.Release.config");
#endif
    }
}
```

### [ASP.NET](https://ASP.NET) Core 环境配置

```csharp
builder.Host.UseNLog((context, config) =>
{
    var env = context.HostingEnvironment;
    var configFile = $"nlog.{env.EnvironmentName}.config";
    
    if (File.Exists(configFile))
    {
        config.LoadFromFile(configFile);
    }
    else
    {
        config.LoadFromFile("nlog.config");
    }
});
```

## 6. 配置验证

### 方法1：在线验证

访问 [NLog Schema Validator](https://www.nlog-project.org/schemas/NLog.xsd) 验证XML格式

### 方法2：代码验证

```csharp
using NLog.Config;

try
{
    var config = new XmlLoggingConfiguration("nlog.config");
    LogManager.Configuration = config;
    
    // 验证配置是否有效
    foreach (var target in config.AllTargets)
    {
        Console.WriteLine($"目标: {target.Name}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"配置文件错误: {ex.Message}");
}
```

### 方法3：启用内部日志

在配置文件中启用内部日志查看加载错误：

```xml
<nlog internalLogLevel="Trace" internalLogFile="nlog-internal.log">
```

然后查看 `nlog-internal.log` 文件

## 7. 常见问题和解决方案

### 问题1：配置文件未被复制

**解决方案**：在 csproj 中添加：

```xml
<ItemGroup>
  <None Update="nlog.config">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </None>
</ItemGroup>
```

### 问题2：配置文件路径错误

```csharp
// 调试时查看配置路径
var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "nlog.config");
Console.WriteLine($"配置文件路径: {configPath}");
Console.WriteLine($"文件是否存在: {File.Exists(configPath)}");
```

### 问题3：日志未写入

```csharp
// 手动刷新配置
LogManager.ReconfigExistingLoggers();

// 确保调用 Shutdown
LogManager.Shutdown();
```

## 8. 最佳实践

1. **使用变量**：避免重复路径
2. **启用 autoReload**：方便调试
3. **设置内部日志**：便于排查问题
4. **按环境分离配置**：开发、测试、生产不同
5. **使用异步日志**：提高性能
6. **合理设置日志级别**：生产环境 Info 或 Warn
7. **定期清理日志**：设置 maxArchiveFiles

这样您就可以完全掌握 nlog.config 的设置了！