namespace Shared
{
    public static class LinqExtensionMethod
    {
        public static bool Contains(this List<string> list, string value, bool ignoreCase = false)
        {
            return ignoreCase ?
                list.Any(s => s.Equals(value, StringComparison.OrdinalIgnoreCase)) :
                list.Contains(value);
        }
    }
}
