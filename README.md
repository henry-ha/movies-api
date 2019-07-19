# movies-api
Search and rank movies

Please run the following line 3 times in Package Manager Console to update the database and seed data:
entityframework\update-database

Notes: 
* 1st update-database populates movies and users
* 2nd update-database populates user ratings
* 3rd works out the average rating stored on the movies table as a cache so it doesn't need to compute the average every time it is queried from the API