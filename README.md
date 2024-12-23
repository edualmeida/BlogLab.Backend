# BlogLab.API

Domain => state core

- Article has an internal constructor, can only be created by using the ArticleBuilder (with validations), which is exposed by the IArticleBuilder to the other layers

Infrastructure => repository access

- Have Application reference 
- Where the access to repositories is located
- Provide the implementation on how to acces the database

Application => business rules

- Have Domain reference
- Can create a article by using the IArticleBuilder, and then save it in the repository by using the infrastructure
- It has the Mediatr handlers.

Web => outside access

- Have Application reference
- Have access to the application layer by using the Mediatr handlers


