
--23,282,656

truncate table dbo.Staging_StatusUpdate

insert into dbo.Staging_StatusUpdate
select  id,
		[type],

		--on station
		case 
			when charindex('"Onstation":true,', data) > 0 then 1
			else 0
		end,

		--solar panels deployed
		case 
			when charindex(',"SolarPanelsDeployed":true', data) > 0 then 1
			else 0
		end,

		--fuel
		substring(data, 
				CHARINDEX(',"fuel":', data) + len(',"fuel":'),
				charindex(',"power":', data) - (charindex(',"fuel":', data) + len(',"fuel":'))
		),	

		--power
		substring(data, 
				CHARINDEX(',"power":', data) + len(',"power":'),
				charindex(',"PlanetShift":', data) - (charindex(',"power":', data) + len(',"power":'))
		),	
			 
		--planet shift
		case 
			when charindex(',"PlanetShift":true', data) > 0 then 1
			else 0
		end,

		--satellite x
		substring(data, 
				CHARINDEX(',"SatellitePosition":{"X":', data) + len(',"SatellitePosition":{"X":'),
				charindex(',"Y":', data) - (charindex(',"SatellitePosition":{"X":', data) + len(',"SatellitePosition":{"X":'))
		),				

		--satellite y
		substring(data, 
				CHARINDEX(',"Y":', data) + len(',"Y":'),
				charindex('},"SourceX":', data) - (charindex(',"Y":', data) + len(',"Y":'))
		),	
			
		--source x
		substring(data, 
				CHARINDEX(',"SourceX":', data) + len(',"SourceX":'),
				charindex(',"SourceY":', data) - (charindex(',"SourceX":', data) + len(',"SourceX":'))
		),	
			
		--source y
		substring(data, 
				CHARINDEX(',"SourceY":', data) + len(',"SourceY":'),
				charindex(',"DestinationX":', data) - (charindex(',"SourceY":', data) + len(',"SourceY":'))
		),	
			
		--destination x
		substring(data, 
				CHARINDEX(',"DestinationX":', data) + len(',"DestinationX":'),
				charindex(',"DestinationY":', data) - (charindex(',"DestinationX":', data) + len(',"DestinationX":'))
		),	
			
		--destination y
		substring(data, 
				CHARINDEX(',"DestinationY":', data) + len(',"DestinationY":'),
				charindex(',"SatelliteName":', data) - (charindex(',"DestinationY":', data) + len(',"DestinationY":'))
		),	

		--satellite name
		substring(data, 
				CHARINDEX(',"SatelliteName":"', data) + len(',"SatelliteName":"'),
				charindex(',"AscentDirection":"', data) - (charindex(',"SatelliteName":"', data) + len(',"SatelliteName":"') + 1)
		),	
			
		--ascent direction
		substring(data, 
				CHARINDEX(',"AscentDirection":"', data) + len(',"AscentDirection":"'),
				len(data) - (charindex(',"AscentDirection":"', data) + len(',"AscentDirection":"') + 1)
		),	

		--created
		created

FROM [Satellite].[dbo].[Updates]
where [type] like '%StatusUpdate%'

select count(*) recordCount from dbo.Staging_StatusUpdate
select top 100 * from dbo.Staging_StatusUpdate

