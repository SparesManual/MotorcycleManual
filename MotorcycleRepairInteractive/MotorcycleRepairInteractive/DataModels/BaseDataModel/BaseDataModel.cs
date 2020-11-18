using System;
using System.Collections.Generic;
using System.Text;

namespace MRI.Core
{ 
    public class BaseDataModel
    {
        #region Public Methods

        public int IntParse(string text)
        {
            int result = 0;

            if (text.Length == 0) result = 0;
            else
            {
                try
                {
                    result = int.Parse(text);
                }
                catch
                {
                    throw new Exception();
                }
            }
            

            return result;
        }

        /// <summary>
        /// Tries to set the SpecificToModel enum from the string
        /// and if it fails, return the sent in default
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The string to find in the enum choices</param>
        /// <param name="defaultValue">The passed in default enum</param>
        /// <returns></returns>
        public static T ParseEnum<T>(string value, T defaultValue) where T : struct
        {
            // Try to make the string into an enum that matches an existing one
            try
            {
                T enumValue;
                if (!Enum.TryParse(value, true, out enumValue))
                {
                    // Parsing failed, return default enum
                    return defaultValue;
                }
                // Return the correct enum
                return enumValue;
            }
            // If value was null, return the default enum
            catch (Exception)
            {
                // Return default value
                return defaultValue;
            }
        }
        #endregion
    }
}
