

-- Crear procedimiento almacenado para visualizar el calendario
CREATE PROCEDURE VisualizarCalendario
	@FechaConsulta DATE
AS
BEGIN
	-- Seleccionar todas las citas programadas para la fecha específica
	SELECT
		cm.CitaMedicaID,
		cm.PacienteID,
		u.Nombre AS NombrePaciente,
		m.MedicoID,
		u.Nombre AS NombreMedico,
		cm.FechaHoraCita,
		cm.Estado
	FROM
		CitaMedica cm
	INNER JOIN
		Paciente p ON cm.PacienteID = p.PacienteID
	INNER JOIN
		Medico m ON cm.MedicoID = m.MedicoID
	INNER JOIN
		Usuario u ON m.UsuarioID = u.UsuarioID
	WHERE
		CONVERT(DATE, cm.FechaHoraCita) = @FechaConsulta;
END;



-- Crear procedimiento almacenado para cancelar una cita médica
CREATE PROCEDURE CancelarCitaMedica
	@CitaMedicaID INT
AS
BEGIN
	-- Actualizar el estado de la cita a "Cancelada"
	UPDATE CitaMedica
	SET Estado = 'Cancelada'
	WHERE CitaMedicaID = @CitaMedicaID;
END;

GO
