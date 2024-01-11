USE [CentroOdontologico]
GO

USE [CentroOdontologico]
GO

CREATE OR ALTER PROCEDURE [dbo].[GestionarTipoTratamiento]
    @Accion NVARCHAR(20),
    @TipoTratamientoID INT = NULL,
    @NombreTratamiento VARCHAR(100) = NULL,
    @Descripcion VARCHAR(500) = NULL,
    @CostoAsociado DECIMAL(10, 2) = NULL,
    @Estado CHAR(1) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

	-------------------------------------------------------------------------------------------
        IF @Accion = 'Insertar'
        BEGIN
            BEGIN TRANSACTION;
            -- Realizar accion
            INSERT INTO TipoTratamiento (NombreTratamiento, Descripcion, CostoAsociado)
            VALUES (@NombreTratamiento, @Descripcion, @CostoAsociado);

            SET @TipoTratamientoID = SCOPE_IDENTITY();
            COMMIT;

            -- Devolver objeto
            SELECT * FROM TipoTratamiento WHERE TipoTratamientoID = @TipoTratamientoID;
        END

	-------------------------------------------------------------------------------------------
        ELSE IF @Accion = 'Actualizar'
        BEGIN
            -- Verificar si NO existe
            IF NOT EXISTS (SELECT 1 FROM TipoTratamiento WHERE TipoTratamientoID = @TipoTratamientoID AND Estado <> 'N')
            BEGIN
                -- Devolver mensaje error
                SELECT 'Registro con ID '+ CAST(@TipoTratamientoID AS NVARCHAR(10)) +' no existe.' AS Mensaje;
            END
            ELSE
            BEGIN
                -- Realizar accion
                BEGIN TRANSACTION;
                UPDATE TipoTratamiento
                SET NombreTratamiento = ISNULL(@NombreTratamiento, NombreTratamiento),
                    Descripcion = ISNULL(@Descripcion, Descripcion),
                    CostoAsociado = ISNULL(@CostoAsociado, CostoAsociado),
                    Estado = ISNULL(UPPER(@Estado), Estado)
                WHERE TipoTratamientoID = @TipoTratamientoID;

                COMMIT;

                -- Devolver objeto
                SELECT * FROM TipoTratamiento WHERE TipoTratamientoID = @TipoTratamientoID;
            END
        END

	-------------------------------------------------------------------------------------------
        ELSE IF @Accion = 'Eliminar'
        BEGIN
            -- Verificar si existe
            IF NOT EXISTS (SELECT 1 FROM TipoTratamiento WHERE TipoTratamientoID = @TipoTratamientoID AND Estado <> 'N')
            BEGIN
                -- Devolver mensaje error
                SELECT 'Registro con ID '+ CAST(@TipoTratamientoID AS NVARCHAR(10)) +' no existe.' AS Mensaje;
            END
            ELSE
            BEGIN
                BEGIN TRANSACTION;
                -- Realizar accion
                UPDATE TipoTratamiento
                SET Estado = 'N'
                WHERE TipoTratamientoID = @TipoTratamientoID;

                COMMIT;

                -- Devolver mensaje
                SELECT 1 AS Exito, 'Eliminacion exitosa.' AS Mensaje;
            END
        END

	-------------------------------------------------------------------------------------------
        ELSE IF @Accion = 'ConsultarPorID'
        BEGIN
			-- Verificar si NO existe
            IF NOT EXISTS (SELECT 1 FROM TipoTratamiento WHERE TipoTratamientoID = @TipoTratamientoID AND Estado <> 'N')
            BEGIN
                -- Devolver mensaje error
                SELECT 'Registro con ID '+ CAST(@TipoTratamientoID AS NVARCHAR(10)) +' no existe.' AS Mensaje;
            END
            ELSE
            BEGIN
				-- Realizar accion
				SELECT * FROM TipoTratamiento 
				WHERE TipoTratamientoID = @TipoTratamientoID
				AND Estado <> 'N';
			END
        END

	-------------------------------------------------------------------------------------------
        ELSE IF @Accion = 'ConsultarTodos'
        BEGIN
            -- Realizar accion
            SELECT * FROM TipoTratamiento WHERE Estado <> 'N';
        END

    END TRY
    BEGIN CATCH
        ROLLBACK;
        SELECT 'Ha ocurrido un error en la Base de Datos: ' + ERROR_MESSAGE() AS Mensaje;
    END CATCH
END;
GO
