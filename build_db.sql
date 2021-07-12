# --------------------
# Создание базы данных
# --------------------
CREATE DATABASE IF NOT EXISTS TenzoTest;

USE TenzoTest;
# ------------------------------
# Создание таблицы пользователей
# -------------------------------
DROP TABLE IF EXISTS `Users`;
CREATE TABLE `Users` (
  `UserId` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(64) NOT NULL,
  `Phone` varchar(16) NOT NULL,
  `Email` varchar(64) NOT NULL,
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB;

INSERT INTO `Users` (`UserId`, `Name`, `Phone`, `Email`) VALUES
(1,	'Administrator',	'8-987-1234-56-78',	'admin@mail.com'),
(2,	'User',	'8-999-1234-99-78',	'simple.user@mail.com');

# -------------------------------
# Создание таблицы координат мыши
# -------------------------------
DROP TABLE IF EXISTS `Positions`;
CREATE TABLE `Positions` (
  `UserId` int NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `Event` varchar(8) NOT NULL,
  `Points` varchar(32) NOT NULL,
  KEY `UserId` (`UserId`),
  KEY `CreatedAt` (`CreatedAt`),
  FOREIGN KEY (`UserId`) REFERENCES `Users` (`UserId`) ON DELETE CASCADE
) ENGINE=InnoDB;

# --------------------------
# Создание хранимых процедур
# --------------------------
DELIMITER ;;
# ---------------------------------------------
# Процедура получения позиций мыши постранично
# ---------------------------------------------

DROP PROCEDURE IF EXISTS `sp_getPositonsWithPaging`;;
CREATE PROCEDURE `sp_getPositonsWithPaging`(IN `pageNumber` int, IN `pageSize` int, IN `startDate` datetime, IN `endDate` datetime, IN `events` varchar(255))
BEGIN
select * from Positions 
where CreatedAt between startDate and endDate and Event REGEXP events
LIMIT pageSize OFFSET pageNumber;
END;;

# ---------------------------------
# Процедура записывает позиций мыши
# ---------------------------------
DROP PROCEDURE IF EXISTS `sp_setPositon`;;
CREATE PROCEDURE `sp_setPositon`(IN `userId` int, IN `createdAt` datetime(2), IN `event` varchar(8), IN `points` varchar(32))
BEGIN
INSERT INTO Positions (UserId, CreatedAt, Event, Points)
VALUES (userId, createdAt, event, points);
END;;

# ---------------------------------------------------------
# Процедура возвращает общее кол-во строк в таблице позиций
# ---------------------------------------------------------
DROP PROCEDURE IF EXISTS `sp_getTotalRows`;;
CREATE PROCEDURE `sp_getTotalRows`()
SELECT COUNT(*) FROM Positions;;

# -------------------------------------------------------
# Процедура возвращает пользователя по заданному телефону
# -------------------------------------------------------
DROP PROCEDURE IF EXISTS `sp_getUser`;;
CREATE PROCEDURE `sp_getUser`(IN `phone` varchar(16))
SELECT * FROM Users
WHERE Users.Phone = phone;;

DELIMITER ;