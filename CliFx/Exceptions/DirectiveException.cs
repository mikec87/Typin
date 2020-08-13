﻿namespace CliFx.Exceptions
{
    using System;

    /// <summary>
    /// Thrown when a directive cannot proceed with normal execution due to an error.
    /// Use this exception if you want to report an error that occured during the execution of a directive.
    /// This exception also allows specifying exit code which will be returned to the calling process.
    /// </summary>
    public class DirectiveException : Exception
    {
        private const int DefaultExitCode = 1;

        private readonly bool _isMessageSet;

        /// <summary>
        /// Exit code returned by the application when this exception is handled.
        /// </summary>
        public int ExitCode { get; }

        /// <summary>
        /// Whether to show the help text after handling this exception.
        /// </summary>
        public bool ShowHelp { get; }

        /// <summary>
        /// Initializes an instance of <see cref="DirectiveException"/>.
        /// </summary>
        /// <remarks>
        /// On Unix systems an exit code is 8-bit unsigned integer so it's strongly recommended to use values between 1 and 255 to avoid overflow.
        /// </remarks>
        public DirectiveException(string? message,
                                  Exception? innerException,
                                  int exitCode = DefaultExitCode,
                                  bool showHelp = false)
            : base(message, innerException)
        {
            ExitCode = exitCode;
            ShowHelp = showHelp;

            // Message property has a fallback so it's never empty, hence why we need this check
            _isMessageSet = !string.IsNullOrWhiteSpace(message);
        }

        /// <summary>
        /// Initializes an instance of <see cref="DirectiveException"/>.
        /// </summary>
        /// <remarks>
        /// On Unix systems an exit code is 8-bit unsigned integer so it's strongly recommended to use values between 1 and 255 to avoid overflow.
        /// </remarks>
        public DirectiveException(string? message,
                                  int exitCode = DefaultExitCode,
                                  bool showHelp = false)
            : this(message, null, exitCode, showHelp)
        {

        }

        /// <summary>
        /// Initializes an instance of <see cref="DirectiveException"/>.
        /// </summary>
        /// <remarks>
        /// On Unix systems an exit code is 8-bit unsigned integer so it's strongly recommended to use values between 1 and 255 to avoid overflow.
        /// </remarks>
        public DirectiveException(int exitCode = DefaultExitCode,
                                  bool showHelp = false)
            : this(null, exitCode, showHelp)
        {

        }

        /// <inheritdoc />
        public override string ToString()
        {
            return _isMessageSet ? Message : base.ToString();
        }
    }
}