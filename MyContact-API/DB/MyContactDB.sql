-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Nov 20, 2024 at 12:00 PM
-- Server version: 10.4.28-MariaDB
-- PHP Version: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `mycontactdb`
--

-- --------------------------------------------------------



CREATE TABLE Sites (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Ville TEXT NOT NULL
);

CREATE TABLE Services (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nom TEXT NOT NULL
);

CREATE TABLE Salaries (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nom TEXT NOT NULL,
    Prenom TEXT NOT NULL,
    TelephoneFixe TEXT,
    TelephonePortable TEXT,
    Email TEXT,
    ServiceId INT,
    SiteId INT,
    FOREIGN KEY (ServiceId) REFERENCES Services(Id),
    FOREIGN KEY (SiteId) REFERENCES Sites(Id)
);
