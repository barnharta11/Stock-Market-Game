USE master
GO

--drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

--create tables
CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	email nvarchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL
	CONSTRAINT PK_user PRIMARY KEY (user_id)
)

Create Table game_status (
	status_id int NOT NULL,
	status_name varchar(50) NOT NULL,
	Constraint pk_status_id Primary Key (status_id),
)

CREATE TABLE games (
	game_id int IDENTITY(1,1) NOT NULL,
	game_name varchar(50) NOT NULL,
	creator_id int NOT NULL,
	start_date datetime NOT NULL,
	end_date datetime NOT NULL,
	--winner_id int,
	Constraint fk_games_users_creator Foreign Key (creator_id) References users(user_id), 
	--Constraint fk_games_users_winner Foreign Key (winner_id) References users (user_id),
	constraint pk_games Primary Key (game_id)
)

Create Table user_games (
	user_id int NOT NULL,
	game_id int NOT NULL,
	balance int NOT NULL,
	user_game_id int IDENTITY(1,1) NOT NULL,
	status_code int NOT NULL,
	Constraint pk_user_game_id Primary Key (user_game_id),
	Constraint fk_user_games_user_id Foreign Key (user_id) References users(user_id),
	Constraint fk_user_games_game_id Foreign Key (game_id) References games (game_id),
	Constraint fk_user_games_game_status Foreign Key (status_code) References game_status (status_id),
	Constraint ck_status_code check(status_code in (0,1))
)



--populate default data
INSERT INTO users (username, email, password_hash, salt, user_role) VALUES ('user', 'makesomethingup@someplace.com', 'Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, email, password_hash, salt, user_role) VALUES ('admin', 'totallynotgamblingatwork@hotmail.com','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');
INSERT INTO	game_status (status_id, status_name) VALUES (0, 'Pending'), (1, 'Accepted'), (2, 'Completed');
GO