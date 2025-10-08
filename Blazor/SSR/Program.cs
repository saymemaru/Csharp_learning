using SSR.Components;

//asp.net
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// 注入服务
builder.Services.AddRazorComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
// 是否是开发环境, 不是则启用异常处理页面
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection(); //使用 https 重定向

app.UseStaticFiles(); //允许图片等静态文件
app.UseAntiforgery(); //启用防伪造请求, 保护应用程序免受跨站请求伪造 (CSRF) 攻击

app.MapRazorComponents<App>(); //映射 Razor 组件, 指定根组件为 App

app.Run();
