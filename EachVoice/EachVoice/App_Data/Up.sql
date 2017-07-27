--***********************Set Up***********************
--create database first but if it exits drop it off first
drop database if exists [EVUser]	
create database [EVUser] on primary(
name=[EVUser],filename='C:\Users\mazhe\Documents\cs461\returnnull_zhendong\EachVoice\EachVoice\App_Data\EVUser.mdf')
log on(name=[User_log], filename='C:\Users\mazhe\Documents\cs461\returnnull_zhendong\EachVoice\EachVoice\App_Data\EVUser_log.ldf')
--*************************************************************
--drop if off even it is in use, this piss me off all the time and Math as well
alter database [EVUser] set single_user with rollback immediate;

use [EVUser]
go

CREATE TABLE [dbo].[AspNetRoles] (
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY ([Id])
)

CREATE TABLE [dbo].[AspNetUsers] (
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256),
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max),
	[SecurityStamp] [nvarchar](max),
	[PhoneNumber] [nvarchar](450),
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL default 1,
	[LockoutEndDateUtc] [datetime],
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[County] [nvarchar](100) not null,	
	CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY ([Id])
)
--******************--ZMA create a unique column which null is not allowed and is not a key, is imposible so, I create a index to handle this,
--******************whenever a null comes in then there will be a unique column in this case will be user id to be converted to the same type of the phonenumber
alter table [aspnetusers] add PhoneNumber_Converted as (case when PhoneNumber is null then cast(id as nvarchar(450)) else PhoneNumber end)
CREATE UNIQUE INDEX UQPhoneNumber_idx ON [aspnetusers] (PhoneNumber_Converted)
GO



CREATE TABLE [dbo].[AspNetUserClaims] (
	[Id] [int] NOT NULL IDENTITY,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max),
	[ClaimValue] [nvarchar](max),
	CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]([UserId])
CREATE TABLE [dbo].[AspNetUserLogins] (
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey], [UserId])
)
CREATE INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]([UserId])
CREATE TABLE [dbo].[AspNetUserRoles] (
	[RoleId] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY ([RoleId], [UserId])
)
 

CREATE INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]([RoleId])
CREATE INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]([UserId])
ALTER TABLE [dbo].[AspNetUserClaims] ADD CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[AspNetUserLogins] ADD CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[AspNetUserRoles] ADD CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[AspNetUserRoles] ADD CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE

--************************* to create a highest normal form table, should have break original userComments into 3 tables************************************
--table 1 ([pk_blid], bltitle)
--table 2 ([pk_uid+blid], votebit)
--table 3 ([pk_uid+blid+comt])
--I only have create one table for convient to create EF and does not affect anything so far


create table [dbo].[UserComments]
(
	id int not null identity(1,1),
	blid nvarchar(450) not null,
	comt nvarchar(max) not null,
	netuid nvarchar(128),
	bltitle nvarchar(max) not null,
	constraint [pk_UserComments] primary key clustered([id]asc)
	--constraint [pk_UserComments] primary key ([blid],[netuid])
)
--a clustered id index added which is necessary to use EFramework
create table [dbo].[UserVotes](
	id int not null identity(1,1),
	ucblid nvarchar(450) not null,
	ucnetuid nvarchar(128) not null,
	votebit int,
	constraint [pk_UserVotes] primary key ([id]asc)
	--composite key at the entity table not working properly? 
	--constraint [fk_UserVotes_UserComments] foreign key ([ucblid],[ucnetuid]) references[dbo].[UserComments]([blid],[netuid])
	--constraint [fk_UserVotes_UserComments] foreign key ([ucnetuid]) references[dbo].[UserComments]([netuid])
)
--***************new table from math
CREATE TABLE [dbo].[RepVotes] (
   [Id]       INT  NOT NULL identity(1,1),
   [UserID]   NVARCHAR (50) NOT NULL,
   [RepName]  NVARCHAR (50) NOT NULL,
   [RepID]    NVARCHAR (50) NOT NULL,
   [Approval] INT           NOT NULL
    CONSTRAINT [PK_dbo.RepVotes] PRIMARY KEY ([Id])
);

CREATE TABLE [dbo].[Event] (
    [id]         INT          IDENTITY (1, 1) NOT NULL,
    [text]       VARCHAR (50) NOT NULL,
    [eventstart] DATETIME     NOT NULL,
    [eventend]   DATETIME     NOT NULL,
    CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED ([id] ASC)
);

