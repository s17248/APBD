-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2020-07-13 10:25:46.652

-- tables
-- Table: Action
CREATE TABLE Action (
    IdAction int  NOT NULL,
    StartTime datetime  NOT NULL,
    EndTime datetime  NULL,
    NeedSpecialEquipment bit  NOT NULL,
    CONSTRAINT Action_pk PRIMARY KEY  (IdAction)
);

-- Table: FireTruck
CREATE TABLE FireTruck (
    IdFireTruck int  NOT NULL,
    OperationalNumber nvarchar(10)  NOT NULL,
    SpecialEquipment bit  NOT NULL,
    CONSTRAINT FireTruck_pk PRIMARY KEY  (IdFireTruck)
);

-- Table: FireTruck_Action
CREATE TABLE FireTruck_Action (
    IdFireTruckAction int  NOT NULL,
    IdFireTruck int  NOT NULL,
    IdAction int  NOT NULL,
    AssignmentDate datetime  NOT NULL,
    CONSTRAINT FireTruck_Action_pk PRIMARY KEY  (IdFireTruckAction)
);

-- Table: Firefighter
CREATE TABLE Firefighter (
    IdFirefighter int  NOT NULL,
    FirstName nvarchar(30)  NOT NULL,
    LastName nvarchar(50)  NOT NULL,
    CONSTRAINT Firefighter_pk PRIMARY KEY  (IdFirefighter)
);

-- Table: Firefighter_Action
CREATE TABLE Firefighter_Action (
    IdAction int  NOT NULL,
    IdFirefighter int  NOT NULL,
    CONSTRAINT Firefighter_Action_pk PRIMARY KEY  (IdAction,IdFirefighter)
);

-- foreign keys
-- Reference: FireTruck_Action_Action (table: FireTruck_Action)
ALTER TABLE FireTruck_Action ADD CONSTRAINT FireTruck_Action_Action
    FOREIGN KEY (IdAction)
    REFERENCES Action (IdAction);

-- Reference: FireTruck_Action_FireTruck (table: FireTruck_Action)
ALTER TABLE FireTruck_Action ADD CONSTRAINT FireTruck_Action_FireTruck
    FOREIGN KEY (IdFireTruck)
    REFERENCES FireTruck (IdFireTruck);

-- Reference: Firefighter_Action_Action (table: Firefighter_Action)
ALTER TABLE Firefighter_Action ADD CONSTRAINT Firefighter_Action_Action
    FOREIGN KEY (IdAction)
    REFERENCES Action (IdAction);

-- Reference: Firefighter_Action_Firefighter (table: Firefighter_Action)
ALTER TABLE Firefighter_Action ADD CONSTRAINT Firefighter_Action_Firefighter
    FOREIGN KEY (IdFirefighter)
    REFERENCES Firefighter (IdFirefighter);

-- End of file.

USE s17248;
INSERT INTO Firefighter (IdFirefighter, FirstName, LastName) VALUES (1, 'Jan', 'Kowalski');
INSERT INTO Firefighter (IdFirefighter, FirstName, LastName) VALUES (2, 'Robert', 'Nowak');
INSERT INTO Firefighter (IdFirefighter, FirstName, LastName) VALUES (3, 'Kacper', 'Mysliwski');

INSERT INTO Action (IdAction, StartTime, EndTime, NeedSpecialEquipment) VALUES (1, '20200630 05:00:00 AM', '20200713 09:30:00 AM', 0);
INSERT INTO Action (IdAction, StartTime, EndTime, NeedSpecialEquipment) VALUES (2, '20200701 10:00:00 PM', '20200701 11:45:00 PM', 0);
INSERT INTO Action (IdAction, StartTime, EndTime, NeedSpecialEquipment) VALUES (3, '20200703 11:00:00 PM', '20200703 12:00:00 PM', 0);
INSERT INTO Action (IdAction, StartTime, EndTime, NeedSpecialEquipment) VALUES (4, '20200704 12:00:00 PM', '20200704 03:50:00 PM', 1);
INSERT INTO Action (IdAction, StartTime, EndTime, NeedSpecialEquipment) VALUES (5, '20200709 07:00:00 PM', '20200709 08:30:00 PM', 1);
INSERT INTO Action (IdAction, StartTime, EndTime, NeedSpecialEquipment) VALUES (6, '20200713 12:00:00 PM', '20200713 13:50:00 PM', 0);

INSERT INTO FireTruck (IdFireTruck, OperationalNumber, SpecialEquipment) VALUES (1, 'WOZ001', 0);
INSERT INTO FireTruck (IdFireTruck, OperationalNumber, SpecialEquipment) VALUES (2, 'WOZ002', 0);
INSERT INTO FireTruck (IdFireTruck, OperationalNumber, SpecialEquipment) VALUES (3, 'WOZ003', 1);

INSERT INTO Firefighter_Action (IdFirefighter, IdAction) VALUES (1, 1);
INSERT INTO Firefighter_Action (IdFirefighter, IdAction) VALUES (2, 1);
INSERT INTO Firefighter_Action (IdFirefighter, IdAction) VALUES (3, 2);
INSERT INTO Firefighter_Action (IdFirefighter, IdAction) VALUES (1, 3);
INSERT INTO Firefighter_Action (IdFirefighter, IdAction) VALUES (3, 3);
INSERT INTO Firefighter_Action (IdFirefighter, IdAction) VALUES (2, 4);
INSERT INTO Firefighter_Action (IdFirefighter, IdAction) VALUES (3, 4);
INSERT INTO Firefighter_Action (IdFirefighter, IdAction) VALUES (1, 5);
INSERT INTO Firefighter_Action (IdFirefighter, IdAction) VALUES (2, 5);
INSERT INTO Firefighter_Action (IdFirefighter, IdAction) VALUES (3, 6);

SELECT * FROM Firefighter;
SELECT * FROM Action;
SELECT * FROM FireTruck;
SELECT * FROM Firefighter_Action;


SELECT f.IdFirefighter, a.IdAction, a.StartTime, a.EndTime FROM Action a JOIN Firefighter_Action f ON a.IdAction = f.IdAction ORDER BY f.IdFirefighter;
