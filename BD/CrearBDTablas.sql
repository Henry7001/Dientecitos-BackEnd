-- Crear la base de datos
CREATE DATABASE CentroOdontologico;
GO

-- Usar la base de datos
USE CentroOdontologico;
GO

-- Creación de la tabla Usuario
CREATE TABLE Usuario
(
	UsuarioID INT PRIMARY KEY IDENTITY(1,1),
	Cedula VARCHAR(10) UNIQUE NOT NULL,
	Nombre VARCHAR(50) NOT NULL,
	Telefono VARCHAR(15),
	Rol VARCHAR(20) CHECK (Rol IN ('Paciente', 'Medico', 'Administrativo')),
	Contraseña VARBINARY(128) NOT NULL,
	Estado char(1) NOT NULL DEFAULT 'A' CHECK (Estado IN ('A', 'I', 'N'))
);

-- Creación de la tabla PersonalAdministrativo
CREATE TABLE PersonalAdministrativo
(
	UsuarioID INT PRIMARY KEY,
	RolAdicional VARCHAR(50),
	Estado char(1) NOT NULL DEFAULT 'A' CHECK (Estado IN ('A', 'I', 'N')),
	FOREIGN KEY (UsuarioID) REFERENCES Usuario(UsuarioID)
);

-- Creación de la tabla Paciente
CREATE TABLE Paciente
(
	PacienteID INT PRIMARY KEY IDENTITY(1,1),
	UsuarioID INT NOT NULL,
	Direccion VARCHAR(100),
	NumeroContacto VARCHAR(15),
	Estado char(1) NOT NULL DEFAULT 'A' CHECK (Estado IN ('A', 'I', 'N')),
	FOREIGN KEY (UsuarioID) REFERENCES Usuario(UsuarioID)
);

-- Creación de la tabla Medico
CREATE TABLE Medico
(
	MedicoID INT PRIMARY KEY IDENTITY(1,1),
	UsuarioID INT NOT NULL,
	Especialidad VARCHAR(50) NOT NULL,
	Estado char(1) NOT NULL DEFAULT 'A' CHECK (Estado IN ('A', 'I', 'N')),
	FOREIGN KEY (UsuarioID) REFERENCES Usuario(UsuarioID)
);

-- Creación de la tabla TipoTratamiento
CREATE TABLE TipoTratamiento
(
	TipoTratamientoID INT PRIMARY KEY IDENTITY(1,1),
	NombreTratamiento VARCHAR(100) NOT NULL,
	Descripcion VARCHAR(500),
	CostoAsociado DECIMAL(10, 2) NOT NULL,
	Estado char(1) NOT NULL DEFAULT 'A' CHECK (Estado IN ('A', 'I', 'N'))
);

-- Creación de la tabla CitaMedica
CREATE TABLE CitaMedica
(
	CitaMedicaID INT PRIMARY KEY IDENTITY(1,1),
	TipoTratamientoID INT,
	PacienteID INT,
	MedicoID INT,
	FechaHoraCita DATETIME NOT NULL,
	Observaciones VARCHAR(200),
	Diagnostico VARCHAR(200),
	Estado VARCHAR(20) DEFAULT 'Pendiente' CHECK (Estado IN ('Pendiente', 'Cancelada', 'Finalizada')),
	FOREIGN KEY (TipoTratamientoID) REFERENCES TipoTratamiento(TipoTratamientoID),
	FOREIGN KEY (PacienteID) REFERENCES Paciente(PacienteID),
	FOREIGN KEY (MedicoID) REFERENCES Medico(MedicoID)
);

-- Creación de la tabla HorarioLaboral
CREATE TABLE HorarioLaboral
(
	HorarioID INT PRIMARY KEY IDENTITY(1,1),
	MedicoID INT,
	DiaSemana VARCHAR(10) CHECK (DiaSemana IN ('Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado', 'Domingo')),
	HoraInicio TIME NOT NULL,
	HoraFin TIME NOT NULL,
	Estado char(1) NOT NULL DEFAULT 'A' CHECK (Estado IN ('A', 'I', 'N')),
	FOREIGN KEY (MedicoID) REFERENCES Medico(MedicoID)
);

GO
