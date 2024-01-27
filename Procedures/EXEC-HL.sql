USE [CentroOdontologico]
GO

DECLARE @RC int
DECLARE @Accion nvarchar(20)
DECLARE @MedicoID int
DECLARE @HorarioID int
DECLARE @DiaSemana varchar(10)
DECLARE @HoraInicio Time(7)
DECLARE @HoraFin Time(7)
DECLARE @Estado CHAR(1)

SET @Accion = 'Actualizar'
SET @HorarioID = 3
SET @MedicoID = null
SET @DiaSemana = 'Miércoles'
SET @HoraInicio = null
SET @HoraFin = null
SET @Estado = 'I'


EXECUTE @RC = [dbo].[GestionarHorarioLaboral] 
   @Accion
  ,@HorarioID
  ,@MedicoID
  ,@DiaSemana
  ,@HoraInicio
  ,@HoraFin
  ,@Estado
GO
