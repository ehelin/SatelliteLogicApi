using Shared.dto.source;
using Shared.dto.destination;
using Shared.interfaces;
using System.Collections.Generic;
using Newtonsoft.Json;
using Shared;
using System;

namespace LoadData
{
    public class LoadDataImpl : ILoadData
    {
        public void LoadSatelliteData()
        {
            int iterationCnt = 0;

            try
            {
                RecordInterationError("Starting! " + DateTime.Now.ToString());
                bool done = false;

                //while (iterationCnt < SourceDbConstants.MAX_INTERATION_CNT)
                while(!done)
                {
                    done = RunLoad();
                    iterationCnt++;
                    RecordInterationError("Iteration is " + iterationCnt.ToString() + " is done!");
                }
            }
            catch (Exception e)
            {
                RecordInterationError("ERROR: Iteration is " + iterationCnt.ToString() + ", Error Msg: " + e.Message);
            }

            RecordInterationError("Done! " + DateTime.Now.ToString());
        }

        private void RecordInterationError(string msg)
        {
            IDestinationDatabase dd = new DestinationDatabase.DestinationDatabase();
            ClientUpdate cu = new ClientUpdate();
            cu.Type = msg;
            cu.Created = DateTime.Now;
            dd.InsertClientUpdate(cu);

            StatusUpdate su = new StatusUpdate();
            su.Type = msg;
            su.Created = DateTime.Now;
            dd.InsertStatusUpdate(su);
        }

        private bool RunLoad()
        {
            bool done = false;
            ISourceDatabase sd = new Database.SourceDatabase();
            IDestinationDatabase dd = new DestinationDatabase.DestinationDatabase();

            long lastMaxReadRecordId = dd.GetLastReadMaxRecordId();
            IList<SourceData> srcUpdates = sd.GetBuffer(lastMaxReadRecordId);
            int ctr = 0;

            if (srcUpdates == null || srcUpdates.Count <= 0)
            {
                done = true;
            }
            else {

                foreach (SourceData srcUpdate in srcUpdates)
                {
                    if (srcUpdate.Type.IndexOf(SourceDbConstants.CLIENT_UPDATE_SRCH_TERM) != -1)
                    {
                        ClientUpdate cu = JsonConvert.DeserializeObject<ClientUpdate>(srcUpdate.Data);
                        cu.Created = srcUpdate.Created;
                        cu.Type = srcUpdate.Type;
                        cu.DbId = srcUpdate.Id;
                        InsertClientUpdate(cu);
                    }
                    else
                    {
                        StatusUpdate su = JsonConvert.DeserializeObject<StatusUpdate>(srcUpdate.Data);
                        su.Created = srcUpdate.Created;
                        su.Type = srcUpdate.Type;
                        su.DbId = srcUpdate.Id;
                        InsertStatusUpdate(su);
                    }

                    ctr++;
                }
            }

            return done;
        }

        private void InsertClientUpdate(ClientUpdate cu)
        {
            IDestinationDatabase dd = new DestinationDatabase.DestinationDatabase();
            bool loaded = false;
            int errorRetry = 0;

            while (!loaded)
            {
                try
                {
                    dd.InsertClientUpdate(cu);
                    loaded = true;
                }
                catch (Exception e)
                {
                    if (errorRetry > DestinationDbConstants.MAX_ERROR_RETRY)
                    {
                        RecordInterationError(e.Message);
                        throw new Exception("Cannot continue.  Error: " + e.Message);
                    }
                    else
                    {
                        errorRetry++;
                    }
                }
                finally
                {
                    //nothing to close out?
                }
            }
        }

        private void InsertStatusUpdate(StatusUpdate su)
        {
            IDestinationDatabase dd = new DestinationDatabase.DestinationDatabase();
            bool loaded = false;
            int errorRetry = 0;

            while (!loaded)
            {
                try
                {
                    dd.InsertStatusUpdate(su);
                    loaded = true;
                }
                catch (Exception e)
                {
                    if (errorRetry > DestinationDbConstants.MAX_ERROR_RETRY)
                    {
                        RecordInterationError(e.Message);
                        throw new Exception("Cannot continue.  Error: " + e.Message);
                    }
                    else
                    {
                        errorRetry++;
                    }
                }
                finally
                {
                    //nothing to close out?
                }
            }
        }
    }
}