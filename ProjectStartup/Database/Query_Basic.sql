

select a.title, b.id, u.username, *
from Bookmarks b
inner join Articles a on a.id = b.ArticleId
LEFT join AspNetUsers u on u.id = b.UserId

select * from AspNetUsers


85c8e95d-c1d8-4e80-af00-f64589602764
ed173029-95ca-4575-9be2-8b984f329bb4
