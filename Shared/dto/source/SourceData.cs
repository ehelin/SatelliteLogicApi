using System;

namespace Shared.dto.source
{
    public class SourceData
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
        public DateTime Created { get; set; }
    }
}
