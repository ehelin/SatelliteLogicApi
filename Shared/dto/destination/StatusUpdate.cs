using System;
using Newtonsoft.Json;

namespace Shared.dto.destination
{
    public class StatusUpdate
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
        public bool Fuel { get; set; }

        [JsonProperty]
        public decimal Power { get; set; }

        [JsonProperty]
        public bool PlanetShift { get; set; }

        [JsonProperty]
        public Position SatellitePosition { get; set; }

        [JsonProperty]
        public decimal SourceX { get; set; }

        [JsonProperty]
        public decimal SourceY { get; set; }

        [JsonProperty]
        public decimal DestinationX { get; set; }

        [JsonProperty]
        public decimal DestinationY { get; set; }

        [JsonProperty]
        public string AscentDirection { get; set; }

        public StatusUpdate()
        {
            this.SatelliteName = string.Empty;
            this.Type = string.Empty;
            this.Created = DateTime.Now;
            this.AscentDirection = string.Empty;

            SatellitePosition = new Position(0,0);
        }
    }
}
