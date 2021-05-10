-- TransactionalEmail.Users definition

CREATE TABLE `Users` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` text NOT NULL,
  `Email` text NOT NULL,
  `Password` text NOT NULL,
  `ResetToken` text,
  `ResetTokenExpireDate` datetime,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
