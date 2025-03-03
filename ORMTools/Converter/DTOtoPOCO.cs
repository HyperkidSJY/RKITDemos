using System;
using System.Reflection;

namespace ORMTools.Converter
{
    #region "DTOtoPOCO Converter"

    /// <summary>
    /// Provides extension methods for converting Data Transfer Objects (DTOs) to Plain Old CLR Objects (POCOs).
    /// </summary>
    public static class DTOtoPOCO
    {
        /// <summary>
        /// Converts a DTO object to a POCO of the specified type.
        /// </summary>
        /// <typeparam name="T">The target POCO type to convert the DTO to.</typeparam>
        /// <param name="dto">The DTO object to convert.</param>
        /// <returns>A POCO object populated with values from the DTO.</returns>
        public static T Convert<T>(this object dto)
        {
            // Get the type of the POCO
            Type pocoType = typeof(T);

            // Create an instance of the POCO
            T pocoInstance = (T)Activator.CreateInstance(pocoType);

            // Get the properties of the DTO and POCO
            PropertyInfo[] dtoProperties = dto.GetType().GetProperties();
            PropertyInfo[] pocoProperties = pocoType.GetProperties();

            // Iterate over the DTO properties
            foreach (PropertyInfo dtoProperty in dtoProperties)
            {
                // Find the corresponding POCO property by name
                PropertyInfo pocoProperty = Array.Find(pocoProperties, p => p.Name == dtoProperty.Name);

                // If the property exists and types match, set the value in the POCO
                if (dtoProperty != null && dtoProperty.PropertyType == pocoProperty.PropertyType)
                {
                    object value = dtoProperty.GetValue(dto);
                    pocoProperty.SetValue(pocoInstance, value);
                }
            }

            // Return the populated POCO instance
            return pocoInstance;
        }
    }

    #endregion "DTOtoPOCO Converter"
}
