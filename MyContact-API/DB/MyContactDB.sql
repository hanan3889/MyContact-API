-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Nov 20, 2024 at 12:00 PM
-- Server version: 10.4.28-MariaDB
-- PHP Version: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";

-- Définition de la base de données
CREATE DATABASE IF NOT EXISTS `mycontactdb`;
USE `mycontactdb`;

-- Création de la table Sites
CREATE TABLE `Sites` (
    `Id` INT PRIMARY KEY AUTO_INCREMENT,
    `Ville` TEXT NOT NULL
);

-- Création de la table Services
CREATE TABLE `Services` (
    `Id` INT PRIMARY KEY AUTO_INCREMENT,
    `Nom` TEXT NOT NULL
);

-- Création de la table Salaries
CREATE TABLE `Salaries` (
    `Id` INT PRIMARY KEY AUTO_INCREMENT,
    `Nom` TEXT NOT NULL,
    `Prenom` TEXT NOT NULL,
    `TelephoneFixe` TEXT,
    `TelephonePortable` TEXT,
    `Email` TEXT,
    `ServiceId` INT,
    `SiteId` INT,
    FOREIGN KEY (`ServiceId`) REFERENCES `Services`(`Id`),
    FOREIGN KEY (`SiteId`) REFERENCES `Sites`(`Id`)
);

-- Validation des modifications
COMMIT;
