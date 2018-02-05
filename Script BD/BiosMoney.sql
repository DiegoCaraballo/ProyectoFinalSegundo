USE master
GO

if exists (select * from sys.databases where name = 'BiosMoney')
BEGIN
	DROP DATABASE BiosMoney
END 
GO

CREATE DATABASE BiosMoney
GO

USE BiosMoney
GO

create table usuario
(
cedula int not null primary key,
nomUsu varchar(15) not null,
pass int not null check(len(pass) = 7),
nomComp varchar(50) not null
)

go

create table gerente
(
cedula int not null primary key foreign key references usuario(cedula),
correo varchar(50) not null
)

go

create table cajero
(
cedula int not null primary key foreign key references usuario(cedula),
horaIni datetime not null,
horaFin datetime not null
)

go

create table horasExtras
(
cedula int not null primary key foreign key references cajero(cedula),
fecha datetime not null check(fecha <= getdate()),
minutos int not null
)

go

create table pago
(
id int not null identity(1,1) primary key,
fecha date not null check(fecha = getdate()),
monto int not null check(monto > 0)
)

go

create table empresa
(
codigo int not null primary key check(len(codigo) = 4),
rut int unique not null check(len(rut) <=12),
dirFiscal varchar(100) not null,
telefono varchar(30) not null
)

go

create table tipoContrato
(
codEmp int foreign key references empresa(codigo) not null,
codigo int not null check(len(codigo) = 2),
nombre varchar(30) not null,
primary key(codEmp,codigo)
)

go
create table factura --esto no estoy muy seguro, ya que no es una tabla, sino que es una relacion...
-- y no se si dejarlo con este nombre... (lo puse asi por el mer)
(
idPago int foreign key references pago(id),
codContrato int foreign key references tipoContrato(codigo),
monto int not null check(monto >0),
codCli int not null check(len(codCli) =6)
)
































