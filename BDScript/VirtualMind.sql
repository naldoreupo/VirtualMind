USE [VirtualMindDB]
GO
/****** Object:  Table [dbo].[Purchase]    Script Date: 11/22/2020 5:25:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchase](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Amount] [float] NOT NULL,
	[CurrencyCode] [char](3) NOT NULL,
	[ExchangeRate] [float] NOT NULL,
	[ExchangeAmount] [float] NOT NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Purchase] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Purchase] ADD  CONSTRAINT [DF_Purchase_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
