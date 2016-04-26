using Shared.dto.destination;

namespace Shared.interfaces
{
    public interface IDestinationDatabase
    {
        long GetLastReadMaxRecordId();
        bool InsertClientUpdate(ClientUpdate cu);
        bool InsertStatusUpdate(StatusUpdate su);
    }
}
