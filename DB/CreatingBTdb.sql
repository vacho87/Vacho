/* First of all to create database execute:
CREATE DATABASE BTdb;
*/
USE BTdb;

CREATE TABLE Users
(
	ID tinyint CONSTRAINT PK_UserID PRIMARY KEY IDENTITY,
	UserName nvarchar(20) CONSTRAINT UQ_UserName UNIQUE NOT NULL,
	Pasword nvarchar(10) NOT NULL
)


CREATE TABLE Ranks
(
	ID tinyint IDENTITY CONSTRAINT PK_RankId PRIMARY KEY,
	[Rank] nvarchar(20) UNIQUE NOT NULL
)


CREATE TABLE LocalityTypes
(
	ID tinyint IDENTITY CONSTRAINT PK_LocalityTypeID PRIMARY KEY,
	[Type] nvarchar(15) UNIQUE NOT NULL	
)


CREATE TABLE Localities
(
	ID tinyint IDENTITY CONSTRAINT PK_LocalityID PRIMARY KEY,
	[Name] nvarchar(30) UNIQUE NOT NULL,
	LocalityTypeID tinyint FOREIGN KEY REFERENCES LocalityTypes (ID) NOT NULL
)


CREATE TABLE BusinessTripPurposes
(
	ID tinyint IDENTITY CONSTRAINT PK_PurposeID PRIMARY KEY,
	Purpose nvarchar UNIQUE NOT NULL,
	ExpenditureItem smallint CHECK (ExpenditureItem = 2026 OR ExpenditureItem = 4026 OR ExpenditureItem = 4146) NOT NULL
)


CREATE TABLE TransitRates
(
	ID tinyint IDENTITY,
	BeginDate date NOT NULL,
	EndDate date NUll,
	Rate smallMoney NOT NULL
)


CREATE TABLE DailyRates
(
	ID tinyint IDENTITY,
	BeginDate date NOT NULL,
	EndDate date NUll,
	Rate smallMoney NOT NULL
)


CREATE TABLE DwellingRates
(
	ID tinyint IDENTITY,
	LocalityTypeId tinyint FOREIGN KEY REFERENCES LocalityTypes (ID),	
	BeginDate date NOT NULL,
	EndDate date NUll,
	Rate smallMoney NOT NULL
)


CREATE TABLE Staff
(
	ID smallint CONSTRAINT PK_EmployeeID PRIMARY KEY IDENTITY,
	RankID tinyint NULL,
	FirstName nvarchar(30) NOT NULL,
	LastName nvarchar(30)NOT NULL,
	Patronymic nvarchar(30) NULL,
	UserID tinyint NOT NULL,
	CONSTRAINT FK_RankID FOREIGN KEY (RankID) REFERENCES Ranks (ID),
	CONSTRAINT FK_UserID FOREIGN KEY (UserID) REFERENCES Users (ID)
)

CREATE TABLE BusinessTrips
(
	ID int PRIMARY KEY IDENTITY,
	EmployeeID smallint FOREIGN KEY REFERENCES Staff (ID) NOT NULL,
	Info nchar(300) NULL,
	BusinessTripState tinyint NOT NULL
)


CREATE TABLE OrderInfo
(
	ID smallint IDENTITY PRIMARY KEY,
	BusinessTripId int FOREIGN KEY REFERENCES BusinessTrips (ID) UNIQUE,
	OrderNumber smallint NOT NULL,
	OrderDate date NOT NULL,
	StartDate date NOT NULL,
	EndDate date NOT NULL,
	LocalityId tinyint FOREIGN KEY REFERENCES Localities (ID) NOT NULL,
	Permanent bit NOT NULL,
	TrafficForward bit NOT NULL,
	TrafficBack bit NOT NULL,
	Feeding bit NOT NULL,
	Transport bit NOT NULL,
	Lodging bit NOT NULL,
	RationPack tinyint NOT NULL,
	TeamId smallint NOT NULL,
	PurposeId tinyint NOT NULL
)


CREATE TABLE ChangedOrderInfo
(	
	OrderInfoId smallint FOREIGN KEY REFERENCES OrderInfo (ID) UNIQUE,
	OrderNumber smallint NOT NULL,
	OrderDate date NOT NULL,
	StartDate date NULL,
	EndDate date NULL,
	LocalityId tinyint FOREIGN KEY REFERENCES Localities (ID) NULL,
	Permanent bit NULL,
	TrafficForward bit NULL,
	TrafficBack bit NULL,
	Feeding bit NULL,
	Transport bit NULL,
	Lodging bit NULL,
	RationPack tinyint NULL,
	PurposeId tinyint NULL
)


CREATE TABLE StatementInfo
(	
	BusinessTripId int FOREIGN KEY REFERENCES BusinessTrips (ID) UNIQUE,
	ChangeOrderNumber smallint NOT NULL,
	StartDate date NOT NULL,
	EndDate date NOT NULL,
	LocalityId tinyint FOREIGN KEY REFERENCES Localities (ID) NOT NULL,
	Permanent bit NOT NULL,
	TrafficForward bit NOT NULL,
	TrafficBack bit NOT NULL,
	Feeding bit NOT NULL,
	Transport bit NOT NULL,
	Lodging bit NOT NULL,
	RationPack tinyint NOT NULL,
	TeamId smallint NOT NULL
)


CREATE TABLE Calculations
(
	ID smallint PRIMARY KEY IDENTITY,
	BusinessTripID int FOREIGN KEY REFERENCES BusinessTrips (ID)NOT NULL,
	TransitCosts smallmoney NULL,
	DailyCosts smallmoney NULL,
	DwellingCosts smallmoney NULL,
	PaySheetNumber tinyint NOT NULL,
	PaySheetDate date NOT NULL
)

ALTER TABLE Calculations
ADD CONSTRAINT UQ_Calculation_BusinessTripId UNIQUE (BusinessTripId);

ALTER TABLE ChangedOrderInfo
ADD FOREIGN KEY (PurposeId) REFERENCES BusinessTripPurposes (ID); 

ALTER TABLE OrderInfo
ADD FOREIGN KEY (PurposeId) REFERENCES BusinessTripPurposes (ID); 


