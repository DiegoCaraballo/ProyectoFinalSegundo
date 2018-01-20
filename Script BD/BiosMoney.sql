--Script
--creo la base de datos de BiosMoney
use master;
if exists(Select * FROM SysDataBases WHERE name='BiosMoney')
BEGIN
	DROP DATABASE BiosMoney
END
go
 
create DataBase BiosMoney

go

use BiosMoney;

create table usuario
(
	cedula int primary key not null check(cedula >0),
	nomUsu varchar(15) not null,
	pass varchar(7) check(len(pass) =7) not null,
	nomCompleto varchar(40) not null
)

go

create table cajero
(
	cedula int foreign key references usuario(cedula) not null,
	horaIni datetime not null,
	horaFin datetime not null
)

go

create table gerente
(
	cedula int foreign key references usuario(cedula) not null,
	correo varchar(50) not null
)

go

create table empresa
(
	codigo int primary key not null,
	rut varchar(12) not null check(len(rut) <=12),
	dirFiscal varchar(40) not null,
	telefono varchar(15) not null
)

go

create table pago
(
	numeroInt int primary key identity (1,1) not null,
	fecha datetime not null check(fecha >= getdate()),
	montoTotal decimal not null,
	cedulaCajero int foreign key references usuario(cedula) not null,
	codigoEmp int foreign key references empresa(codigo)
			
)

go

create table factura
(
	codBarra varchar(25) primary key not null check(len(codBarra) = 25),
	codCli int not null,
	fechaVen datetime not null,
	monto decimal not null
)

go

create table tipoContrato
(
	codContrato int primary key not null,
	codEmpresa int foreign key references empresa(codigo) not null,
	nombre varchar(19) not null
)






















