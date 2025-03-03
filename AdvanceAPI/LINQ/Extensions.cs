namespace AdvanceAPI.LINQ
{
    /// <summary>
    /// Provides extension methods for working with collections.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Filters a list of records based on a provided condition.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the list.</typeparam>
        /// <param name="records">The list of records to filter.</param>
        /// <param name="func">The function used to determine if a record should be included in the filtered list.</param>
        /// <returns>A new list containing only the records that meet the condition defined by the provided function.</returns>
        public static List<T> Filter<T>(this List<T> records, Func<T, bool> func)
        {
            List<T> filteredList = new List<T>();

            // Iterating through the records and adding those that match the condition
            foreach (T record in records)
            {
                if (func(record))
                {
                    filteredList.Add(record);
                }
            }
            return filteredList;
        }
    }
}
