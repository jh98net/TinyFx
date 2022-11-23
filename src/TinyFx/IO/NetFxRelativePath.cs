using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace TinyFx.IO
{
    internal static class NetFxRelativePath
    {
        public static string GetRelativePath(string relativeTo, string path)
        {
            return GetRelativePath(relativeTo, path, StringComparison);
        }

        private static string GetRelativePath(string relativeTo, string path, StringComparison comparisonType)
        {
            if (string.IsNullOrEmpty(relativeTo)) throw new ArgumentNullException(nameof(relativeTo));
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));
            Debug.Assert(comparisonType == StringComparison.Ordinal ||
                         comparisonType == StringComparison.OrdinalIgnoreCase);

            relativeTo = Path.GetFullPath(relativeTo);
            path = Path.GetFullPath(path);

            if (!PathInternalNetCore.AreRootsEqual(relativeTo, path, comparisonType))
                return path;

            int commonLength = PathInternalNetCore.GetCommonPathLength(relativeTo, path,
                ignoreCase: comparisonType == StringComparison.OrdinalIgnoreCase);

            if (commonLength == 0)
                return path;

            // Trailing separators aren't significant for comparison
            int relativeToLength = relativeTo.Length;
            if (PathInternalNetCore.EndsInDirectorySeparator(relativeTo))
                relativeToLength--;

            bool pathEndsInSeparator = PathInternalNetCore.EndsInDirectorySeparator(path);
            int pathLength = path.Length;
            if (pathEndsInSeparator)
                pathLength--;

            if (relativeToLength == pathLength && commonLength >= relativeToLength) return ".";

            StringBuilder
                sb = new StringBuilder(); //StringBuilderCache.Acquire(Math.Max(relativeTo.Length, path.Length));

            if (commonLength < relativeToLength)
            {
                sb.Append("..");

                for (int i = commonLength + 1; i < relativeToLength; i++)
                {
                    if (PathInternalNetCore.IsDirectorySeparator(relativeTo[i]))
                    {
                        sb.Append(DirectorySeparatorChar);
                        sb.Append("..");
                    }
                }
            }
            else if (PathInternalNetCore.IsDirectorySeparator(path[commonLength]))
            {
                commonLength++;
            }

            int differenceLength = pathLength - commonLength;
            if (pathEndsInSeparator)
                differenceLength++;

            if (differenceLength > 0)
            {
                if (sb.Length > 0)
                {
                    sb.Append(DirectorySeparatorChar);
                }

                sb.Append(path, commonLength, differenceLength);
            }

            return sb.ToString(); //StringBuilderCache.GetStringAndRelease(sb);
        }

        public static readonly char DirectorySeparatorChar = PathInternalNetCore.DirectorySeparatorChar;
        public static readonly char AltDirectorySeparatorChar = PathInternalNetCore.AltDirectorySeparatorChar;
        public static readonly char VolumeSeparatorChar = PathInternalNetCore.VolumeSeparatorChar;
        public static readonly char PathSeparator = PathInternalNetCore.PathSeparator;

        internal static StringComparison StringComparison => StringComparison.OrdinalIgnoreCase;
    }
    internal static class PathInternalNetCore
    {
        internal const char DirectorySeparatorChar = '\\';
        internal const char AltDirectorySeparatorChar = '/';
        internal const char VolumeSeparatorChar = ':';
        internal const char PathSeparator = ';';

        internal const string ExtendedDevicePathPrefix = @"\\?\";
        internal const string UncPathPrefix = @"\\";
        internal const string UncDevicePrefixToInsert = @"?\UNC\";
        internal const string UncExtendedPathPrefix = @"\\?\UNC\";
        internal const string DevicePathPrefix = @"\\.\";

        internal const int DevicePrefixLength = 4;

        internal static bool AreRootsEqual(string first, string second, StringComparison comparisonType)
        {
            int firstRootLength = GetRootLength(first);
            int secondRootLength = GetRootLength(second);

            return firstRootLength == secondRootLength
                   && string.Compare(
                       strA: first,
                       indexA: 0,
                       strB: second,
                       indexB: 0,
                       length: firstRootLength,
                       comparisonType: comparisonType) == 0;
        }

        internal static int GetRootLength(string path)
        {
            int i = 0;
            int volumeSeparatorLength = 2; // Length to the colon "C:"
            int uncRootLength = 2; // Length to the start of the server name "\\"

            bool extendedSyntax = path.StartsWith(ExtendedDevicePathPrefix);
            bool extendedUncSyntax = path.StartsWith(UncExtendedPathPrefix);
            if (extendedSyntax)
            {
                if (extendedUncSyntax)
                {
                    uncRootLength = UncExtendedPathPrefix.Length;
                }
                else
                {
                    volumeSeparatorLength += ExtendedDevicePathPrefix.Length;
                }
            }

            if ((!extendedSyntax || extendedUncSyntax) && path.Length > 0 && IsDirectorySeparator(path[0]))
            {
                i = 1; //  Drive rooted (\foo) is one character
                if (extendedUncSyntax || (path.Length > 1 && IsDirectorySeparator(path[1])))
                {
                    i = uncRootLength;
                    int n = 2; // Maximum separators to skip
                    while (i < path.Length && (!IsDirectorySeparator(path[i]) || --n > 0)) i++;
                }
            }
            else if (path.Length >= volumeSeparatorLength &&
                     path[volumeSeparatorLength - 1] == NetFxRelativePath.VolumeSeparatorChar)
            {
                i = volumeSeparatorLength;
                if (path.Length >= volumeSeparatorLength + 1 && IsDirectorySeparator(path[volumeSeparatorLength])) i++;
            }

            return i;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsDirectorySeparator(char c)
        {
            return c == NetFxRelativePath.DirectorySeparatorChar || c == NetFxRelativePath.AltDirectorySeparatorChar;
        }

        internal static int GetCommonPathLength(string first, string second, bool ignoreCase)
        {
            int commonChars = EqualStartingCharacterCount(first, second, ignoreCase: ignoreCase);

            if (commonChars == 0)
                return commonChars;

            if (commonChars == first.Length
                && (commonChars == second.Length || IsDirectorySeparator(second[commonChars])))
                return commonChars;

            if (commonChars == second.Length && IsDirectorySeparator(first[commonChars]))
                return commonChars;

            while (commonChars > 0 && !IsDirectorySeparator(first[commonChars - 1]))
                commonChars--;

            return commonChars;
        }

        internal static unsafe int EqualStartingCharacterCount(string first, string second, bool ignoreCase)
        {
            if (string.IsNullOrEmpty(first) || string.IsNullOrEmpty(second)) return 0;

            int commonChars = 0;

            fixed (char* f = first)
            fixed (char* s = second)
            {
                char* l = f;
                char* r = s;
                char* leftEnd = l + first.Length;
                char* rightEnd = r + second.Length;

                while (l != leftEnd && r != rightEnd
                                    && (*l == *r || (ignoreCase &&
                                                     char.ToUpperInvariant((*l)) == char.ToUpperInvariant((*r)))))
                {
                    commonChars++;
                    l++;
                    r++;
                }
            }

            return commonChars;
        }

        internal static bool EndsInDirectorySeparator(string path)
            => path.Length > 0 && IsDirectorySeparator(path[path.Length - 1]);
    }
}
