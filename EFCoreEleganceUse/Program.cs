using Autofac;
using Autofac.Extensions.DependencyInjection;
using EFCoreEleganceUse.EF.Mysql;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

internal class Program
{
    async static Task Main(string[] args)
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        var host = CreateHostBuilder(args).Build();
        host.UseSwagger();
        host.UseSwaggerUI();
        host.MapControllers();
        host.UseAuthentication();
        host.UseAuthorization();

        await host.RunAsync();
    }

    public static WebApplicationBuilder CreateHostBuilder(string[] args)
    {
        var hostBuilder = WebApplication.CreateBuilder(options:new WebApplicationOptions() 
        {
            Args = args,
            EnvironmentName = Environments.Development
        });
        hostBuilder.Host.UseSerilog((context, logger) =>//Serilog
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");//按时间创建文件夹
            logger.ReadFrom.Configuration(context.Configuration);
            logger.Enrich.FromLogContext();
            logger.WriteTo.File($"Logs/{date}/", rollingInterval: RollingInterval.Hour);//按小时分日志文件
        }).UseServiceProviderFactory(new AutofacServiceProviderFactory()).UseEnvironment(Environments.Development);
        //生产下需要通过命令行参数或者配置文件设置环境：开发，测试，生产

        hostBuilder.Host.ConfigureServices((hostContext, services) =>
        {
            //注入mysql，生产中应该放置在应用层
            var mysqlConfig = hostContext.Configuration.GetSection("Mysql").Get<MysqlOptions>();
            var serverVersion = new MariaDbServerVersion(new Version(mysqlConfig.Version));
            services.AddDbContext<LibraryDbContext>(options =>
            {
                options.UseMySql(mysqlConfig.ConnectionString, serverVersion, optionsBuilder =>
                {
                    optionsBuilder.MinBatchSize(4);
                    optionsBuilder.CommandTimeout(10);
                    optionsBuilder.MigrationsAssembly(mysqlConfig.MigrationAssembly);
                    optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                }).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

                if (hostContext.HostingEnvironment.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                }
            });
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        });

        hostBuilder.Host.ConfigureContainer<ContainerBuilder>((hcontext, containerBuilder) =>
        {
            //生产中由应用层聚合各种基础设置等模块，最后交由Host程序注册应用层模块
            containerBuilder.RegisterModule<EFCoreEleganceUseEFCoreModule>();
        });

        return hostBuilder;
    }
}