UPDATE individuals SET family_id = null WHERE Id in (
	SELECT Id FROM Individuals WHERE Family_Id not in (SELECT Id FROM Families)
)

/****** Object:  ForeignKey [FK_IndividualEvent]    Script Date: 06/15/2013 11:34:59 ******/
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_IndividualEvent] FOREIGN KEY([Individual_Id])
REFERENCES [dbo].[Individuals] ([Id]);
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_IndividualEvent];
/****** Object:  ForeignKey [FK_Childrens]    Script Date: 06/15/2013 11:34:59 ******/
ALTER TABLE [dbo].[Individuals]  WITH CHECK ADD  CONSTRAINT [FK_Childrens] FOREIGN KEY([Family_Id])
REFERENCES [dbo].[Families] ([Id]);
ALTER TABLE [dbo].[Individuals] CHECK CONSTRAINT [FK_Childrens];
/****** Object:  ForeignKey [FK_Media_Individuals]    Script Date: 06/15/2013 11:34:59 ******/
ALTER TABLE [dbo].[Media]  WITH CHECK ADD  CONSTRAINT [FK_Media_Individuals] FOREIGN KEY([Individual_Id])
REFERENCES [dbo].[Individuals] ([Id]);
ALTER TABLE [dbo].[Media] CHECK CONSTRAINT [FK_Media_Individuals];

CREATE NONCLUSTERED INDEX [IDX_Individual1] ON [dbo].[Individuals] 
(
	[FirstName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY];

CREATE NONCLUSTERED INDEX [IDX_Individual2] ON [dbo].[Individuals] 
(
	[SurName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY];

CREATE NONCLUSTERED INDEX [IDX_Individual3] ON [dbo].[Individuals] 
(
	[FirstName] ASC,
	[SurName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY];

CREATE NONCLUSTERED INDEX [IDX_Individual4] ON [dbo].[Individuals] 
(
	[Family_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY];

CREATE NONCLUSTERED INDEX [IDX_Individual5] ON [dbo].[Individuals] 
(
	[Original_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY];



