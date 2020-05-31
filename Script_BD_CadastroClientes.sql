--Criando banco de dados
CREATE DATABASE CadastroClintes
GO

USE CadastroClintes
GO

--Criando tabela
CREATE TABLE CadastroClientes(
	Id INT IDENTITY (1,1),
	Nome VARCHAR (50) NOT NULL,
	Email VARCHAR (50) NOT NULL,
	Telefone VARCHAR(11) NOT NULL,
	CEP VARCHAR (8) NOT NULL,
	Logradouro VARCHAR (50) NOT NULL,
	Numero VARCHAR (50) NOT NULL,
	Bairro VARCHAR (50) NOT NULL,
	Cidade VARCHAR (50) NOT NULL,
	Estatdo VARCHAR (50)NOT NULL,
)
GO

SELECT * FROM CadastroClientes
GO

