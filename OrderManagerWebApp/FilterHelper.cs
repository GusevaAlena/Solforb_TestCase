using System.Collections;

namespace OrderManagerWebApp
{
    public static class FilterHelper
    {
        public static bool IsContainValueInFilter(string value, IEnumerable<string> filters)
        {
            if (filters == null)
                return true;
            foreach (var filter in filters)
            {
                if (value == filter) { return true; }
            }
            return false;
        }
    }
}
