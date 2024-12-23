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

--
-- Table structure for table `sites`
--

CREATE TABLE `sites` (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `ville` varchar(200) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `sites`
--

INSERT INTO `sites` (`id`, `ville`) VALUES
(1, 'Paris'),
(2, 'Nantes'),
(3, 'Toulouse'),
(4, 'Nice'),
(5, 'Lille');

-- --------------------------------------------------------

--
-- Table structure for table `services`
--

CREATE TABLE `services` (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `nom` varchar(200) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `services`
--

INSERT INTO `services` (`id`, `nom`) VALUES
(1, 'Siège administratif'),
(2, 'Production');

-- --------------------------------------------------------

--
-- Table structure for table `salaries`
--

CREATE TABLE `salaries` (
  `Id` INT PRIMARY KEY AUTO_INCREMENT,
  `Nom` TEXT NOT NULL,
  `Prenom` TEXT NOT NULL,
  `TelephoneFixe` TEXT,
  `TelephonePortable` TEXT,
  `Email` TEXT,
  `ServiceId` INT,
  `SiteId` INT,
  FOREIGN KEY (`ServiceId`) REFERENCES `services`(`id`),
  FOREIGN KEY (`SiteId`) REFERENCES `sites`(`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `salaries`
--

INSERT INTO `salaries` (`Nom`, `Prenom`, `TelephoneFixe`, `TelephonePortable`, `Email`, `ServiceId`, `SiteId`)
VALUES
('Johnson', 'Michael', '04-56-78-90-12', '06-78-12-34-56', 'michael.johnson@blocalimentation.fr', 1, 1),
('Smith', 'Emily', '04-23-45-67-89', '06-23-45-67-89', 'emily.smith@blocalimentation.fr', 2, 2),
('Williams', 'James', '04-87-65-43-21', '06-98-76-54-32', 'james.williams@blocalimentation.fr', 1, 3),
('Brown', 'Sophia', '04-56-78-90-32', '06-45-67-89-01', 'sophia.brown@blocalimentation.fr', 2, 4),
('Jones', 'Daniel', '04-34-67-89-01', '06-12-34-56-78', 'daniel.jones@blocalimentation.fr', 1, 5),
('Rossi', 'Giovanni', '04-98-76-54-32', '06-78-12-34-12', 'giovanni.rossi@blocalimentation.fr', 2, 1),
('Esposito', 'Chiara', '04-65-43-21-87', '06-98-76-54-43', 'chiara.esposito@blocalimentation.fr', 1, 2),
('Bianchi', 'Luca', '04-76-54-32-10', '06-87-65-43-21', 'luca.bianchi@blocalimentation.fr', 2, 3),
('Romano', 'Francesca', '04-32-10-87-65', '06-76-54-32-10', 'francesca.romano@blocalimentation.fr', 1, 4),
('Ferrari', 'Matteo', '04-21-98-76-54', '06-65-43-21-98', 'matteo.ferrari@blocalimentation.fr', 2, 5),
('Jean-Baptiste', 'Thierry', '04-54-32-10-98', '06-54-32-10-98', 'thierry.jeanbaptiste@blocalimentation.fr', 1, 1),
('Louison', 'Marie-Lyne', '04-78-56-34-21', '06-34-78-56-12', 'marie-lyne.louison@blocalimentation.fr', 2, 2),
('Mathurin', 'Jean-Marc', '04-89-76-54-32', '06-89-76-54-43', 'jean-marc.mathurin@blocalimentation.fr', 1, 3),
('Germain', 'Lucienne', '04-21-10-98-76', '06-21-10-98-43', 'lucienne.germain@blocalimentation.fr', 2, 4),
('Bélizaire', 'Joseph', '04-32-54-76-89', '06-32-54-76-98', 'joseph.belizaire@blocalimentation.fr', 1, 5),
('Garcia', 'Carlos', '04-12-34-56-78', '06-12-34-56-78', 'carlos.garcia@blocalimentation.fr', 2, 1),
('Martinez', 'Isabella', '04-45-67-89-12', '06-45-67-89-32', 'isabella.martinez@blocalimentation.fr', 1, 2),
('Rodriguez', 'Miguel', '04-56-78-90-34', '06-56-78-90-34', 'miguel.rodriguez@blocalimentation.fr', 2, 3),
('Lopez', 'Carmen', '04-78-90-12-34', '06-78-90-12-34', 'carmen.lopez@blocalimentation.fr', 1, 4),
('Hernandez', 'Alejandro', '04-98-76-54-32', '06-98-76-54-43', 'alejandro.hernandez@blocalimentation.fr', 2, 5);

COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
