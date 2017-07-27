--******************************************************************c&p	
insert into aspnetusers ([Id],[EmailConfirmed],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],
	[LockoutEnabled],[AccessFailedCount],[UserName],[County])values('1',0,'2069460762',0,1,1,0,'abc','wa')
insert into aspnetusers ([Id],[EmailConfirmed],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],
	[LockoutEnabled],[AccessFailedCount],[UserName],[County])values('2',0,null,0,1,1,0,'ab','wa')
insert into aspnetusers ([Id],[EmailConfirmed],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],
	[LockoutEnabled],[AccessFailedCount],[UserName],[County])values('3',0,null,0,1,1,0,'a','wa')
update usercomments set netuid = 'e03a9087-ccdf-4b5a-b72c-bb026fd0cb1e' where comt='well done'


--create user role, admin at this case below:
insert into aspnetuserroles (roleid,userid)values(1,'c3c4dc93-bec3-4c96-a57a-a4ec1394d3d6')
insert into [AspNetRoles] values(1,'admin');


insert into usercomments (blid,comt,netuid,bltitle) values('WAB00012685',
		'hahaha it is useful site that we spill out our own opinion',
		'ead3d990-7967-4ada-ac9d-5f785923dd99','haha');

insert into usercomments (blid,comt,netuid,bltitle) values('WAB00012685',
		'haha again it is useful site that we spill out our own opinion',
		'ead3d990-7967-4ada-ac9d-5f785923dd99','haha');




select * from aspnetusers
delete from aspnetusers where id ='471f1e02-27f5-42d7-a409-3053319deb03'
select * from aspnetuserroles
select * from [AspNetUsers]
select * from [AspNetUserLogins]
select * from [AspNetUserRoles]
select * from [AspNetRoles]
select * from aspnetuserclaims
select * from userVotes
select * from userComments




