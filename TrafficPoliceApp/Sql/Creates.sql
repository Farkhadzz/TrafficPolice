USE [TrafficPoliceDb];

CREATE TABLE Fines (
	[Id] INT PRIMARY KEY identity,
	[FineName] NVARCHAR(20) NOT NULL,
	[CarNumber] NVARCHAR(10),
	[CarModel] NVARCHAR(10),
	[Price] DECIMAL NOT NULL
)

CREATE TABLE Users (
	[Id] INT PRIMARY KEY identity,
	[FirstName] NVARCHAR(30),
	[LastName] NVARCHAR(30),
	[Age] INT,
	[Email] NVARCHAR(30),
	[Password] NVARCHAR(255)
)

CREATE TABLE Logs (
	[LogId] int primary key identity,
    [UserId] int,
    [Url] nvarchar(max),
    [MethodType] nvarchar(10),
    [StatusCode] int,
    [RequestBody] nvarchar(max),
    [ResponseBody] nvarchar(max)
)