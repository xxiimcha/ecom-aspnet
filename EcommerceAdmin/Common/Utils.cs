namespace Ecommerce.Common {
    public class Utils {
    }
    public static class StringExtensions {
        public static string ToTitleCase(this string str) {
            if (string.IsNullOrEmpty(str))
                return str;

            return string.Join(" ", str.Split(' ').Select(word =>
                char.ToUpper(word[0]) + word.Substring(1).ToLower()));
        }
    }
}
