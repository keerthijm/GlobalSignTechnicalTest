using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GlobalSignTechnicalTest
{
    internal class TextUtility
    {
        /// <summary>
        /// Removes unwanted characters from a given line of text.
        /// </summary>
        /// <param name="line">The line to remove the characters from.</param>
        /// <returns>The modified line of text with all unwanted characters removed,and normalized to all lower case. The CleanLine method uses a regular expression to 
        /// remove characters from the line. This gives us a single place to modify if the list of characters to remove changes.
        /// </remarks>
        internal static string CleanLine(string line)
        {
            // Removing everything except ASCII alphanumerics, hyphens (for hyphenated words), and apostrophes (for contractions).
            // N.B. This doesn't handle the case of embedded quotes within text like the following:
            // John said, "He told me, 'I want to see you.'" Furthermore, punctuation without a space will be collapsed (e.g., "hello,world" will be rendered as "helloworld").
            string pattern = @"[^a-zA-Z0-9'\- ]";
            string cleanedLine = Regex.Replace(line, pattern, string.Empty);
            return cleanedLine.ToLowerInvariant();
        }


        /// <summary>
        /// Compares two KeyValuePair{string, int} objects by value.
        /// </summary>
        /// <param name="first">The KeyValuePair{string, int} to compare.</param>
        /// <param name="second">The KeyValuePair{string, int} to compare to.</param>
        /// <returns>An integer value indicating the relative value of the KeyValuePair{string, int} objects. </returns>
        /// <remarks>A negative value means the first pair has a lower number in its Value property than the second. A positive value means the first pair has a higher value in its Value property than the second.
        /// A zero value means that the Value properties of the two pairs are equal.</remarks>
        internal static int ComparePairs(KeyValuePair<string, int> first, KeyValuePair<string, int> second)
        {
            // The CompareTo() returns an int indicating relative value of the two objects. It returns a negative value if the current  instance is before the operand in the sort order, returns a 
            // positive value if the current instance is after the operand in the sort order, and zero if they are equal. We compare the second to the first, because we want a descending sort.
            // Though the spec doesn't specify, we could define a behavior for sorting if the values are equal here (e.g., sort equally frequent words alphabetically).
            return second.Value.CompareTo(first.Value);
        }

    }
}