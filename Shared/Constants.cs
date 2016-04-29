
namespace Shared
{
    public class SourceDbConstants
    {
        public const int MAX_INTERATION_CNT = 5;
        public const int SOURCE_BUFFER_SIZE = 2000;
        public const string DB_CONNECTION = "";

        public const string CLIENT_UPDATE_SRCH_TERM = "ClientUpdate";
        public const string STATUS_UPDATE_SRCH_TERM = "StatusUpdate";

        public const string SQL_GET_RECORDS_PRE = "SELECT TOP ";

        //Removed 'order by id' at end of this statement since I used a pre-processing statement to order all of the ids to save on time 
        public const string SQL_GET_RECORDS_POST = "  [id],[type],[data],[created] FROM [dbo].[updatesordered] where id > @lastId ";

        public const string TOKEN = "";
    }
    public class DestinationDbConstants
    {
        public const int MAX_ERROR_RETRY = 5;

        public const string DB_CONNECTION = "Data Source=;Initial Catalog=Satellite;Integrated Security=true;";

        public const string SQL_INSERT_CLIENT_UPDATE = "INSERT INTO [dbo].[SatelliteClientUpdatev3]   "
                                                           + "     ([id]   "
                                                           + "     ,[satellitename]  "
                                                           + "     ,[type]  "
                                                           + "     ,[created]  "
                                                           + "     ,[onstation]  "
                                                           + "     ,[solarpanelsdeployed]  "
                                                           + "     ,[planetshift]  "
                                                           + "     ,[destinationx]  "
                                                           + "     ,[destinationy], [inserted])  "
                                                         + " VALUES  "
                                                           + "     (@id, "
                                                           + "     @satellitename, "
                                                           + "     @type, "
                                                           + "     @created, "
                                                           + "     @onstation, "
                                                           + "     @solarpanelsdeployed, "
                                                           + "     @planetshift, "
                                                           + "     @destinationx, "
                                                           + "     @destinationy, "
                                                          + "        @inserted)";

        public const string SQL_INSERT_STATUS_UPDATE = "INSERT INTO [dbo].[SatelliteStatusUpdatev3]  "
                                                      + "      ([id]  "
                                                       + "     ,[satellitename]  "
                                                      + "      ,[type]  "
                                                      + "      ,[created]  "
                                                      + "      ,[onstation]  "
                                                      + "      ,[solarpanelsdeployed]  "
                                                      + "      ,[fuel]  "
                                                      + "      ,[power]  "
                                                      + "      ,[planetshift]  "
                                                      + "      ,[satellitepositionx]  "
                                                      + "      ,[satellitepositiony]  "
                                                      + "      ,[sourcex]  "
                                                      + "      ,[sourcey]  "
                                                      + "      ,[destinationx]  "
                                                      + "      ,[destinationy]  "
                                                      + "      ,[ascentdirection], [inserted])  "
                                                 + "     VALUES  "
                                                  + "          (@id,  "
                                                  + "          @satellitename,  "
                                                  + "          @type,  "
                                                  + "          @created,  "
                                                  + "          @onstation,  "
                                                  + "          @solarpanelsdeployed,  "
                                                  + "          @fuel,  "
                                                  + "          @power,  "
                                                   + "         @planetshift,  "
                                                   + "         @satellitepositionx,  "
                                                   + "         @satellitepositiony,  "
                                                   + "         @sourcex,  "
                                                   + "         @sourcey,  "
                                                   + "         @destinationx,  "
                                                   + "         @destinationy,  "
                                                    + "        @ascentdirection, " 
                                                    + "        @inserted)";
        
        public const string SQL_GET_MAX_LAST_READ_RECORD = " declare @maxId bigint      "
                                                        + "  declare @clientUpdateId bigint      "
                                                        + "  declare @statusUpdateId bigint      "
                                                        + "        "
                                                        + "  select @clientUpdateId = max(id) from dbo.SatelliteClientUpdatev3      "
                                                        + "  select @statusUpdateId = max(id) from dbo.SatelliteStatusUpdatev3      "
                                                        + "        "
                                                        + "  if @clientUpdateId is null      "
                                                        + "      set @clientUpdateId = 0      "
                                                        + "        "
                                                        + "  if @statusUpdateId is null      "
                                                        + "      set @statusUpdateId = 0      "
                                                        + "        "
                                                        + "  if (@clientUpdateId > @statusUpdateId)      "
                                                        + "     set @maxId = @clientUpdateId      "
                                                        + "  else      "
                                                        + "     set @maxId = @statusUpdateId      "
                                                        + "        "
                                                        + "  if (@maxId is null)      "
                                                        + "     set @maxId = 0    "
                                                        + "         "
                                                        + "  select @maxId ";
    }
}
