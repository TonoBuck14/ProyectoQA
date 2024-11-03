use DBProject;

-- Deshabilitar restricciones de claves foráneas
ALTER TABLE Appointment NOCHECK CONSTRAINT ALL;

-- Eliminar todos los registros de la tabla Appointment
DELETE FROM Appointment;

-- Restaurar las restricciones de claves foráneas
ALTER TABLE Appointment CHECK CONSTRAINT ALL;

-- Insertar registros de prueba en la tabla Appointment con diferentes estados de factura
INSERT INTO Appointment (DoctorID, PatientID, Date, Appointment_Status, Bill_Amount, Bill_Status, DoctorNotification, PatientNotification, FeedbackStatus, Disease, Progress, Prescription)
VALUES 
    (3, 12, GETDATE(), 2, 150.00, 1, 2, 1, 0, 'Cold', 'Recovering', 'Take rest and stay hydrated'), 
    (3, 12, GETDATE(), 2, 200.00, 0, 2, 1, 0, 'Flu', 'Stable', 'Continue medication'), 
    (3, 12, GETDATE(), 2, 175.00, 2, 2, 1, 0, 'Infection', 'Improving', 'Antibiotics for 5 days'), 
    (3, 12, GETDATE(), 2, 220.00, 3, 2, 1, 0, 'Headache', 'Relieved', 'Pain reliever as needed');  

-- Confirmar las inserciones
SELECT * FROM Appointment;

-- Crear el procedimiento alternativo RetrieveBillHistoryAlt
CREATE PROCEDURE RetrieveBillHistoryAlt    
    @pID INT,
    @count INT OUTPUT
AS
BEGIN    
    SET NOCOUNT ON;

    -- Consulta para obtener el historial de facturación asociado con el PatientID proporcionado
    SELECT 
        AppointID, DoctorID, PatientID, Date, Appointment_Status, Bill_Amount, 
        Bill_Status, DoctorNotification, PatientNotification, FeedbackStatus, 
        Disease, Progress, Prescription
    INTO #BillHistoryAlt -- Tabla temporal para almacenar los resultados    
    FROM Appointment
    WHERE PatientID = @pID;

    -- Contar el número de registros recuperados    
    SELECT @count = COUNT(*) FROM #BillHistoryAlt;

    -- Devolver los resultados al usuario
    SELECT * FROM #BillHistoryAlt;

    -- Limpiar la tabla temporal    
    DROP TABLE #BillHistoryAlt;
END;
GO

-- Ejecución del procedimiento
DECLARE @count INT;
EXEC RetrieveBillHistoryAlt @pID = 12, @count = @count OUTPUT;
SELECT @count AS 'Count';

-- Inserción de más registros en Appointment
INSERT INTO Appointment (DoctorID, PatientID, Date, Appointment_Status, Bill_Amount, Bill_Status, DoctorNotification, PatientNotification, FeedbackStatus, Disease, Progress, Prescription)
VALUES 
    (3, 12, GETDATE(), 3, 150.00, 1, 2, 1, 0, 'Cold', 'Recovering', 'Take rest and stay hydrated'),
    (3, 12, DATEADD(day, -7, GETDATE()), 3, 200.00, 1, 2, 1, 0, 'Flu', 'Stable', 'Continue medication');
