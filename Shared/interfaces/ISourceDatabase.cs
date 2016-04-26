using System.Collections.Generic;
using Shared.dto.source;

namespace Shared.interfaces
{
    public interface ISourceDatabase
    {
        IList<SourceData> GetBuffer(long lastId);
    }
}
