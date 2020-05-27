-- -----------------------------------------------------
-- Database wakeapp
-- -----------------------------------------------------
DROP DATABASE wakeapp;
CREATE DATABASE IF NOT EXISTS wakeapp;

USE wakeapp;

-- -----------------------------------------------------
-- Table `group`
-- -----------------------------------------------------
CREATE TABLE `group` (
  `GroupId` INT AUTO_INCREMENT PRIMARY KEY,
  `Name` VARCHAR(45) NOT NULL,
  `Password` VARCHAR(45) NOT NULL,
  `Salt` VARCHAR(10) NOT NULL,
  `Create_on` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- -----------------------------------------------------
-- Table `user`
-- -----------------------------------------------------
CREATE TABLE `user` (
  `UserId` INT AUTO_INCREMENT PRIMARY KEY,
  `Name` VARCHAR(45) NOT NULL,
  `Password` VARCHAR(45) NOT NULL,
  `Salt` VARCHAR(10) NOT NULL,
  `Email` VARCHAR(45) NOT NULL,
  `Create_on` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UserRole` VARCHAR(45) NOT NULL,
  `GroupId` INT NULL,
    CONSTRAINT fk_User_Group
    FOREIGN KEY (GroupId) 
    REFERENCES `group`(GroupId)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- -----------------------------------------------------
-- Table `device`
-- -----------------------------------------------------
CREATE TABLE `device` (
  `DeviceId` INT AUTO_INCREMENT PRIMARY KEY,
  `Mac` VARCHAR(100) NOT NULL,
  `Name` VARCHAR(45) NOT NULL,
  `DeviceType` VARCHAR(45) NOT NULL,
  `UserId` INT DEFAULT NULL,
    CONSTRAINT fk_Device_User
    FOREIGN KEY (UserId) 
    REFERENCES user(UserId)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- -----------------------------------------------------
-- Table `alarm`
-- -----------------------------------------------------
CREATE TABLE `alarm` (
  `AlarmId` INT AUTO_INCREMENT PRIMARY KEY,
  `DateStart` DATE NOT NULL,
  `Sequence` INT NULL,
  `DateEnd` date DEFAULT NULL,
  `Time` time NOT NULL,
  `Comment` VARCHAR(45) DEFAULT NULL,
  `DeviceId` INT DEFAULT NULL,
    CONSTRAINT fk_Alarm_Device
    FOREIGN KEY (DeviceId) 
    REFERENCES device(DeviceId)
	ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

