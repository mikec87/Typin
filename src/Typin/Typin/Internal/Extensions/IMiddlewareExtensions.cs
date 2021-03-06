﻿namespace Typin.Internal.Extensions
{
    using System.Threading;
    using System.Threading.Tasks;

    internal static class IMiddlewareExtensions
    {
        public static CommandPipelineHandlerDelegate PipelineTermination => () => Task.CompletedTask;

        public static CommandPipelineHandlerDelegate Next(this IMiddleware commandMiddleware,
                                                          ICliContext cliContext,
                                                          CommandPipelineHandlerDelegate next,
                                                          CancellationToken cancellationToken)
        {
            return () => commandMiddleware.HandleAsync(cliContext, next, cancellationToken);
        }
    }
}
