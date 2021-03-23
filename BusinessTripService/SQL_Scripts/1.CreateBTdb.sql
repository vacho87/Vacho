CREATE DATABASE BTdb;
GO

USE BTdb;

CREATE TABLE Users
(
	ID tinyint CONSTRAINT PK_UserID PRIMARY KEY IDENTITY,
	UserName nvarchar(20) UNIQUE NOT NULL,
	Pasword nvarchar(10) NOT NULL
)


CREATE TABLE Ranks
(
	ID tinyint IDENTITY PRIMARY KEY,
	[Rank] nvarchar(20) UNIQUE NOT NULL
)


CREATE TABLE LocalityTypes
(
	ID tinyint IDENTITY PRIMARY KEY,
	[Type] nvarchar(25) UNIQUE NOT NULL	
)


CREATE TABLE Localities
(
	ID tinyint IDENTITY PRIMARY KEY,
	[Name] nvarchar(30) UNIQUE NOT NULL,
	LocalityTypeID tinyint FOREIGN KEY REFERENCES LocalityTypes (ID) NOT NULL
)


CREATE TABLE BusinessTripPurposes
(
	ID tinyint IDENTITY PRIMARY KEY,
	Purpose nvarchar(60) UNIQUE NOT NULL,
	ExpenditureItem smallint NOT NULL
)


CREATE TABLE TransitRates
(
	ID tinyint IDENTITY PRIMARY KEY,
	BeginDate date NOT NULL,
	EndDate date NUll,
	Rate smallMoney NOT NULL
)


CREATE TABLE DailyRates
(
	ID tinyint IDENTITY PRIMARY KEY,
	BeginDate date NOT NULL,
	EndDate date NUll,
	Rate smallMoney NOT NULL
)


CREATE TABLE DwellingRates
(
	ID tinyint IDENTITY PRIMARY KEY,
	LocalityTypeID tinyint FOREIGN KEY REFERENCES LocalityTypes (ID),	
	BeginDate date NOT NULL,
	EndDate date NUll,
	Rate smallMoney NOT NULL
)


CREATE TABLE Staff
(
	ID smallint IDENTITY PRIMARY KEY,
	RankID tinyint FOREIGN KEY REFERENCES Ranks (ID) NULL,
	FirstName nvarchar(30) NOT NULL,
	LastName nvarchar(30)NOT NULL,
	Patronymic nvarchar(30) NULL,
	UserID tinyint FOREIGN KEY REFERENCES Users (ID) NOT NULL	
)

CREATE TABLE BusinessTripStates   
(
	ID tinyint IDENTITY PRIMARY KEY,  
	Summary nchar(50) NOT NULL
)

CREATE TABLE BusinessTrips
(
	ID int PRIMARY KEY IDENTITY,
	EmployeeID smallint FOREIGN KEY REFERENCES Staff (ID) ON DELETE CASCADE NOT NULL,
	Info nchar(300) NULL,
	StateID tinyint FOREIGN KEY REFERENCES BusinessTripStates(ID) NOT NULL
)


CREATE TABLE OrderInfo
(
	ID smallint IDENTITY PRIMARY KEY,
	BusinessTripID int UNIQUE FOREIGN KEY REFERENCES BusinessTrips (ID) ON DELETE CASCADE NOT NULL,
	PurposeID tinyint FOREIGN KEY (PurposeID) REFERENCES BusinessTripPurposes (ID) NOT NULL,
	LocalityID tinyint FOREIGN KEY REFERENCES Localities (ID) NOT NULL,
	OrderNumber smallint NOT NULL,
	OrderDate date NOT NULL,
	StartDate date NOT NULL,
	EndDate date NOT NULL,	
	Permanent bit NOT NULL,
	TrafficForward bit NOT NULL,
	TrafficBack bit NOT NULL,
	Feeding bit NOT NULL,
	Transport bit NOT NULL,
	Lodging bit NOT NULL,
	RationPack tinyint NOT NULL,
	TeamID smallint NULL,
	Changed bit DEFAULT (0) NOT NULL 
)


CREATE TABLE ChangedOrderInfo
(	
	ID smallint IDENTITY PRIMARY KEY,
	OrderInfoID smallint FOREIGN KEY REFERENCES OrderInfo (ID) ON DELETE CASCADE UNIQUE NOT NULL,
	OrderNumber smallint NOT NULL,
	OrderDate date NOT NULL,
	StartDate date NULL,
	EndDate date NULL,
	LocalityID tinyint FOREIGN KEY REFERENCES Localities (ID) NULL,
	Permanent bit NULL,
	TrafficForward bit NULL,
	TrafficBack bit NULL,
	Feeding bit NULL,
	Transport bit NULL,
	Lodging bit NULL,
	RationPack tinyint NULL,
	PurposeID tinyint FOREIGN KEY REFERENCES BusinessTripPurposes (ID) NULL
)


CREATE TABLE StatementInfo
(	
	ID smallint IDENTITY PRIMARY KEY,
	BusinessTripID int UNIQUE FOREIGN KEY REFERENCES BusinessTrips (ID) ON DELETE CASCADE NOT NULL,
	ChangeOrderNumber smallint NOT NULL,
	StartDate date NOT NULL,
	EndDate date NOT NULL,
	LocalityID tinyint FOREIGN KEY REFERENCES Localities (ID) NOT NULL,
	Permanent bit NOT NULL,
	TrafficForward bit NOT NULL,
	TrafficBack bit NOT NULL,
	Feeding bit NOT NULL,
	FeedingBegin date NULL,
	FeedingEnd date NULL,
	Transport bit NOT NULL,
	Lodging bit NOT NULL,
	RationPack tinyint NOT NULL,
	PrePay smallmoney NULL
)


CREATE TABLE Calculations
(
	ID smallint PRIMARY KEY IDENTITY,
	BusinessTripID int UNIQUE FOREIGN KEY REFERENCES BusinessTrips (ID) ON DELETE CASCADE  NOT NULL,
	TransitCosts smallmoney NULL,
	DailyCosts smallmoney NULL,
	DwellingCosts smallmoney NULL,
	PrePay smallmoney NULL,
	DueToPay smallmoney NULL,
	PaySheetNumber tinyint NOT NULL,
	PaySheetDate date NOT NULL
)


