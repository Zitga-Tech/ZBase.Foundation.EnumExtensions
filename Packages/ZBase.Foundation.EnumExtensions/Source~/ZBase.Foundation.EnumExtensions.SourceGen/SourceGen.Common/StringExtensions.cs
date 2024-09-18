using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace ZBase.Foundation.SourceGen
{
    public static class StringExtensions
    {
        public static string ToValidIdentifier(this string value)
            => value
                .Replace('.', '_')
                .Replace("-", "__")
                .Replace('<', 'ᐸ')
                .Replace('>', 'ᐳ')
                .Replace("[]", "Array")
                ;

        public static string ToValidNamespace(this string value)
            => value
                .Replace("-", "_")
                .Replace('<', 'ᐸ')
                .Replace('>', 'ᐳ')
                .Replace("[]", "Array")
                ;

        public static string ToTitleCase(this string value)
        {
            // Remove leading and trailing underscores
            value = value.Trim('_');

            // Split the string at capital letters or underscores
            string[] words = Regex.Split(value, @"(?<!^)(?=[A-Z])|_");

            // Create a TextInfo based on the "en-US" culture.
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            // Convert to title case and filter out any empty entries
            words = words
                .Where(word => !string.IsNullOrEmpty(word)) // Remove any empty strings resulting from split
                .Select(word => textInfo.ToTitleCase(word.ToLower())) // Convert each word to title case
                .ToArray();

            // Join all words with a single space and return
            return string.Join(" ", words).Trim();
        }
        
        public static string ToSentenceCase(this string value)
        {
            // Remove leading and trailing underscores
            value = value.Trim('_');

            // Split the string at capital letters or underscores
            string[] words = Regex.Split(value, @"(?<!^)(?=[A-Z])|_");

            // Convert to lower case and capitalize the first letter of each word
            words = words
                .Where(word => !string.IsNullOrEmpty(word)) // Remove any empty strings resulting from split
                .Select(word => {
                    word = word.ToLower();
                    return word.Length > 0 ? char.ToUpper(word[0]) + word.Substring(1) : word;
                }) // Capitalize the first letter of each word
                .ToArray();

            // Join all words with a space and return
            return string.Join(" ", words).Trim();
        }
        
        public static string ToSnakeCase(this string input)
        {
            // Remove leading and trailing underscores
            input = input.Trim('_');

            // Split the string at capital letters or underscores
            string[] words = Regex.Split(input, @"(?<!^)(?=[A-Z])|_");

            // Convert to lower case and filter out any empty entries
            words = words
                .Where(word => !string.IsNullOrEmpty(word)) // Remove any empty strings resulting from split
                .Select(word => word.ToLower()) // Convert each word to lower case
                .ToArray();

            // Join all words with an underscore and return
            return string.Join("_", words);
        }
    }
}
