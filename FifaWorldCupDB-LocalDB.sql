USE [FifaWorldCupDB]
GO
/****** Object:  Table [dbo].[Matches]    Script Date: 2022-11-21 9:17:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Matches](
	[MatchId] [int] IDENTITY(1,1) NOT NULL,
	[MatchNumber] [int] NULL,
	[Stage] [nvarchar](50) NULL,
	[TeamOneId] [int] NOT NULL,
	[TeamTwoId] [int] NOT NULL,
	[TeamOneGoals] [int] NULL,
	[TeamTwoGoals] [int] NULL,
	[WinningTeamId] [int] NULL,
 CONSTRAINT [PK__Matches__4218C817D87FB3F4] PRIMARY KEY CLUSTERED 
(
	[MatchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teams]    Script Date: 2022-11-21 9:17:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teams](
	[TeamId] [int] IDENTITY(1,1) NOT NULL,
	[TeamName] [nvarchar](100) NOT NULL,
	[GroupName] [nchar](10) NULL,
	[WinningFactor] [float] NULL,
	[RoundOf16Winner] [bit] NULL,
	[QuarterFinalWinner] [bit] NULL,
	[SemiFinalWinner] [bit] NULL,
	[FinalWinner] [bit] NULL,
	[CheatTeam] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[TeamId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Teams] ON 

INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (1, N'Netherlands', N'A         ', 0.7, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (2, N'Ecuador', N'A         ', 0.7, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (3, N'Qatar', N'A         ', 0.3, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (4, N'Senegal', N'A         ', 0.3, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (5, N'England', N'B         ', 0.7, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (6, N'Wales', N'B         ', 0.7, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (7, N'USA', N'B         ', 0.3, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (8, N'Iran', N'B         ', 0.3, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (9, N'Argentina', N'C         ', 0.7, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (10, N'Mexico', N'C         ', 0.7, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (11, N'Poland', N'C         ', 0.3, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (12, N'Saudi Arabia', N'C         ', 0.3, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (13, N'France', N'D         ', 0.7, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (14, N'Denmark', N'D         ', 0.7, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (15, N'Australia', N'D         ', 0.3, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (16, N'Tunisia', N'D         ', 0.3, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (17, N'Germany', N'E         ', 0.7, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (18, N'Spain', N'E         ', 0.7, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (19, N'Japan', N'E         ', 0.3, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (20, N'Costa Rica', N'E         ', 0.3, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (21, N'Belgium', N'F         ', 0.7, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (22, N'Canada', N'F         ', 0.7, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (23, N'Croatia', N'F         ', 0.3, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (24, N'Morocco', N'F         ', 0.3, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (25, N'Brazil', N'G         ', 0.7, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (26, N'Switzerland', N'G         ', 0.7, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (27, N'Cameroon', N'G         ', 0.3, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (28, N'Serbia', N'G         ', 0.3, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (29, N'Portugal', N'H         ', 0.7, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (30, N'Uruguay', N'H         ', 0.7, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (31, N'South Korea', N'H         ', 0.3, 0, 0, 0, 0, 0)
INSERT [dbo].[Teams] ([TeamId], [TeamName], [GroupName], [WinningFactor], [RoundOf16Winner], [QuarterFinalWinner], [SemiFinalWinner], [FinalWinner], [CheatTeam]) VALUES (32, N'Ghana', N'H         ', 0.3, 0, 0, 0, 0, 0)
SET IDENTITY_INSERT [dbo].[Teams] OFF
GO
ALTER TABLE [dbo].[Teams] ADD  DEFAULT ((0)) FOR [RoundOf16Winner]
GO
ALTER TABLE [dbo].[Teams] ADD  DEFAULT ((0)) FOR [QuarterFinalWinner]
GO
ALTER TABLE [dbo].[Teams] ADD  DEFAULT ((0)) FOR [SemiFinalWinner]
GO
ALTER TABLE [dbo].[Teams] ADD  DEFAULT ((0)) FOR [FinalWinner]
GO
ALTER TABLE [dbo].[Teams] ADD  DEFAULT ((0)) FOR [CheatTeam]
GO
ALTER TABLE [dbo].[Matches]  WITH CHECK ADD  CONSTRAINT [FK_Matches_ToTeams_1] FOREIGN KEY([TeamOneId])
REFERENCES [dbo].[Teams] ([TeamId])
GO
ALTER TABLE [dbo].[Matches] CHECK CONSTRAINT [FK_Matches_ToTeams_1]
GO
ALTER TABLE [dbo].[Matches]  WITH CHECK ADD  CONSTRAINT [FK_Matches_ToTeams_2] FOREIGN KEY([TeamTwoId])
REFERENCES [dbo].[Teams] ([TeamId])
GO
ALTER TABLE [dbo].[Matches] CHECK CONSTRAINT [FK_Matches_ToTeams_2]
GO
