USE [CentroOdontologico]
GO

/****** Object:  StoredProcedure [dbo].[GestionarUsuario]    Script Date: 24/12/2023 18:16:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[GestionarUsuario]
    @Accion NVARCHAR(20),
    @UsuarioID INT = NULL,
    @Cedula VARCHAR(10) = NULL,
    @Nombre VARCHAR(50) = NULL,
    @Telefono VARCHAR(15) = NULL,
    @Rol VARCHAR(20) = NULL,
    @Contraseña VARCHAR(128) = NULL,
    @NuevaContraseña VARCHAR(128) = NULL,
	@PacienteID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @Accion = 'Insertar'
	BEGIN
		-- Verificar si la Cédula ya está registrada
		IF EXISTS (SELECT 1 FROM Usuario WHERE Cedula = @Cedula)
		BEGIN
			-- Si la Cédula ya está registrada, generar un error
			RAISERROR('La Cédula ya está registrada en la base de datos.', 16, 1);
		END
		ELSE
		BEGIN
			-- Insertar usuario con contraseña convertida a varbinary
			INSERT INTO Usuario (Cedula, Nombre, Telefono, Rol, Contraseña)
			VALUES (@Cedula, @Nombre, @Telefono, @Rol, CONVERT(VARBINARY(128), @Contraseña));

			-- Obtener el UsuarioID del usuario recién insertado
			SET @UsuarioID = SCOPE_IDENTITY();

			-- Mostrar el registro insertado
			SELECT * FROM Usuario WHERE UsuarioID = @UsuarioID;
		END
	END
    ELSE IF @Accion = 'Actualizar'
	BEGIN
		-- Verificar si el usuario con @UsuarioID existe
		IF NOT EXISTS (SELECT 1 FROM Usuario WHERE UsuarioID = @UsuarioID)
		BEGIN
			-- Si el usuario no existe, generar un error
			RAISERROR('El Usuario con UsuarioID %d no existe en la base de datos.', 16, 1, @UsuarioID);
		END
		ELSE
		BEGIN
			-- Actualizar usuario
			UPDATE Usuario
			SET Cedula = ISNULL(@Cedula, Cedula),
				Nombre = ISNULL(@Nombre, Nombre),
				Telefono = ISNULL(@Telefono, Telefono),
				Rol = ISNULL(@Rol, Rol)
			WHERE UsuarioID = @UsuarioID;

			SELECT * FROM Usuario WHERE UsuarioID = @UsuarioID;
		END
	END
    ELSE IF @Accion = 'Eliminar'
    BEGIN
		-- Verificar si el usuario con @UsuarioID existe
		IF NOT EXISTS (SELECT 1 FROM Usuario WHERE UsuarioID = @UsuarioID)
		BEGIN
			-- Si el usuario no existe, generar un error
			RAISERROR('El Usuario con UsuarioID %d no existe en la base de datos.', 16, 1, @UsuarioID);
		END
		ELSE
		BEGIN
			-- Actualizar usuario
			UPDATE Usuario
			SET Estado = 'I'
			WHERE UsuarioID = @UsuarioID;
		END
    END
	ELSE IF @Accion = 'IniciarSesion'
	BEGIN
		-- Verificar si el usuario y la contraseña son válidos
		DECLARE @UsuarioEncontrado INT;

		SELECT @UsuarioEncontrado = UsuarioID
		FROM Usuario
		WHERE Cedula = @Cedula AND Contraseña = CONVERT(VARBINARY(128), @Contraseña);

		IF @UsuarioEncontrado IS NULL
		BEGIN
			-- Si no se encuentra ningún usuario, generar un error de inicio de sesión
			RAISERROR('Credenciales de inicio de sesión no válidas.', 16, 1);
		END
		ELSE
		BEGIN
			-- Si el usuario es válido, devolver la información de inicio de sesión
			SELECT *
			FROM Usuario
			WHERE UsuarioID = @UsuarioEncontrado;
		END
	END
    ELSE IF @Accion = 'ConsultarPorId'
    BEGIN
		Select * from Usuario WHERE UsuarioID = @UsuarioID;
    END
    ELSE IF @Accion = 'ConsultarPacientePorId'
    BEGIN
		Select * from Usuario WHERE UsuarioID = @UsuarioID AND Rol = @Rol;
    END
END;
GO