truncate table dbo.Staging_ClientUpdate

insert into dbo.Staging_ClientUpdate
select  id,
		[type],

		--satellite name
		substring(data, 
				CHARINDEX('{"SatelliteName":"', data) + len('{"SatelliteName":"'),
				charindex('","Onstation":', data) - len('{"SatelliteName":"') -1
		),

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
			 
		--planet shift
		case 
		when charindex(',"PlanetShift":true', data) > 0 then 1
		else 0
		end,

		--destination x
		substring(data, 
				CHARINDEX(',"DestinationX":', data) + len(',"DestinationX":'),
				charindex(',"DestinationY":', data) - (charindex(',"DestinationX":', data) + len(',"DestinationX":'))
		),	

		--destination y
		substring(data, 
				CHARINDEX(',"DestinationY":', data) + len(',"DestinationY":'),
				len(data) - (charindex(',"DestinationY":', data) + len(',"DestinationY":'))
		),
					
		--created
		created
FROM [Satellite].[dbo].[Updates]
where [type] like '%ClientUpdate%'
--where [type] like '%StatusUpdate%'

select * from dbo.Staging_ClientUpdate