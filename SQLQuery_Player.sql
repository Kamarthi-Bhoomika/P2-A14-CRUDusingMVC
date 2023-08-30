create database PlayerDb
use PlayerDb

create table Players
(PlayerId int,
FirstName nvarchar(50),
LastName nvarchar(50),
JerseyNumber int,
Position int,
Team nvarchar(50) )

insert into Players values(202011,'Virat','Kholi',18,3,'RCB')

select * from Players
