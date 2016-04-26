using System.Data.SqlClient;
using System;

namespace Shared
{
    public class Utilities
    {
        public static void CloseDbObjects(SqlConnection conn, SqlCommand cmd, SqlDataReader rdr)
        {
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }

            if (cmd != null)
            {
                cmd.Dispose();
                cmd = null;
            }

            if (rdr != null)
            {
                rdr.Close();
                rdr.Dispose();
                rdr = null;
            }
        }
        public static Int64 GetSafeLong(object val)
        {
            Int64 result = 0;
            Int64 valInt = 0;

            if (val != DBNull.Value && val != null && val.ToString().Length > 0)
            {
                string valStr = val.ToString();
                Int64.TryParse(valStr, out valInt);

                if (valInt != 0)
                    result = valInt;
            }

            return result;
        }
        public static int GetSafeInt(object val)
        {
            int result = 0;
            int valInt = 0;

            if (val != DBNull.Value && val != null && val.ToString().Length > 0)
            {
                string valStr = val.ToString();
                Int32.TryParse(valStr, out valInt);

                if (valInt != 0)
                    result = valInt;
            }

            return result;
        }
        public static DateTime GetSafeDate(object val)
        {
            DateTime result = DateTime.MinValue;

            if (val != DBNull.Value && val != null)
            {
                string valStr = val.ToString();
                DateTime.TryParse(valStr, out result);
            }

            return result;
        }
        public static string GetSafeString(object val)
        {
            string result = string.Empty;

            if (val != DBNull.Value && val != null)
                result = Convert.ToString(val);

            return result;
        }
    }
}
