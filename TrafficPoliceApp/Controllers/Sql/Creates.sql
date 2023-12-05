create database TrafficPoliceDb;

use TrafficPoliceDb;

create table Fines (
    [Id] int primary key identity,
    [Name] nvarchar(100),
    [Price] money,
    [CarNumber] nvarchar(100),
    [CarName] nvarchar(100),
)