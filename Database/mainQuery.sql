SELECT *	FROM public."AspNetUsers"

select * from public."Articles"
select * from public."Categories"
select * from public."Thumbnails"
select * from public."Bookmarks"

select * 
from public."Articles" a
inner join public."Bookmarks" b on b."ArticleId" = a."Id"

delete from public."Articles"
delete from public."Bookmarks"
delete FROM public."AspNetUsers"
