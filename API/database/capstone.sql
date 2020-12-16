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
);

Create Table game_status (
	game_status_id int NOT NULL,
	game_status_name varchar(50) NOT NULL,
	Constraint pk_game_status_id Primary Key (game_status_id),
);

CREATE TABLE games (
	game_id int IDENTITY(1,1) NOT NULL,
	game_name varchar(50) NOT NULL,
	creator_id int NOT NULL,
	start_date date NOT NULL,
	end_date date NOT NULL,
	game_status_code int default(0) NOT NULL,
	Constraint fk_games_game_status Foreign Key (game_status_code) References game_status (game_status_id),
	Constraint fk_games_users_creator Foreign Key (creator_id) References users(user_id), 
	--Constraint fk_games_users_winner Foreign Key (winner_id) References users (user_id),
	constraint pk_games Primary Key (game_id),
	Constraint ck_status_code check(game_status_code in (0, 1, 2))
);

Create Table portfolio (
	portfolio_id int IDENTITY(1,1) NOT NULL,
	user_game_id int NOT NULL,
	Constraint pk_portfolio_portfolio_id Primary Key (portfolio_id),
);	

Create Table player_status (
	player_status_id int NOT NULL,
	player_status_name varchar(50) NOT NULL,
	Constraint pk_player_status_player_status_id Primary Key (player_status_id),
);

Create Table user_games (
	user_game_id int IDENTITY(1,1) NOT NULL,
	user_id int NOT NULL,
	game_id int NOT NULL,
	player_status_code int NOT NULL,
	Constraint pk_user_game_id Primary Key (user_game_id),
	Constraint fk_user_games_user_id Foreign Key (user_id) References users(user_id),
	Constraint fk_user_games_game_id Foreign Key (game_id) References games (game_id),
	Constraint fk_user_games_player_status Foreign Key (player_status_code) References player_status (player_status_id),
	
);

Create Table assets (
	asset_id int IDENTITY(1,1) NOT NULL,
	symbol varchar(50) NOT NULL,
	company_name varchar(50) NOT NULL,
	current_price money NOT NULL,
	time_updated datetime not null,
	Constraint pk_assets_asset_id Primary Key (asset_id),
);

Create Table portfolio_assets (
	portfolio_assets_id int IDENTITY(1,1) NOT NULL,
	portfolio_id int NOT NULL,
	asset_id int NOT NULL,
	quantity_held decimal(8,2) NOT NULL,
	final_networth decimal default(0) Not NULL,
	Constraint pk_portfolio_assets_portfolio_assets_id Primary Key (portfolio_assets_id),
	Constraint fk_portfolio_assets_portfolio Foreign Key (portfolio_id) References portfolio(portfolio_id),
	Constraint fk_portfolio_assets_asset Foreign Key (asset_id) References assets(asset_id),
);

Create Table transaction_names (
	trans_id int NOT NULL,
	trans_name varchar(25) NOT NULL,
	Constraint pk_transactions_names_trans_id Primary Key (trans_id),
);

Create Table transactions (
	transaction_id int IDENTITY(1,1) NOT NULL,
	trans_type_id int NOT NULL,
	cost_basis money NOT NULL,
	time datetime  NOT NULL,
	user_game_id int NOT NULL,
	asset_id int NOT NULL,
	Constraint pk_transaction_id Primary Key (transaction_id),
	Constraint fk_transactions_assets_user_games Foreign Key (user_game_id) References user_games(user_game_id),
	Constraint fk_transactions_assets_assets Foreign Key (asset_id) References assets(asset_id),
	Constraint ck_trans_type_id check(trans_type_id in (0,1)),
	Constraint fk_transactions_type_transaction_names Foreign Key (trans_type_id) References transaction_names(trans_id),
);

--populate default data
INSERT INTO users (username, email, password_hash, salt, user_role) VALUES ('user', 'makesomethingup@someplace.com', 'Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, email, password_hash, salt, user_role) VALUES ('admin', 'totallynotgamblingatwork@hotmail.com','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');
INSERT INTO users (username, email, password_hash, salt, user_role) VALUES ('alwaysSaves', 'demo@demo.com', 'WVPEyXYY/Wj/shQki85RQWyGrF4=', 'Eu6A5Mtv+JA=', 'user');
INSERT INTO users (username, email, password_hash, salt, user_role) VALUES ('RichieRich', 'richieRich@rich.com', '0XkvLICHdsx3a/WJjXo3DrUdH/U=', '0yU1wWbuPPE=', 'user');
INSERT INTO users (username, email, password_hash, salt, user_role) VALUES ('TiercelCap', 'brad.grasl@gmail.com', 'v6Dr5iJQD/S7J21sblFM2ZdHeMw=', 'v3iL2Wcq8nE=', 'user');
INSERT INTO users (username, email, password_hash, salt, user_role) VALUES ('alicia', 'aliciambarnhart@gmail.com', 'xxtiEUohNZYPs9U3gkrTEqBC9LM=', 'p+2HGbb6gE0=', 'user');

INSERT INTO	game_status (game_status_id, game_status_name) VALUES (0, 'Inactive'), (1, 'Active'), (2, 'Completed');
INSERT INTO	player_status (player_status_id, player_status_name) VALUES (0, 'Pending'), (1, 'Accepted');
INSERT INTO assets (symbol, company_name, current_price, time_updated) VALUES('USD', 'United States Dollar', 1, GETDATE());
INSERT INTO	transaction_names (trans_id, trans_name) VALUES (0, 'Buy'), (1, 'Sell');

INSERT INTO games (game_name, creator_id, start_date, end_date, game_status_code) VALUEs ('Wall St. Wolves', 1, '12/07/2020', '12/16/2020', 1);
INSERT INTO games (game_name, creator_id, start_date, end_date, game_status_code) VALUEs ('Day Trading Daddies', 1, '12/08/2020', '12/17/2020', 1);
INSERT INTO games (game_name, creator_id, start_date, end_date, game_status_code) VALUEs ('Martha Stewart Exchange', 2, '12/09/2020', '12/18/2020', 1);
INSERT INTO games (game_name, creator_id, start_date, end_date, game_status_code) VALUEs ('Feds Keep Out', 2, '12/10/2020', '12/19/2020', 1);
INSERT INTO games (game_name, creator_id, start_date, end_date, game_status_code) VALUEs ('Nothing to SEC Here', 1, '12/11/2020', '12/20/2020', 1);
INSERT INTO games (game_name, creator_id, start_date, end_date, game_status_code) VALUEs ('I Like Big Bucks', 2, '12/11/2020', '12/20/2020', 1);

INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (1, 1, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (1, 2, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (1, 3, 0);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 0)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (2, 3, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (2, 4, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (2, 1, 0);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 0)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (2, 5, 0);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 0)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (1, 5, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (1, 6, 0);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 0)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (2, 6, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)

--table alterations

Alter Table portfolio ADD Constraint fk_portfolio_user_games Foreign Key (user_game_id) References user_games(user_game_id)

GO