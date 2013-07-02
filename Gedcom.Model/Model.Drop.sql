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

