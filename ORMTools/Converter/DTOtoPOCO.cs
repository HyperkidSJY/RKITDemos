using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ORMTools.Converter
{
    public static class DTOtoPOCO
    {
        public static T Convert<T>(this object dto)
        {
            Type pocoType = typeof(T);

            T pocoInstance = (T)Activator.CreateInstance(pocoType);

            PropertyInfo[] dtoProperties = dto.GetType().GetProperties();

            PropertyInfo[] pocoProperties = pocoType.GetProperties();

            foreach (PropertyInfo dtoProperty in dtoProperties)
            {

                PropertyInfo pocoProperty = Array.Find(pocoProperties, p => p.Name == dtoProperty.Name);


                if (dtoProperty != null && dtoProperty.PropertyType == pocoProperty.PropertyType)
                {
                    object value = dtoProperty.GetValue(dto);
                    pocoProperty.SetValue(pocoInstance, value);
                }
            }

            return pocoInstance;
        }
    }
}