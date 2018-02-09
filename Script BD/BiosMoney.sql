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
nomUsu varchar(15) unique not null,
pass varchar(7) not null check(len(pass) = 7),
nomComppleto varchar(50) not null
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
horaIni time not null,
horaFin time not null
)

go

create table horasExtras
(
cedula int not null foreign key references cajero(cedula),
fecha datetime not null check(fecha <= getdate()),
minutos int not null
primary key(cedula, fecha)
)

go

create table pago
(
numeroInt int not null identity(1,1) primary key,
fecha date not null default getdate(),
montoTotal int not null check(montoTotal > 0)
)

go

create table empresa
(
codEmpresa int not null primary key check(len(codEmpresa) >= 1 AND len(codEmpresa) <= 4),
rut int unique not null check(len(rut) <= 12),
dirFiscal varchar(100) not null,
telefono varchar(30) not null
)

go

create table tipoContrato
(
codEmp int foreign key references empresa(codEmpresa) not null,
codContrato int not null check(len(codContrato) >= 1 AND len(codContrato) <= 2),
nombre varchar(30) not null,
primary key(codEmp, codContrato)
)

go
create table factura 
(
idPago int foreign key references pago(numeroInt),
codContrato int not null,
codEmp int not null,
monto int not null check(len(monto) >= 1 AND len(monto) <= 5),
codCli int not null check(len(codCli) >= 1 AND len(codCli) <= 6)
foreign key (codEmp, codContrato) references tipoContrato,
primary key (idPago, codEmp, CodContrato)
)



-- Usuario de IIS que utiliza el WCF para acceder a la Base de Datos
USE master
GO

CREATE LOGIN [IIS APPPOOL\DefaultAppPool] FROM WINDOWS WITH DEFAULT_DATABASE = master
GO

USE BiosMoney
GO

CREATE USER [IIS APPPOOL\DefaultAppPool] FOR LOGIN [IIS APPPOOL\DefaultAppPool]
GO

GRANT Execute to [IIS APPPOOL\DefaultAppPool]
go


------------------------------------------


Create Procedure AltaUsuSql @nomUsu varchar(15), @pass varchar(7), @Rol varchar(30) AS
Begin
	
	--primero creo el usuario de logueo
	Declare @VarSentencia varchar(200)
	Set @VarSentencia = 'CREATE LOGIN [' +  @nombre + '] WITH PASSWORD = ' + QUOTENAME(@pass, '''')
	Exec (@VarSentencia)
	
	if (@@ERROR <> 0)
		return -1
		
	
	--segundo asigno rol especificao al usuario recien creado
	Exec sp_addsrvrolemember @loginame=@nombre, @rolename=@Rol
	
	if (@@ERROR = 0)
		return 1
	else
		return -2

End
go

Create Procedure AltaUsuBD @nombre varchar(15), @Rol varchar(30), @logueo varchar(10) AS
Begin
	--primero creo el usuario 
	Declare @VarSentencia varchar(200)
	Set @VarSentencia = 'Create User [' +  @nombre + '] From Login [' + @logueo + ']'
	Exec (@VarSentencia)
	
	if (@@ERROR <> 0)
		return -1
		
	
	--segundo asigno rol especificao al usuario recien creado
	Exec sp_addrolemember @rolename=@rol, @membername=@nombre
	
	if (@@ERROR = 0)
		return 1
	else
		return -2
	

End
go



























