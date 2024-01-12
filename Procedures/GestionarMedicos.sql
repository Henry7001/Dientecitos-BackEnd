USE [CentroOdontologico]
GO

CREATE OR ALTER PROCEDURE [dbo].[GestionarMedicos]
    @Accion NVARCHAR(20),
    @MedicoID INT = NULL,
	@UsuarioID INT = NULL,
    @Especialidad VARCHAR(50) = NULL,
	@Cedula VARCHAR(10) = NULL,
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
				INSERT INTO Medico (UsuarioID, Especialidad)
				VALUES (@UsuarioID, @Especialidad);

				SET @MedicoID = SCOPE_IDENTITY();
				COMMIT;

				-- Devolver objeto
				SELECT * FROM Medico WHERE MedicoID = @MedicoID AND Estado <> 'N';
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
            ELSE IF NOT EXISTS (SELECT 1 FROM Medico WHERE MedicoID = @MedicoID AND Estado <> 'N')
            BEGIN
                -- Devolver mensaje error
                SELECT 'Registro con ID '+ CAST(@MedicoID AS NVARCHAR(10)) +' no existe.' AS Mensaje;
            END
            ELSE
            BEGIN
                -- Realizar accion
                BEGIN TRANSACTION;
                UPDATE Medico
                SET UsuarioID = ISNULL(@UsuarioID, UsuarioID),
					Especialidad = ISNULL(@Especialidad, Especialidad),
                    Estado = ISNULL(UPPER(@Estado), Estado)
                WHERE MedicoID = @MedicoID;

                COMMIT;

                -- Devolver objeto
                SELECT * FROM Medico WHERE MedicoID = @MedicoID AND Estado <> 'N';
            END
        END

	-------------------------------------------------------------------------------------------
        ELSE IF @Accion = 'Eliminar'
        BEGIN
            -- Verificar si existe
            IF NOT EXISTS (SELECT 1 FROM Medico WHERE MedicoID = @MedicoID AND Estado <> 'N')
            BEGIN
                -- Devolver mensaje error
                SELECT 'Registro con ID '+ CAST(@MedicoID AS NVARCHAR(10)) +' no existe.' AS Mensaje;
            END
            ELSE
            BEGIN
                BEGIN TRANSACTION;
                -- Realizar accion
                UPDATE Medico
                SET Estado = 'N'
                WHERE MedicoID = @MedicoID;

                COMMIT;

                -- Devolver mensaje
                SELECT 1 AS Exito, 'Eliminacion exitosa.' AS Mensaje;
            END
        END

	-------------------------------------------------------------------------------------------
        ELSE IF @Accion = 'ConsultarPorID'
        BEGIN
			-- Verificar si NO existe
            IF NOT EXISTS (SELECT 1 FROM Medico WHERE MedicoID = @MedicoID AND Estado <> 'N')
            BEGIN
                -- Devolver mensaje error
                SELECT 'Registro con ID '+ CAST(@MedicoID AS NVARCHAR(10)) +' no existe.' AS Mensaje;
            END
            ELSE
            BEGIN
				-- Realizar accion
				SELECT * FROM Medico 
				WHERE MedicoID = @MedicoID
				AND Estado <> 'N';
			END
        END

	-------------------------------------------------------------------------------------------
        ELSE IF @Accion = 'ConsultarPorCedula'
        BEGIN
			-- Verificar si NO existe
            IF NOT EXISTS (SELECT 1 FROM Medico m INNER JOIN Usuario u 
							ON m.UsuarioID = u.UsuarioID
							WHERE u.Cedula = @Cedula
							AND m.Estado <> 'N' AND u.Estado <> 'N')
            BEGIN
                -- Devolver mensaje error
                SELECT 'Medico con cedula '+ @Cedula +' no existe.' AS Mensaje;
            END
            ELSE
            BEGIN
				-- Realizar accion
				SELECT m.MedicoID, m.UsuarioID, m.Especialidad, m.Estado 
				FROM Medico m INNER JOIN Usuario u 
				ON m.UsuarioID = u.UsuarioID
				WHERE u.Cedula = @Cedula
				AND m.Estado <> 'N' AND u.Estado <> 'N';
			END
        END

	-------------------------------------------------------------------------------------------
        ELSE IF @Accion = 'ConsultarTodos'
        BEGIN
            -- Realizar accion
            SELECT * FROM Medico WHERE Estado <> 'N';
        END

    END TRY
    BEGIN CATCH
        ROLLBACK;
        SELECT 'Ha ocurrido un error en la Base de Datos: ' + ERROR_MESSAGE() AS Mensaje;
    END CATCH
END;
GO
