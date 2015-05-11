USE [DemoDB]
GO

ALTER TABLE [dbo].[Movie] DROP CONSTRAINT [FK_Movie_Genre]
GO

/****** Object:  Table [dbo].[Movie]    Script Date: 2015-05-08 21:32:00 ******/
DROP TABLE [dbo].[Movie]
GO

/****** Object:  Table [dbo].[Movie]    Script Date: 2015-05-08 21:32:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Movie](
	[MovieID] [int] IDENTITY(1,1) NOT NULL,
	[GenreID] [int] NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Year] [date] NOT NULL,
	[Profit] [decimal](18, 2) NULL,
	[OpenningProfit] [decimal](18, 2) NULL,
	[Budget] [decimal](18, 2) NULL,
	[DirectorID] [int] NULL,
 CONSTRAINT [PK_Movie] PRIMARY KEY CLUSTERED 
(
	[MovieID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Movie]  WITH CHECK ADD  CONSTRAINT [FK_Movie_Genre] FOREIGN KEY([GenreID])
REFERENCES [dbo].[Genre] ([GenreID])
GO

ALTER TABLE [dbo].[Movie] CHECK CONSTRAINT [FK_Movie_Genre]
GO


