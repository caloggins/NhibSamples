begin try
	begin tran;
	create table [Users] (
		Id bigint primary key identity
		,Name varchar(max) not null
	);

	create table UserDetail (
		UserId bigint primary key
		,DriversLicense varchar(max)
		,IsDonor bit not null default 0
	);

	create table Customer (
		Id bigint primary key identity
		,Name varchar(max) not null
		,DateOfBirth date not null default getdate()
		);
		
	create table CustomerAccount (
		CustomerId bigint
		,AccountId bigint
		,[Ownership] varchar(max)
		primary key (CustomerId, AccountId)
		);

	create table Account (
		Id bigint primary key identity
		,AccountNumber varchar(max) not null
	)

	alter table UserDetail with check
		add constraint FK_User_Id foreign key (UserId) references [Users](Id);
		
	alter table CustomerAccount with check
		add constraint FK_Customer_Id foreign key (CustomerId) references Customer(Id);
		
	alter table CustomerAccount with check
		add constraint FK_Account_Id foreign key (AccountId) references Account(Id);
	commit tran;
end try
begin catch
	rollback tran;
end catch;

