using System.Collections.Generic;
using System.Web.Http;
using Shared.interfaces;
using LoadData;

namespace SatelliteLogicApi.Controllers
{
    public class DataLoadController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            //TODO - decide what to return
            throw new System.NotImplementedException();
        }

        // GET api/values/5
        public string Get(int id)
        {
            //TODO - decide what to return
            throw new System.NotImplementedException();
        }

        //--------------------------------------------------------------------------------
        // POST api/values
        [HttpPost]
        public string Post([FromBody]string value)
        {
            bool successful = false;
            string loadMsg = string.Empty;

            if (!string.IsNullOrEmpty(value) && value.Equals("Load") && IsAuthenticated())
            {
                ILoadData ld = new LoadDataImpl();
                ld.LoadSatelliteData();

                successful = true;
            }

            loadMsg = "Load Result: " + successful.ToString();

            return loadMsg;
        }

        private bool IsAuthenticated()
        {
            bool isValidRequest = false;
            string token = Shared.SourceDbConstants.TOKEN;
            string authHeader = string.Empty;

            if (this.Request != null && this.Request.Headers != null && this.Request.Headers.Authorization != null)
            {
                authHeader = this.Request.Headers.Authorization.ToString();

                if (authHeader.Equals(token))
                    isValidRequest = true;
            }

            return isValidRequest;
        }

        //---------------------------------------------------------------------------------

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
            //TODO - decide what to return
            throw new System.NotImplementedException();
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            //TODO - decide what to return
            throw new System.NotImplementedException();
        }
    }
}