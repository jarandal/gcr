USE [gcr_db]
GO

/****** Object:  Table [dbo].[Request]    Script Date: 07/04/2013 15:20:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Request](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[Phone] [nvarchar](200) NULL,
	[Text] [nvarchar](200) NOT NULL,
	[Original_Id] [nvarchar](200) NULL,
	[Date] [datetime] NOT NULL,
	[IP] [nvarchar](50) NULL,
	[Reason] [nvarchar](100) NOT NULL,
	[AttachmentsQty] [int] NOT NULL,
	[Attachments] [nvarchar](2000) NULL,
 CONSTRAINT [PK_Request] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Request] ADD  CONSTRAINT [DF_Request_AttachmentsQty]  DEFAULT ((0)) FOR [AttachmentsQty]
GO


