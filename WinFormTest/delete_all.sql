truncate table events
update individuals set family_id = null 
update dbo.Events set Individual_Id = null 
update dbo.Media set Individual_Id = null 
delete from dbo.Events
delete from Media
delete from families
delete from individuals

