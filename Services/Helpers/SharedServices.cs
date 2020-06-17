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

        public static bool IsNull (object validateData)
        {
            return validateData.GetType().GetProperties().All(p => p.GetValue(validateData) != null);
        }

    }
}
