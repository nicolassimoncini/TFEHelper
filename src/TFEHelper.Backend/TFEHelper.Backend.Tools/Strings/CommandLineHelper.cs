using System.Collections.Generic;
using System.Linq;

namespace TFEHelper.Backend.Tools.Strings
{
    /// <summary>
    /// Set of routines for command line string handling.
    /// </summary>
    public static class CommandLineHelper
    {
        /// <summary>
        /// Returns an argument for specific parameter.<br>
        /// For example: if command line is "command --parameter argument", then <paramref name="args"/> will be ["--parameter", "argument"] and <paramref name="option"/> should be "--parameter" if "argument" value is needed. 
        /// <br><i>(GetArgument(args, "--parameter")</i>.</br></br>
        /// </summary>
        /// <param name="args">The list of parameters and its arguments as received from command line.</param>
        /// <param name="option">The specific parameter for which the argument is needed.</param>
        /// <returns>The required parameter's argument.</returns>
        public static string? GetArgument(IEnumerable<string> args, string option)
        {
            return args.SkipWhile(i => i != option).Skip(1).Take(1).FirstOrDefault();
        }
    }
}
