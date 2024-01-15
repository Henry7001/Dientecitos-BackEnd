
USE [CentroOdontologico]
GO

CREATE OR ALTER PROCEDURE [dbo].[GestionarCitaMedica]
    @Accion NVARCHAR(20),
    @CitaMedicaID INT = NULL,
    @TipoTratamientoID INT = NULL,
	@MedicoID INT = NULL,
    @PacienteID INT = NULL,
    @FechaHoraCita datetime = NULL,
    @Observaciones varchar(200) = NULL,
	@Diagnostico varchar(200) = NULL,
    @Estado CHAR(10) = NULL
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

			-- Verificar si NO existe FK
            ELSE IF @PacienteID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM Paciente WHERE PacienteID = @PacienteID AND Estado <> 'N')
			BEGIN
                -- Devolver mensaje error
                SELECT 'Paciente con ID '+ CAST(@PacienteID AS NVARCHAR(10)) +' no existe.' AS Mensaje;
            END

			-- Verificar si NO existe FK
            ELSE IF @TipoTratamientoID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM TipoTratamiento WHERE TipoTratamientoID = @TipoTratamientoID AND Estado <> 'N')
			BEGIN
                -- Devolver mensaje error
                SELECT 'Tipo tratamiento con ID '+ CAST(@TipoTratamientoID AS NVARCHAR(10)) +' no existe.' AS Mensaje;
            END

            ELSE
            BEGIN
				BEGIN TRANSACTION;
				-- Realizar accion
				INSERT INTO CitaMedica (TipoTratamientoID, MedicoID, PacienteID, FechaHoraCita, Observaciones, Diagnostico)
				VALUES (@TipoTratamientoID, @MedicoID, @PacienteID, @FechaHoraCita, @Observaciones, @Diagnostico);

				SET @CitaMedicaID = SCOPE_IDENTITY();
				COMMIT;

				-- Devolver objeto
				SELECT * FROM CitaMedica WHERE CitaMedicaID = @CitaMedicaID;
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

			-- Verificar si NO existe FK
            ELSE IF @PacienteID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM Paciente WHERE PacienteID = @PacienteID AND Estado <> 'N')
			BEGIN
                -- Devolver mensaje error
                SELECT 'Paciente con ID '+ CAST(@PacienteID AS NVARCHAR(10)) +' no existe.' AS Mensaje;
            END

			-- Verificar si NO existe FK
            ELSE IF @TipoTratamientoID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM TipoTratamiento WHERE TipoTratamientoID = @TipoTratamientoID AND Estado <> 'N')
			BEGIN
                -- Devolver mensaje error
                SELECT 'Tipo tratamiento con ID '+ CAST(@TipoTratamientoID AS NVARCHAR(10)) +' no existe.' AS Mensaje;
            END

			-- Verificar si NO existe
            ELSE IF NOT EXISTS (SELECT 1 FROM CitaMedica WHERE CitaMedicaID = @CitaMedicaID AND Estado <> 'Cancelada')
            BEGIN
                -- Devolver mensaje error
                SELECT 'Cita médica con ID '+ CAST(@CitaMedicaID AS NVARCHAR(10)) +' no existe o ya está cancelada.' AS Mensaje;
            END

            ELSE
            BEGIN
                -- Realizar accion
                BEGIN TRANSACTION;
                UPDATE CitaMedica
                SET MedicoID = ISNULL(@MedicoID, MedicoID),
					PacienteID = ISNULL(@PacienteID, PacienteID),
					TipoTratamientoID = ISNULL(@TipoTratamientoID, TipoTratamientoID),
                    FechaHoraCita = ISNULL(@FechaHoraCita, FechaHoraCita),
                    Observaciones = ISNULL(@Observaciones, Observaciones),
					Diagnostico = ISNULL(@Diagnostico, Diagnostico),
                    Estado = ISNULL(UPPER(@Estado), Estado)
                WHERE CitaMedicaID = @CitaMedicaID;

                COMMIT;

                -- Devolver objeto
                SELECT * FROM CitaMedica WHERE CitaMedicaID = @CitaMedicaID;
            END
        END

	-------------------------------------------------------------------------------------------
        ELSE IF @Accion = 'Cancelar'
        BEGIN
            -- Verificar si NO existe
            IF NOT EXISTS (SELECT 1 FROM CitaMedica WHERE CitaMedicaID = @CitaMedicaID AND Estado <> 'Cancelada')
            BEGIN
                -- Devolver mensaje error
                SELECT 'Cita médica con ID '+ CAST(@CitaMedicaID AS NVARCHAR(10)) +' no existe o ya está cancelada.' AS Mensaje;
            END
            ELSE
            BEGIN
                BEGIN TRANSACTION;
                -- Realizar accion
                UPDATE CitaMedica
                SET Estado = 'Cancelada'
                WHERE CitaMedicaID = @CitaMedicaID;

                COMMIT;

                -- Devolver mensaje
                SELECT 1 AS Exito, 'Cancelacion exitosa.' AS Mensaje;
            END
        END

	-------------------------------------------------------------------------------------------
        ELSE IF @Accion = 'ConsultarPorID'
        BEGIN
			-- Verificar si NO existe
            IF NOT EXISTS (SELECT 1 FROM CitaMedica WHERE CitaMedicaID = @CitaMedicaID)
            BEGIN
                -- Devolver mensaje error
                SELECT 'Registro con ID '+ CAST(@CitaMedicaID AS NVARCHAR(10)) +' no existe.' AS Mensaje;
            END
            ELSE
            BEGIN
				-- Realizar accion
				SELECT * FROM CitaMedica 
				WHERE CitaMedicaID = @CitaMedicaID;
			END
        END

	-------------------------------------------------------------------------------------------
        ELSE IF @Accion = 'ConsultarTodos'
        BEGIN
			-- Verificar si NO existe FK
            IF @MedicoID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM Medico WHERE MedicoID = @MedicoID AND Estado <> 'N')
            BEGIN
                -- Devolver mensaje error
                SELECT 'Medico con ID '+ CAST(@MedicoID AS NVARCHAR(10)) +' no existe.' AS Mensaje;
            END

			-- Verificar si NO existe FK
            ELSE IF @PacienteID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM Paciente WHERE PacienteID = @PacienteID AND Estado <> 'N')
			BEGIN
                -- Devolver mensaje error
                SELECT 'Paciente con ID '+ CAST(@PacienteID AS NVARCHAR(10)) +' no existe.' AS Mensaje;
            END

			-- Verificar si NO existe FK
            ELSE IF @TipoTratamientoID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM TipoTratamiento WHERE TipoTratamientoID = @TipoTratamientoID AND Estado <> 'N')
			BEGIN
                -- Devolver mensaje error
                SELECT 'Tipo tratamiento con ID '+ CAST(@TipoTratamientoID AS NVARCHAR(10)) +' no existe.' AS Mensaje;
            END

            ELSE
            BEGIN
				-- Realizar accion
				SELECT * FROM CitaMedica
				WHERE (@FechaHoraCita IS NULL OR CONVERT(DATE, FechaHoraCita) = CONVERT(DATE, @FechaHoraCita))
				AND (MedicoID = ISNULL(@MedicoID, MedicoID) OR @MedicoID IS NULL)
				AND (PacienteID = ISNULL(@PacienteID, PacienteID) OR @PacienteID IS NULL)
				AND (TipoTratamientoID = ISNULL(@TipoTratamientoID, TipoTratamientoID) OR @TipoTratamientoID IS NULL)
				AND (Estado = ISNULL(UPPER(@Estado), Estado) OR @Estado IS NULL);
			END
        END

    END TRY
    BEGIN CATCH
        ROLLBACK;
        SELECT 'Ha ocurrido un error en la Base de Datos: ' + ERROR_MESSAGE() AS Mensaje;
    END CATCH
END;
GO
