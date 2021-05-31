create table pera(dea varchar(5));
drop table dbo.Zone;

drop database MemoApp;

create database MemoApp;
create table Zone (
	Id bigint IDENTITY(1,1) primary key,
	ZoneName nvarchar(MAX) Not Null,
	AspNetUsersId nvarchar(450) Foreign Key References AspNetUsers(Id) Not Null ,
	DateFormat nvarchar(50) Not Null,
	TimeFormat nvarchar(50) Not Null,
	Culture nvarchar(50) Not Null
)
create table Status(
	Id int IDENTITY(1,1) primary key,
	Name nvarchar(50) Not Null,
	Description nvarchar(MAX)
)

create table Memo (
	Id bigint IDENTITY(1,1) primary key,
	Title nvarchar(50) Not Null,
	Note nvarchar(MAX),
	CreatedAt datetime Not Null,
	UpdatedAt datetime,
	StatusId int Foreign Key References Status(Id) Not Null,
	AspNetUsersId nvarchar(450) Foreign Key References AspNetUsers(Id) Not Null 
)
create table Tag (
	Id bigint IDENTITY(1,1) primary key,
	MemoId bigint Foreign Key References Memo(Id) Not Null,
	Name nvarchar(MAX)
	
)
select * from AspNetRoleClaims;
select * from AspNetRoles;
select * from AspNetUserClaims;
select * from AspNetUserLogins;
select * from AspNetUserRoles;
select * from AspNetUsers;
select * from AspNetUserTokens;

select * from Memo;
select * from Zone;
select * from Status;
Insert into Status (Name, Description) values ('Active', 'This is activated');
Insert into Status (Name, Description) values ('Deleted', 'This is deleted');
Insert into Memo (Title, Note, CreatedAt, StatusId, AspNetUsersId) values
	('Beleska', 'Prva beleska', '2020-01-01',1, '70b53fab-d312-4f2e-b0d3-21b66d07c13d')
Insert into Memo (Title, Note, CreatedAt, StatusId, AspNetUsersId) values
	('Beleska2', 'Druga beleska', '2020-02-01',1, '70b53fab-d312-4f2e-b0d3-21b66d07c13d')
Insert into Memo (Title, Note, CreatedAt, StatusId, AspNetUsersId) values
	('Beleska3', 'Treca beleska', '2020-03-01',1, '70b53fab-d312-4f2e-b0d3-21b66d07c13d')
Insert into Memo (Title, Note, CreatedAt, StatusId, AspNetUsersId) values
	('Beleska4', 'Cetvrta beleska', '2020-04-01',1, '70b53fab-d312-4f2e-b0d3-21b66d07c13d')

Insert into AspNetUserRoles(RoleId, UserId) Values ('f37f7bd7-bf2b-4140-bc69-ad080c60a450', '70b53fab-d312-4f2e-b0d3-21b66d07c13d')
Insert into AspNetUserRoles(RoleId, UserId) Values ('05fd7d06-439f-40a8-a924-37786402c08b', 'ebfc8101-e7dd-44a3-b39e-00bed8ca5ad9')

Insert	into Tag(MemoId, Name) Values (1, 'Tag1')
Insert	into Tag(MemoId, Name) Values (1, 'PrviTag')

Insert	into Tag(MemoId, Name) Values (2, 'Tag2')
Insert	into Tag(MemoId, Name) Values (2, 'DrugiTag')

Insert	into Tag(MemoId, Name) Values (3, 'Tag3')
Insert	into Tag(MemoId, Name) Values (3, 'TreciTag')

Insert	into Tag(MemoId, Name) Values (4, 'Tag4')
Insert	into Tag(MemoId, Name) Values (4, 'CetvrtiTag')

Insert into Zone(ZoneName, AspNetUsersId, DateFormat, TimeFormat, Culture) 
Values ('European', '67f7799a-350d-4771-be19-fc58979fabb3',
'dd.mm.yyyy', 'hh:mm:ss', 'fr-FR');
Insert into Zone(ZoneName, AspNetUsersId, DateFormat, TimeFormat, Culture) 
Values ('American', '70b53fab-d312-4f2e-b0d3-21b66d07c13d',
'mm-dd-yyyy', 'hh:mm', 'en-US');
Insert into Zone(ZoneName, AspNetUsersId, DateFormat, TimeFormat, Culture) 
Values ('Spanish', 'ebfc8101-e7dd-44a3-b39e-00bed8ca5ad9',
'YYYY/MM/DD', 'hh:mm:ss.s', 'es');
