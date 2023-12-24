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
    @NuevaContraseña VARCHAR(128) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @Accion = 'Insertar'
    BEGIN
        -- Insertar usuario con contraseña convertida a varbinary
        INSERT INTO Usuario (Cedula, Nombre, Telefono, Rol, Contraseña)
        VALUES (@Cedula, @Nombre, @Telefono, @Rol, CONVERT(VARBINARY(128), @Contraseña));

		-- Obtener el UsuarioID del usuario recién insertado
		SET @UsuarioID = SCOPE_IDENTITY();

        -- Mostrar el registro insertado
		SELECT * FROM Usuario WHERE UsuarioID = @UsuarioID;
    END
    ELSE IF @Accion = 'Actualizar'
    BEGIN
        -- Actualizar usuario
        UPDATE Usuario
        SET Cedula = ISNULL(@Cedula, Cedula),
            Nombre = ISNULL(@Nombre, Nombre),
            Telefono = ISNULL(@Telefono, Telefono),
            Rol = ISNULL(@Rol, Rol)
        WHERE UsuarioID = @UsuarioID;
    END
    ELSE IF @Accion = 'Eliminar'
    BEGIN
        -- Eliminar usuario
        DELETE FROM Usuario
        WHERE UsuarioID = @UsuarioID;
    END
    ELSE IF @Accion = 'IniciarSesion'
    BEGIN
        -- Iniciar sesión con conversión de contraseña
        SELECT UsuarioID, Nombre, Rol
        FROM Usuario
        WHERE Cedula = @Cedula AND Contraseña = CONVERT(VARBINARY(128), @Contraseña);
    END
    ELSE IF @Accion = 'CambiarContraseña'
    BEGIN
        -- Cambiar contraseña con conversión de contraseñas
        UPDATE Usuario
        SET Contraseña = CONVERT(VARBINARY(128), @NuevaContraseña)
        WHERE UsuarioID = @UsuarioID AND Contraseña = CONVERT(VARBINARY(128), @Contraseña);
    END
END;
GO


