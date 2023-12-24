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
    @Contrase�a VARCHAR(128) = NULL,
    @NuevaContrase�a VARCHAR(128) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @Accion = 'Insertar'
    BEGIN
        -- Insertar usuario con contrase�a convertida a varbinary
        INSERT INTO Usuario (Cedula, Nombre, Telefono, Rol, Contrase�a)
        VALUES (@Cedula, @Nombre, @Telefono, @Rol, CONVERT(VARBINARY(128), @Contrase�a));

		-- Obtener el UsuarioID del usuario reci�n insertado
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
        -- Iniciar sesi�n con conversi�n de contrase�a
        SELECT UsuarioID, Nombre, Rol
        FROM Usuario
        WHERE Cedula = @Cedula AND Contrase�a = CONVERT(VARBINARY(128), @Contrase�a);
    END
    ELSE IF @Accion = 'CambiarContrase�a'
    BEGIN
        -- Cambiar contrase�a con conversi�n de contrase�as
        UPDATE Usuario
        SET Contrase�a = CONVERT(VARBINARY(128), @NuevaContrase�a)
        WHERE UsuarioID = @UsuarioID AND Contrase�a = CONVERT(VARBINARY(128), @Contrase�a);
    END
END;
GO


