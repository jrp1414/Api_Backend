USE [AngPract]
GO

/****** Object:  Table [dbo].[Student]    Script Date: 10/23/2020 13:36:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Student](
	[StudentId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[MobileNo] [nvarchar](20) NULL,
	[EmailId] [nvarchar](50) NULL,
	[AddressId] [int] NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Student]  WITH NOCHECK ADD  CONSTRAINT [FK_Student_Address1] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Address] ([AddressId])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Address1]
GO

