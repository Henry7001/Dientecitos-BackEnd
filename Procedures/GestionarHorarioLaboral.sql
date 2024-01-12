USE [CentroOdontologico]
GO

CREATE OR ALTER PROCEDURE [dbo].[GestionarHorarioLaboral]
    @Accion NVARCHAR(20),
    @HorarioID INT = NULL,
	@MedicoID INT = NULL,
    @DiaSemana VARCHAR(10) = NULL,
    @HoraInicio Time(7) = NULL,
    @HoraFin Time(7) = NULL,
    @Estado CHAR(1) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

	-------------------------------------------------------------------------------------------
        IF @Accion = 'Insertar'
        BEGIN
			-- Verificar si NO existe FK
            IF @MedicoID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM Medico WHERE MedicoID = @MedicoID AND Estado <> 'N')
            BEGIN
                -- Devolver mensaje error
                SELECT 'Medico con ID '+ CAST(@MedicoID AS NVARCHAR(10)) +' no existe.' AS Mensaje;
            END
            ELSE
            BEGIN
				BEGIN TRANSACTION;
				-- Realizar accion
				INSERT INTO HorarioLaboral (MedicoID, DiaSemana, HoraInicio, HoraFin)
				VALUES (@MedicoID, @DiaSemana, @HoraInicio, @HoraFin);

				SET @HorarioID = SCOPE_IDENTITY();
				COMMIT;

				-- Devolver objeto
				SELECT * FROM HorarioLaboral WHERE HorarioID = @HorarioID AND Estado <> 'N';
			END
        END

	-------------------------------------------------------------------------------------------
        ELSE IF @Accion = 'Actualizar'
        BEGIN
			-- Verificar si NO existe FK
            IF @MedicoID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM Medico WHERE MedicoID = @MedicoID AND Estado <> 'N')
            BEGIN
                -- Devolver mensaje error
                SELECT 'Medico con ID '+ CAST(@MedicoID AS NVARCHAR(10)) +' no existe.' AS Mensaje;
            END
			-- Verificar si NO existe
            ELSE IF NOT EXISTS (SELECT 1 FROM HorarioLaboral WHERE HorarioID = @HorarioID AND Estado <> 'N')
            BEGIN
                -- Devolver mensaje error
                SELECT 'Registro con ID '+ CAST(@HorarioID AS NVARCHAR(10)) +' no existe.' AS Mensaje;
            END
            ELSE
            BEGIN
                -- Realizar accion
                BEGIN TRANSACTION;
                UPDATE HorarioLaboral
                SET MedicoID = ISNULL(@MedicoID, MedicoID),
					DiaSemana = ISNULL(@DiaSemana, DiaSemana),
                    HoraInicio = ISNULL(@HoraInicio, HoraInicio),
                    HoraFin = ISNULL(@HoraFin, HoraFin),
                    Estado = ISNULL(UPPER(@Estado), Estado)
                WHERE HorarioID = @HorarioID;

                COMMIT;

                -- Devolver objeto
                SELECT * FROM HorarioLaboral WHERE HorarioID = @HorarioID AND Estado <> 'N';
            END
        END

	-------------------------------------------------------------------------------------------
        ELSE IF @Accion = 'Eliminar'
        BEGIN
            -- Verificar si existe
            IF NOT EXISTS (SELECT 1 FROM HorarioLaboral WHERE HorarioID = @HorarioID AND Estado <> 'N')
            BEGIN
                -- Devolver mensaje error
                SELECT 'Registro con ID '+ CAST(@HorarioID AS NVARCHAR(10)) +' no existe.' AS Mensaje;
            END
            ELSE
            BEGIN
                BEGIN TRANSACTION;
                -- Realizar accion
                UPDATE HorarioLaboral
                SET Estado = 'N'
                WHERE HorarioID = @HorarioID;

                COMMIT;

                -- Devolver mensaje
                SELECT 1 AS Exito, 'Eliminacion exitosa.' AS Mensaje;
            END
        END

	-------------------------------------------------------------------------------------------
        ELSE IF @Accion = 'ConsultarPorID'
        BEGIN
			-- Verificar si NO existe
            IF NOT EXISTS (SELECT 1 FROM HorarioLaboral WHERE HorarioID = @HorarioID AND Estado <> 'N')
            BEGIN
                -- Devolver mensaje error
                SELECT 'Registro con ID '+ CAST(@HorarioID AS NVARCHAR(10)) +' no existe.' AS Mensaje;
            END
            ELSE
            BEGIN
				-- Realizar accion
				SELECT * FROM HorarioLaboral 
				WHERE HorarioID = @HorarioID
				AND Estado <> 'N';
			END
        END

	-------------------------------------------------------------------------------------------
        ELSE IF @Accion = 'ConsultarPorMedico'
        BEGIN
			-- Verificar si NO existe FK
            IF NOT EXISTS (SELECT 1 FROM Medico WHERE MedicoID = @MedicoID AND Estado <> 'N')
            BEGIN
                -- Devolver mensaje error
                SELECT 'Medico con ID '+ CAST(@MedicoID AS NVARCHAR(10)) +' no existe.' AS Mensaje;
            END
            ELSE
            BEGIN
				-- Realizar accion
				SELECT * FROM HorarioLaboral 
				WHERE MedicoID = @MedicoID
				AND Estado <> 'N';
			END
        END

	-------------------------------------------------------------------------------------------
        ELSE IF @Accion = 'ConsultarTodos'
        BEGIN
            -- Realizar accion
            SELECT * FROM HorarioLaboral WHERE Estado <> 'N';
        END

    END TRY
    BEGIN CATCH
        ROLLBACK;
        SELECT 'Ha ocurrido un error en la Base de Datos: ' + ERROR_MESSAGE() AS Mensaje;
    END CATCH
END;
GO
