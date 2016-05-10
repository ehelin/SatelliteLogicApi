truncate table dbo.SatelliteUpdate
delete from dbo.SatelliteUpdateType
delete from dbo.Satellite

DBCC CHECKIDENT ('[dbo].[SatelliteUpdateType]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[Satellite]', RESEED, 0);

insert into dbo.SatelliteUpdateType select 'ClientUpdate'
insert into dbo.SatelliteUpdateType select 'StatusUpdate'

insert into dbo.Satellite
select distinct satellitename
from dbo.Staging_ClientUpdate
union 
select distinct satellitename
from dbo.Staging_StatusUpdate

select * from dbo.Satellite
select * from dbo.SatelliteUpdateType

insert into dbo.SatelliteUpdate
select (select Id from dbo.Satellite s where s.SatelliteName = cu.SatelliteName),
       (select Id from dbo.SatelliteUpdateType sut where sut.SatelliteUpdateType = 'ClientUpdate'),
	   cu.Onstation,
	   cu.SolarPanelsDeployed,
	   cu.PlanetShift,
	   -1, --no fuel on client update
	   -1, --no power on client update
	   -1, --no satellite x on client update
	   -1, --no satellite y on client update
	   -1, --no source x on client update
	   -1, --no source y on client update
	   cu.DestinationX,
	   cu.DestinationY,
	   '', --no ascent direction on client update
	   cu.Created
from dbo.Staging_ClientUpdate cu

insert into dbo.SatelliteUpdate
select (select Id from dbo.Satellite s where s.SatelliteName = su.SatelliteName),
       (select Id from dbo.SatelliteUpdateType sut where sut.SatelliteUpdateType = 'StatusUpdate'),
	   su.Onstation,
	   su.SolarPanelsDeployed,
	   su.PlanetShift,
	   su.fuel,
	   su.[power],
	   su.SatellitePosition_X, 
	   su.SatellitePosition_Y, 
	   su.SourceX, 
	   su.SourceY, 
	   su.DestinationX,
	   su.DestinationY,
	   su.AscentDirection, 
	   su.Created
from dbo.Staging_StatusUpdate su

--select top 10 * from dbo.Staging_ClientUpdate
--select top 10 * from dbo.Staging_StatusUpdate

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

