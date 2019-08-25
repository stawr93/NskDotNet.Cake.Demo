using System;
using Cake.Common;
using Cake.Core;
using Cake.Core.Annotations;

namespace NskDotNet.Cake.Demo
{
    /// <summary>
    /// Contains Aliases for helping work with combinations of Argument and Environment variables.
    /// Inspired by https://github.com/patridge/Cake.ArgumentHelpers
    /// </summary>
    [CakeAliasCategory("Argument")]
    [CakeAliasCategory("Environment")]
    public static class ArgumentOrEnvironmentVariableAliases
    {
        /// <summary>
        /// Get a bool variable from various script inputs: first via Argument, then falling back on EnvironmentVariable, finally falling back on a default.
        /// </summary>
        /// <param name="name">The argument name to attempt to find in the command line parameters, prefixing with <paramref name="environmentNamePrefix"/> to attempt to find in environment variables.</param>
        /// <param name="environmentNamePrefix">An optional prefix used to qualify the same variable name when present in EnvironmentVariable form (e.g., "MySetting" command-line argument vs. "MyTool_MySetting" environment variable).</param>
        /// <param name="defaultValue">Default value which will be returned in case when argumen or variable not foud</param>
        /// <returns>Value found or default, first checked in command-line argument, then environment variable.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory("Argument")]
        [CakeAliasCategory("Environment")]
        public static bool ArgumentOrEnvironmentVariable(this ICakeContext context, string name, string environmentNamePrefix, bool defaultValue)
        {
            return ArgumentAliases.Argument(
                    context,
                    name,
                    EnvironmentAliases.EnvironmentVariable(context, EnvVarName(environmentNamePrefix, name))
                    ?? defaultValue.ToString())
                .Equals("true", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Get a bool variable from various script inputs: first via Argument, then falling back on EnvironmentVariable, finally falling back on a default.
        /// </summary>
        /// <param name="name">The argument name to attempt to find in either the command line parameters or environment variables.</param>
        /// <param name="defaultValue">Default value which will be returned in case when argumen or variable not foud</param>
        /// <returns>Value found or default, first checked in command-line argument, then environment variable.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory("Argument")]
        [CakeAliasCategory("Environment")]
        public static bool ArgumentOrEnvironmentVariable(this ICakeContext context, string name, bool defaultValue)
        {
            return context.ArgumentOrEnvironmentVariable(name, null, defaultValue);
        }

        /// <summary>
        /// Get a string variable from various script inputs: first via Argument, then falling back on EnvironmentVariable, finally falling back on a default.
        /// </summary>
        /// <param name="name">The argument name to attempt to find in the command line parameters, prefixing with <paramref name="environmentNamePrefix"/> to attempt to find in environment variables.</param>
        /// <param name="environmentNamePrefix">An optional prefix used to qualify the same variable name when present in EnvironmentVariable form (e.g., "MySetting" command-line argument vs. "MyTool_MySetting" environment variable).</param>
        /// <param name="defaultValue">Default value which will be returned in case when argumen or variable not foud</param>
        /// <returns>Value found or default, first checked in command-line argument, then environment variable.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory("Argument")]
        [CakeAliasCategory("Environment")]
        public static string ArgumentOrEnvironmentVariable(this ICakeContext context, string name, string environmentNamePrefix, string defaultValue = null)
        {
            return ArgumentAliases.Argument<string>(
                       context,
                       name,
                       EnvironmentAliases.EnvironmentVariable(context, EnvVarName(environmentNamePrefix, name))) ??
                   defaultValue;
        }

        /// <summary>
        /// Get a string variable from various script inputs: first via Argument, then falling back on EnvironmentVariable, finally falling back on a default.
        /// </summary>
        /// <param name="name">The argument name to attempt to find in either the command line parameters or environment variables.</param>
        /// <param name="defaultValue">Default value which will be returned in case when argumen or variable not foud</param>
        /// <returns>Value found or default, first checked in command-line argument, then environment variable.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory("Argument")]
        [CakeAliasCategory("Environment")]
        public static string ArgumentOrEnvironmentVariable(this ICakeContext context, string name, string defaultValue = null)
        {
            return context.ArgumentOrEnvironmentVariable(name, string.Empty, defaultValue);
        }

        private static string EnvVarName(string prefix, string name)
        {
            return (prefix ?? string.Empty) + name;
        }
    }
}