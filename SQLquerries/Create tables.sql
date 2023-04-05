CREATE TABLE Teachers (
	teacherId INT NOT NULL IDENTITY(1,1),
	email VARCHAR(52) NOT NULL,
	password VARCHAR(52) NOT NULL,
	PRIMARY KEY(teacherId)
);

CREATE TABLE Tokens (
	tokenId INT NOT NULL
	PRIMARY KEY(tokenId)
);

CREATE TABLE Subjects (
	subjectId INT NOT NULL IDENTITY(1,1),
	teacherId INT NOT NULL,
	title VARCHAR(52) NOT NULL,
	PRIMARY KEY(subjectId),
	FOREIGN KEY(teacherId) REFERENCES Teachers(teacherId)
);

CREATE TABLE Laboratories (
	laboratoryId INT NOT NULL IDENTITY(1,1),
	subjectId INT NOT NULL,
	number INT NOT NULL,
	date DATE NOT NULL,
	title VARCHAR(52) NOT NULL,
	curricula VARCHAR(104) NOT NULL,
	description VARCHAR(405) NOT NULL,
	assignmentName VARCHAR(52),
	assignmentDL date,
	assignmentDescription VARCHAR(405),
	PRIMARY KEY(laboratoryId),
	FOREIGN KEY(subjectId) REFERENCES Subjects(subjectId),
);

CREATE TABLE Students (
	studentId INT NOT NULL IDENTITY(1,1),
	email VARCHAR(52) NOT NULL,
	password VARCHAR(52) NOT NULL,
	name VARCHAR(52) NOT NULL,
	group INT NOT NULL,
	hobby VARCHAR(104),
	PRIMARY KEY(studentId)
);

CREATE TABLE Submissions (
	submissionId INT NOT NULL IDENTITY(1,1),
	labroatoryId INT NOT NULL,
	studentId INT NOT NULL,
	link VARCHAR(52) NOT NULL,
	comment VARCHAR(104) NOT NULL,
	grade INT,
	PRIMARY KEY(submissionId),
	FOREIGN KEY(laboratoryId) REFERENCES Laboratories(laboratoryId),
	FOREIGN KEY(studentId) REFERENCES Students(studentId),
);

CREATE TABLE Attendance (
	attendanceId INT NOT NULL IDENTITY(1,1),
	labroatoryId INT NOT NULL,
	studentId INT NOT NULL,
	attended INT NOT NULL,
	PRIMARY KEY(attendanceId),
	FOREIGN KEY(laboratoryId) REFERENCES Laboratories(laboratoryId),
	FOREIGN KEY(studentId) REFERENCES Students(studentId),
);

