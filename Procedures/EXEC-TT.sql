USE [CentroOdontologico]
GO

DECLARE @RC int
DECLARE @Accion nvarchar(20)
DECLARE @TipoTratamientoID int
DECLARE @NombreTratamiento varchar(100)
DECLARE @Descripcion varchar(500)
DECLARE @CostoAsociado decimal(10,2)
DECLARE @Estado CHAR(1)

SET @Accion = 'Actualizar'
SET @TipoTratamientoID = 3
SET @NombreTratamiento = null
SET @Descripcion = null
SET @CostoAsociado = 22
SET @Estado = 's'


EXECUTE @RC = [dbo].[GestionarTipoTratamiento] 
   @Accion
  ,@TipoTratamientoID
  ,@NombreTratamiento
  ,@Descripcion
  ,@CostoAsociado
  ,@Estado
GO


