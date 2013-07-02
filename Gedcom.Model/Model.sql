/****** Object:  ForeignKey [FK_IndividualEvent]    Script Date: 06/15/2013 11:34:59 ******/
IF (OBJECT_ID('FK_IndividualEvent', 'F') IS NOT NULL)
BEGIN
ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_IndividualEvent];
END
/****** Object:  ForeignKey [FK_Childrens]    Script Date: 06/15/2013 11:34:59 ******/
IF (OBJECT_ID('FK_Childrens', 'F') IS NOT NULL)
BEGIN
ALTER TABLE [dbo].[Individuals] DROP CONSTRAINT [FK_Childrens];
END
/****** Object:  ForeignKey [FK_Media_Individuals]    Script Date: 06/15/2013 11:34:59 ******/
IF (OBJECT_ID('FK_Media_Individuals', 'F') IS NOT NULL)
BEGIN
ALTER TABLE [dbo].[Media] DROP CONSTRAINT [FK_Media_Individuals];
END
/****** Object:  Table [dbo].[Events]    Script Date: 06/15/2013 11:34:59 ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Events]') AND TYPE IN (N'U'))
DROP TABLE [dbo].[Events];
/****** Object:  Table [dbo].[Media]    Script Date: 06/15/2013 11:34:59 ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Media]') AND TYPE IN (N'U'))
DROP TABLE [dbo].[Media];
/****** Object:  Table [dbo].[Individuals]    Script Date: 06/15/2013 11:34:59 ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Individuals]') AND TYPE IN (N'U'))
DROP TABLE [dbo].[Individuals];
/****** Object:  Table [dbo].[Families]    Script Date: 06/15/2013 11:34:59 ******/
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Families]') AND TYPE IN (N'U'))
DROP TABLE [dbo].[Families];
/****** Object:  Table [dbo].[Families]    Script Date: 06/15/2013 11:34:59 ******/
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
CREATE TABLE [dbo].[Families](
	[Id] [nvarchar](200) NOT NULL,
	[Notes] [varchar](max) COLLATE Modern_Spanish_CI_AI NULL,
	[NotesSummary] [nvarchar](max) COLLATE Modern_Spanish_CI_AI NULL,
	[Husband_Id] [nvarchar](200) NULL,
	[Wife_Id] [nvarchar](200) NULL,
	[Date] [datetime2](7) NULL);
 
/****** Object:  Table [dbo].[Individuals]    Script Date: 06/15/2013 11:34:59 ******/
CREATE TABLE [dbo].[Individuals](
	[Id] [nvarchar](200) NOT NULL,
	[Original_Id] [nvarchar](200) NOT NULL,
	[FirstName] [nvarchar](200) COLLATE Modern_Spanish_CI_AI NOT NULL,
	[SurName] [nvarchar](200) COLLATE Modern_Spanish_CI_AI NOT NULL,
	[Sex] [nvarchar](1) NOT NULL,
	[Notes] [varchar](max) COLLATE Modern_Spanish_CI_AI NULL,
	[NotesSummary] [nvarchar](max) COLLATE Modern_Spanish_CI_AI NULL,
	[Family_Id] [nvarchar](200) NULL,
	[BirthDate] [datetime2](7) NULL,
	[BirthPlace] [nvarchar](200) COLLATE Modern_Spanish_CI_AI NULL,
	[DeathDate] [datetime2](7) NULL,
	[DeathPlace] [nvarchar](200) COLLATE Modern_Spanish_CI_AI NULL,
	[Dead] [bit] NULL);
/****** Object:  Table [dbo].[Media]    Script Date: 06/15/2013 11:34:59 ******/
CREATE TABLE [dbo].[Media](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Filename] [nvarchar](500) NULL,
	[Title] [nvarchar](max) COLLATE Modern_Spanish_CI_AI NULL,
	[Notes] [nvarchar](max) COLLATE Modern_Spanish_CI_AI NULL,
	[SortOrder] [int] NULL,
	[Individual_Id] [nvarchar](200) NULL);
/****** Object:  Table [dbo].[Events]    Script Date: 06/15/2013 11:34:59 ******/
CREATE TABLE [dbo].[Events](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](max) NOT NULL,
	[Date] [datetime2](7) NULL,
	[Notes] [nvarchar](max) COLLATE Modern_Spanish_CI_AI NULL,
	[Individual_Id] [nvarchar](200) NOT NULL,
	[Place] [nvarchar](200) COLLATE Modern_Spanish_CI_AI NULL);

ALTER TABLE [dbo].[Families] ADD CONSTRAINT [PK_Families] PRIMARY KEY CLUSTERED ([Id] ASC)
	WITH (ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY];
ALTER TABLE [dbo].[Individuals] ADD CONSTRAINT [PK_Individuals] PRIMARY KEY CLUSTERED ([Id] ASC)
	WITH (ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY];
ALTER TABLE [dbo].[Events] ADD CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED ([Id] ASC)
	WITH (ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY];
ALTER TABLE [dbo].[Media] ADD CONSTRAINT [PK_Media] PRIMARY KEY CLUSTERED ([Id] ASC)
	WITH (ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY];
