USE [DemoDB]
GO

ALTER TABLE [dbo].[Cast] DROP CONSTRAINT [FK_Cast_Movie]
GO

ALTER TABLE [dbo].[Cast] DROP CONSTRAINT [FK_Cast_Actor]
GO

/****** Object:  Table [dbo].[Cast]    Script Date: 2015-05-08 21:31:37 ******/
DROP TABLE [dbo].[Cast]
GO

/****** Object:  Table [dbo].[Cast]    Script Date: 2015-05-08 21:31:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cast](
	[CastID] [int] IDENTITY(1,1) NOT NULL,
	[ActorID] [int] NOT NULL,
	[MovieID] [int] NOT NULL,
 CONSTRAINT [PK_Cast] PRIMARY KEY CLUSTERED 
(
	[CastID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Cast]  WITH CHECK ADD  CONSTRAINT [FK_Cast_Actor] FOREIGN KEY([ActorID])
REFERENCES [dbo].[Actor] ([ActorID])
GO

ALTER TABLE [dbo].[Cast] CHECK CONSTRAINT [FK_Cast_Actor]
GO

ALTER TABLE [dbo].[Cast]  WITH CHECK ADD  CONSTRAINT [FK_Cast_Movie] FOREIGN KEY([MovieID])
REFERENCES [dbo].[Movie] ([MovieID])
GO

ALTER TABLE [dbo].[Cast] CHECK CONSTRAINT [FK_Cast_Movie]
GO


