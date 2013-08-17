IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PK_Families_temp]') AND TYPE IN (N'PK'))
ALTER TABLE [dbo].[Families_temp] DROP CONSTRAINT [PK_Families_temp];
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PK_Individuals_temp]') AND TYPE IN (N'PK'))
ALTER TABLE [dbo].[Individuals_temp] DROP CONSTRAINT [PK_Individuals_temp];
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PK_Events_temp]') AND TYPE IN (N'PK'))
ALTER TABLE [dbo].[Events_temp] DROP CONSTRAINT [PK_Events_temp];
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PK_Media_temp]') AND TYPE IN (N'PK'))
ALTER TABLE [dbo].[Media_temp] DROP CONSTRAINT [PK_Media_temp];

/****** Object:  ForeignKey [FK_IndividualEvent]    Script Date: 06/15/2013 11:34:59 ******/
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.[FK_IndividualEvent]')  AND parent_object_id = OBJECT_ID(N'dbo.Events'))
ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_IndividualEvent];
/****** Object:  ForeignKey [FK_Childrens]    Script Date: 06/15/2013 11:34:59 ******/
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.[FK_Childrens]')  AND parent_object_id = OBJECT_ID(N'dbo.Individuals'))
ALTER TABLE [dbo].[Individuals] DROP CONSTRAINT [FK_Childrens];
/****** Object:  ForeignKey [FK_Media_Individuals]    Script Date: 06/15/2013 11:34:59 ******/
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.[FK_Media_Individuals]')  AND parent_object_id = OBJECT_ID(N'dbo.Media'))
ALTER TABLE [dbo].[Media] DROP CONSTRAINT [FK_Media_Individuals];
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

EXEC sp_rename 'Events_temp' , 'Events';
EXEC sp_rename 'Families_temp' , 'Families';
EXEC sp_rename 'Media_temp' , 'Media';
EXEC sp_rename 'Individuals_temp' , 'Individuals';

ALTER TABLE [dbo].[Families] ADD CONSTRAINT [PK_Families] PRIMARY KEY CLUSTERED ([Id] ASC)
	WITH (ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY];
ALTER TABLE [dbo].[Individuals] ADD CONSTRAINT [PK_Individuals] PRIMARY KEY CLUSTERED ([Id] ASC)
	WITH (ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY];
ALTER TABLE [dbo].[Events] ADD CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED ([Id] ASC)
	WITH (ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY];
ALTER TABLE [dbo].[Media] ADD CONSTRAINT [PK_Media] PRIMARY KEY CLUSTERED ([Id] ASC)
	WITH (ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY];
