CREATE DATABASE organitask;

--

USE organitask;

CREATE TABLE [User] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Username] varchar(50) NOT NULL,
  [Email] varchar(100) UNIQUE NOT NULL,
  [Password] varchar(255) NOT NULL
);

CREATE TABLE [Dashboard] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] varchar(100) NOT NULL,
  [Description] text,
  [UserId] int NOT NULL,
  CONSTRAINT FK_Dashboard_User FOREIGN KEY (UserId)
	REFERENCES [User] (Id)
	ON DELETE CASCADE
);

CREATE TABLE [Category] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Title] varchar(50) NOT NULL,
  [DashboardId] int NOT NULL,
  CONSTRAINT FK_Category_Dashboard FOREIGN KEY (DashboardId)
	REFERENCES [Dashboard] (Id)
	ON DELETE CASCADE
);

CREATE TABLE [Tag] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] varchar(50) NOT NULL,
  [Color] varchar(20),
  [Description] text,
  [CategoryId] int NOT NULL,
  CONSTRAINT FK_Tag_Category FOREIGN KEY (CategoryId)
	REFERENCES Category (Id)
	ON DELETE CASCADE
);

CREATE TABLE [Task] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Title] varchar(100) NOT NULL,
  [Description] text,
  [StartDate] datetime,
  [EndDate] datetime,
  [DashboardId] int NOT NULL,
  CONSTRAINT FK_Task_Dashboard FOREIGN KEY (DashboardId)
        REFERENCES [Dashboard] (Id)
        ON DELETE CASCADE
);

CREATE TABLE [TaskTag] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [TaskId] int NOT NULL,
  [TagId] int NULL,
  CONSTRAINT FK_TaskTag_Task FOREIGN KEY (TaskId)
	REFERENCES [Task] (Id)
	ON DELETE CASCADE,
  CONSTRAINT FK_TaskTag_Tag FOREIGN KEY (TagId)
	REFERENCES [Tag] (Id)
	ON DELETE NO ACTION
);

--

USE organitask;

INSERT INTO [User] (Username, Email, Password)
VALUES ('alice', 'alice@example.com', 'password1');

INSERT INTO [User] (Username, Email, Password)
VALUES ('bob', 'bob@example.com', 'password2');

--
INSERT INTO [Dashboard] (Name, Description, UserId)
VALUES ('Personal Board', 'Tablero personal de Alice', 1);

INSERT INTO [Dashboard] (Name, Description, UserId)
VALUES ('Work Board', 'Tablero laboral de Bob', 2);

INSERT INTO [Category] (Title, DashboardId)
VALUES ('Status', 1),
       ('Priority', 1);

INSERT INTO [Category] (Title, DashboardId)
VALUES ('Status', 2),
       ('Priority', 2),
       ('Nature', 2);

INSERT INTO [Tag] (Name, Color, Description, CategoryId)
VALUES ('On Hold', 'Gray', 'Task is on hold', 1),
       ('In Progress', 'Blue', 'Task in progress', 1),
       ('Finished', 'Green', 'Task is finished', 1);

INSERT INTO [Tag] (Name, Color, Description, CategoryId)
VALUES ('Low', 'LightGreen', 'Low priority task', 2),
       ('Medium', 'Yellow', 'Medium priority task', 2),
       ('High', 'Red', 'High priority task', 2);

INSERT INTO [Tag] (Name, Color, Description, CategoryId)
VALUES ('On Hold', 'Gray', 'Task is on hold', 3),
       ('In Progress', 'Blue', 'Task in progress', 3),
       ('Finished', 'Green', 'Task is finished', 3);

INSERT INTO [Tag] (Name, Color, Description, CategoryId)
VALUES ('Low', 'LightGreen', 'Low priority task', 4),
       ('Medium', 'Yellow', 'Medium priority task', 4),
       ('High', 'Red', 'High priority task', 4);

INSERT INTO [Tag] (Name, Color, Description, CategoryId)
VALUES ('Bug', 'Orange', 'Bug related task', 5),
       ('Feature', 'Purple', 'Feature development task', 5),
       ('Research', 'Cyan', 'Research task', 5);

INSERT INTO [Task] (Title, Description, StartDate, EndDate, DashboardId)
VALUES ('Task A', 'Description for Task A', 
        CONVERT(datetime, '2025-03-15 09:00', 120), 
        CONVERT(datetime, '2025-03-15 17:00', 120), 1);

INSERT INTO [Task] (Title, Description, StartDate, EndDate, DashboardId)
VALUES ('Task B', 'Description for Task B', 
        CONVERT(datetime, '2025-03-16 10:00', 120), 
        CONVERT(datetime, '2025-03-16 15:00', 120), 1);

INSERT INTO [Task] (Title, Description, StartDate, EndDate, DashboardId)
VALUES ('Task C', 'Description for Task C', 
        CONVERT(datetime, '2025-03-17 08:30', 120), 
        CONVERT(datetime, '2025-03-17 12:30', 120), 2);

INSERT INTO [Task] (Title, Description, StartDate, EndDate, DashboardId)
VALUES ('Task D', 'Description for Task D', 
        CONVERT(datetime, '2025-03-18 13:00', 120), 
        CONVERT(datetime, '2025-03-18 18:00', 120), 2);

INSERT INTO [TaskTag] (TaskId, TagId)
VALUES (1, 2),  -- 'In Progress'
       (1, 6);  -- 'High'

INSERT INTO [TaskTag] (TaskId, TagId)
VALUES (2, 1),  -- 'On Hold'
       (2, 5);  -- 'Medium'

INSERT INTO [TaskTag] (TaskId, TagId)
VALUES (3, 9),   -- 'Finished'
       (3, 10),  -- 'Low'
       (3, 14);  -- 'Feature'

INSERT INTO [TaskTag] (TaskId, TagId)
VALUES (4, 8),   -- 'In Progress'
       (4, 12),  -- 'High'
       (4, 13);  -- 'Bug'