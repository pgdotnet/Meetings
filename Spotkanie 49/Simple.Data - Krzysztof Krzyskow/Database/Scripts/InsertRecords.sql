USE [DemoDB]
GO

DELETE FROM [dbo].[Cast]
DELETE FROM [dbo].[Actor]
DBCC CHECKIDENT ('[dbo].[Actor]', RESEED, 0)
DELETE FROM [dbo].[Movie]
DBCC CHECKIDENT ('[dbo].[Movie]', RESEED, 0)
DELETE FROM [dbo].[Genre]
DBCC CHECKIDENT ('[dbo].[Director]', RESEED, 0)
DELETE FROM [dbo].[Director]

INSERT INTO [dbo].[Genre]([GenreID],[Type])VALUES(1,'Sci-Fi');
INSERT INTO [dbo].[Genre]([GenreID],[Type])VALUES(2,'Action');
INSERT INTO [dbo].[Genre]([GenreID],[Type])VALUES(3,'Western');
INSERT INTO [dbo].[Genre]([GenreID],[Type])VALUES(4,'Comedy');
INSERT INTO [dbo].[Genre]([GenreID],[Type])VALUES(5,'Drama');
INSERT INTO [dbo].[Genre]([GenreID],[Type])VALUES(6,'Criminal');

INSERT INTO [dbo].[Actor]([FirstName],[LastName]) VALUES ('Matthew','McConaughey');
INSERT INTO [dbo].[Actor]([FirstName],[LastName]) VALUES ('Anna','Hathaway');
INSERT INTO [dbo].[Actor]([FirstName],[LastName]) VALUES ('Leonardo','DiCaprio');
INSERT INTO [dbo].[Actor]([FirstName],[LastName]) VALUES ('Matt','Damon');
INSERT INTO [dbo].[Actor]([FirstName],[LastName]) VALUES ('Michael','Caine');
INSERT INTO [dbo].[Actor]([FirstName],[LastName]) VALUES ('Tom','Hardy');--6
INSERT INTO [dbo].[Actor]([FirstName],[LastName]) VALUES ('Joseph','Gordon-Levitt');--7

INSERT INTO [dbo].[Director]([FirstName],[LastName]) VALUES ('Christopher','Nolan');
INSERT INTO [dbo].[Director]([FirstName],[LastName]) VALUES ('Quentin','Tarantino');
INSERT INTO [dbo].[Director]([FirstName],[LastName]) VALUES ('Martin','Scorsese');
INSERT INTO [dbo].[Director]([FirstName],[LastName]) VALUES ('Jean-Marc','Vallée');

INSERT INTO [dbo].[Movie]([GenreID],[Title],[Year],[Profit],[OpenningProfit],[Budget],[DirectorID]) VALUES (1,'Interstellar' ,'07-SEP-2014',665.3,47.5,215.0,1);
INSERT INTO [dbo].[Cast]([ActorID],[MovieID]) VALUES (1,1)
INSERT INTO [dbo].[Cast]([ActorID],[MovieID]) VALUES (2,1)
INSERT INTO [dbo].[Cast]([ActorID],[MovieID]) VALUES (4,1)
INSERT INTO [dbo].[Cast]([ActorID],[MovieID]) VALUES (5,1)

INSERT INTO [dbo].[Movie]([GenreID],[Title],[Year],[Profit],[OpenningProfit],[Budget],[DirectorID]) VALUES (3,'Dallas Buyers Club' ,'14-MAR-2014',55.0,0.2,5.0, 4);
INSERT INTO [dbo].[Cast]([ActorID],[MovieID]) VALUES (1,2)

INSERT INTO [dbo].[Movie]([GenreID],[Title],[Year],[Profit],[OpenningProfit],[Budget],[DirectorID]) VALUES (4,'The Wolf of Wall Street' ,'03-JAN-2014',391.9,18.3,130.0, 3);
INSERT INTO [dbo].[Cast]([ActorID],[MovieID]) VALUES (1,3)
INSERT INTO [dbo].[Cast]([ActorID],[MovieID]) VALUES (3,3)

INSERT INTO [dbo].[Movie]([GenreID],[Title],[Year],[Profit],[OpenningProfit],[Budget],[DirectorID]) VALUES (6,'The Departed' ,'27-OCT-2006',289.8,26.8,128.0, 3);
INSERT INTO [dbo].[Cast]([ActorID],[MovieID]) VALUES (3,4)
INSERT INTO [dbo].[Cast]([ActorID],[MovieID]) VALUES (4,4)

INSERT INTO [dbo].[Movie]([GenreID],[Title],[Year],[Profit],[OpenningProfit],[Budget],[DirectorID]) VALUES (1,'Inception' ,'07-MAY-2015',823.5,62.7,235.0, 1 );
INSERT INTO [dbo].[Cast]([ActorID],[MovieID]) VALUES (3,5)
INSERT INTO [dbo].[Cast]([ActorID],[MovieID]) VALUES (5,5)
INSERT INTO [dbo].[Cast]([ActorID],[MovieID]) VALUES (6,5)
INSERT INTO [dbo].[Cast]([ActorID],[MovieID]) VALUES (7,5)

INSERT INTO [dbo].[Movie]([GenreID],[Title],[Year],[Profit],[OpenningProfit],[Budget],[DirectorID]) VALUES (3,'Django Unchained' ,'18-JAN-2013',425.3,30.1,125.0, 2 );
INSERT INTO [dbo].[Cast]([ActorID],[MovieID]) VALUES (3,6)

INSERT INTO [dbo].[Movie]([GenreID],[Title],[Year],[Profit],[OpenningProfit],[Budget],[DirectorID]) VALUES (1,'The Dark Knight Rises' ,'27-JUL-2012',1079.3,160.8,325.0, 1 );
INSERT INTO [dbo].[Cast]([ActorID],[MovieID]) VALUES (5,7)
INSERT INTO [dbo].[Cast]([ActorID],[MovieID]) VALUES (2,7)
INSERT INTO [dbo].[Cast]([ActorID],[MovieID]) VALUES (6,7)
INSERT INTO [dbo].[Cast]([ActorID],[MovieID]) VALUES (7,7)



GO


GO


