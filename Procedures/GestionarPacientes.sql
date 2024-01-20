USE [CentroOdontologico]
GO

CREATE OR ALTER PROCEDURE [dbo].[GestionarPacientes]
    @Accion NVARCHAR(20),
	@PacienteID INT = NULL,
	@UsuarioID INT = NULL,
	@Cedula VARCHAR(10) = NULL,
    @Direccion VARCHAR(100) = NULL,
	@NumeroContacto VARCHAR(15) = NULL,
    @Estado CHAR(1) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

	-------------------------------------------------------------------------------------------
        IF @Accion = 'Insertar'
        BEGIN
			-- Verificar si NO existe FK
            IF @UsuarioID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM Usuario WHERE UsuarioID = @UsuarioID AND Estado <> 'N')
            BEGIN
                -- Devolver mensaje error
                SELECT 'Usuario con ID '+ CAST(@UsuarioID AS NVARCHAR(10)) +' no existe.' AS Mensaje;
            END
            ELSE
            BEGIN
				BEGIN TRANSACTION;
				-- Realizar accion
				INSERT INTO Paciente (UsuarioID, Direccion, NumeroContacto)
				VALUES (@UsuarioID, @Direccion, @NumeroContacto);

				SET @PacienteID = SCOPE_IDENTITY();
				COMMIT;

				-- Devolver objeto
				SELECT * FROM Paciente WHERE PacienteID = @PacienteID AND Estado <> 'N';
			END
        END

	-------------------------------------------------------------------------------------------
        ELSE IF @Accion = 'Actualizar'
        BEGIN
			-- Verificar si NO existe FK
            IF @UsuarioID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM Usuario WHERE UsuarioID = @UsuarioID AND Estado <> 'N')
            BEGIN
                -- Devolver mensaje error
                SELECT 'Usuario con ID '+ CAST(@UsuarioID AS NVARCHAR(10)) +' no existe.' AS Mensaje;
            END
			-- Verificar si NO existe
            ELSE IF NOT EXISTS (SELECT 1 FROM Paciente WHERE PacienteID = @PacienteID AND Estado <> 'N')
            BEGIN
                -- Devolver mensaje error
                SELECT 'Registro con ID '+ CAST(@PacienteID AS NVARCHAR(10)) +' no existe.' AS Mensaje;
            END
            ELSE
            BEGIN
                -- Realizar accion
                BEGIN TRANSACTION;
                UPDATE Paciente
                SET UsuarioID = ISNULL(@UsuarioID, UsuarioID),
					Direccion = ISNULL(@Direccion, Direccion),
					NumeroContacto = ISNULL(@Direccion, Direccion),
                    Estado = ISNULL(UPPER(@Estado), Estado)
                WHERE PacienteID = @PacienteID;

                COMMIT;

                -- Devolver objeto
                SELECT * FROM Paciente WHERE PacienteID = @PacienteID AND Estado <> 'N';
            END
        END

	-------------------------------------------------------------------------------------------
        ELSE IF @Accion = 'Eliminar'
        BEGIN
            -- Verificar si existe
            IF NOT EXISTS (SELECT 1 FROM Paciente WHERE PacienteID = @PacienteID AND Estado <> 'N')
            BEGIN
                -- Devolver mensaje error
                SELECT 'Registro con ID '+ CAST(@PacienteID AS NVARCHAR(10)) +' no existe.' AS Mensaje;
            END
            ELSE
            BEGIN
                BEGIN TRANSACTION;
                -- Realizar accion
                UPDATE Paciente
                SET Estado = 'N'
                WHERE PacienteID = @PacienteID;

                COMMIT;

                -- Devolver mensaje
                SELECT 1 AS Exito, 'Eliminacion exitosa.' AS Mensaje;
            END
        END

	-------------------------------------------------------------------------------------------
        ELSE IF @Accion = 'ConsultarPorID'
        BEGIN
			-- Verificar si NO existe
            IF NOT EXISTS (SELECT 1 FROM Paciente WHERE PacienteID = @PacienteID AND Estado <> 'N')
            BEGIN
                -- Devolver mensaje error
                SELECT 'Registro con ID '+ CAST(@PacienteID AS NVARCHAR(10)) +' no existe.' AS Mensaje;
            END
            ELSE
            BEGIN
				-- Realizar accion
				SELECT * FROM Paciente 
				WHERE PacienteID = @PacienteID
				AND Estado <> 'N';
			END
        END

	-------------------------------------------------------------------------------------------
        ELSE IF @Accion = 'ConsultarPorCedula'
        BEGIN
			-- Verificar si NO existe
            IF NOT EXISTS (SELECT 1 FROM Paciente m INNER JOIN Usuario u 
							ON m.UsuarioID = u.UsuarioID
							WHERE u.Cedula = @Cedula
							AND m.Estado <> 'N' AND u.Estado <> 'N')
            BEGIN
                -- Devolver mensaje error
                SELECT 'Paciente con cedula '+ @Cedula +' no existe.' AS Mensaje;
            END
            ELSE
            BEGIN
				-- Realizar accion
				SELECT m.PacienteID, m.UsuarioID, m.Direccion, m.NumeroContacto, m.Estado 
				FROM Paciente m INNER JOIN Usuario u 
				ON m.UsuarioID = u.UsuarioID
				WHERE u.Cedula = @Cedula
				AND m.Estado <> 'N' AND u.Estado <> 'N';
			END
        END

	-------------------------------------------------------------------------------------------
        ELSE IF @Accion = 'ConsultarTodos'
        BEGIN
            -- Realizar accion
            SELECT * FROM Paciente WHERE Estado <> 'N';
        END

    END TRY
    BEGIN CATCH
        ROLLBACK;
        SELECT 'Ha ocurrido un error en la Base de Datos: ' + ERROR_MESSAGE() AS Mensaje;
    END CATCH
END;
GO
