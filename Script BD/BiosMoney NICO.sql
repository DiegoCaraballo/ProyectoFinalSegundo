
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
GO---

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
drop login [IIS APPPOOL\DefaultAppPool]
CREATE LOGIN [IIS APPPOOL\DefaultAppPool] FROM WINDOWS WITH DEFAULT_DATABASE = master
GO

USE BiosMoney
GO
drop user [IIS APPPOOL\DefaultAppPool]
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

Declare @VarSentencia varchar(200)
	Set @VarSentencia = 'CREATE LOGIN [' +  @nomUsu + '] WITH PASSWORD = ' + QUOTENAME(@pass, '''') + ',DEFAULT_DATABASE = BiosMoney'
	Exec (@VarSentencia)
	
	set @Error=@@ERROR
	if(@Error<>0)
	begin
		return -5
	end
		
	--Asigno rol al usuario recien creado
	Exec sp_addsrvrolemember @loginame=@nomUsu, @rolename='securityadmin'
	
	if (@@ERROR <> 0)
	if(@Error<>0)
	begin
		return -5
	end

--Creo usuario de acceso a base de datos 
	Set @VarSentencia = 'Create User [' +  @nomUsu + '] From Login [' + @nomUsu + ']'
	Exec (@VarSentencia)
	
	set @Error=@@ERROR
	if(@Error<>0)
	begin
		return -5
	end
	--segundo asigno rol especifico al usuario recien creado
	Exec sp_addrolemember @rolename='db_owner', @membername=@nomUsu
	
	if (@@ERROR <> 0)
if(@Error<>0)
	begin
		return -5
	end
	
end

go
--exec AltaGerente 45848621,'hitokiri','123456a','Nicolas Rodriguez', 'uncorreo@hotmail.com'
--delete from gerente
--delete from usuario

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

	
Declare @VarSentencia varchar(200)
	Set @VarSentencia = 'CREATE LOGIN [' +  @nomUsu + '] WITH PASSWORD = ' + QUOTENAME(@pass, '''') + ',DEFAULT_DATABASE = BiosMoney'
	Exec (@VarSentencia)
	
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
	Set @VarSentencia = 'Create User [' +  @nomUsu + '] From Login [' + @nomUsu + ']'
	Exec (@VarSentencia)
	
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

Declare @VarSentencia varchar(200)
	Set @VarSentencia = 'Drop Login [' + (select nomUsu from usuario  u join cajero c on c.cedula=u.cedula where c.cedula=@cedula ) + ']'
	Exec (@VarSentencia)

	set @Error=@@Error
	if(@Error<>0)
	begin
		return -3
	end

	Set @VarSentencia = 'Drop User [' + (select nomUsu from usuario  u join cajero c on c.cedula=u.cedula where c.cedula=@cedula ) + ']'
	Exec (@VarSentencia)

	set @Error=@@Error
	if(@Error<>0)
	begin
		return -4
	end
	return 1
 end


	Set @VarSentencia = 'Drop Login [' + (select nomUsu from usuario  u join cajero c on c.cedula=u.cedula where c.cedula=@cedula ) + ']'
	Exec (@VarSentencia)

	set @Error=@@Error
	if(@Error<>0)
	begin
		return -3
	end

	Set @VarSentencia = 'Drop User [' + (select nomUsu from usuario  u join cajero c on c.cedula=u.cedula where c.cedula=@cedula ) + ']'
	Exec (@VarSentencia)

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

	Declare @VarSentencia varchar(200)
	Set @VarSentencia = 'Alter User [' + (select nomUsu from usuario  u join cajero c on c.cedula=u.cedula where c.cedula=@cedula ) +  ' WITH NAME = ' +@nomUsu+']'
	Exec (@VarSentencia)

	set @Error=@@Error
	if(@Error<>0)
	begin
		return -3
	end
	Set @VarSentencia = 'Alter Login [' + (select nomUsu from usuario  u join cajero c on c.cedula=u.cedula where c.cedula=@cedula ) +  ' WITH NAME = ' +@nomUsu+']'
	Exec (@VarSentencia)

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

Declare @VarSentencia varchar(200)
	Set @VarSentencia = 'Alter Login [' + (select nomUsu from usuario  where cedula=@cedula ) +  ' WITH PASSWORD =' +@pass+ ']'
	Exec (@VarSentencia)

	set @Error=@@Error
	if(@Error<>0)
	begin
		return -2
	end
	
	Set @VarSentencia = 'Alter User [' + (select nomUsu from usuario  where cedula=@cedula ) +  ' WITH PASSWORD =' + @pass+']'
	Exec (@VarSentencia)

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
CREATE PROC AltaPago @fecha date, @montoTotal int, @cedulaCajero int, @numInterno int output AS
BEGIN
	INSERT INTO pago (fecha, montoTotal, cedulaCajero) VALUES (@fecha, @montoTotal, @cedulaCajero)
	SET @numInterno = SCOPE_IDENTITY()
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

--BUSCAR FACTURA POR CODIGO DE BARRA
create proc pagoDeUnaFactura @codContra int, @codEmp int, @monto int, @codCli int, @fecha date as
begin
	if exists(select * from factura f where f.codContrato= @codContra and f.codEmp= @codEmp and f.monto= @monto and f.codCli= @codCli and f.fechaVto= @fecha)
	begin

		select * from pago p where p.numeroInt in(
		select idPago from factura f where f.codContrato= @codContra and f.codEmp= @codEmp and f.monto= @monto and f.codCli= @codCli and f.fechaVto= @fecha
		)
	end
end
go

exec CargarFacturaDeUnPago 2

--FACTURAS DE UN PAGO
CREATE PROC CargarFacturaDeUnPago @idPago int AS 
BEGIN
	SELECT * FROM factura WHERE idPago =  @idPago
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

--Alta Tipo de Contrato ---TODO REvisar, agregue un para de IF mas para verificar la foreign key
Create Proc AltaTipoContrato @codEmp int , @codContrato int,@nombre varchar (30) as
Begin
if not exists (select * from empresa where codEmpresa=@codEmp)
return -1

if exists (select * from empresa where codEmpresa=@codEmp and activo =0)
return -2


	If exists (Select * from TipoContrato where codEmp = @codEmp and codContrato =@codContrato and activo =1)
		return -3;

	If exists (Select * from TipoContrato where codEmp = @codEmp and codContrato =@codContrato and activo=0)
		update TipoContrato set activo =1 where codEmp = @codEmp and codContrato =@codContrato	

	else
		Insert Into TipoContrato(codEmp,codContrato ,nombre)
		Values(@codEmp,@codContrato,@nombre);

	IF(@@Error=0)
		RETURN 1;
	ELSE
		RETURN -4;
End
Go

Create Proc ModificarTipoContrato @codEmp int , @codContrato int,@nombre varchar (30)as
Begin

if not exists (select * from empresa where codEmpresa=@codEmp)
return -1

if exists (select * from empresa where codEmpresa=@codEmp and activo =0)
return -2



	If exists (Select * from TipoContrato where codEmp = @codEmp and codContrato =@codContrato and activo=0)
		return -3	

		update tipoContrato set nombre=@nombre where @codContrato=@codContrato and codEmp=codEmp;
	
End

--exec AltaTipoContrato 1234, 33, "Hola Mundo", 1

--select * from empresa
--select * from tipoContrato WHERE codEmp = 1234 AND codContrato = 33

--EXEC BuscarContrato 1234, 33
-------------------------------------------------------------------------
--EMPRESA
go


create proc BajaTipoContrato @codEmp int, @codContrato int as
begin
if not exists (select * from empresa where codEmpresa=@codEmp)
return -1

if not exists (select * from tipoContrato where codContrato=@codContrato and codEmp=@codEmp)
return -2

if exists ((select codContrato from factura where codContrato in(select codContrato from tipoContrato where codContrato =@codContrato and codEmp =@codEmp)))
begin
update tipoContrato set activo = 0 where codContrato=@codContrato and codEmp=@codEmp;
return 1
end

delete from tipoContrato where codContrato=@codContrato and codEmp=@codEmp;

end

go




---------------Empresa
--BUSCAR EMPRESA
CREATE PROC BuscarEmpresa @codEmpresa int AS
BEGIN
	SELECT * FROM empresa WHERE codEmpresa = @codEmpresa
END
GO

--Altar Empresa


create proc AltaEmpresa @codEmp int, @rut int, @direccion varchar(100), @telefono varchar(30) as
begin

if exists(select * from empresa where codEmpresa =@codEmp)
return -1

if exists (select * from empresa where codEmpresa =@codEmp and activo = 0)
begin
update empresa set activo =1 , rut=@rut, dirFiscal=@direccion, telefono=@telefono where codEmpresa=@codEmp;
return 1
end

insert into empresa(codEmpresa,rut,dirFiscal,telefono) values(@codEmp,@rut,@direccion,@telefono);
if (@@ERROR <>0)
begin
return-2
end


end
go

create proc ModificarEmpresa @codEmp int, @rut int, @direccion varchar(100), @telefono varchar(30) as
begin

if not exists(select * from empresa where codEmpresa =@codEmp)
return -1

update empresa set  rut=@rut, dirFiscal=@direccion, telefono=@telefono where codEmpresa=@codEmp;

if (@@ERROR <>0)
begin
return-2
end


end
go

create proc BajaEmpresa @codEmp int as
begin
declare @Error int;
if not exists(select * from empresa where codEmpresa=@codEmp)
return -1

if exists(select * from factura where codContrato in(select codContrato from tipoContrato where codEmp= @codEmp))
begin
begin tran
update tipoContrato set activo = 0 where codContrato in(select codContrato from tipoContrato where codEmp= @codEmp);
set @Error=@@Error
	if(@Error<>0)
	begin
		rollback tran
		return -2
	end
update empresa set activo=0 where codEmpresa=@codEmp;
set @Error=@@Error
	if(@Error<>0)
	begin
		rollback tran
		return -3
	end

	begin 
	commit tran
	end

return 1
end


begin tran 

delete from tipoContrato where codEmp=@codEmp;
set @Error=@@Error
	if(@Error<>0)
	begin
		rollback tran
		return -4
	end
delete from empresa where codEmpresa=@codEmp;
set @Error=@@Error
	if(@Error<>0)
	begin
		rollback tran
		return -5
	end
	begin 
		commit tran
	 end
end

go








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
select * from usuario
--exec ModificarCajero 4565442,'pruebaMod','1236542','usuModificado', '01:00:00','09:00:00'



--TODO 
--El Alta Gerente Funciona, revisar los rollback tran
--El Alta Cajero Funciona, Revisar los rollback tran

--El Baja Cajero Funciona, REvisar con datos de pagos cuando esten prontos.


--El cambio de Pass no Funciona....

use BiosMoney
select * from factura