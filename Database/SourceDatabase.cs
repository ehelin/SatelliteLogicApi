using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Shared.dto.source;
using Shared.interfaces;
using Shared;

namespace Database
{
    public class SourceDatabase : ISourceDatabase
    {
        public IList<SourceData> GetBuffer(long lastId)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            IList<SourceData> updates = new List<SourceData>();

            try
            {
                conn = new SqlConnection(SourceDbConstants.DB_CONNECTION);
                cmd = conn.CreateCommand();
                cmd.CommandText = SourceDbConstants.SQL_GET_RECORDS_PRE + SourceDbConstants.SOURCE_BUFFER_SIZE.ToString() + SourceDbConstants.SQL_GET_RECORDS_POST;
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.Add(new SqlParameter("@lastId", lastId));

                cmd.Connection.Open();

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    SourceData sd = new SourceData();

                    sd.Id = Utilities.GetSafeInt(rdr[0]);
                    sd.Type = Utilities.GetSafeString(rdr[1]);
                    sd.Data = Utilities.GetSafeString(rdr[2]);
                    sd.Created = Utilities.GetSafeDate(rdr[3]);

                    updates.Add(sd);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Utilities.CloseDbObjects(conn, cmd, rdr);
            }

            return updates;
        }
    }
}
