create database OngAdocao;

use OngAdocao;


Create Table Adotante(
CPF varchar(11) not null,
NomeAdotante varchar(50) not null,
DataNascimento Date not null,
Sexo varchar(1) 

Constraint CPF Primary Key (CPF)
);


Create Table EnderecoAdotante(
CPF varchar(11) not null,
CEP varchar(9) not null,
Bairro varchar(50) not null,
Cidade varchar(50) not null,
Estado varchar(30) not null,
TipoLogradouro varchar(50) not null,
NomeLogradouro varchar(50) not null

FOREIGN KEY (CPF) REFERENCES Adotante(CPF)
);


Create Table ContatoAdotante(
CPF varchar(11) not null,
Numero varchar (20) not null,
Tipo varchar(30) not null,

FOREIGN KEY (CPF) REFERENCES Adotante(CPF)
);


Create Table Animal(
Chip varchar(4) not null,
NomeAnimal varchar(50) not null,
Sexo varchar(1) not null,

Constraint Chip Primary Key (Chip)
);


Create Table Adocao(
Chip varchar(4) not null,
CPF varchar(11) not null,
DataAdocao Date not null,

FOREIGN KEY (CPF) REFERENCES Adotante(CPF),
FOREIGN KEY (Chip) REFERENCES Animal(Chip),
Constraint PK_Adocao Primary Key (Chip, CPF)
);


Create Table TipoAnimal(
Chip varchar(4) not null,
Familia varchar(30) not null,
Raca varchar(30) not null,

FOREIGN KEY (Chip) REFERENCES Animal(Chip)
);
