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
INSERT INTO	game_status (game_status_id, game_status_name) VALUES (0, 'Inactive'), (1, 'Active'), (2, 'Completed');
INSERT INTO	player_status (player_status_id, player_status_name) VALUES (0, 'Pending'), (1, 'Accepted');
INSERT INTO assets (symbol, company_name, current_price, time_updated) VALUES('USD', 'United States Dollar', 1, GETDATE());
INSERT INTO	transaction_names (trans_id, trans_name) VALUES (0, 'Buy'), (1, 'Sell');
--users
INSERT INTO users (username, email, password_hash, salt, user_role) VALUES ('niceAsset', 'makesomethingup@someplace.com', 'Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, email, password_hash, salt, user_role) VALUES ('jordanBelfort', 'totallynotgamblingatwork@hotmail.com','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');
INSERT INTO users (username, email, password_hash, salt, user_role) VALUES ('alwaysSaves', 'demo@demo.com', 'WVPEyXYY/Wj/shQki85RQWyGrF4=', 'Eu6A5Mtv+JA=', 'user');
INSERT INTO users (username, email, password_hash, salt, user_role) VALUES ('RichieRich', 'richieRich@rich.com', '0XkvLICHdsx3a/WJjXo3DrUdH/U=', '0yU1wWbuPPE=', 'user');
INSERT INTO users (username, email, password_hash, salt, user_role) VALUES ('TiercelCap', 'brad.grasl@gmail.com', 'v6Dr5iJQD/S7J21sblFM2ZdHeMw=', 'v3iL2Wcq8nE=', 'user');
INSERT INTO users (username, email, password_hash, salt, user_role) VALUES ('alicia', 'aliciambarnhart@gmail.com', 'imQZUVTRU3Iqm9ABP19lBKLkOng=', 'p+2HGbb6gE0=', 'user');
INSERT INTO users (username, email, password_hash, salt, user_role) VALUES ('TostitosSnoop', 'demo@someplace.com', 'ZSkYwE+f3Z1/D4Y+0izBIBqsdRU=', 'qjdT3KTlaT4=', 'user');
INSERT INTO users (username, email, password_hash, salt, user_role) VALUES ('Perez4Prez', 'demo@someplace.com', 'imQZUVTRU3Iqm9ABP19lBKLkOng=','3j8ozS7RLPU=','user');
INSERT INTO users (username, email, password_hash, salt, user_role) VALUES ('MikeVP', 'demo@someplace.com','4P/ZGvDHHO5YZqRaYLku+4N6Le4=', 'VZIkmIdeK0E=','user');
INSERT INTO users (username, email, password_hash, salt, user_role) VALUES ('AllAboutTheBENjamins', 'demo@someplace.com', 'OYNF8bp/0cYwBCIalbe9mKR2GnE=', 'd6u4z5VTvvo=', 'user');
INSERT INTO users (username, email, password_hash, salt, user_role) VALUES ('FrankTheFinanceTank', 'demo@someplace.com', 'pa9JoTeJGOOkXRHrpFNYZhUcobU=', 'Bv8VqwHqIyg=', 'user');
INSERT INTO users (username, email, password_hash, salt, user_role) VALUES ('MoMoneyMarty', 'demo@someplace.com', 'xxkIlpC2BuRHADPOTT6OGehQHBA=', 'fH1/ohg0FQ8=', 'user');
INSERT INTO users (username, email, password_hash, salt, user_role) VALUES ('VALuableAsset', 'demo@someplace.com', 'C3vmFxARap9v60J9TVrAYfsUT88=', 'eElzhpzGEIU=', 'user');
INSERT INTO users (username, email, password_hash, salt, user_role) VALUES ('jSON_hWIE', 'demo@someplace.com', '+yHIPAqQ80yfLeOV/5buV3JrJT0=', 'B29dTvxtME0=', 'user');
INSERT INTO users (username, email, password_hash, salt, user_role) VALUES ('HighYieldYoav', 'demo@someplace.com', 'JKSGM4tGjEjLBU2omevzn9rgXxE=', 'i1EtgVdlfz8=', 'user');
--games
INSERT INTO games (game_name, creator_id, start_date, end_date, game_status_code) VALUEs ('Wall St. Wolves', 2, '12/07/2020', '12/14/2020', 2); --1
INSERT INTO games (game_name, creator_id, start_date, end_date, game_status_code) VALUEs ('Day Trading Daddies', 1, '12/14/2020', '12/18/2020', 1); --2
INSERT INTO games (game_name, creator_id, start_date, end_date, game_status_code) VALUEs ('Martha Stewart Exchange', 3, '12/15/2020', '12/21/2020', 1); --3
INSERT INTO games (game_name, creator_id, start_date, end_date, game_status_code) VALUEs ('Feds Keep Out', 5, '12/16/2020', '12/22/2020', 1); --4
INSERT INTO games (game_name, creator_id, start_date, end_date, game_status_code) VALUEs ('Nothing to SEC Here', 6, '12/16/2020', '12/22/2020', 1); --5
INSERT INTO games (game_name, creator_id, start_date, end_date, game_status_code) VALUEs ('I Like Big Bucks', 4, '12/17/2020', '12/23/2020', 0); --6
INSERT INTO games (game_name, creator_id, start_date, end_date, game_status_code) VALUEs ('Woke Up Feelin Bullish', 8, '12/18/2020', '12/25/2020', 0); --7
INSERT INTO games (game_name, creator_id, start_date, end_date, game_status_code) VALUEs ('C# > Java', 9, '12/14/2020', '12/18/2020', 1); --8
INSERT INTO games (game_name, creator_id, start_date, end_date, game_status_code) VALUEs ('Elevated Trading', 12, '12/15/2020', '12/21/2020', 1); --9
INSERT INTO games (game_name, creator_id, start_date, end_date, game_status_code) VALUEs ('WWWBD', 2, '12/21/2020', '12/28/2020', 0); --10
INSERT INTO games (game_name, creator_id, start_date, end_date, game_status_code) VALUEs ('Heavily Invested', 1, '12/23/2020', '12/30/2020', 0); --11
INSERT INTO games (game_name, creator_id, start_date, end_date, game_status_code) VALUEs ('Cash Me Outside', 3, '12/17/2020', '12/23/2020', 0); --12
INSERT INTO games (game_name, creator_id, start_date, end_date, game_status_code) VALUEs ('Stockopoly', 5, '12/22/2020', '12/29/2020', 0); --13
INSERT INTO games (game_name, creator_id, start_date, end_date, game_status_code) VALUEs ('Bond Girls', 6, '12/15/2020', '12/21/2020', 1); --14
INSERT INTO games (game_name, creator_id, start_date, end_date, game_status_code) VALUEs ('No Girls! They Attract Bears', 3, '12/22/2020', '12/29/2020', 0); --15
--game 1
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (1, 2, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (15, 2, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (12, 2, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (7, 2, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (8, 2, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (13, 2, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (4, 2, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
--game 2
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (2, 1, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (14, 1, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (11, 1, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (10, 1, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (3, 1, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (1, 1, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
--game 3
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (3, 3, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (4, 3, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (5, 3, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (7, 3, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
--game 4
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (5, 4, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (11, 4, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (12, 4, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (8, 4, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (13, 4, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
--game 5
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (6, 5, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (5, 5, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (4, 5, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (3, 5, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (2, 5, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (1, 5, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (8, 5, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
--game 6
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (4, 6, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (15, 6, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (8, 6, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (12, 6, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (5, 6, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (1, 6, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
--game 7
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (8, 7, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (12, 7, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (13, 7, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (11, 7, 0);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 0)
--game 8
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (9, 8, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (3, 8, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (4, 8, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (5, 8, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (6, 8, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (8, 8, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (14, 8, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
--game 9
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (12, 9, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (13, 9, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (14, 9, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (15, 9, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (10, 9, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (9, 9, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (8, 9, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (11, 9, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
--game 10
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (2, 10, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (1, 10, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (3, 10, 0);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 0)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (7, 10, 0);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 0)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (9, 10, 0);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 0)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (11, 10, 0);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 0)
--game 11
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (1, 11, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (3, 10, 0);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 0)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (4, 10, 0);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 0)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (5, 10, 0);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 0)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (6, 10, 0);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 0)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (8, 10, 0);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 0)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (7, 10, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (9, 10, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (11, 10, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
--game 12
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (3, 12, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (11, 12, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (10, 12, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (8, 12, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (7, 12, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
--game 13
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (5, 13, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (1, 13, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (3, 13, 0);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 0)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (4, 13, 0);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 0)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (6, 13, 0);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 0)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (9, 13, 0);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 0)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (8, 13,0);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 0)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (11, 13, 0);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 0)
--game 14
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (6, 14, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (1, 14, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (13, 14, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (7, 14, 0);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
--game 15
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (3, 15, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (2, 15, 0);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 0)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (4, 15, 0);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 0)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (5, 15, 0);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 0)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (8, 15, 0);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 0)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (9, 15, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (10, 15, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (11, 15, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (12, 15, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (14, 15, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)
INSERT INTO user_games (user_id, game_id, player_status_code) VALUEs (15, 15, 1);
INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY);
INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUEs(@@IDENTITY, 1, 100000)

--table alterations

Alter Table portfolio ADD Constraint fk_portfolio_user_games Foreign Key (user_game_id) References user_games(user_game_id)

GO