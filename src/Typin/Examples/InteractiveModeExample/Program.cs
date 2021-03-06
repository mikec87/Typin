﻿namespace InteractiveModeExample
{
    using System.Threading.Tasks;
    using InteractiveModeExample.Directives;
    using InteractiveModeExample.Middlewares;
    using InteractiveModeExample.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Typin;
    using Typin.Directives;

    public static class Program
    {
        private static void GetServiceCollection(IServiceCollection services)
        {
            services.AddSingleton<LibraryService>();
        }

        public static async Task<int> Main()
        {
            return await new CliApplicationBuilder()
                .ConfigureServices(GetServiceCollection)
                .AddCommandsFromThisAssembly()
                .AddDirective<DebugDirective>()
                .AddDirective<PreviewDirective>()
                .AddDirective<CustomInteractiveModeOnlyDirective>()
                .UseMiddleware<ExitCodeMiddleware>()
                .UseMiddleware<ExecutionTimingMiddleware>()
                .UseMiddleware<ExecutionLogMiddleware>()
                .UseInteractiveMode()
                .UseStartupMessage("{title} CLI {version} {{title}} {executable} {{{description}}} {test}")
                .Build()
                .RunAsync();
        }
    }
}