select *
from Instructor

select *
from Trainee

select * 
from AspNetRoles


select * 
from Course


select * 
from  Department

select *
from AspNetRoles

select *
from AspNetUsers

select *
from AspNetUserRoles

--query to select the users who has roles
select u.UserName , R.Name as RoleN
from AspNetUsers U , AspNetUserRoles UR , AspNetRoles R
where R.Id = UR.RoleId 
and U.Id = UR.UserId

select * 
from CourseResult

select *
from Trainee

select * 
from Course

insert into CourseResult
( Degree ,TraineeId ,CourseId )
values(60 , 1 , 2) , 
( 35 ,1, 3),
( 60 ,1, 4),
( 75 ,1, 5),
( 88 ,1, 6),
( 25 ,1, 7),
( 89 ,1, 8),
( 70 ,1, 9) ,

( 35 ,2, 3),
( 60 ,2, 4),
( 75 ,2, 5),
( 88 ,2, 6),
( 25 ,2, 7),
( 89 ,2, 8),
( 70 ,2, 9) ,

( 35 ,3, 6),
( 60 ,3, 2),
( 75 ,3, 1),
( 88 ,3, 6),
( 25 ,3, 3),
( 89 ,3, 4),
( 70 ,3, 11) ,

( 35 ,4, 2),
( 60 ,4, 5),
( 75 ,4, 2),
( 88 ,4, 8),
( 25 ,4, 9),
( 89 ,4, 1),
( 70 ,4, 13) 
