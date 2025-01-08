using System;
using System.Reflection;

namespace CRUDDemo.Helper
{
    /// <summary>
    /// Extension methods for converting Data Transfer Objects (DTO) to Plain Old CLR Objects (POCO).
    /// </summary>
    public static class DTOtoPOCOExtensionMethod
    {
        /// <summary>
        /// Converts the given DTO to a POCO object of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the POCO class to which the DTO is being converted.</typeparam>
        /// <param name="dto">The DTO object that needs to be converted.</param>
        /// <returns>A POCO object of type <typeparamref name="T"/> containing the mapped properties from the DTO.</returns>
        public static T Convert<T>(this object dto)
        {
            // Get the type of the POCO class to convert to
            Type pocoType = typeof(T);

            // Create an instance of the POCO class
            T pocoInstance = (T)Activator.CreateInstance(pocoType);

            // Get the properties of the DTO and POCO class
            PropertyInfo[] dtoProperties = dto.GetType().GetProperties();
            PropertyInfo[] pocoProperties = pocoType.GetProperties();

            // Loop through the properties of the DTO and map them to the POCO object
            foreach (PropertyInfo dtoProperty in dtoProperties)
            {
                // Find the corresponding property in the POCO class
                PropertyInfo pocoProperty = Array.Find(pocoProperties, p => p.Name == dtoProperty.Name);

                // If the property exists and types match, copy the value
                if (dtoProperty != null && dtoProperty.PropertyType == pocoProperty.PropertyType)
                {
                    object value = dtoProperty.GetValue(dto);
                    pocoProperty.SetValue(pocoInstance, value);
                }
            }

            // Return the populated POCO object
            return pocoInstance;
        }
    }
}
