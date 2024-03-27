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



insert into CourseResult
( Degree ,TraineeId ,CourseId )
values(60 , 5 , 2) , 
( 35 ,5, 3),
( 60 ,5, 4),
( 75 ,5, 5),
( 88 ,5, 6),
( 25 ,5, 7),
( 89 ,5, 8),
( 70 ,5, 9) ,

( 35 ,6, 3),
( 60 ,6, 4),
( 75 ,6, 5),
( 88 ,6, 6),
( 25 ,6, 7),
( 89 ,6, 8),
( 70 ,6, 9) ,

( 35 ,13, 6),
( 60 ,13, 2),
( 75 ,13, 1),
( 88 ,13, 6),
( 25 ,13, 3),
( 89 ,13, 4),
( 70 ,13, 11) ,

( 35 ,8, 2),
( 60 ,8, 5),
( 75 ,8, 2),
( 88 ,8, 8),
( 25 ,8, 9),
( 89 ,8, 1),
( 70 ,8, 13) ,

( 35 ,9, 3),
( 60 ,9, 4),
( 75 ,9, 5),
( 88 ,9, 6),
( 25 ,9, 7),
( 89 ,9, 8),
( 70 ,9, 9) ,

( 35 ,10, 3),
( 60 ,10, 4),
( 75 ,10, 5),
( 88 ,10, 6),
( 25 ,10, 7),
( 89 ,10, 8),
( 70 ,10, 9) ,

( 35 ,11, 6),
( 60 ,11, 2),
( 75 ,11, 1),
( 88 ,11, 6),
( 25 ,11, 3),
( 89 ,11, 4),
( 70 ,11, 11) ,

( 35 ,12, 2),
( 60 ,12, 5),
( 75 ,12, 2),
( 88 ,12, 8),
( 25 ,12, 9),
( 89 ,12, 1),
( 70 ,12, 13) 

