
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
correo varchar(50) not null check(correo LIKE '%_@_%_.__%')
)


go

create table cajero
(
cedula int not null primary key foreign key references usuario(cedula),
horaIni time not null,
horaFin time not null,
activo bit not null default 1,
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
CREATE ROLE UsuarioWeb AUTHORIZATION [IIS APPPOOL\DefaultAppPool];
GO

CREATE ROLE UsuarioCajero
GO


--Alta Empresa
Create Proc AltaEmpresa
@codEmpresa int,@rut int,@dirFiscal varchar(100),@telefono varchar(30),@activo bit as
Begin

  If exists (Select codEmpresa from empresa where codEmpresa = @codEmpresa and rut <> @rut)
   return -1;

If exists (Select rut from empresa where rut = @rut)
   return -2;

If exists (Select codEmpresa from empresa where codEmpresa = @codEmpresa and rut = @rut and activo=0)
	update empresa set activo =1 where codEmpresa = @codEmpresa and rut = @rut
	return 2

    Insert Into empresa(codEmpresa,rut,dirFiscal ,telefono ,activo)
    Values(@codEmpresa,@rut,@dirFiscal,@telefono,@activo);

if(@@ERROR=0)
begin
commit tran
return 1;
end
else
begin
rollback tran
return -3;
end
End
Go


--Eliminar Empresa
create proc BajaEmpresa @codEmpresa int
as
begin
 
  If not exists (Select codEmpresa from empresa  where codEmpresa = @codEmpresa)
   return -1;

If not exists (Select codEmp from factura  where codEmp = @codEmpresa)
	delete from empresa where codEmpresa =@codEmpresa
 if(@@ERROR = 0)
	begin
		commit tran;
		return 1;
	end
else
	begin
		rollback tran;
		return -2;
	end		

 If exists (Select codEmp from factura  where codEmp = @codEmpresa)
	update empresa set activo=0 where codEmpresa =@codEmpresa

if(@@ERROR = 0)
	begin
		commit tran;
		return 2;
	end
else
	begin
		rollback tran;
		return -2;
	end		
end	
 
go

--Modificacion Empresa
create proc ModificoEmpresa
@codEmpresa int,@rut int,@dirFiscal varchar(100),@telefono varchar(30),@activo bit as
 
Begin
 
if (not exists(select * from empresa  where codEmpresa=@codEmpresa))
return -1;
 
Update empresa set rut = @rut, dirFiscal= @dirFiscal,telefono=@telefono,activo= @activo where codEmpresa=@codEmpresa;
 			 
	if(@@ERROR=0)
	begin
		commit tran;
		return 1;
	end
	else
	begin
		rollback tran;
		return 0;
	end	
end
 
go

--Listar facturas de empresa especifica
create proc FacturasEmpresa @codEmpresa int as
begin 
select * from factura where codEmp =@codEmpresa
END
go
--GRANT Execute on "Procedimientos del Usuario Web" to UsuarioWeb
--GRANT Execute on "Procedimientos del Usuario Cajero" to UsuarioCajero

