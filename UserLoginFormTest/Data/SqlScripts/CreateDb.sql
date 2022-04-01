
CREATE DATABASE TestDb;
USE TestDb;
CREATE TABLE 
	Users ( 
		ID int NOT NULL IDENTITY(1,1) PRIMARY KEY, 
		Email varchar(255) NOT NULL UNIQUE, 
		PasswordHash varchar(255) NOT NULL, 
		Salt varchar(255) NOT NULL
	);

CREATE INDEX index_email
ON Users (Email);