USE [CentroOdontologico]
GO

DECLARE @RC int
DECLARE @Accion nvarchar(20)
DECLARE @MedicoID int
DECLARE @UsuarioID int
DECLARE @Especialidad varchar(50)
DECLARE @Estado CHAR(1)

SET @Accion = 'Actualizar'
SET @MedicoID = 2
SET @UsuarioID = null
SET @Especialidad = null
SET @Estado = 'I'


EXECUTE @RC = [dbo].[GestionarMedicos] 
   @Accion
  ,@MedicoID
  ,@UsuarioID
  ,@Especialidad
  ,@Estado
GO


