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

        // ////////////////////////////////////////////////////////
        // Prueba 1
        // ID: 026
        // Nombre: Cargar Paciente devuelve la tabla con datos
        // Descripción: Recibir la tabla de pacientes con datos
        // Datos de prueba: No se requieren datos de prueba
        // Resultado esperado: La tabla contiene datos
        // ////////////////////////////////////////////////////////

        [Fact]
        public void LoadPatient_Retorna_DataTable_Con_Datos()
        {
            // Arrange
            var instancia = new myDAL();
            var resultado = new DataTable();
            // Buscamos cargar a todos los pacientes
            string searchQuery = "";

            // Act
            instancia.LoadPatient(ref resultado, searchQuery);

            // Assert
            Assert.True(resultado.Rows.Count > 0);
            Assert.NotNull(resultado);
        }


        // ////////////////////////////////////////////////////////
        // Prueba 2
        // ID: 027
        // Nombre: Cargar Paciente devuelve la tabla vacía si no hay pacientes
        // Descripción: Recibir la tabla de pacientes vacía para comprobar que la tabla solo recibe datos de pacientes
        // Datos de prueba: {searchQuery = "qwertyuiopasdfghjklzxcvbnm"}
        // Resultado esperado: La tabla está vacía
        // ////////////////////////////////////////////////////////

        [Fact]
        public void LoadPatient_Retorna_DataTable_Vacia_Si_No_Hay_Pacientes()
        {
            // Arrange
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

        // ////////////////////////////////////////////////////////
        // Prueba 3
        // ID: 028
        // Nombre: Cargar Paciente devuelve la tabla filtrada
        // Descripción: Recibir la tabla de pacientes usando un filtro de nomber de un paciente específico
        // Datos de prueba: {searchQuery = "ABC"} 
        // Resultado esperado: La tabla contiene datos filtrados del paciente con nombre 'ABC'
        // ////////////////////////////////////////////////////////

        [Fact]
        public void LoadPatient_FiltradoPorNombre_DevuelveDataTableFiltrado()
        {
            // Arrange
            var instancia = new myDAL();
            var resultado = new DataTable();

            // Act
            // La base datos por defecto incluye un paciente de prueba con nombre 'ABC'
            instancia.LoadPatient(ref resultado, "ABC");

            // Assert
            Assert.NotNull(resultado);
            Assert.True(resultado.Rows.Count > 0);
            // Es necesario comprobar que el nombre del paciente sea 'ABC'
            Assert.Equal("ABC", resultado.Rows[0]["Name"].ToString());
        }

        // ----------------------------------------------

        // 2. LoadOtherStaff

        // ////////////////////////////////////////////////////////
        // Prueba 4
        // ID: 029
        // Nombre: Cargar Otro Personal devuelve la tabla con datos
        // Descripción: Recibir la tabla de otro personal con datos
        // Datos de prueba: No se requieren datos de prueba
        // Resultado esperado: La tabla contiene datos
        // ////////////////////////////////////////////////////////

        [Fact]
        public void LoadOtherStaff_Retorna_DataTable_Con_Datos()
        {
            // Arrange
            var instancia = new myDAL();
            var resultado = new DataTable();

            // Act
            // Buscamos cargar a todo el personal
            instancia.LoadOtherStaff(ref resultado, "");

            // Assert
            Assert.True(resultado.Rows.Count > 0);
            Assert.NotNull(resultado);
        }

        // ////////////////////////////////////////////////////////
        // Prueba 5
        // ID: 030
        // Nombre: Cargar Otro Personal devuelve la tabla vacía si no hay personal
        // Descripción: Recibir la tabla de otro personal vacía para comprobar que la tabla solo recibe datos de otro personal
        // Datos de prueba: {searchQuery = "qwertyuiopasdfghjklzxcvbnm"}
        // Resultado esperado: La tabla está vacía
        // ////////////////////////////////////////////////////////

        [Fact]
        public void LoadOtherStaff_Retorna_DataTable_Vacia_Si_No_Hay_Personal()
        {
            // Arrange
            var instancia = new myDAL();
            var resultado = new DataTable();
            // Termino no existente en la base de datos
            string searchQuery = "qwertyuiopasdfghjklzxcvbnm";

            // Act
            instancia.LoadOtherStaff(ref resultado, searchQuery);

            // Assert
            Assert.NotNull(resultado);
            Assert.Empty(resultado.Rows);
        }

        // ////////////////////////////////////////////////////////
        // Prueba 6
        // ID: 031
        // Nombre: Cargar Otro Personal devuelve la tabla filtrada
        // Descripción: Recibir la tabla de otro personal usando un filtro de nomber de un personal específico
        // Datos de prueba: {searchQuery = "HAMZA"}
        // Resultado esperado: La tabla contiene datos filtrados del personal con nombre 'ALI'
        // ////////////////////////////////////////////////////////

        [Fact]
        public void LoadOtherStaff_Retorna_DataTable_Filtrado()
        {
            // Arrange
            var instancia = new myDAL();
            var resultado = new DataTable();

            // Act
            instancia.LoadOtherStaff(ref resultado, "HAMZA");

            // Assert
            Assert.NotNull(resultado);
            Assert.True(resultado.Rows.Count > 0);
            Assert.Equal("HAMZA", resultado.Rows[0]["Name"].ToString());
        }

        // ----------------------------------------------

        // 3. GETPATIENT

        // ////////////////////////////////////////////////////////
        // Prueba 7
        // ID: 032
        // Nombre: Obtener Paciente devuelve información del paciente
        // Descripción: Recibir información de un paciente específico
        // Datos de prueba: {Password = 'gotam0805', Email = 'damianwayne@enterprisewayne.com, Type = 1}, {Name = 'Damian Wayne', Phone = '9876543210', Address = 'Wayne Manor, Gotham', BirthDate = '2005-08-05', 'M'}
        // Resultado esperado: La información del paciente es correcta
        // ////////////////////////////////////////////////////////

        [Fact]
        public void GETPATIENT_RetornaInformacionDelPaciente()
        {
            // Arrange
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();

                // Inserta un usuario de prueba en LoginTable
                SqlCommand insertLoginCmd = new SqlCommand(
                    "INSERT INTO LoginTable (Password, Email, Type) VALUES ('gotham0805', 'damianwayne@enterprise.com', 1); SELECT SCOPE_IDENTITY();", con);
                int loginId = Convert.ToInt32(insertLoginCmd.ExecuteScalar());

                // Inserta el paciente asociado en Patient
                SqlCommand insertPatientCmd = new SqlCommand(
                    "INSERT INTO Patient (PatientID, Name, Phone, Address, BirthDate, Gender) VALUES (@PatientID, 'Damian Wayne', '9876543210', 'Wayne Manor, Gotham', '2005-08-05', 'M');", con);
                insertPatientCmd.Parameters.AddWithValue("@PatientID", loginId);
                insertPatientCmd.ExecuteNonQuery();

                // Act
                var instancia = new myDAL();
                string name = null, phone = null, address = null, birthDate = null, gender = null;
                int age = 0;

                int status = instancia.GETPATIENT(loginId, ref name, ref phone, ref address, ref birthDate, ref age, ref gender);

                // Assert
                Assert.Equal(0, status);
                Assert.Equal("Damian Wayne", name);
                Assert.Equal("9876543210", phone.Trim());
                Assert.Equal("Wayne Manor, Gotham", address);
                Assert.Equal("2005-08-05", birthDate);
                Assert.True(age > 0, "La edad debería ser mayor a 0");
                Assert.Equal("M", gender);

                // Eliminar los datos de prueba de la tabla Patient
                SqlCommand deletePatientCmd = new SqlCommand("DELETE FROM Patient WHERE PatientID = @PatientID;", con);
                deletePatientCmd.Parameters.AddWithValue("@PatientID", loginId);
                deletePatientCmd.ExecuteNonQuery();
                // Eliminar los datos de prueba de la tabla LoginTable
                SqlCommand deleteLoginCmd = new SqlCommand("DELETE FROM LoginTable WHERE LoginID = @LoginID;", con);
                deleteLoginCmd.Parameters.AddWithValue("@LoginID", loginId);
                deleteLoginCmd.ExecuteNonQuery();
            }
        }


        // ////////////////////////////////////////////////////////
        // Prueba 8
        // ID: 033
        // Nombre: Obtener Paciente devuelve error si el paciente no existe
        // Descripción: Recibir una excepción al intentar obtener información de un paciente que no existe
        // Datos de prueba: {PatientID = -0}
        // Resultado esperado: Se espera una excepción InvalidCastException
        // ////////////////////////////////////////////////////////

        [Fact]
        public void GETPATIENT_Retorna_Error_Si_Paciente_No_Existe()
        {
            // Arrange
            var instancia = new myDAL();
            // Por la configuración de la base de datos, no puede haber un ID de paciente negativo
            int nonExistentPatientId = -0;

            // Variables de salida
            string name = null;
            string phone = null;
            string address = null;
            string birthDate = null;
            int age = 0;
            string gender = null;

            // Act & Assert
            Assert.Throws<InvalidCastException>(() =>
            {
                instancia.GETPATIENT(nonExistentPatientId, ref name, ref phone, ref address, ref birthDate, ref age, ref gender);
            });
        }



        // ----------------------------------------------

        // 4. GET_DOCTOR_PROFILE

        // ////////////////////////////////////////////////////////
        // Prueba 9
        // ID: 034
        // Nombre: Obtener Perfil del Doctor devuelve información del doctor
        // Descripción: Recibir información de un doctor específico
        // Datos de prueba: {DoctorID = 2, Name = 'Farhan Shoukat', Phone = '156133213', 'M', ChargesPerVisit = 2500, ReputeIndex = 4, PatientsTreated = 0, Qualification = 'PHD IN EVERY FIELD',
        //                   ExpectedSpecialization = 'ENJOY', WorkExperience = 10, BirthDate = '1996-04-12'}
        // Resultado esperado: La información del doctor es correcta
        // ////////////////////////////////////////////////////////

        [Fact]
        public void GET_DOCTOR_PROFILE_Retorna_Informacion_Del_Doctor()
        {
            // Arrange
            int doctorId = 2;
            string expectedName = "Farhan Shoukat";
            string expectedPhone = "156133213";
            string expectedGender = "M";
            float expectedChargesPerVisit = 2500;
            float expectedReputeIndex = 4;
            int expectedPatientsTreated = 0;
            string expectedQualification = "PHD IN EVERY FIELD KNOWN TO MAN";
            string expectedSpecialization = "ENJOY";
            int expectedWorkExperience = 10;
            DateTime birthDate = new DateTime(1996, 4, 12);

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
        }

        // ////////////////////////////////////////////////////////
        // Prueba 10
        // ID: 035
        // Nombre: Obtener Perfil del Doctor devuelve error si el doctor no existe
        // Descripción: Recibir una excepción al intentar obtener información de un doctor que no existe
        // Datos de prueba: {DoctorID = -0}
        // Resultado esperado: Se espera una excepción InvalidCastException
        // ////////////////////////////////////////////////////////

        [Fact]
        public void GET_DOCTOR_PROFILE_Retorna_Error_Si_Doctor_No_Existe()
        {
            // Arrange
            var instancia = new myDAL();
            int nonExistentDoctorId = -0;

            // Variables de salida
            string name = null, phone = null, gender = null, qualification = null, specialization = null;
            int deptNo = 0, workExperience = 0, age = 0;
            float charges_Per_Visit = 0, reputeIndex = 0;

            // Act & Assert
            Assert.Throws<InvalidCastException>(() =>
            {
                instancia.GET_DOCTOR_PROFILE(nonExistentDoctorId, ref name, ref phone, ref gender, ref charges_Per_Visit, ref reputeIndex, ref deptNo, ref qualification, ref specialization, ref workExperience, ref age);
            });
        }

        // ////////////////////////////////////////////////////////
        // Prueba 11
        // ID: 036
        // Nombre: Obtener Perfil del Doctor por Departamento devuelve información del doctor
        // Descripción: Recibir información de un doctor específico por departamento
        // Datos de prueba: {DoctorID = 2, Name = 'Farhan Shoukat', Phone = '156133213', 'M', ChargesPerVisit = 2500, ReputeIndex = 4, PatientsTreated = 0, Qualification = 'PHD IN EVERY FIELD', Specialization = 'ENJOY', WorkExperience = 10, BirthDate = '1996-04-12'}
        //                  {DoctorID = 3, Name = 'KASHAN', Phone = '156133213', 'M', ChargesPerVisit = 3000, ReputeIndex = 3.5, PatientsTreated = 0, Qualification = 'PHD IN EVERY FIELD', Specialization = 'ENJOY', WorkExperience = 10, BirthDate = '1996-12-12'}
        //                  {DoctorID = 4, Name = 'HASSAAN', Phone = '156133213', 'M', ChargesPerVisit = 1500, ReputeIndex = 5, PatientsTreated = 0, Qualification = 'PHD IN EVERY FIELD', Specialization = 'ENJOY', WorkExperience = 10, BirthDate = '1996-12-12'}
        //                  {DoctorID = 5, Name = 'HARIS MUNEER', Phone = '156133213', 'M', ChargesPerVisit = 1000, ReputeIndex = 4.5, PatientsTreated = 0, Qualification = 'PHD IN EVERY FIELD', Specialization = 'ENJOY', WorkExperience = 10, BirthDate = '1990-05-04'}
        // Resultado esperado: La información del doctor es correcta
        // ////////////////////////////////////////////////////////

        [Fact]
        public void GET_DOCTOR_PROFILE_Retorna_Informacion_Del_Doctor_Por_Departamento()
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
            }
        }

        // ----------------------------------------------

        // 5. GETSATFF

        // ////////////////////////////////////////////////////////
        // Prueba 12
        // ID: 037
        // Nombre: Obtener Personal devuelve información del personal
        // Descripción: Recibir información de un personal específico
        // Datos de prueba: {StaffID = 2, Name = 'Hamza'}
        // Resultado esperado: La información del personal es correcta
        // ////////////////////////////////////////////////////////

        [Fact]
        public void GETSATFF_RetornaInformacionDelPersonal()
        {
            // Arrange
            var instancia = new myDAL();
            string name = null, phone = null, address = null, gender = null, desig = null;
            int sal = 0;
            int staffId = 2;

            // Act
            int resultado = instancia.GETSATFF(staffId, ref name, ref phone, ref address, ref gender, ref desig, ref sal);

            // Assert
            Assert.Equal(1, resultado);
            Assert.Equal("HAMZA", name);
            Assert.NotNull(phone);
            Assert.NotNull(address);
            Assert.NotNull(gender);
            Assert.Equal("SWEEPER", desig);
            Assert.NotEqual(0, sal);
        }

        // ////////////////////////////////////////////////////////
        // Prueba 13
        // ID: 038
        // Nombre: Obtener Personal devuelve error si el personal no existe
        // Descripción: Recibir una excepción al intentar obtener información de un personal que no existe
        // Datos de prueba: {StaffID = -0}
        // Resultado esperado: Se espera una excepción InvalidCastException
        // ////////////////////////////////////////////////////////

        [Fact]
        public void GETSATFF_Retorna_Error_Si_Personal_No_Existe()
        {
            // Arrange
            var instancia = new myDAL();
            // Por la configuración de la base de datos, no puede haber un ID de personal negativo
            int nonExistentStaffId = -0;
            string name = null;
            string phone = null;
            string address = null;
            string gender = null;
            string desig = null;
            int sal = 0;

            // Act & Assert
            Assert.Throws<InvalidCastException>(() =>
            {
                instancia.GETSATFF(nonExistentStaffId, ref name, ref phone, ref address, ref gender, ref desig, ref sal);
            });
        }

        // ----------------------------------------------

        // 6. patientInfoDisplayer

        // ////////////////////////////////////////////////////////
        // Prueba 14
        // ID: 039
        // Nombre: Desplegar Información del Paciente devuelve información del paciente correcto
        // Descripción: Desplegar la información de un paciente específico
        // Datos de prueba: {PatientID = 12, Name = 'ABC', Phone = '61536516', Address = 'ENJOY, LAHORE", Gender = 'M', BirthDate = '1996-04-04'}
        // Resultado esperado: La información del paciente desplegado es correcta
        // ////////////////////////////////////////////////////////

        [Fact]
        public void patientInfoDisplayer_Despliega_Informacion_Del_Paciente_Correcto()
        {
            // Arrange
            var instancia = new myDAL();
            int patientId = 12;
            string expectedName = "ABC";
            string expectedPhone = "61536516";
            string expectedAddress = "ENJOY, LAHORE";
            string expectedGender = "M";
            DateTime expectedBirthDate = new DateTime(1996, 4, 4);
            int expectedAge = DateTime.Now.Year - expectedBirthDate.Year;

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
            Assert.Equal(0, status);
            Assert.Equal(expectedName, name);
            Assert.Equal(expectedPhone.Trim(), phone.Trim());
            Assert.Equal(expectedAddress, address);
            Assert.Equal(expectedGender, gender);
        }

        // ////////////////////////////////////////////////////////
        // Prueba 15
        // ID: 040
        // Nombre: Desplegar Información del Paciente devuelve error si el paciente no existe
        // Descripción: Recibir una excepción al intentar desplegar información de un paciente que no existe
        // Datos de prueba: {PatientID = -0}
        // Resultado esperado: Se espera una excepción InvalidCastException
        // ////////////////////////////////////////////////////////

        [Fact]
        public void patientInfoDisplayer_Despliega_Error_Si_Paciente_No_Existe()
        {
            // Arrange
            var instancia = new myDAL();
            // Por la configuración de la base de datos, no puede haber un ID de paciente negativo
            int nonExistentPatientId = -1;

            string name = null, phone = null, address = null, birthDate = null, gender = null;
            int age = 0;

            // Act & Assert
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

        // ----------------------------------------------
        // 7. getBillHistory

        // ////////////////////////////////////////////////////////
        // 7. getBillHistory
        // ID: 041
        // Nombre: Obtener Historial de Facturas devuelve facturas asociadas al paciente
        // Descripción: Recibir facturas asociadas a un paciente específico
        // Datos de prueba: {PatientID = 996036}
        // Resultado esperado: Se espera que se devuelvan facturas asociadas al paciente
        // ////////////////////////////////////////////////////////

        [Fact]
        public void getBillHistory_Retorna_Facturas_Asociadas_Al_Paciente()
        {
            // Arrange
            var instancia = new myDAL();
            var resultado = new DataTable();
            int patientId = 12;

            // Act
            int estado = instancia.getBillHistory(patientId, ref resultado);

            // Assert
            Assert.True(estado > 0);
            Assert.True(resultado.Rows.Count > 0);
            Assert.NotNull(resultado);
        }

        // ////////////////////////////////////////////////////////
        // Prueba 17
        // ID: 042
        // Nombre: Obtener Historial de Facturas devuelve error si el paciente no tiene facturas
        // Descripción: Recibir una excepción al intentar obtener facturas de un paciente que no tiene facturas
        // Datos de prueba: {PatientID = -0}
        // Resultado esperado: Se espera una excepción InvalidCastException
        // ////////////////////////////////////////////////////////

        [Fact]
        public void getBillHistory_Retorna_Error_Si_Paciente_No_Tiene_Facturas()
        {
            // Arrange
            var instancia = new myDAL();
            var resultado = new DataTable();
            // Por la configuración de la base de datos, no puede haber un ID de paciente negativo
            int patientId = -0;

            // Act
            instancia.getBillHistory(patientId, ref resultado);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(0, resultado.Rows.Count);
        }

        // ----------------------------------------------
        // 8. appointmentTodayDisplayer

        // ////////////////////////////////////////////////////////
        // Prueba 18
        // ID: 043
        // Nombre: Obtener Citas de Hoy devuelve citas de hoy de un paciente
        // Descripción: Recibir citas de hoy de un paciente específico
        // Datos de prueba: {PatientID = 996036}
        // Resultado esperado: Se espera que se devuelvan citas de hoy de un paciente
        // ////////////////////////////////////////////////////////

        [Fact]
        public void appointmentTodayDisplayer_Retorna_Las_Citas_De_Hoy_De_Un_Paciente()
        {
            // Arrange
            var instancia = new myDAL();
            // Este paciente esta programado por defecto en la base de datos para tener 2 citas diarias
            int patientId = 12;
            string dName = null;
            string timings = null;

            // Act
            int estado = instancia.appointmentTodayDisplayer(patientId, ref dName, ref timings);

            // Assert
            // Revisa que el paciente de prueba efectivamente tenga 2 citas hoy
            Assert.Equal(2, estado);
            Assert.NotNull(dName);
            Assert.NotNull(timings);
        }

        // ////////////////////////////////////////////////////////
        // Prueba 19
        // ID: 044
        // Nombre: Obtener Citas de Hoy devuelve error si el paciente no tiene citas hoy
        // Descripción: Recibir una excepción al intentar obtener citas de hoy de un paciente que no tiene citas hoy
        // Datos de prueba: {PatientID = -0}
        // Resultado esperado: Se espera una excepción InvalidCastException
        // ////////////////////////////////////////////////////////

        [Fact]
        public void appointmentTodayDisplayer_Retorna_Error_Si_Paciente_No_Tiene_Citas_Hoy()
        {
            // Arrange
            var instancia = new myDAL();
            // Por la configuración de la base de datos, no puede haber un ID de paciente negativo
            int patientId = -0;
            string dName = null;
            string timings = null;

            // Act
            int status = instancia.appointmentTodayDisplayer(patientId, ref dName, ref timings);

            // Assert
            Assert.Equal(0, status);
            Assert.Null(dName);
            Assert.Null(timings);
        }

        // ----------------------------------------------
        // 9. getTreatmentHistory

        // ////////////////////////////////////////////////////////
        // Prueba 20
        // ID: 045
        // Nombre: Obtener Historial de Tratamientos devuelve tratamientos asociados al paciente
        // Descripción: Recibir tratamientos asociados a un paciente específico
        // Datos de prueba: {PatientID = 996036}
        // Resultado esperado: Se espera que se devuelvan tratamientos asociados al paciente
        // ////////////////////////////////////////////////////////

        [Fact]
        public void getTreatmentHistory_Retorna_Tratamientos_Asociados_Al_Paciente()
        {
            // Arrange
            var instancia = new myDAL();
            var resultado = new DataTable();
            int patientId = 12;

            // Act
            int estado = instancia.getTreatmentHistory(patientId, ref resultado);

            // Assert
            Assert.True(estado >= 0);
            Assert.NotNull(resultado);
            Assert.True(resultado.Columns.Count > 0);
            Assert.True(resultado.Rows.Count > 0);
        }

        // ////////////////////////////////////////////////////////
        // Prueba 21
        // ID: 046
        // Nombre: Obtener Historial de Tratamientos devuelve DataTable vacío si el paciente no tiene tratamientos
        // Descripción: Recibir un DataTable vacío al intentar obtener tratamientos de un paciente que no tiene tratamientos
        // Datos de prueba: {PatientID = -0}
        // Resultado esperado: Se espera un DataTable vacío
        // ////////////////////////////////////////////////////////

        [Fact]
        public void getTreatmentHistory_Retorna_DataTable_Vacio_Si_Paciente_No_Tiene_Tratamientos()
        {
            // Arrange
            var instancia = new myDAL();
            var resultado = new DataTable();
            int patientId = -0;

            // Act
            instancia.getTreatmentHistory(patientId, ref resultado);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(0, resultado.Rows.Count);
        }

        // ----------------------------------------------
        // Adicionales

        // ////////////////////////////////////////////////////////
        // Prueba 22
        // ID: 047
        // Nombre: Obtener Historial de Facturas devuelve facturas pendientes
        // Descripción: Recibir facturas pendientes asociadas a un paciente específico
        // Datos de prueba: {PatientID = 996036}
        // Resultado esperado: Se espera que se devuelvan facturas pendientes asociadas al paciente
        // ////////////////////////////////////////////////////////

        [Fact]
        public void getBillHistory_RetornaFacturasPendientes()
        {
            // Arrange
            var instancia = new myDAL();
            var resultado = new DataTable();
            int patientId = 12;

            // Act
            int estado = instancia.getBillHistory(patientId, ref resultado);
            var facturasPendientes = resultado.Select("Bill_Status = 0");

            // Assert
            Assert.Equal(1, facturasPendientes.Length);
            Assert.True(facturasPendientes.Length > 0);
            Assert.NotNull(resultado);
        }

        // ////////////////////////////////////////////////////////`
        // Prueba 23
        // ID: 048
        // Nombre: Cargar paciente usando múltiples filtros
        // Descripción: Se carga un paciente usando múltiples filtros, desde el nombre hasta la dirección
        // Datos de prueba: {Nombre = 'Filtered Patient', Phone = '555-1234', Address = '456 Filter St', BirthDate = '1985-05-15', 'M'}
        // Resultado esperado: Se espera que se devuelvan resultados coincidentes
        // ////////////////////////////////////////////////////////

        [Fact]
        public void LoadPatient_MultipleFiltrosDevuelveResultados()
        {
            // Arrange
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();

                SqlCommand insertLoginCmd = new SqlCommand(
                    "INSERT INTO LoginTable (Password, Email, Type) VALUES ('barry1234', 'barryallen@flashgod.com', 1); SELECT SCOPE_IDENTITY();", con);
                int loginId = Convert.ToInt32(insertLoginCmd.ExecuteScalar());

                SqlCommand insertPatientCmd = new SqlCommand(
                    "INSERT INTO Patient (PatientID, Name, Phone, Address, BirthDate, Gender) " +
                    "VALUES (@PatientID, 'Barry Allen', '555-9876', 'Speed Force Lane', '1985-05-15', 'M');", con);
                insertPatientCmd.Parameters.AddWithValue("@PatientID", loginId);
                insertPatientCmd.ExecuteNonQuery();

                var instancia = new myDAL();
                var resultado = new DataTable();
                string filtro = "Barry";

                // Act
                instancia.LoadPatient(ref resultado, filtro);

                // Assert
                Assert.NotNull(resultado);
                Assert.True(resultado.Rows.Count > 0);

                var row = resultado.Rows[0];
                Assert.Equal("Barry Allen", row["Name"].ToString());
                Assert.Equal("555-9876", row["Phone"].ToString().Trim());

                SqlCommand deletePatientCmd = new SqlCommand("DELETE FROM Patient WHERE PatientID = @PatientID;", con);
                deletePatientCmd.Parameters.AddWithValue("@PatientID", loginId);
                deletePatientCmd.ExecuteNonQuery();

                SqlCommand deleteLoginCmd = new SqlCommand("DELETE FROM LoginTable WHERE LoginID = @LoginID;", con);
                deleteLoginCmd.Parameters.AddWithValue("@LoginID", loginId);
                deleteLoginCmd.ExecuteNonQuery();
            }
        }

        // ////////////////////////////////////////////////////////
        // Prueba 24
        // ID: 049
        // Nombre: Obtener historial de facturas devuelve facturas pagadas
        // Descripción: Recibir facturas pagadas asociadas a un paciente específico
        // Datos de prueba: {PatientID = 996036}
        // Resultado esperado: Se espera que se devuelvan facturas pagadas asociadas al paciente
        // ////////////////////////////////////////////////////////

        [Fact]
        public void getBillHistory_Retorna_Facturas_Pagadas()
        {
            // Arrange
            var instancia = new myDAL();
            var resultado = new DataTable();
            int patientId = 12; // ID del paciente para el cual se buscan las facturas

            // Act
            int estado = instancia.getBillHistory(patientId, ref resultado); // Asume que este método obtiene las facturas

            // Assert
            Assert.NotNull(resultado);
            Assert.True(resultado.Rows.Count > 0);
            Assert.Contains(resultado.AsEnumerable(), row => Convert.ToInt32(row["Bill_Status"]) == 1);
        }


        // ////////////////////////////////////////////////////////
        // Prueba 25
        // ID: 050
        // Nombre: Obtener un staff usando múltiples filtros
        // Descripción: Se obtiene un miembro del personal usando múltiples filtros, desde el nombre, cargo, cualificación y salario
        // Datos de prueba: {StaffID = 5, Name = 'KALEEM', Designation = 'GUARD', Highest_Qualification = 'MATRIC', Salary = 5000}
        // Resultado esperado: Se espera que se devuelvan resultados coincidentes del miembro del personal
        // ////////////////////////////////////////////////////////

        [Fact]
        public void GET_SATFF_Retorna_Personal_Con_Multiples_Filtros()
        {
            // Arrange
            var instancia = new myDAL();
            string name = "Kaleem", phone = null, address = null, gender = null, desig = "GUARD";
            int sal = 5000;
            int staffId = 5; 

            // Act
            int resultado = instancia.GETSATFF(staffId, ref name, ref phone, ref address, ref gender, ref desig, ref sal);

            // Assert
            Assert.Equal(1, resultado); 
            Assert.Equal("KALEEM", name); 
            Assert.Equal("GUARD", desig); 
            Assert.Equal(5000, sal);
        }

    }


    }