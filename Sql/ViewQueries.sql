--initial count on raw update load
select count(*) TotalRecords from dbo.Updates

--counts on staging load
select count(*) TotalClientUpdates from dbo.Staging_ClientUpdate
select count(*) TotalStatusUpdates from dbo.Staging_StatusUpdate

--counts on warehouse load
select count(*) TotalImportedRecords from dbo.SatelliteUpdate su
inner join dbo.Satellite s on s.Id = su.SatelliteId
inner join dbo.SatelliteUpdateType sut on sut.Id = su.SatelliteUpdateTypeId

select top 10 s.SatelliteName,
               sut.SatelliteUpdateType,
			   su.AscentDirection,
			   su.Created,
			   su.DestinationX,
			   su.DestinationY,
			   su.fuel,
			   su.[power],
			   su.SatelliteX,
			   su.SatelliteY,
			   su.SourceX,
			   su.SourceY,
			   su.onStation,
			   su.PlanetShift,
			   su.solarPanelsDeployed
from dbo.SatelliteUpdate su
inner join dbo.Satellite s on s.Id = su.SatelliteId
inner join dbo.SatelliteUpdateType sut on sut.Id = su.SatelliteUpdateTypeId
where sut.SatelliteUpdateType = 'ClientUpdate'

select top 10 s.SatelliteName,
               sut.SatelliteUpdateType,
			   su.AscentDirection,
			   su.Created,
			   su.DestinationX,
			   su.DestinationY,
			   su.fuel,
			   su.[power],
			   su.SatelliteX,
			   su.SatelliteY,
			   su.SourceX,
			   su.SourceY,
			   su.onStation,
			   su.PlanetShift,
			   su.solarPanelsDeployed
from dbo.SatelliteUpdate su
inner join dbo.Satellite s on s.Id = su.SatelliteId
inner join dbo.SatelliteUpdateType sut on sut.Id = su.SatelliteUpdateTypeId
where sut.SatelliteUpdateType = 'StatusUpdate'
