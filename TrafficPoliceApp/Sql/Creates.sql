USE [TrafficPoliceDb];

CREATE TABLE Fines (
	[Id] INT PRIMARY KEY identity,
	[FineName] NVARCHAR(20) NOT NULL,
	[CarNumber] NVARCHAR(10) NOT NULL,
	[CarModel] NVARCHAR(10) NOT NULL,
	[Price] DECIMAL NOT NULL
)

CREATE TABLE Users (
	[Id] INT PRIMARY KEY identity,
	[FirstName] NVARCHAR(30) NOT NULL,
	[LastName] NVARCHAR(30) NOT NULL,
	[Age] INT NOT NULL,
	[Email] NVARCHAR(30) NOT NULL,
	[Password] NVARCHAR(255) NOT NULL
)