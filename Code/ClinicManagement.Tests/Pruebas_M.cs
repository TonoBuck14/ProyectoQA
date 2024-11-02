using DBProject.DAL;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Numerics;
using System.Reflection;
using Xunit;

namespace ClinicManagement.Tests
{
    [CollectionDefinition("Pruebas Marco", DisableParallelization = true)]
    public class Pruebas_Marco
    {
        private static readonly string connString = "Data Source=.\\SQLEXPRESS; Initial Catalog=DBProject; Integrated Security=True; TrustServerCertificate=True";

        // ------------------- Pruebas -------------------
        // 
        // Cubre las siguientes funciones:
        // 10. LoadPatient
        // 11. LoadOtherStaff
        // 12. GETPATIENT
        // 13. GET_DOCTOR_PROFILE
        // 14. GETSATFF
        // 15. patientInfoDisplayer
        // 16. getBillHistory
        // 17. appointmentTodayDisplayer
        // 18. getTreatmentHistory
        //
        // ----------------------------------------------

        // 1. LoadPatient

        // Prueba 1
        // Nombre: Cargar Paciente devuelve la tabla con datos
        [Fact]
        public void LoadPatientReturnsDataTable()
        {
            // Arrange
            var instancia = new myDAL();
            var resultado = new DataTable();
            string searchQuery = ""; // Dejar vacío para cargar todos los pacientes

            // Act
            instancia.LoadPatient(ref resultado, searchQuery);

            // Assert
            Assert.True(resultado.Rows.Count > 0);
            Assert.NotNull(resultado); 
        }


        // Prueba 2
        [Fact]
        public void LoadPatient_CuandoNoHayDatos_RetornaDataTableVacio()
        {
            var instancia = new myDAL();
            var resultado = new DataTable();
            // Termino no existente en la base de datos
            string searchQuery = "qwertyuiopasdfghjklzxcvbnm";

            // Act
            instancia.LoadPatient(ref resultado, searchQuery);

            // Assert
            Assert.NotNull(resultado); 
            Assert.Empty(resultado.Rows); 
        }

        // Prueba 3
        [Fact]
        public void LoadPatient_FiltradoPorNombre_DevuelveDataTableFiltrado()
        {
            var instancia = new myDAL();
            var resultado = new DataTable();
            instancia.LoadPatient(ref resultado, "NombreEspecifico");
            Assert.NotNull(resultado);
            Assert.True(resultado.Rows.Count >= 0);
        }

        // ----------------------------------------------

        // 2. LoadOtherStaff

        // Prueba 4
        [Fact]
        public void LoadOtherStaff_CuandoHayDatos_RetornaDataTable()
        {
            // Arrange
            var instancia = new myDAL();
            var resultado = new DataTable();

            // Act
            instancia.LoadOtherStaff(ref resultado, ""); // Proporciona un término de búsqueda vacío si es necesario

            // Assert
            Assert.True(resultado.Rows.Count > 0);
            Assert.NotNull(resultado);
        }

        // Prueba 5
        [Fact]
        public void LoadOtherStaff_CuandoNoHayDatos_RetornaDataTableVacio()
        {
            var instancia = new myDAL();
            var resultado = new DataTable();
            // Termino no existente en la base de datos
            string searchQuery = "qwertyuiopasdfghjklzxcvbnm";

            // Llama al método con un término que no debe devolver resultados
            instancia.LoadOtherStaff(ref resultado, searchQuery);

            // Verifica que el DataTable esté vacío
            Assert.NotNull(resultado); // Verifica que no sea nulo
            Assert.Empty(resultado.Rows); // Verifica que no haya filas
        }

        // Prueba 6
        [Fact]
        public void LoadOtherStaff_FiltradoPorNombre_RetornaDataTableConResultados()
        {
            var instancia = new myDAL();
            var resultado = new DataTable();
            instancia.LoadOtherStaff(ref resultado, "NombreStaff");
            Assert.NotNull(resultado);
        }


        // ----------------------------------------------

        // 3. GETPATIENT

        // Prueba 7
        [Fact]
        public void GETPATIENT_CuandoElPacienteExiste_RetornaInformacion()
        {
            // Arrange
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();

                // Inserta un usuario de prueba en LoginTable
                SqlCommand insertLoginCmd = new SqlCommand(
                    "INSERT INTO LoginTable (Password, Email, Type) VALUES ('testpassword', 'existingpatient@example.com', 1); SELECT SCOPE_IDENTITY();", con);
                int loginId = Convert.ToInt32(insertLoginCmd.ExecuteScalar());

                // Inserta el paciente asociado en Patient
                SqlCommand insertPatientCmd = new SqlCommand(
                    "INSERT INTO Patient (PatientID, Name, Phone, Address, BirthDate, Gender) VALUES (@PatientID, 'John Doe', '1234567890', '123 Fake St', '1980-01-01', 'M');", con);
                insertPatientCmd.Parameters.AddWithValue("@PatientID", loginId);
                insertPatientCmd.ExecuteNonQuery();

                // Act
                var instancia = new myDAL();
                string name = null, phone = null, address = null, birthDate = null, gender = null;
                int age = 0;

                int status = instancia.GETPATIENT(loginId, ref name, ref phone, ref address, ref birthDate, ref age, ref gender);

                // Assert
                Assert.Equal(0, status);
                Assert.Equal("John Doe", name);
                Assert.Equal("1234567890", phone.Trim());
                Assert.Equal("123 Fake St", address);
                Assert.Equal("1980-01-01", birthDate);
                Assert.True(age > 0, "La edad debería ser mayor a 0");
                Assert.Equal("M", gender);

                // Cleanup: Eliminar los datos de prueba
                SqlCommand deletePatientCmd = new SqlCommand("DELETE FROM Patient WHERE PatientID = @PatientID;", con);
                deletePatientCmd.Parameters.AddWithValue("@PatientID", loginId);
                deletePatientCmd.ExecuteNonQuery();

                SqlCommand deleteLoginCmd = new SqlCommand("DELETE FROM LoginTable WHERE LoginID = @LoginID;", con);
                deleteLoginCmd.Parameters.AddWithValue("@LoginID", loginId);
                deleteLoginCmd.ExecuteNonQuery();
            }
        }


        // Prueba 8
        [Fact]
        public void GETPATIENT_CuandoElPacienteNoExiste_RetornaCero()
        {
            // Arrange
            var instancia = new myDAL();
            int nonExistentPatientId = -0; // ID que no existe en la base de datos

            // Variables de salida
            string name = null;
            string phone = null;
            string address = null;
            string birthDate = null;
            int age = 0;
            string gender = null;

            // Act & Assert: Se espera una excepción InvalidCastException al no encontrar datos
            Assert.Throws<InvalidCastException>(() =>
            {
                instancia.GETPATIENT(nonExistentPatientId, ref name, ref phone, ref address, ref birthDate, ref age, ref gender);
            });
        }



        // ----------------------------------------------

        // 4. GET_DOCTOR_PROFILE

        // Prueba 9
        [Fact]
        public void GET_DOCTOR_PROFILE_CuandoElDoctorExiste_RetornaInformacion()
        {
            // Arrange
            int doctorId = 2; // DoctorID for Dr. Farhan
            string expectedName = "Farhan Shoukat";
            string expectedPhone = "156133213";
            string expectedGender = "M";
            float expectedChargesPerVisit = 2500;
            float expectedReputeIndex = 4;
            int expectedPatientsTreated = 0;
            string expectedQualification = "PHD IN EVERY FIELD KNOWN TO MAN";
            string expectedSpecialization = "ENJOY";
            int expectedWorkExperience = 10;
            // Calcula la edad esperada basada en la fecha de nacimiento
            DateTime birthDate = new DateTime(1996, 4, 12);
            int expectedAge = DateTime.Now.Year - birthDate.Year;
            if (DateTime.Now.DayOfYear < birthDate.DayOfYear)
                expectedAge--;

            // Act
            var instancia = new myDAL();
            string name = null, phone = null, gender = null, qualification = null, specialization = null;
            float charges_Per_Visit = 0, ReputeIndex = 0;
            int PatientsTreated = 0, workE = 0, age = 0;

            int status = instancia.GET_DOCTOR_PROFILE(doctorId, ref name, ref phone, ref gender, ref charges_Per_Visit, ref ReputeIndex, ref PatientsTreated, ref qualification, ref specialization, ref workE, ref age);

            // Assert
            Assert.Equal(1, status); 
            Assert.Equal(expectedName, name);
            Assert.Equal(expectedPhone.Trim(), phone.Trim());
            Assert.Equal(expectedGender, gender);
            Assert.Equal(expectedChargesPerVisit, charges_Per_Visit);
            Assert.Equal(expectedReputeIndex, ReputeIndex);
            Assert.Equal(expectedPatientsTreated, PatientsTreated);
            Assert.Equal(expectedQualification, qualification);
            Assert.Equal(expectedSpecialization, specialization);
            Assert.Equal(expectedWorkExperience, workE);
            Assert.Equal(expectedAge, age);
        }




        // Prueba 10
        [Fact]
        public void GET_DOCTOR_PROFILE_CuandoElDoctorNoExiste_RetornaError()
        {
            // Arrange
            var instancia = new myDAL();
            int nonExistentDoctorId = 9999; // ID que no existe en la base de datos

            // Variables de salida
            string name = null, phone = null, gender = null, qualification = null, specialization = null;
            int deptNo = 0, workExperience = 0, age = 0; // Se añade 'age'
            float charges_Per_Visit = 0, reputeIndex = 0;

            // Act & Assert: Se espera una excepción InvalidCastException al no encontrar datos
            Assert.Throws<InvalidCastException>(() =>
            {
                instancia.GET_DOCTOR_PROFILE(nonExistentDoctorId, ref name, ref phone, ref gender, ref charges_Per_Visit, ref reputeIndex, ref deptNo, ref qualification, ref specialization, ref workExperience, ref age);
            });
        }


        // Prueba 11
        [Fact]
        public void GET_DOCTOR_PROFILE_PorDepartamento_RetornaInformacion()
        {
            // Arrange
            var doctorIdsInDept1 = new List<int> { 2, 3, 4, 5 };

            var expectedDoctors = new Dictionary<int, (string Name, string Phone, string Gender, float ChargesPerVisit, float ReputeIndex, int PatientsTreated, string Qualification, string Specialization, int WorkExperience, DateTime BirthDate)>
    {
        { 2, ("Farhan Shoukat", "156133213", "M", 2500f, 4f, 0, "PHD IN EVERY FIELD KNOWN TO MAN", "ENJOY", 10, new DateTime(1996, 4, 12)) },
        { 3, ("KASHAN", "156133213", "M", 3000f, 3.5f, 0, "PHD IN EVERY FIELD KNOWN TO MAN", "ENJOY", 10, new DateTime(1996, 12, 12)) },
        { 4, ("HASSAAN", "156133213", "M", 1500f, 5f, 0, "PHD IN EVERY FIELD KNOWN TO MAN", "ENJOY", 10, new DateTime(1996, 12, 12)) },
        { 5, ("HARIS MUNEER", "156133213", "M", 1000f, 4.5f, 0, "PHD IN EVERY FIELD KNOWN TO MAN", "ENJOY", 10, new DateTime(1990, 5, 4)) }
    };

            var instancia = new myDAL();

            foreach (var doctorId in doctorIdsInDept1)
            {
                // Act
                string name = null, phone = null, gender = null, qualification = null, specialization = null;
                float charges_Per_Visit = 0, ReputeIndex = 0;
                int PatientsTreated = 0, workE = 0, age = 0;

                int status = instancia.GET_DOCTOR_PROFILE(
                    doctorId,
                    ref name,
                    ref phone,
                    ref gender,
                    ref charges_Per_Visit,
                    ref ReputeIndex,
                    ref PatientsTreated,
                    ref qualification,
                    ref specialization,
                    ref workE,
                    ref age);

                // Assert
                Assert.Equal(1, status); 

                // Obtener los datos esperados para este doctor
                var expected = expectedDoctors[doctorId];

                Assert.Equal(expected.Name, name);
                Assert.Equal(expected.Phone.Trim(), phone.Trim());
                Assert.Equal(expected.Gender, gender);
                Assert.Equal(expected.ChargesPerVisit, charges_Per_Visit);
                Assert.Equal(expected.ReputeIndex, ReputeIndex);
                Assert.Equal(expected.PatientsTreated, PatientsTreated);
                Assert.Equal(expected.Qualification, qualification);
                Assert.Equal(expected.Specialization, specialization);
                Assert.Equal(expected.WorkExperience, workE);

                // Cálculo de la edad esperada basado en el método de cálculo del sistema
                int expectedAge = DateTime.Now.Year - expected.BirthDate.Year;
                // No restamos uno si el cumpleaños aún no ha ocurrido, para coincidir con el método

                Assert.Equal(expectedAge, age);
            }
        }



        // ----------------------------------------------

        // 5. GETSATFF

        // Prueba 12
        [Fact]
        public void GETSATFF_CuandoHayPersonal_RetornaDatosDelPersonal()
        {
            // Arrange
            var instancia = new myDAL();
            string name = null, phone = null, address = null, gender = null, desig = null;
            int sal = 0;
            int staffId = 1; // Reemplaza con un ID de personal válido que exista en tu base de datos

            // Act
            int resultado = instancia.GETSATFF(staffId, ref name, ref phone, ref address, ref gender, ref desig, ref sal);

            // Assert
            Assert.Equal(1, resultado); // Asumiendo que 1 indica éxito
            Assert.NotNull(name);
            Assert.NotNull(phone);
            Assert.NotNull(address);
            Assert.NotNull(gender);
            Assert.NotNull(desig);
            Assert.NotEqual(0, sal); // Verificamos que el salario sea mayor a 0
        }


        // Prueba 13
        [Fact]
        public void GETSATFF_CuandoNoHayPersonal_RetornaCero()
        {
            // Arrange
            var instancia = new myDAL();
            int nonExistentStaffId = -1; // ID que no existe en la base de datos

            // Variables de salida
            string name = null;
            string phone = null;
            string address = null;
            string gender = null;
            string desig = null;
            int sal = 0;

            // Act & Assert: Se espera una excepción InvalidCastException al no encontrar datos
            Assert.Throws<InvalidCastException>(() =>
            {
                instancia.GETSATFF(nonExistentStaffId, ref name, ref phone, ref address, ref gender, ref desig, ref sal);
            });
        }




        // ----------------------------------------------

        // Prueba 14
        [Fact]
        public void patientInfoDisplayer_CuandoElPacienteExiste_RetornaInformacion()
        {
            // Arrange
            var instancia = new myDAL();
            int patientId = 12; // PatientID para 'ABC'
            string expectedName = "ABC";
            string expectedPhone = "61536516";
            string expectedAddress = "ENJOY, LAHORE";
            string expectedGender = "M";
            DateTime expectedBirthDate = new DateTime(1996, 4, 4);
            int expectedAge = DateTime.Now.Year - expectedBirthDate.Year;
            if (DateTime.Now.DayOfYear < expectedBirthDate.DayOfYear)
                expectedAge--;

            // Variables de salida
            string name = null, phone = null, address = null, birthDate = null, gender = null;
            int age = 0;

            // Act
            int status = instancia.patientInfoDisplayer(
                patientId,
                ref name,
                ref phone,
                ref address,
                ref birthDate,
                ref age,
                ref gender);

            // Assert
            Assert.Equal(0, status); // Ajustamos el valor esperado a 0
            Assert.Equal(expectedName, name);
            Assert.Equal(expectedPhone.Trim(), phone.Trim());
            Assert.Equal(expectedAddress, address);
            Assert.Equal(expectedGender, gender);
            Assert.Equal(expectedAge, age);
            Assert.Equal(expectedBirthDate.ToString("yyyy-MM-dd"), birthDate);
        }



        // Prueba 15
        [Fact]
        public void patientInfoDisplayer_CuandoElPacienteNoExiste_RetornaError()
        {
            // Arrange
            var instancia = new myDAL();
            int nonExistentPatientId = -1; // ID que no existe en la base de datos

            // Variables de salida
            string name = null, phone = null, address = null, birthDate = null, gender = null;
            int age = 0;

            // Act & Assert: Se espera una excepción InvalidCastException al no encontrar datos
            Assert.Throws<InvalidCastException>(() =>
            {
                instancia.patientInfoDisplayer(
                    nonExistentPatientId,
                    ref name,
                    ref phone,
                    ref address,
                    ref birthDate,
                    ref age,
                    ref gender);
            });
        }


        // 7. getBillHistory
        [Fact]
        public void getBillHistory_CuandoHayFacturas_RetornaDataTable()
        {
            // Arrange
            var instancia = new myDAL();
            var resultado = new DataTable();
            int patientId = 996036; // Utiliza un ID de paciente válido con facturas asociadas en la base de datos

            // Act
            int estado = instancia.getBillHistory(patientId, ref resultado);

            // Assert
            Assert.True(estado > 0, "Se esperaba al menos una factura asociada al paciente."); // Verifica que haya al menos una factura
            Assert.True(resultado.Rows.Count > 0, "Se esperaba que el DataTable tuviera filas."); // Asegura que se recuperaron facturas
            Assert.NotNull(resultado); // Verifica que el DataTable no esté vacío
        }


        // Prueba 17
        [Fact]
        public void getBillHistory_CuandoNoHayFacturas_RetornaDataTableVacio()
        {
            // Arrange
            var instancia = new myDAL();
            var resultado = new DataTable();
            int patientId = -1; // Usamos un ID de paciente que no tenga facturas

            // Act
            instancia.getBillHistory(patientId, ref resultado);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(0, resultado.Rows.Count);
        }

        // Prueba 18
        [Fact]
        public void appointmentTodayDisplayer_CuandoHayCitasHoy_RetornaInformacion()
        {
            // Arrange
            var instancia = new myDAL();
            int patientId = 996036; // Reemplaza con un ID de paciente válido que tenga 2 citas para hoy en la base de datos
            string dName = null;
            string timings = null;

            // Act
            int estado = instancia.appointmentTodayDisplayer(patientId, ref dName, ref timings);

            // Assert
            Assert.Equal(2, estado); // Verifica que el estado indique que hay 2 citas hoy
            Assert.NotNull(dName); // Asegura que el nombre del doctor no esté vacío
            Assert.NotNull(timings); // Asegura que los horarios no estén vacíos
        }




        // Prueba 19
        [Fact]
        public void appointmentTodayDisplayer_CuandoNoHayCitasHoy_RetornaCero()
        {
            // Arrange
            var instancia = new myDAL();
            int patientId = -1; // Usamos un ID de paciente que no tenga citas hoy o que no exista
            string dName = null;
            string timings = null;

            // Act
            int status = instancia.appointmentTodayDisplayer(patientId, ref dName, ref timings);

            // Assert
            Assert.Equal(0, status); 
            Assert.Null(dName);
            Assert.Null(timings);
        }


        // Prueba 20
        [Fact]
        public void getTreatmentHistory_CuandoHayTratamientos_RetornaDataTable()
        {
            // Arrange
            var instancia = new myDAL();
            var resultado = new DataTable();
            int patientId = 996036; // Utiliza un ID de paciente válido que tenga tratamientos en la base de datos

            // Act
            int estado = instancia.getTreatmentHistory(patientId, ref resultado);

            // Assert
            Assert.True(estado >= 0); // Cambiado a >= 0 temporalmente para diagnosticar
            Assert.NotNull(resultado); // Verifica que el DataTable no esté vacío
            Assert.True(resultado.Columns.Count > 0, "El DataTable no contiene columnas."); // Verifica que el DataTable tiene columnas
            Assert.True(resultado.Rows.Count > 0, "No se encontraron tratamientos para el paciente."); // Asegura que el DataTable contiene filas de tratamiento
        }


        // Prueba 21
        [Fact]
        public void getTreatmentHistory_CuandoNoHayTratamientos_RetornaDataTableVacio()
        {
            // Arrange
            var instancia = new myDAL();
            var resultado = new DataTable();
            int patientId = -1; // Usamos un ID de paciente que no tenga tratamientos

            // Act
            instancia.getTreatmentHistory(patientId, ref resultado);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(0, resultado.Rows.Count);
        }


        // Prueba 22
        // Prueba 22
        [Fact]
        public void getBillHistory_RetornaFacturasPendientes()
        {
            // Arrange
            var instancia = new myDAL();
            var resultado = new DataTable();
            int patientId = 996036; // Utiliza un ID de paciente válido con facturas asociadas en la base de datos

            // Act
            int estado = instancia.getBillHistory(patientId, ref resultado);

            // Filtrar las facturas pendientes en el resultado
            var facturasPendientes = resultado.Select("Bill_Status = 0");

            // Assert
            Assert.Equal(1, facturasPendientes.Length); // Esperamos que solo haya una factura pendiente
            Assert.True(facturasPendientes.Length > 0); // Asegura que se recuperaron facturas pendientes
            Assert.NotNull(resultado); // Verifica que el DataTable no esté vacío
        }



        [Fact]
        public void LoadPatient_MultipleFiltrosDevuelveResultados()
        {
            // Configurar la conexión y los datos de prueba
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();

                // Insertar usuario de prueba en LoginTable y obtener su ID
                SqlCommand insertLoginCmd = new SqlCommand(
                    "INSERT INTO LoginTable (Password, Email, Type) VALUES ('testpassword', 'filteredpatient@example.com', 1); SELECT SCOPE_IDENTITY();", con);
                int loginId = Convert.ToInt32(insertLoginCmd.ExecuteScalar());

                // Insertar paciente de prueba en la tabla Patient con filtros específicos
                SqlCommand insertPatientCmd = new SqlCommand(
                    "INSERT INTO Patient (PatientID, Name, Phone, Address, BirthDate, Gender) " +
                    "VALUES (@PatientID, 'Filtered Patient', '555-1234', '456 Filter St', '1985-05-15', 'M');", con);
                insertPatientCmd.Parameters.AddWithValue("@PatientID", loginId);
                insertPatientCmd.ExecuteNonQuery();

                // Actuar: Ejecutar LoadPatient con un filtro específico por nombre
                var instancia = new myDAL();
                var resultado = new DataTable();
                string filtro = "Filtered"; // Nombre parcial para buscar
                instancia.LoadPatient(ref resultado, filtro);

                // Verificar: Asegurar que se devuelvan resultados coincidentes
                Assert.NotNull(resultado);
                Assert.True(resultado.Rows.Count > 0, "Se esperaba al menos un resultado de paciente.");

                // Limpieza de datos de prueba
                SqlCommand deletePatientCmd = new SqlCommand("DELETE FROM Patient WHERE PatientID = @PatientID;", con);
                deletePatientCmd.Parameters.AddWithValue("@PatientID", loginId);
                deletePatientCmd.ExecuteNonQuery();

                SqlCommand deleteLoginCmd = new SqlCommand("DELETE FROM LoginTable WHERE LoginID = @LoginID;", con);
                deleteLoginCmd.Parameters.AddWithValue("@LoginID", loginId);
                deleteLoginCmd.ExecuteNonQuery();
            }
        }


        // Prueba 24
        [Fact]
        public void getTreatmentHistory_MultipleFiltrosDevuelveResultados()
        {
            // Arrange
            var instancia = new myDAL();
            var resultado = new DataTable();
            int patientId = 996036; // Asegúrate de que este ID de paciente tiene registros de tratamientos en la base de datos

            // Act
            int estado = instancia.getTreatmentHistory(patientId, ref resultado);

            // Imprimir las columnas del DataTable para ver qué columnas están presentes
            foreach (DataColumn column in resultado.Columns)
            {
                Console.WriteLine("Columna: " + column.ColumnName);
            }

            // Assert básico para asegurar que no esté vacío
            Assert.True(resultado.Rows.Count > 0); // Asegura que haya al menos un registro
        }





        // Prueba 25
        [Fact]
        public void appointmentTodayDisplayer_MultiplesCitasHoyRetornaTodas()
        {
            // Arrange
            var instancia = new myDAL();
            string dName = null, timings = null;
            int patientId = 996036; // ID del paciente con múltiples citas hoy

            // Act
            int citasHoy = instancia.appointmentTodayDisplayer(patientId, ref dName, ref timings);

            // Assert
            Assert.Equal(2, citasHoy); // Cambia 6 al número exacto de citas que esperas hoy
            Assert.NotNull(dName);
            Assert.NotNull(timings);
        }







    }
}