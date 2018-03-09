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
--drop login [IIS APPPOOL\DefaultAppPool]
CREATE LOGIN [IIS APPPOOL\DefaultAppPool] FROM WINDOWS WITH DEFAULT_DATABASE = master
GO

USE BiosMoney
GO
--drop user [IIS APPPOOL\DefaultAppPool]
CREATE USER [IIS APPPOOL\DefaultAppPool] FOR LOGIN [IIS APPPOOL\DefaultAppPool]
GO

GRANT Execute TO [IIS APPPOOL\DefaultAppPool]

USE BiosMoney;

go
--------------------------------------------------------------------------------------------------------------------------------------------
--*--												Alta Gerente																       --*--
--------------------------------------------------------------------------------------------------------------------------------------------

--Alta Gerente
create proc AltaGerente @cedula int, @nomUsu varchar(15), @pass varchar(7), @nomCompleto varchar(50), @correo varchar(70) as
begin

if exists (select * from usuario where cedula = @cedula)
	return -1

if exists(select * from gerente where cedula= @cedula)
	return -2

declare @Error int;
begin tran
	--Creo Usuario
	insert into Usuario(cedula,nomUsu,pass,nomCompleto) values(@cedula,@nomUsu,@pass,@nomCompleto)
	set @Error = @@ERROR
	if(@Error<>0)
	begin
		rollback tran
		return -3
	end
	--Creo Gerente
	insert into Gerente values (@cedula,@correo)
	set @Error=@@ERROR
	if(@Error<>0)
	begin
		rollback tran
		return -4
	end
	-- Creo Usuario de Logueo en el servidor
Declare @VarSentencia varchar(200)
	Set @VarSentencia = 'CREATE LOGIN [' +  @nomUsu + '] WITH PASSWORD = ' + QUOTENAME(@pass, '''') + ',DEFAULT_DATABASE = BiosMoney'
	Exec (@VarSentencia)
	set @Error=@@ERROR
	if(@Error<>0)
	begin
		rollback tran
		return -5
	end
	--Creo usuario de BD
	Set @VarSentencia = 'Create User [' +  @nomUsu + '] From Login [' + @nomUsu + ']'
	Exec (@VarSentencia)
	set @Error=@@ERROR
	if(@Error<>0)
	begin
		rollback tran
		return -6
	end
	else	
	begin
		commit tran
	end
	--Asigno ROL de Servidor al usuario recien creado
	Exec sp_addsrvrolemember @loginame=@nomUsu, @rolename='securityadmin'
	set @Error=@@ERROR
	if(@Error<>0)
		return -7
	--Asigno ROl de BD al usuario recien creado	
	Exec sp_addrolemember @rolename='db_owner', @membername=@nomUsu
	set @Error=@@ERROR
	if(@Error<>0)
		return -8
	else
		return 1
end
go

--Logeuo Gerente
create proc LogueoGerente @nomUsu varchar(15) as
begin
select u.*,g.correo from gerente g join usuario u on g.cedula= u.cedula where nomUsu= @nomUsu;
end
go

--------------------------------------------------------------------------------------------------------------------------------------------
--*--														ABM Cajero															       --*--
--------------------------------------------------------------------------------------------------------------------------------------------
create proc AltaCajero @cedula int, @nomUsu varchar(15), @pass varchar(7), @nomCompleto varchar(50), @horaini time, @horaFin time as
begin
	Declare @VarSentencia varchar(200);
	declare @Error int;

	if exists(select * from usuario where cedula=@cedula)
		return-1

	if exists(select * from cajero where cedula=@cedula and activo=1)
		return-2

	if exists(select * from cajero where cedula=@cedula and activo=0)
	begin
	begin tran
		--Acutalizo datos del cajero inactivo
		update cajero set activo =1, horaIni=@horaini,horaFin=@horaFin where cedula=@cedula;
		set @Error=@@ERROR
		if(@Error<>0)
		begin
			rollback tran
			return -3
		end
		--Actualizo datos del cajero inactivo
		update usuario set nomUsu = @nomUsu, pass =@pass, nomCompleto=@nomCompleto where cedula=@cedula
		set @Error=@@ERROR
		if(@Error<>0)
		begin
			rollback tran
			return -4
		end
		--Creo usuario de acceso al servidor
		Set @VarSentencia = 'CREATE LOGIN [' +  @nomUsu + '] WITH PASSWORD = ' + QUOTENAME(@pass, '''') + ',DEFAULT_DATABASE = BiosMoney'
		Exec (@VarSentencia)
		set @Error=@@ERROR
		if(@Error<>0)
		begin
			rollback tran
			return -5
		end

	--Creo usuario de acceso a base de datos
		Set @VarSentencia = 'Create User [' +  @nomUsu + '] From Login [' + @nomUsu + ']'
		Exec (@VarSentencia)
		set @Error=@@ERROR
		if(@Error<>0)
		begin
			rollback tran
			return -6
		end
		else
		begin 
			commit tran
		end
				
		Set @VarSentencia = 'Grant execute on AltaPago to [' +  @nomUsu + ']'
		Exec (@VarSentencia)
		set @Error=@@ERROR
		if(@Error<>0)
			return -6

		Set @VarSentencia = 'Grant execute on CambioPass to [' +  @nomUsu + ']'
		Exec (@VarSentencia)
		set @Error=@@ERROR
		if(@Error<>0)
		return -6

		Set @VarSentencia = 'Grant execute on AltaFactura to [' +  @nomUsu + ']'
		Exec (@VarSentencia)
		set @Error=@@ERROR
		if(@Error<>0)
			return -6

		Set @VarSentencia = 'Grant execute on LogueoCajero to [' +  @nomUsu + ']'
		Exec (@VarSentencia)
		set @Error=@@ERROR
		if(@Error<>0)
			return -6
		else
			return 1
	end

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

	Set @VarSentencia = 'CREATE LOGIN [' +  @nomUsu + '] WITH PASSWORD = ' + QUOTENAME(@pass, '''') + ',DEFAULT_DATABASE = BiosMoney'
	Exec (@VarSentencia)
	set @Error=@@ERROR
	if(@Error<>0)
	begin
		rollback tran
		return-4
	end
	--Creo usuario de acceso a base de datos
	Set @VarSentencia = 'Create User [' +  @nomUsu + '] From Login [' + @nomUsu + ']'
	Exec (@VarSentencia)
	set @Error=@@ERROR
	if(@Error<>0)
	begin
		rollback tran
		return-4
	end
	else
	begin
		commit tran
	end
	
	Set @VarSentencia = 'Grant execute on AltaPago to [' +  @nomUsu + ']'
	Exec (@VarSentencia)
	set @Error=@@ERROR
	if(@Error<>0)
		return -6

	Set @VarSentencia = 'Grant execute on CambioPass to [' +  @nomUsu + ']'
	Exec (@VarSentencia)
	set @Error=@@ERROR
	if(@Error<>0)
		return -6
		
	Set @VarSentencia = 'Grant execute on AltaFactura to [' +  @nomUsu + ']'
		Exec (@VarSentencia)
		set @Error=@@ERROR
		if(@Error<>0)
			return -6
	Set @VarSentencia = 'Grant execute on LogueoCajero to [' +  @nomUsu + ']'
	Exec (@VarSentencia)
	set @Error=@@ERROR
	if(@Error<>0)
		return -6
	else
		return 1
end
go

--Baja Cajero
create proc BajaCajero @cedula int as
begin
	declare @Error int;
	Declare @VarSentencia varchar(200);

	if not exists(select * from cajero where cedula=@cedula)
		return -1

	if exists(select * from gerente where cedula=@cedula)
		return -2

	if exists(select * from pago where cedulaCajero=@cedula)
	begin
		begin tran
			delete from horasExtras where cedula=@cedula;
			set @Error=@@Error
			if(@Error<>0)
			begin
				rollback tran
				return -3
			end
			
			update cajero set activo = 0 where cedula=@cedula
			set @Error=@@Error
			if(@Error<>0)
			begin
				rollback tran
				return -3
			end

			Set @VarSentencia = 'Drop Login [' + (select nomUsu from usuario  u join cajero c on c.cedula=u.cedula where c.cedula=@cedula ) + ']'
			Exec (@VarSentencia)
			set @Error=@@Error
			if(@Error<>0)
			begin
				rollback tran
				return -3
			end
			
			Set @VarSentencia = 'Drop User [' + (select nomUsu from usuario  u join cajero c on c.cedula=u.cedula where c.cedula=@cedula ) + ']'
			Exec (@VarSentencia)
			set @Error=@@Error
			if(@Error<>0)
			begin
				rollback tran
				return -3
			end
			else
			begin
				commit tran
				return 1
			end
		end

begin tran
	Set @VarSentencia = 'Drop Login [' + (select nomUsu from usuario  u join cajero c on c.cedula=u.cedula where c.cedula=@cedula ) + ']'
	Exec (@VarSentencia)
	set @Error=@@Error
	if(@Error<>0)
	begin
		rollback tran
		return -3
	end

	Set @VarSentencia = 'Drop User [' + (select nomUsu from usuario  u join cajero c on c.cedula=u.cedula where c.cedula=@cedula ) + ']'
	Exec (@VarSentencia)
	set @Error=@@Error
	if(@Error<>0)
	begin
		rollback tran
		return -3
	end
	
	delete from horasExtras where cedula=@cedula;
	set @Error=@@Error
	if(@Error<>0)
	begin
		rollback tran
		return -5
	end

	delete from cajero where cedula = @cedula
	set @Error=@@Error
	if(@Error<>0)
	begin
		rollback tran
		return -6
	end

	delete from usuario where cedula =@cedula
	set @Error=@@Error
	if(@Error<>0)
	begin
		rollback tran
		return -7
	end
	else
	begin
		commit tran
		return 1
	end

End
go


--Modificar Cajero
create proc ModificarCajero @cedula int, @nomCompleto varchar(50), @horaini time, @horaFin time as
begin

	if not exists(select * from cajero where cedula=@cedula)
		return -1

	if exists(select * from gerente where cedula= @cedula)
		return -2

	declare @Error int

	begin tran
		update cajero set horaIni=@horaini, horaFin=@horaFin where cedula=@cedula

	set @Error=@@Error
		if(@Error<>0)
		begin
		rollback tran
			return -5
		end

	update usuario set nomCompleto=@nomCompleto where cedula = @cedula
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


--Buscar Cajero
create proc BuscarCajero @cedula int as
begin
	select u.*,c.horaIni,c.horaFin from cajero c join usuario u on c.cedula= u.cedula where u.cedula= @cedula and activo =1
end
go
create proc BuscarCajeroInactivo @cedula int as
begin
	select u.*,c.horaIni,c.horaFin from cajero c join usuario u on c.cedula= u.cedula where u.cedula= @cedula and activo =0
end
go

--Cambiar Pass
--drop proc CambioPass
--exec CambioPass 1233211,'rafiki1'
--alter login rafiki with password = '1231231'
create proc CambioPass @cedula int, @pass varchar(7) as
begin
	if not exists(select * from usuario where cedula=@cedula)
		return -1

	declare @Error int;
	Declare @VarSentencia varchar(200);
	
	begin tran
	Set @VarSentencia = 'Alter Login [' + (select nomUsu from usuario  where cedula=@cedula ) +  '] WITH PASSWORD = ''' +@pass+ ';';
	Exec (@VarSentencia)
	set @Error=@@Error
	if(@Error<>0)
	begin
		rollback tran
		return -4
	end

	Set @VarSentencia = 'Alter User [' + (select nomUsu from usuario  where cedula=@cedula ) +  '] WITH PASSWORD = ''' +@pass+';';
	Exec (@VarSentencia)
	set @Error=@@Error
	if(@Error<>0)
	begin
		rollback tran
		return -4
	end

	update usuario set pass = @pass where cedula=@cedula
	set @Error=@@Error
	if(@Error<>0)
	begin
		rollback tran
		return -4
	end	
	begin
		commit tran
		return 1
	end
end

go


--LogueoCajero
create proc LogueoCajero @nomUsu varchar(15) as
begin
	select u.*,c.horaIni,c.horaFin from cajero c join usuario u on c.cedula= u.cedula where nomUsu= @nomUsu;
end

go
create proc AgregarHorasExtras @cedula int ,@fecha date, @minutos int as
begin
	if not exists(select * from cajero where cedula =@cedula)
		return -1
	if exists(select * from horasExtras where cedula= @cedula and fecha=@fecha)
	begin
		update horasExtras set minutos = @minutos where cedula=@cedula and fecha=@fecha
		return 1
	end

	insert into horasExtras(cedula,fecha,minutos) values (@cedula,@fecha,@minutos)
		return 2
	end
go

--------------------------------------------------------------------------------------------------------------------------------------------
--*--											      	Pago																	       --*--
--------------------------------------------------------------------------------------------------------------------------------------------

--AGREGAR PAGO
CREATE PROC AltaPago @fecha date, @montoTotal int, @cedulaCajero int, @numInterno int output AS
BEGIN
	IF NOT EXISTS(SELECT * FROM cajero WHERE cedula = @cedulaCajero)
		RETURN -1
	if exists(select * from cajero where cedula= @cedulaCajero and activo=0)
	return -3

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


--------------------------------------------------------------------------------------------------------------------------------------------
--*--												Factura																		       --*--
--------------------------------------------------------------------------------------------------------------------------------------------

--AGREGAR FACTURA
CREATE PROC AltaFactura @idPago int, @codContrato int, @codEmp int, @monto int, @codCli int, @fechaVto datetime AS
BEGIN
	IF (EXISTS (SELECT * from factura WHERE idPago = @idPago AND codEmp = @codEmp AND codContrato = @codContrato and codCli = @codCli))
		RETURN -1

	IF (Not EXISTS (SELECT * FROM empresa WHERE codEmpresa = @codEmp))
		RETURN -2
	IF (EXISTS (SELECT * FROM empresa WHERE codEmpresa = @codEmp AND activo = 0))
		RETURN -2

	IF (EXISTS (SELECT * FROM tipoContrato WHERE codEmp = @codEmp AND codContrato = @codContrato AND activo = 0))
		RETURN -3

	INSERT INTO factura (idPago, codContrato, codEmp, monto, codCli, fechaVto) VALUES (@idPago, @codContrato, @codEmp, @monto, @codCli, @fechaVto)
	IF(@@ERROR = 0)
		RETURN 1
	ELSE
		RETURN -4
END
GO

--BUSCAR FACTURA POR CODIGO DE BARRA
create proc pagoDeUnaFactura @codContra int, @codEmp int, @monto int, @codCli int, @fecha date as
begin
	select p.fecha from pago p where p.numeroInt in(
	select idPago from factura f where f.codContrato = @codContra and f.codEmp = @codEmp and f.monto = @monto and f.codCli = @codCli and f.fechaVto = @fecha)
end
go


--FACTURAS DE UN PAGO
CREATE PROC CargarFacturaDeUnPago @idPago int AS
BEGIN
	SELECT * FROM factura WHERE idPago = @idPago
END
GO


--------------------------------------------------------------------------------------------------------------------------------------------
--*--												Tipo Contrato																       --*--
--------------------------------------------------------------------------------------------------------------------------------------------
--BUSCAR CONTRATO
CREATE PROC BuscarContrato @codEmp int, @codContrato int AS
BEGIN
	SELECT * FROM tipoContrato WHERE codEmp = @codEmp AND codContrato = @codContrato and activo =1;
END
GO

--Alta Tipo de Contrato
Create Proc AltaTipoContrato @codEmp int , @codContrato int,@nombre varchar (30) as
Begin
	if not exists (select * from empresa where codEmpresa = @codEmp)
		return -1

	if exists (select * from empresa where codEmpresa = @codEmp and activo = 0)
		return -2

	If exists (Select * from TipoContrato where codEmp = @codEmp and codContrato = @codContrato and activo = 1)
		return -3;

	If exists (Select * from TipoContrato where codEmp = @codEmp and codContrato = @codContrato and activo = 0)
		update TipoContrato set activo =1 where codEmp = @codEmp and codContrato = @codContrato

	else
		Insert Into TipoContrato(codEmp,codContrato ,nombre)
		Values(@codEmp,@codContrato,@nombre);

	IF(@@Error=0)
		RETURN 1;
	ELSE
		RETURN -4;
End
Go


--Modificar Contrato
Create Proc ModificarTipoContrato @codEmp int , @codContrato int,@nombre varchar (30)as
Begin

	If exists (Select * from TipoContrato where codEmp = @codEmp and codContrato = @codContrato and activo = 0)
		return -3

	update tipoContrato set nombre = @nombre where @codContrato = @codContrato and codEmp = codEmp;
	IF(@@Error=0)
		RETURN 1;
	ELSE
		RETURN -4;

End

go


--Baja Contrato
create proc BajaTipoContrato @codEmp int, @codContrato int as
begin

	if not exists (select * from tipoContrato where codContrato = @codContrato and codEmp = @codEmp)
		return -2

	if exists (select * from factura where codContrato = @codContrato and codEmp = @codEmp)
	begin
		update tipoContrato set activo = 0 where codContrato=@codContrato and codEmp = @codEmp;
			return 1
	end

	delete from tipoContrato where codContrato=@codContrato and codEmp=@codEmp;
	IF(@@Error=0)
		RETURN 1;
	ELSE
		RETURN -4;

end

go

create proc ListarContratos as
begin
	select * from tipoContrato;
end

go

--------------------------------------------------------------------------------------------------------------------------------------------
--*--												Empresa																		       --*--
--------------------------------------------------------------------------------------------------------------------------------------------
--BUSCAR EMPRESA
CREATE PROC BuscarEmpresa @codEmpresa int AS
BEGIN
	SELECT * FROM empresa WHERE codEmpresa = @codEmpresa and activo = 1;
END
GO

--Altar Empresa
create proc AltaEmpresa @codEmp int, @rut int, @direccion varchar(100), @telefono varchar(30) as
begin

	if exists(select * from empresa where codEmpresa = @codEmp and activo = 1)
		return -1

	if exists (select * from empresa where codEmpresa = @codEmp and activo = 0)
	begin
	update empresa set activo = 1 , rut = @rut, dirFiscal = @direccion, telefono = @telefono where codEmpresa = @codEmp;
		return 1
	end

	insert into empresa(codEmpresa, rut, dirFiscal, telefono) values(@codEmp, @rut, @direccion, @telefono);
	IF(@@Error=0)
		RETURN 1;
	ELSE
		RETURN -2;

end
go


--Modificar Empresa
create proc ModificarEmpresa @codEmp int, @rut int, @direccion varchar(100), @telefono varchar(30) as
begin

	if not exists(select * from empresa where codEmpresa = @codEmp)
		return -1

	IF EXISTS (SELECT * FROM empresa where codEmpresa = @codEmp and activo = 0)
		return -2

	update empresa set  rut=@rut, dirFiscal=@direccion, telefono=@telefono where codEmpresa=@codEmp;
	IF(@@Error=0)
		RETURN 1;
	ELSE
		RETURN -3;

end
go


--Baja Empresa
create proc BajaEmpresa @codEmp int as
begin
	declare @Error int;

	if not exists(select * from empresa where codEmpresa = @codEmp)
		return -1

	if exists(select * from factura where codEmp  = @codEmp)
	begin
	begin tran
	update tipoContrato set activo = 0 where codContrato in(select codContrato from tipoContrato where codEmp = @codEmp);
	set @Error=@@Error
		if(@Error<>0)
		begin
			rollback tran
			return -2
		end
	update empresa set activo = 0 where codEmpresa = @codEmp;
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

	delete from tipoContrato where codEmp = @codEmp;
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

create proc ListarEmpresas as
begin
	select * from empresa
end
go


--------------------------------------------------------------------------
					----Datos Para Probar
--------------------------------------------------------------------------
exec AltaEmpresa 1234, 123456789, 'asdasdas','123456'
exec AltaEmpresa 9999, 654882789, 'asdasdas','123456'
exec AltaEmpresa 5555, 66866789, 'asdasdas','123456'
exec AltaEmpresa 2345, 121212121, 'pepe grillo', '123456'

exec AltaTipoContrato 1234, 33, 'Hola Mundo'
exec AltaTipoContrato 9999, 01, 'kjasnd'
exec AltaTipoContrato 9999, 33, 'Hola Mqweniqwue'
exec AltaTipoContrato 5555, 33, 'Holajhbsdo'
exec AltaTipoContrato 5555, 52, 'HokjdausndMundo'
exec AltaTipoContrato 5555, 25, 'Hoanksdjndo'




--select * from empresa
--select * from tipoContrato WHERE codEmp = 1234 AND codContrato = 33

--EXEC BuscarContrato 1234, 33

exec AltaGerente 45848621,'hitokiri','123456a','Nicolas Rodriguez', 'uncorreo@hotmail.com'
go
exec AltaCajero 4565442,'rafiki','123654a','usuario cajero', '2018-01-01 00:00:00','2018-01-01 08:00:00';
go
exec AltaCajero 1233211,'pepegrillo','1234567','Pepe grillo', '2018-01-01 00:00:00','2018-01-01 08:00:00';
go
--update Cajero set HoraFin= '2018-01-01 20:00:00', HoraIni ='2018-01-01 19:00:00';