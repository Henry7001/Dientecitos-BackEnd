USE [CentroOdontologico]
GO

DECLARE @RC int
DECLARE @Accion nvarchar(20)
DECLARE @CitaMedicaID INT
DECLARE @TipoTratamientoID int
DECLARE @MedicoID int
DECLARE @PacienteID int
DECLARE @FechaHoraCita DATETIME
DECLARE @Observaciones VARCHAR(200)
DECLARE @Diagnostico VARCHAR(200)
DECLARE @Estado CHAR(10)

SET @Accion = 'ConsultarTodos'
SET @CitaMedicaID = 99;
SET @TipoTratamientoID = 2;
SET @PacienteID = null
SET @MedicoID = null
SET @FechaHoraCita = null
SET @Observaciones = null
SET @Diagnostico = null
SET @Estado = 'bc'

-- Corregir la forma de ejecutar el procedimiento almacenado
EXECUTE @RC = [dbo].[GestionarCitaMedica]
   @Accion = @Accion,
   @CitaMedicaID = @CitaMedicaID,
   @TipoTratamientoID = @TipoTratamientoID,
   @PacienteID = @PacienteID,
   @MedicoID = @MedicoID,
   @FechaHoraCita = @FechaHoraCita,
   @Observaciones = @Observaciones,
   @Diagnostico = @Diagnostico,
   @Estado = @Estado
GO
