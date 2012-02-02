create table Orders
(
	OrderId bigint primary key identity
	,Comments varchar(max) not null default ''
	,DatePlaced datetime not null default getdate()
	,LastModified timestamp not null
)