using System;
using Shared.dto.destination;
using Shared.interfaces;
using System.Data.SqlClient;
using Shared;

namespace DestinationDatabase
{
    public class DestinationDatabase : IDestinationDatabase
    {
        public long GetLastReadMaxRecordId()
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            long lastReadRecord = 0;

            try
            {
                conn = new SqlConnection(SourceDbConstants.DB_CONNECTION);
                cmd = conn.CreateCommand();
                cmd.CommandText = DestinationDbConstants.SQL_GET_MAX_LAST_READ_RECORD;
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Connection.Open();

                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    lastReadRecord = Utilities.GetSafeLong(rdr[0]);
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

            return lastReadRecord;
        }

        public bool InsertClientUpdate(ClientUpdate cu)
        {
            bool goodInsert = false;
            SqlConnection conn = null;
            SqlCommand cmd = null;
            string curFile = string.Empty;

            try
            {
                conn = new SqlConnection(SourceDbConstants.DB_CONNECTION);
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = DestinationDbConstants.SQL_INSERT_CLIENT_UPDATE;
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.Add(new SqlParameter("@id", cu.DbId));
                cmd.Parameters.Add(new SqlParameter("@satellitename", cu.SatelliteName));
                cmd.Parameters.Add(new SqlParameter("@type", cu.Type));
                cmd.Parameters.Add(new SqlParameter("@created", cu.Created));
                cmd.Parameters.Add(new SqlParameter("@onstation", cu.OnStation));
                cmd.Parameters.Add(new SqlParameter("@solarpanelsdeployed", cu.SolarPanelsDeployed));
                cmd.Parameters.Add(new SqlParameter("@planetshift", cu.PlanetShift));
                cmd.Parameters.Add(new SqlParameter("@destinationx", cu.DestinationX));
                cmd.Parameters.Add(new SqlParameter("@destinationy", cu.DestinationY));
                cmd.Parameters.Add(new SqlParameter("@inserted", DateTime.Now));

                conn.Open();

                cmd.ExecuteNonQuery();

                goodInsert = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Utilities.CloseDbObjects(conn, cmd, null);
            }

            return goodInsert;
        }

        public bool InsertStatusUpdate(StatusUpdate su)
        {
            bool goodInsert = false;
            SqlConnection conn = null;
            SqlCommand cmd = null;
            string curFile = string.Empty;

            try
            {
                conn = new SqlConnection(SourceDbConstants.DB_CONNECTION);
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = DestinationDbConstants.SQL_INSERT_STATUS_UPDATE;
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.Add(new SqlParameter("@id", su.DbId));
                cmd.Parameters.Add(new SqlParameter("@satellitename", su.SatelliteName));
                cmd.Parameters.Add(new SqlParameter("@type", su.Type));
                cmd.Parameters.Add(new SqlParameter("@created", su.Created));
                cmd.Parameters.Add(new SqlParameter("@onstation", su.OnStation));
                cmd.Parameters.Add(new SqlParameter("@solarpanelsdeployed", su.SolarPanelsDeployed));
                cmd.Parameters.Add(new SqlParameter("@fuel", su.Fuel));
                cmd.Parameters.Add(new SqlParameter("@power", su.Power));
                cmd.Parameters.Add(new SqlParameter("@planetshift", su.PlanetShift));
                cmd.Parameters.Add(new SqlParameter("@satellitepositionx", su.SatellitePosition.X));
                cmd.Parameters.Add(new SqlParameter("@satellitepositiony", su.SatellitePosition.Y));
                cmd.Parameters.Add(new SqlParameter("@sourcex", su.SourceX));
                cmd.Parameters.Add(new SqlParameter("@sourcey", su.SourceY));
                cmd.Parameters.Add(new SqlParameter("@destinationx", su.DestinationX));
                cmd.Parameters.Add(new SqlParameter("@destinationy", su.DestinationY));
                cmd.Parameters.Add(new SqlParameter("@ascentdirection", su.AscentDirection));
                cmd.Parameters.Add(new SqlParameter("@inserted", DateTime.Now));

                conn.Open();

                cmd.ExecuteNonQuery();

                goodInsert = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Utilities.CloseDbObjects(conn, cmd, null);
            }

            return goodInsert;
        }
    }
}
