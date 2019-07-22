# movies-api
Search and rank movies

* Set both projects as start-up projects from solution properties


In Package Manager Console: **Point Default project to: MoviesWeb**
* Please run the following line 3 times in Package Manager Console to update the database and seed data:
**update-database**

Notes: 
* 1st update-database populates movies and users
* 2nd update-database populates user ratings
* 3rd works out the average rating stored on the movies table as a cache so it doesn't need to compute the average every time it is queried from the API

You can use the search UI in the MoviesWeb .NET Framework project to query the API or anything else like PostMan to query the following endpoint locally:
http://localhost:52630/api/

http://localhost:52630/swagger will list all the API's available endpoint operations in a nice user friendly documentation
