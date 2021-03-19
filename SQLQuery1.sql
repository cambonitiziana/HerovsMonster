
--CREATE DATABASE Game

--USE Game 
--GO

--CREATE TABLE Player (
--ID INT NOT NULL PRIMARY KEY,
--Nome VARCHAR(255),
--)


--CREATE TABLE Weapon(
--ID INT NOT NULL PRIMARY KEY,
--Nome VARCHAR(255),
--DamagePoints INT,
--)

--CREATE TABLE Hero (
--ID INT NOT NULL PRIMARY KEY,
--Nome VARCHAR(255),
--Class VARCHAR(250),
--Score INT, 
--LifePoint int,
--Level int,
--)

--CREATE TABLE RoleClasses(
--Class VARCHAR(15) NOT NULL PRIMARY KEY,
--Role VARChAR(20),
--)



--INSERT INTO Hero
--VALUES (1,'Massala', 'Guerriero', 0, 20, 1)


--DELETE FROM Hero
--WHERE Nome = 'Poseidone'

--CREATE TABLE RoleClasses(
--ID INT IDENTITY (1,1) NOT NULL, NULL PRIMARY KEY,
--Role VARChAR(40),
--Class VARCHAR(40),
--Livello INT,
--WeaponName VARCHAR(40),
--DamagePoint int
--)

--INSERT INTO RoleClasses VALUES 
--('Eroe','Guerriero',1, 'Scettro Infuocato', 4),
--('Eroe','Mago',1, 'Bacchetta lunare', 3),
--('Eroe','Mago',1, 'Bacchetta Solare', 3),
--('Eroe','Mago',1, 'Occhio bianco', 3),
--('Eroe','Mago',2, 'Pietra Magica', 4),
--('Mostro','Cultista',1, 'Anello Diabolico', 2),
--('Mostro','Cultista',2, 'Diamande luminoso', 2),
--('Mostro', 'Orco',1, 'Denti Affamati', 3),
--('Mostro', 'Orco',2, 'Lancia infuocata', 3),
--('Mostro','Signore Del Male',2, 'Scettro Diabolico', 4),
--('Mostro','Signore Del Male',3, 'Occhio che tutto vede', 5)

----ALTER TABLE RoleClasses DROP COLUMN Livello

--SELECT * FROM RoleClasses 
--WHERE Livello = 1 
--AND
--(Class = 'Guerriero')


--CREATE TABLE LevelLifePointScore(
--Livello INT NOT NULL PRIMARY KEY,
--LifePoint INT NOT NULL,
--Score INT NOT NULL,
--)

--INSERT INTO LevelLifePointScore VALUES 
--(1,20,0),(2,40,30),(3,60,60),(4,80,90),(5,100,120)


--INSERT INTO RoleClasses VALUES 
--('Eroe','Guerriero',1, 'Freccia avvelenata', 10)

SELECT * FROM Player
SELECT * FROM Hero
SELECT * FROM RoleClasses
SELECT * FROM LevelLifePointScore

--Delete FROM RoleClasses WHere ID = 12 

                          