using System;
using Newtonsoft.Json;

namespace Shared.dto.destination
{
    public class ClientUpdate
    {
        [JsonProperty]
        public long DbId { get; set; }

        [JsonProperty]
        public string SatelliteName { get; set; }

        [JsonProperty]
        public string Type { get; set; }

        [JsonProperty]
        public DateTime Created { get; set; }

        [JsonProperty]
        public bool OnStation { get; set; }

        [JsonProperty]
        public bool SolarPanelsDeployed { get; set; }

        [JsonProperty]
        public bool PlanetShift { get; set; }

        [JsonProperty]
        public decimal DestinationX { get; set; }

        [JsonProperty]
        public decimal DestinationY { get; set; }

        public ClientUpdate()
        {
            this.SatelliteName = string.Empty;
            this.Type = string.Empty;
            this.Created = DateTime.Now;
        }
    }
}
