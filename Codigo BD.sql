CREATE DATABASE SACGP;
USE SACGP;
CREATE TABLE Usuario(
	nombre_usuario VARCHAR(20) NOT NULL,
	clave VARCHAR(300) NOT NULL,
	esAdmin BIT NOT NULL,
	respuesta VARCHAR(100) NOT NULL,
        estado BIT NOT NULL
	id_Usuario INT NOT NULL IDENTITY(1,1) PRIMARY KEY
);

CREATE TABLE Empleado (
	cedula INT NOT NULL PRIMARY KEY,
	nombre VARCHAR(20) NOT NULL,
	apellido1 VARCHAR(20) NOT NULL,
	apellido2 VARCHAR(20) NOT NULL,
	fecha_nacimiento DATE NOT NULL,
	direccion VARCHAR(250) NOT NULL,
	telefono INT NOT NULL
);

CREATE TABLE Ausencia (
	id_ausencia INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	fecha DATE NOT NULL,
	motivo VARCHAR(255),
	empleado INT FOREIGN KEY REFERENCES Empleado(cedula) 
);

CREATE TABLE Tipo_gasto(
	id_tipogasto INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	tipo VARCHAR(100) NOT NULL
);

CREATE TABLE Presupuesto(
	id_presupuesto INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	monto MONEY,
	fecha_year INT
);

CREATE TABLE Registro_gasto(
	id_gasto INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	tipo INT FOREIGN KEY REFERENCES Tipo_gasto(id_tipogasto),
	monto MONEY,
	fecha DATE,
	presupuesto INT FOREIGN KEY REFERENCES Presupuesto(	id_presupuesto),
	usuario INT FOREIGN KEY REFERENCES Usuario(id_Usuario)
);
