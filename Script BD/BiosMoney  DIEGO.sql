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
nomCompleto varchar(50) not null
)

go

create table gerente
(
cedula int not null primary key foreign key references usuario(cedula),
correo varchar(70) not null check(correo LIKE '%_@_%_.__%')
)


go

create table cajero
(
cedula int not null primary key foreign key references usuario(cedula),
horaIni time not null,
horaFin time not null,
activo bit not null default 1,--1=activo // 0 = inactivo
check(horaFin >horaIni)
)

go

create table horasExtras
(
cedula int not null foreign key references cajero(cedula),
fecha date not null default getdate(),
minutos int not null check(minutos>0)
primary key(cedula, fecha)
)

go

create table pago
(
numeroInt int not null identity(1,1) primary key,
fecha date not null default getdate(),
montoTotal int not null check(montoTotal > 0),
cedulaCajero int not null foreign key references cajero(cedula)
)

go

create table empresa
(
codEmpresa int not null primary key check(len(codEmpresa) >= 1 AND len(codEmpresa) <= 4),
rut int unique not null check(len(rut) <= 12),
dirFiscal varchar(100) not null,
telefono varchar(30) not null,
activo bit not null default 1
)

go

create table tipoContrato
(
codEmp int foreign key references empresa(codEmpresa) not null,
codContrato int not null check(len(codContrato) >= 1 AND len(codContrato) <= 2),
nombre varchar(30) not null,
primary key(codEmp, codContrato),
activo bit not null default 1
)

go
create table factura 
(
idPago int foreign key references pago(numeroInt),
codContrato int not null,
codEmp int not null,
monto int not null check(len(monto) >= 1 AND len(monto) <= 5),
codCli int not null check(len(codCli) >= 1 AND len(codCli) <= 6),
fechaVto Date not null,
foreign key (codEmp, codContrato) references tipoContrato,
primary key (idPago, codEmp, CodContrato)
)

go

-- Usuario de IIS que utiliza el WCF para acceder a la Base de Datos
USE master
GO

CREATE LOGIN [IIS APPPOOL\DefaultAppPool] FROM WINDOWS WITH DEFAULT_DATABASE = master
GO

USE BiosMoney
GO

CREATE USER [IIS APPPOOL\DefaultAppPool] FOR LOGIN [IIS APPPOOL\DefaultAppPool]
GO


-------------------------------------------------------------------------------------------
--PAGO

--AGREGAR PAGO
CREATE PROC AltaPago @numeroInt int, @fecha date, @montoTotal int, @cedulaCajero int AS
BEGIN
	IF(EXISTS(SELECT * FROM pago WHERE numeroInt = @numeroInt))
		RETURN -1

	INSERT INTO pago (numeroInt, fecha, montoTotal, cedulaCajero) VALUES (@numeroInt, @fecha, @montoTotal, @cedulaCajero)
	IF(@@ERROR = 0)
		RETURN 1
	ELSE
		RETURN -2
END
GO

--BUSCAR PAGO
CREATE PROC BuscarPago @numeroInt int AS
BEGIN
	SELECT * FROM pago WHERE numeroInt = @numeroInt
END
GO

--ELIMINAR PAGO
CREATE PROC EliminarPago @numeroInt int AS
BEGIN
	IF(NOT EXISTS(SELECT * FROM pago WHERE numeroInt = @numeroInt))
		RETURN -1

	DELETE FROM pago WHERE numeroInt = @numeroInt
	IF(@@ERROR = 0)
		RETURN 1
	ELSE
		RETURN -2
END
GO

--MODIFICAR PAGO
CREATE PROC ModificarPago @numeroInt int, @fecha date, @montoTotal int, @cedulaCajero int AS
BEGIN
	IF(EXISTS(SELECT * FROM pago WHERE numeroInt = @numeroInt))
		RETURN -1

	UPDATE pago SET fecha = @fecha, montoTotal = @montoTotal, @cedulaCajero = @cedulaCajero WHERE numeroInt = @numeroInt
	IF(@@ERROR = 0)
		RETURN 1
	ELSE
		RETURN -2
END
GO

--LISTAR PAGOS
CREATE PROC ListarPagos AS
BEGIN
	SELECT * FROM pago
END
GO
-------------------------------------------------------------------------------------------
--FACTURA

--AGREGAR FACTURA
CREATE PROC AltaFactura @idPago int, @codContrato int, @codEmp int, @monto int, @codCli int AS
BEGIN
	IF (EXISTS (SELECT * from factura WHERE idPago = @idPago AND codEmp = @codEmp AND codContrato = @codContrato))
		RETURN -1

	INSERT INTO factura (idPago, codContrato, codEmp, monto, codCli) VALUES (@idPago, @codContrato, @codEmp, @monto, @codCli)	
	IF(@@ERROR = 0)
		RETURN 1
	ELSE
		RETURN -2
END
GO

--ELIMINAR FACTURA
CREATE PROC EliminarFactura @idPago int, @codContrato int, @codEmp int AS
BEGIN
	IF(NOT EXISTS(SELECT * FROM factura WHERE idPago = @idPago AND codContrato = @codContrato AND codEmp = @codEmp))
		RETURN -1

	DELETE FROM factura WHERE idPago = @idPago AND codContrato = @codContrato AND codEmp = @codEmp
	IF(@@ERROR = 0)
		RETURN 1
	ELSE
		RETURN -2
END
GO

--BUSCAR FACTURA
CREATE PROC BuscarFactura @idPago int, @codContrato int, @codEmp int AS
BEGIN
	SELECT * FROM factura WHERE idPago = @idPago AND codContrato = @codContrato AND codEmp = @codEmp
END
GO

----------------------------------------------------------------------------------------------------

USE BiosMoney;
CREATE ROLE UsuarioWeb 
GO

CREATE ROLE UsuarioCajero
GO


--GRANT Execute on "Procedimientos del Usuario Web" to UsuarioWeb
--GRANT Execute on "Procedimientos del Usuario Cajero" to UsuarioCajero

