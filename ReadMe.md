cd where dockerfile is
```
docker build -t postgres-db:workshop .
docker volume create database_data
```

running db container
This command creates a new container from an image and starts it. If you use the same container name with "docker run" and a container with that name already exists (even if it's stopped), you'll get a naming conflict error.
```
docker run -d \
--name postgres-container \
-p 5432:5432 \
-v database_data:/var/lib/mysql \
postgres-db:workshop
```


docker start:
This command starts an existing (stopped) container. It doesn't create a new one; it simply re-launches the container that already exists.
```
docker start postgres-container
```

docker stop:
```
 docker stop postgres-container
```


Then run first migration
```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Populate DB (enter password: ieeecs@usf|is|amazing)
```
psql -h localhost -U akmalchik -d workshopdb -f populate.sql
```

HW 
Master All Concepts
Apply HttpContext for all controllers
Add Friendship relationship (so user can see friends post)
Add Count For Likes and Dislikes for each post (several ways how to make)
Improve the way how password is stored (it should not be just plain text)

I will share my solution before Spring Break Starts , so you can spend some time over break learning more usefulstuff
