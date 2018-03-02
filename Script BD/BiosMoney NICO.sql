
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
horaIni datetime not null,
horaFin datetime not null,
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


USE BiosMoney;
CREATE ROLE UsuarioWeb 
GO

CREATE ROLE UsuarioCajero
GO
--------------------------------------------------------------------------------------------------------------------------------------------
--*--												Alta Gerente																       --*--
--------------------------------------------------------------------------------------------------------------------------------------------
create proc AltaGerente @cedula int, @nomUsu varchar(15), @pass varchar(7), @nomCompleto varchar(50), @correo varchar(70) as
begin

if exists (select * from usuario where cedula = @cedula)
	return -1

if exists(select * from gerente where cedula= @cedula)
	return -2

declare @Error int;

begin tran

	insert into Usuario(cedula,nomUsu,pass,nomCompleto) values(@cedula,@nomUsu,@pass,@nomCompleto)


set @Error = @@ERROR
	if(@Error<>0)
	begin 
		rollback tran 
		return -3
	end

--se agrega como gerente
insert into Gerente values (@cedula,@correo)

set @Error=@@ERROR
	if(@Error<>0)
	begin
		rollback tran
		return -4
	end
	
	begin 
		commit tran 
	end

Declare @VarSentenciaSQL varchar(200)
	Set @VarSentenciaSQL = 'CREATE LOGIN [' +  @nomUsu + '] WITH PASSWORD = ' + QUOTENAME(@pass, '''') + ',DEFAULT_DATABASE = BiosMoney'
	Exec (@VarSentenciaSQL)
	
	set @Error=@@ERROR
	if(@Error<>0)
	begin
		return -5
	end
		
	----Asigno rol al usuario recien creado
	--Exec sp_addsrvrolemember @loginame=@nomUsu, @rolename='public'
	
	--if (@@ERROR = 0)
	--	return 1
	--else
	--	return -4



--Creo usuario de acceso a base de datos 
Declare @VarSentenciaBD varchar(200)
	Set @VarSentenciaBD = 'Create User [' +  @nomUsu + '] From Login [' + @nomUsu + ']'
	Exec (@VarSentenciaBD)
	
	set @Error=@@ERROR
	if(@Error<>0)
	begin
		return -6
	end
	----segundo asigno rol especifico al usuario recien creado
	--Exec sp_addrolemember @rolename='public', @membername=@nomUsu
	
	--if (@@ERROR = 0)
	--	return 1
	--else
	--	return -6
	
end

go
--exec AltaGerente 45848621,'hitokiri','123456a','Nicolas Rodriguez', 'uncorreo@hotmail.com'

--------------------------------------------------------------------------------------------------------------------------------------------
--*--														ABM Cajero															       --*--
--------------------------------------------------------------------------------------------------------------------------------------------

create proc AltaCajero @cedula int, @nomUsu varchar(15), @pass varchar(7), @nomCompleto varchar(50), @horaini time, @horaFin time as
begin


if exists(select * from cajero where cedula=@cedula and activo=1)
return-1

if exists(select * from cajero where cedula=@cedula and activo=0)
begin
	update cajero set activo =1, horaIni=@horaini,horaFin=@horaFin where cedula=@cedula;


end
if exists(select * from usuario where cedula=@cedula)
return-2

declare @Error int;

begin tran
insert into usuario values (@cedula,@nomUsu,@pass, @nomCompleto)

set @Error=@@ERROR
	if(@Error<>0)
	begin
		rollback tran
		return-3
	end

insert into cajero values(@cedula,@horaini,@horaFin,1)
set @Error=@@ERROR
	if(@Error<>0)
	begin 
		rollback tran
		return-4
	end


	begin 
	commit tran 
end

	
Declare @VarSentenciaSQL varchar(200)
	Set @VarSentenciaSQL = 'CREATE LOGIN [' +  @nomUsu + '] WITH PASSWORD = ' + QUOTENAME(@pass, '''') + ',DEFAULT_DATABASE = BiosMoney'
	Exec (@VarSentenciaSQL)
	
	set @Error=@@ERROR
	if(@Error<>0)
	begin
		return -5
	end
		
	----Asigno rol al usuario recien creado
	Exec sp_addsrvrolemember @loginame=@nomUsu, @rolename='UsuarioCajero'
	
	if (@@ERROR <> 0)
	begin
		return -4
		end

--Creo usuario de acceso a base de datos 
Declare @VarSentenciaBD varchar(200)
	Set @VarSentenciaBD = 'Create User [' +  @nomUsu + '] From Login [' + @nomUsu + ']'
	Exec (@VarSentenciaBD)
	
	set @Error=@@ERROR
	if(@Error<>0)
	begin
		return -6
	end
	----segundo asigno rol especificao al usuario recien creado
	Exec sp_addrolemember @rolename='UsuarioCajero', @membername=@nomUsu
	
	set @Error=@@ERROR
	if(@Error<>0)
	begin
		return -6
	end

end

go




create proc BajaCajero @cedula int as
begin

declare @Error int;

if not exists(select * from cajero where cedula=@cedula)
return -1

if exists(select * from gerente where cedula=@cedula)
return -2

if exists(select * from pago where cedulaCajero=@cedula)
begin
update cajero set activo = 0 where cedula=@cedula

delete from horasExtras where cedula=@cedula;

Declare @VarSentenciaSQL varchar(200)
	Set @VarSentenciaSQL = 'Drop Login [' + (select nomUsu from usuario  u join cajero c on c.cedula=u.cedula where c.cedula=@cedula ) + ']'
	Exec (@VarSentenciaSQL)

	set @Error=@@Error
	if(@Error<>0)
	begin
		return -3
	end

	Declare @VarSentenciaBD varchar(200)
	Set @VarSentenciaBD = 'Drop User [' + (select nomUsu from usuario  u join cajero c on c.cedula=u.cedula where c.cedula=@cedula ) + ']'
	Exec (@VarSentenciaBD)

	set @Error=@@Error
	if(@Error<>0)
	begin
		return -4
	end
	return 1
 end


	Declare @VarSentenciaSQL2 varchar(200)
	Set @VarSentenciaSQL2 = 'Drop Login [' + (select nomUsu from usuario  u join cajero c on c.cedula=u.cedula where c.cedula=@cedula ) + ']'
	Exec (@VarSentenciaSQL2)

	set @Error=@@Error
	if(@Error<>0)
	begin
		return -3
	end

	Declare @VarSentenciaBD2 varchar(200)
	Set @VarSentenciaBD2 = 'Drop User [' + (select nomUsu from usuario  u join cajero c on c.cedula=u.cedula where c.cedula=@cedula ) + ']'
	Exec (@VarSentenciaBD2)

	set @Error=@@Error
	if(@Error<>0)
	begin
		return -4
	end

begin tran

	delete from cajero where cedula = @cedula
	set @Error=@@Error
	if(@Error<>0)
	begin
		return -5
	end

delete from usuario where cedula =@cedula
set @Error=@@Error
	if(@Error<>0)
	begin
		rollback tran
		return -6
	end

	
delete from horasExtras where cedula=@cedula;
set @Error=@@Error
	if(@Error<>0)
	begin
		rollback tran
		return -7
	end


	begin 
	commit tran 
end

End
go



create proc ModificarCajero @cedula int, @nomUsu varchar(15), @nomCompleto varchar(50), @horaini time, @horaFin time as
begin

if not exists(select * from cajero where cedula=@cedula)
return -1 

if exists(select * from gerente where cedula= @cedula)
return -2

declare @Error int

	Declare @VarSentenciaBD varchar(200)
	Set @VarSentenciaBD = 'Alter User [' + (select nomUsu from usuario  u join cajero c on c.cedula=u.cedula where c.cedula=@cedula ) +  ' WITH NAME = ' +@nomUsu+']'
	Exec (@VarSentenciaBD)

	set @Error=@@Error
	if(@Error<>0)
	begin
		return -3
	end
	Declare @VarSentenciaSQL varchar(200)
	Set @VarSentenciaSQL = 'Alter Login [' + (select nomUsu from usuario  u join cajero c on c.cedula=u.cedula where c.cedula=@cedula ) +  ' WITH NAME = ' +@nomUsu+']'
	Exec (@VarSentenciaSQL)

	set @Error=@@Error
	if(@Error<>0)
	begin
		return -4
	end


begin tran
	update cajero set horaIni=@horaini, horaFin=@horaFin where cedula=@cedula

set @Error=@@Error
	if(@Error<>0)
	begin
	rollback tran
		return -5
	end

update usuario set nomUsu=@nomUsu, nomCompleto=@nomCompleto where cedula = @cedula
set @Error=@@Error
	if(@Error<>0)
	begin
		rollback tran
		return -6
	end


	begin 
		commit tran
	end

	end

go

	create proc BuscarCajero @cedula int as
	begin
	if not exists (select * from cajero where cedula=@cedula)
	return -1

	select u.*,c.horaIni,c.horaFin from cajero c join usuario u on c.cedula= u.cedula where u.cedula= @cedula; 
	end 
	go


create proc CambioPass @cedula int, @pass varchar(7) as

begin
if not exists(select * from usuario where cedula=@cedula)
return -1

declare @Error int;

Declare @VarSentenciaSQL varchar(200)
	Set @VarSentenciaSQL = 'Alter Login [' + (select nomUsu from usuario  where cedula=@cedula ) +  ' WITH PASSWORD =' +@pass+ ']'
	Exec (@VarSentenciaSQL)

	set @Error=@@Error
	if(@Error<>0)
	begin
		return -2
	end
	
	Declare @VarSentenciaBD varchar(200)
	Set @VarSentenciaBD = 'Alter User [' + (select nomUsu from usuario  where cedula=@cedula ) +  ' WITH PASSWORD =' + @pass+']'
	Exec (@VarSentenciaBD)

	set @Error=@@Error
	if(@Error<>0)
	begin
		return -3
	end

begin tran
	update usuario set pass = @pass where cedula=@cedula

	set @Error=@@Error
	if(@Error<>0)
	begin
		rollback tran
		return -4
	end

	begin 
	commit tran
	end

end

go


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
CREATE PROC AltaFactura @idPago int, @codContrato int, @codEmp int, @monto int, @codCli int, @fechaVto datetime AS
BEGIN
	IF (EXISTS (SELECT * from factura WHERE idPago = @idPago AND codEmp = @codEmp AND codContrato = @codContrato))
		RETURN -1

	INSERT INTO factura (idPago, codContrato, codEmp, monto, codCli, fechaVto) VALUES (@idPago, @codContrato, @codEmp, @monto, @codCli, @fechaVto)	
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

--FACTURAS DE UN PAGO
CREATE PROC CargarFacturaDeUnPago @idPago int AS 
BEGIN
	SELECT * FROM factura WHERE idPago = @idPago
END
GO


------------------------------------------------------------------------
--TIPO DE CONTRATO

--BUSCAR CONTRATO
CREATE PROC BuscarContrato @codEmp int, @codContrato int AS
BEGIN
	SELECT * FROM tipoContrato WHERE codEmp = @codEmp AND codContrato = @codContrato
END
GO

--Alta Tipo de Contrato
Create Proc AltaTipoContrato @codEmp int , @codContrato int,@nombre varchar (30),@activo bit as
Begin

	If exists (Select * from TipoContrato where codEmp = @codEmp and codContrato =@codContrato and activo =1)
		return -1;

	If exists (Select * from TipoContrato where codEmp = @codEmp and codContrato =@codContrato and activo=0)
		update TipoContrato set activo =1 where codEmp = @codEmp and codContrato =@codContrato	

	else
		Insert Into TipoContrato(codEmp,codContrato ,nombre, activo)
		Values(@codEmp,@codContrato,@nombre,@activo);

	IF(@@Error=0)
		RETURN 1;
	ELSE
		RETURN -2;
End
Go

exec AltaTipoContrato 1234, 33, "Hola Mundo", 1

select * from empresa
select * from tipoContrato WHERE codEmp = 1234 AND codContrato = 33

EXEC BuscarContrato 1234, 33
-------------------------------------------------------------------------
--EMPRESA

--BUSCAR EMPRESA
CREATE PROC BuscarEmpresa @codEmpresa int AS
BEGIN
	SELECT * FROM empresa WHERE codEmpresa = @codEmpresa
END
GO

--Altar Empresa


--------------------------------------------------------------------------


create proc LogueoCajero @nomUsu varchar(15) as
begin

select u.*,c.horaIni,c.horaFin from cajero c join usuario u on c.cedula= u.cedula where nomUsu= @nomUsu; 

end

go
create proc LogueoGerente @nomUsu varchar(15) as
begin

select u.*,g.correo from gerente g join usuario u on g.cedula= u.cedula where nomUsu= @nomUsu; 

end





grant execute  on AltaPago to UsuarioCajero
grant execute  on CambioPass to UsuarioCajero
go
----------------

--exec AltaGerente 45848621,'hitokiri','123456a','Nicolas Rodriguez', 'uncorreo@hotmail.com'

--exec AltaCajero 4565442,'rafiki','123654a','usuario cajero', '2018-01-01 00:00:00','2018-01-01 08:00:00';

--exec ModificarCajero 4565442,'pruebaMod','1236542','usuModificado', '01:00:00','09:00:00'



--TODO 
--El Alta Gerente Funciona, revisar los rollback tran
--El Alta Cajero Funciona, Revisar los rollback tran

--El Baja Cajero Funciona, REvisar con datos de pagos cuando esten prontos.


--El cambio de Pass no Funciona....


	
