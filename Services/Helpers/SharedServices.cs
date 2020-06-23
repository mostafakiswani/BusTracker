using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Helpers
{
    public class SharedServices
    {
        public static string ConvertGuid(Guid id)
        {
            return id.ToString();
        }

        public static Guid ConvertToGuid(string id)
        {
            return new Guid(id);
        }

        public static bool IsValid (object validateData)
        {
            return validateData.GetType().GetProperties().All(p => p.GetValue(validateData) != null);
        }
        public static bool IsObjectNull(object validateData)
        {
            if (validateData == null)
                return true;

            return false;
        }


    }
}
