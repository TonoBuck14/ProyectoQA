using DBProject.DAL;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using Xunit.Sdk;

namespace ClinicManagement.Tests
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    [assembly: CollectionBehavior(DisableTestParallelization = true)]
    public class TestBeforeAfter : BeforeAfterTestAttribute
    {

        private static readonly string connString = "Data Source=.\\SQLEXPRESS; Initial Catalog=DBProject; Integrated Security=True; TrustServerCertificate=True";

        private int? doctorId;
        private int? patientId;
        private int? citaId;
        private int? citaDoctorId;
        private int? citaStatus;

        private int? departmentId1;
        private int? departmentId2;

        public TestBeforeAfter(int doctorId = -1, int patientId = -1, int citaDoctorId = -1, int citaId = -1, int citaStatus = -1)
        {
            this.doctorId = doctorId == -1 ? null : doctorId;
            this.patientId = patientId == -1 ? null : patientId;
            this.citaId = citaId == -1 ? null : citaId;
            this.citaDoctorId = citaDoctorId == -1 ? null : citaDoctorId;
            this.citaStatus = citaStatus == -1 ? null : citaStatus;
            if (this.citaId != null && this.citaDoctorId == null) throw new Exception("Each appointment must have a doctor.");
            if (this.citaId != null && this.patientId == null) throw new Exception("Each appointment must have a patient.");
            if (doctorId != -1) departmentId1 = 10 * doctorId;
            if (citaDoctorId != -1) departmentId2 = 10 * citaDoctorId;
        }

        public override void Before(MethodInfo methodUnderTest)
        {
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            if (doctorId != null)
            {
                SqlCommand cmd10;
                try
                {
                    cmd10 = new SqlCommand($"DELETE FROM [DBProject].[dbo].[Doctor] WHERE DoctorID = {doctorId};", con)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd10.ExecuteNonQuery();
                }
                catch (SqlException) { }
                try
                {
                    cmd10 = new SqlCommand($"DELETE FROM [DBProject].[dbo].[Department] WHERE DeptNo = {departmentId1};", con)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd10.ExecuteNonQuery();
                }
                catch (SqlException) { }
                SqlCommand cmd1;
                cmd1 = new SqlCommand($"INSERT INTO[DBProject].[dbo].[Department]([DeptNo], [DeptName], [Description]) VALUES({departmentId1}, 'Dept{departmentId1}', 'D99');", con)
                {
                    CommandType = CommandType.Text
                };
                cmd1.ExecuteNonQuery();
                SqlCommand cmd2;
                cmd2 = new SqlCommand("INSERT INTO [DBProject].[dbo].[DOCTOR] (DoctorID, Name, Phone, Address," +
                    " BirthDate, Gender, DeptNo, Charges_Per_Visit, MonthlySalary, ReputeIndex, Patients_Treated," +
                    $" Qualification, Specialization, Work_Experience, status) VALUES ({doctorId}, 'Doctor1', '12345678901', 'Costa Rica', '01-01-1990', 'M', {departmentId1}, " +
                    "100.0, 5000.0, 4.5, 0, 'Master', 'Cardiology', 10, 1) ;", con)
                {
                    CommandType = CommandType.Text
                };
                cmd2.ExecuteNonQuery();
            }
            if (citaDoctorId != null && citaDoctorId != doctorId)
            {
                SqlCommand cmd10;
                try
                {
                    cmd10 = new SqlCommand($"DELETE FROM [DBProject].[dbo].[Doctor] WHERE DoctorID = {citaDoctorId};", con)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd10.ExecuteNonQuery();
                }
                catch (SqlException)
                {

                }
                try
                {
                    cmd10 = new SqlCommand($"DELETE FROM [DBProject].[dbo].[Department] WHERE DeptNo = {departmentId2};", con)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd10.ExecuteNonQuery();
                }
                catch (SqlException)
                {

                }
                SqlCommand cmd1;
                cmd1 = new SqlCommand($"INSERT INTO[DBProject].[dbo].[Department]([DeptNo], [DeptName], [Description]) VALUES({departmentId2}, 'Dept{departmentId2}', 'D99');", con)
                {
                    CommandType = CommandType.Text
                };
                cmd1.ExecuteNonQuery();
                SqlCommand cmd2;
                cmd2 = new SqlCommand("INSERT INTO [DBProject].[dbo].[DOCTOR] (DoctorID, Name, Phone, Address," +
                    " BirthDate, Gender, DeptNo, Charges_Per_Visit, MonthlySalary, ReputeIndex, Patients_Treated," +
                    $" Qualification, Specialization, Work_Experience, status) VALUES ({citaDoctorId}, 'Doctor2', '12345678901', 'Costa Rica', '01-01-1990', 'M', {departmentId2}, " +
                    $"100.0, 5000.0, 4.5, 0, 'Master', 'Cardiology', 10, 1) ;", con)
                {
                    CommandType = CommandType.Text
                };
                cmd2.ExecuteNonQuery();
            }
            if (patientId != null)
            {
                SqlCommand cmd10;
                try
                {
                    cmd10 = new SqlCommand($"DELETE FROM [DBProject].[dbo].[Patient] WHERE PatientID = {patientId};", con)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd10.ExecuteNonQuery();
                }
                catch (SqlException)
                {

                }
                try
                {
                    cmd10 = new SqlCommand($"DELETE FROM [DBProject].[dbo].[LoginTable] WHERE LoginID = {patientId};", con)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd10.ExecuteNonQuery();
                }
                catch (SqlException)
                {

                }
                SqlCommand cmd0;
                cmd0 = new SqlCommand("SET IDENTITY_INSERT LoginTable ON;", con)
                {
                    CommandType = CommandType.Text
                };
                cmd0.ExecuteNonQuery();
                SqlCommand cmd1;
                cmd1 = new SqlCommand($"INSERT INTO[DBProject].[dbo].[LoginTable]([LoginID], [Password], [Email], [Type]) VALUES({patientId}, 'Patient1', 'PatientFull1{patientId}@gmail.com', 1);", con)
                {
                    CommandType = CommandType.Text
                };
                cmd1.ExecuteNonQuery();
                cmd0 = new SqlCommand("SET IDENTITY_INSERT LoginTable OFF;", con)
                {
                    CommandType = CommandType.Text
                };
                cmd0.ExecuteNonQuery();
                cmd1 = new SqlCommand($"insert into Patient(PatientID,Name,Phone,Address,BirthDate,Gender) values ({patientId}, 'Chaximilian', '777','f','1990-05-09','M');", con)
                {
                    CommandType = CommandType.Text
                };
                cmd1.ExecuteNonQuery();
            }
            if (citaId != null)
            {
                try
                {
                    SqlCommand cmd4;
                    cmd4 = new SqlCommand($"delete from Appointment where AppointId = {citaId};", con)
                    {
                        CommandType = CommandType.Text
                    };
                    cmd4.ExecuteNonQuery();
                }
                catch (SqlException)
                {

                }
                SqlCommand cmd0;
                cmd0 = new SqlCommand("SET IDENTITY_INSERT Appointment ON;", con)
                {
                    CommandType = CommandType.Text
                };
                cmd0.ExecuteNonQuery();
                SqlCommand cmd3;
                if (citaStatus != null)
                {
                    cmd3 = new SqlCommand($"insert into Appointment(AppointID,DoctorID,PatientID,Date,Appointment_Status) values ({citaId}, {citaDoctorId}, {patientId},GETDATE(),{citaStatus});", con);
                }
                else
                {
                    cmd3 = new SqlCommand($"insert into Appointment(AppointID,DoctorID,PatientID,Date) values ({citaId}, {citaDoctorId}, {patientId},GETDATE());", con);
                }
                cmd3.CommandType = CommandType.Text;
                cmd3.ExecuteNonQuery();
                cmd0 = new SqlCommand("SET IDENTITY_INSERT Appointment OFF;", con)
                {
                    CommandType = CommandType.Text
                };
                cmd0.ExecuteNonQuery();
            }
            con.Close();
        }

        public override void After(MethodInfo methodUnderTest)
        {
            SqlConnection con = new(connString);
            con.Open();
            if (citaId != null)
            {
                SqlCommand cmd4;
                cmd4 = new SqlCommand($"delete from Appointment where AppointId = {citaId};", con)
                {
                    CommandType = CommandType.Text
                };
                cmd4.ExecuteNonQuery();
            }
            if (patientId != null)
            {
                SqlCommand cmd2;
                cmd2 = new SqlCommand($"DELETE FROM [DBProject].[dbo].[Patient] WHERE PatientID = {patientId};", con)
                {
                    CommandType = CommandType.Text
                };
                cmd2.ExecuteNonQuery();
                cmd2 = new SqlCommand($"DELETE FROM [DBProject].[dbo].[LoginTable] WHERE LoginID = {patientId};", con)
                {
                    CommandType = CommandType.Text
                };
                cmd2.ExecuteNonQuery();
            }
            if (citaDoctorId != null && citaDoctorId != doctorId)
            {
                SqlCommand cmd2;
                cmd2 = new SqlCommand($"DELETE FROM [DBProject].[dbo].[Doctor] WHERE DoctorID = {citaDoctorId};", con)
                {
                    CommandType = CommandType.Text
                };
                cmd2.ExecuteNonQuery();
                cmd2 = new SqlCommand($"DELETE FROM [DBProject].[dbo].[Department] WHERE DeptNo = {departmentId2};", con)
                {
                    CommandType = CommandType.Text
                };
                cmd2.ExecuteNonQuery();
            }
            if (doctorId != null)
            {
                SqlCommand cmd2;
                cmd2 = new SqlCommand($"DELETE FROM [DBProject].[dbo].[Doctor] WHERE DoctorID = {doctorId};", con)
                {
                    CommandType = CommandType.Text
                };
                cmd2.ExecuteNonQuery();
                cmd2 = new SqlCommand($"DELETE FROM [DBProject].[dbo].[Department] WHERE DeptNo = {departmentId1};", con)
                {
                    CommandType = CommandType.Text
                };
                cmd2.ExecuteNonQuery();
            }
            con.Close();
        }
    }
    [CollectionDefinition("Pruebas Maximilian Latysh", DisableParallelization = true)]
    public class Pruebas_ML
    {
        private static readonly string connString = "Data Source=.\\SQLEXPRESS; Initial Catalog=DBProject; Integrated Security=True; TrustServerCertificate=True";

        /**
         * ID: 076
         * Nombre: Obtener citas pendientes con un doctor que no existe.
         * Descripción: Al obtener las citas pendientes con un doctor que no existe se debería retornar un datatable vacío.
         * Datos de prueba:
         * - Doctor que no existe con id 100
         * - Paciente con id 100
         * - Doctor adicional con id 200
         * - Cita con id 100
         * - Tipo de cita [2]
         * Resultado esperado: Que el datatable sea vacío.
         */
        [Fact]
        [TestBeforeAfter(-1, 100, 200, 100, 2)]
        public void GetAllpendingappointments_DAL___Con_Doctor_No_Existe()
        {
            var instancia = new myDAL();
            var res = new DataTable();
            instancia.GetAllpendingappointments_DAL(100, ref res);
            Assert.NotNull(res);
            Assert.Equal(0, res.Rows.Count);
        }

        /**
         * ID: 077
         * Nombre: Obtener citas pendientes con un doctor que no posee citas a su nombre.
         * Descripción: Al obtener las citas pendientes de un doctor que no posee citas el datatable debería ser vacío.
         * Datos de prueba:
         * - Doctor de enfoque con id 101
         * Resultado esperado: Que el datatable sea vacío.
         */
        [Fact]
        [TestBeforeAfter(101)]
        public void GetAllpendingappointments_DAL___Con_Doctor_Sin_Citas()
        {
            var instancia = new myDAL();
            var res = new DataTable();
            instancia.GetAllpendingappointments_DAL(101, ref res);
            Assert.NotNull(res);
            Assert.Equal(0, res.Rows.Count);
        }

        /**
         * ID: 078
         * Nombre: Obtener citas pendientes con un doctor que posee citas.
         * Descripción: Al obtener las citas pendientes de un doctor que posee citas, se retornan.
         * Datos de prueba: 
         * - Doctor de enfoque con id 102
         * - Paciente con id 102
         * - Cita con id 102
         * - Tipo de cita [2]
         * Resultado esperado: Que el datatable contenga una fila con la información solicitada.
         */
        [Fact]
        [TestBeforeAfter(102, 102, 102, 102, 2)]
        public void GetAllpendingappointments_DAL___Con_Doctor_Con_Citas()
        {
            var instancia = new myDAL();
            var res = new DataTable();
            instancia.GetAllpendingappointments_DAL(102, ref res);
            Assert.NotNull(res);
            Assert.Equal(1, res.Rows.Count);
        }

        /**
         * ID: 079
         * Nombre: Aceptar una cita que existe.
         * Descripción: Al aceptar una cita que existe, no se tira ningún error.
         * Datos de prueba:
         * - Paciente con id 103
         * - Doctor adicional con id 103
         * - Cita con id 103
         * - Tipo de cita [2]
         * Resultado esperado: Que el estado sea exitoso.
         */
        [Fact]
        [TestBeforeAfter(-1, 103, 103, 103, 2)]
        public void UpdateAppointment_DAL___Con_Cita_Normal()
        {
            var instancia = new myDAL();
            var estado = instancia.UpdateAppointment_DAL(103);
            Assert.NotEqual(0, estado);
        }

        /**
         * ID: 080
         * Nombre: Aceptar una cita que no existe.
         * Descripción: Al aceptar una cita que no existe, se debería tirar un error.
         * Datos de prueba:
         * - Paciente con id 104
         * - Doctor adicional con id 104
         * - Cita que no existe con id 104
         * Resultado esperado: Que el estado sea no exitoso.
         */
        [Fact]
        [TestBeforeAfter(-1, 104, 104)]
        public void UpdateAppointment_DAL___Con_Cita_No_Existe()
        {
            var instancia = new myDAL();
            var estado = instancia.UpdateAppointment_DAL(104);
            Assert.Equal(-1, estado);
        }

        /**
         * ID: 081
         * Nombre: Cancelar una cita que existe.
         * Descripción: Al cancelar una cita que existe, se quede cancelada con éxito.
         * Datos de prueba:
         * - Paciente con id 105
         * - Doctor adicional con id 105
         * - Cita con id 105
         * - Tipo de cita [2]
         * Resultado esperado: Que el estado sea exitoso.
         */
        [Fact]
        [TestBeforeAfter(-1, 105, 105, 105, 2)]
        public void Deleteappointment_DAL___Con_Cita_Normal()
        {
            var instancia = new myDAL();
            var estado = instancia.Deleteappointment_DAL(105);
            Assert.Equal(1, estado);
        }

        /**
         * ID: 082
         * Nombre: Cancelar una cita que no existe.
         * Descripción: Al cancelar una cita que no existe, se debería tirar un error.
         * Datos de prueba:
         * - Paciente con id 106
         * - Doctor adicional con id 106
         * - Cita que no existe con id 106
         * Resultado esperado: Que el estado sea no exitoso.
         */
        [Fact]
        [TestBeforeAfter(-1, 106, 106)]
        public void Deleteappointment_DAL___Con_Cita_No_Existe()
        {
            var instancia = new myDAL();
            var estado = instancia.Deleteappointment_DAL(106);
            Assert.Equal(-1, estado);
        }

        /**
         * ID: 083
         * Nombre: Buscar pacientes pendientes de un doctor que tiene pendientes.
         * Descripción: Al buscar los pacientes pendientes de un doctor que tiene pacientes pendientes, se retornan sus pacientes pendientes.
         * Datos de prueba:
         * - Doctor de enfoque con id 107
         * - Paciente con id 107
         * - Cita con id 107
         * - Tipo de cita [1]
         * Resultado esperado: Que el estado sea exitoso y que el datatable contenga la información solicitada.
         */
        [Fact]
        [TestBeforeAfter(107, 107, 107, 107, 1)]
        public void search_patient_DAL___Con_Doctor_Normal()
        {
            var instancia = new myDAL();
            var res = new DataTable();
            var estado = instancia.search_patient_DAL(107, ref res);
            Assert.NotNull(res);
            Assert.Equal(1, estado);
            Assert.Equal(1, res.Rows.Count);
        }

        /**
         * ID: 084
         * Nombre: Buscar pacientes pendientes de un doctor que no existe.
         * Descripción: Al buscar los pacientes pendientes de un doctor que no existe, se retorne un error.
         * Datos de prueba:
         * - Doctor que no existe con id 108
         * - Paciente con id 108
         * - Doctor adicional con id 208
         * - Cita con id 108
         * - Tipo de cita [1]
         * Resultado esperado: Que el estado sea no exitoso.
         */
        [Fact]
        [TestBeforeAfter(-1, 108, 208, 108, 1)]
        public void search_patient_DAL___Con_Doctor_No_Existe()
        {
            var instancia = new myDAL();
            var res = new DataTable();
            var estado = instancia.search_patient_DAL(108, ref res);
            Assert.NotNull(res);
            Assert.Equal(0, res.Rows.Count);
            Assert.Equal(-1, estado);
        }

        /**
         * ID: 085
         * Nombre: Buscar pacientes pendientes de un doctor que no posee pacientes pendientes.
         * Descripción: Al buscar los pacientes pendientes de un doctor que no posee pacientes pendientes, no se debería retornar un error pero el resultado debería ser vacío.
         * Datos de prueba:
         * - Doctor de enfoque con id 109
         * Resultado esperado: Que el estado sea exitoso pero que el datatable no contenga ningún resultado.
         */
        [Fact]
        [TestBeforeAfter(109)]
        public void search_patient_DAL___Con_Doctor_Sin_Pacientes()
        {
            var instancia = new myDAL();
            var res = new DataTable();
            var estado = instancia.search_patient_DAL(109, ref res);
            Assert.NotNull(res);
            Assert.Equal(0, res.Rows.Count);
            Assert.Equal(1, estado);
        }

        /**
         * ID: 086
         * Nombre: Actualizar cita con doctor que existe y cita que existe.
         * Descripción: Al actualizar la cita de un doctor con la información correspondiente, no debería ocurrir ningún error.
         * Datos de prueba:
         * - Doctor de enfoque con id 110
         * - Paciente con id 110
         * - Doctor adicional con id 110
         * - Cita con id 110
         * - Tipo de cita [1]
         * - Enefermedad: "a"
         * - Progresión: "b"
         * - Prescripción: "c"
         * Resultado esperado: Que el estado sea exitoso.
         */
        [Fact]
        [TestBeforeAfter(110, 110, 110, 110, 1)]
        public void update_prescription_DAL___Con_Doctor_Y_Cita_Normales()
        {
            var instancia = new myDAL();
            var estado = instancia.update_prescription_DAL(110, 110, "a", "b", "c");
            Assert.Equal(1, estado);
        }

        /**
         * ID: 087
         * Nombre: Actualizar cita con un doctor que no existe.
         * Descripción: Al actualizar la cita de un doctor que no existe, se debería tirar un error.
         * Datos de prueba:
         * - Doctor que no existe con id 111
         * - Paciente con id 111
         * - Doctor adicional con id 211
         * - Cita con id 111
         * - Tipo de cita [1]
         * - Enefermedad: "a"
         * - Progresión: "b"
         * - Prescripción: "c"
         * Resultado esperado: Que el estado sea no exitoso.
         */
        [Fact]
        [TestBeforeAfter(-1, 111, 211, 111, 1)]
        public void update_prescription_DAL___Con_Doctor_No_Existe()
        {
            var instancia = new myDAL();
            var estado = instancia.update_prescription_DAL(111, 111, "a", "b", "c");
            Assert.Equal(0, estado);
        }

        /**
         * ID: 088
         * Nombre: Actualizar cita con una cita que no existe.
         * Descripción: Al actualizar una cita que no existe, se debería tirar un error.
         * Datos de prueba:
         * - Doctor de enfoque con id 112
         * - Paciente con id 112
         * - Cita que no existe con id 112
         * - Enefermedad: "a"
         * - Progresión: "b"
         * - Prescripción: "c"
         * Resultado esperado: Que el estado sea no exitoso.
         */
        [Fact]
        [TestBeforeAfter(112, 112, 112)]
        public void update_prescription_DAL___Con_Cita_No_Existe()
        {
            var instancia = new myDAL();
            var estado = instancia.update_prescription_DAL(112, 112, "a", "b", "c");
            Assert.Equal(0, estado);
        }

        /**
         * ID: 089
         * Nombre: Obtener costo de cita con un doctor que no existe.
         * Descripción: Al obtener el costo de una cita de un doctor que no existe, se debería tirar un error.
         * Datos de prueba:
         * - Doctor que no existe con id 113
         * Resultado esperado: Que el estado no sea exitoso.
         */
        [Fact]
        [TestBeforeAfter()]
        public void generate_bill_DAL___Con_Doctor_No_Existe()
        {
            var instancia = new myDAL();
            var res = new DataTable();
            var estado = instancia.generate_bill_DAL(113, ref res);
            Assert.NotNull(res);
            Assert.Equal(0, res.Rows.Count);
            Assert.Equal(-1, estado);
        }

        /**
         * ID: 090
         * Nombre: Obtener costo de cita con un doctor que existe.
         * Descripción: Al obtener el costo de una cita de un doctor que existe, se debería retornar la información solicitada sin ningún error.
         * Datos de prueba:
         * - Doctor de enfoque con id 114
         * - Paciente con id 114
         * - Cita con id 114
         * - Tipo de cita [1]
         * Resultado esperado: Que el estado sea exitoso y que el datatable contenga la información solicitada.
         */
        [Fact]
        [TestBeforeAfter(114, 114, 114, 114, 1)]
        public void generate_bill_DAL___Con_Doctor_Normal()
        {
            var instancia = new myDAL();
            var res = new DataTable();
            var estado = instancia.generate_bill_DAL(114, ref res);
            Assert.NotNull(res);
            Assert.Equal(1, res.Rows.Count);
            Assert.Equal(1, estado);
        }

        /**
         * ID: 091
         * Nombre: Registar una cita como completada y pagada con un doctor que no existe.
         * Descripción: Al registrar una cita como completada y pagada de un doctor que no existe, se debería tirar un error de SQL.
         * Datos de prueba:
         * - Doctor que no existe con id 115
         * - Paciente con id 115
         * - Doctor adicional con id 215
         * - Cita con id 115
         * - Tipo de cita [1]
         * Resultado esperado: Que se tire un error de SQL.
         */
        [Fact]
        [TestBeforeAfter(-1, 115, 215, 115, 1)]
        public void paid_bill_DAL___Con_Doctor_No_Existe()
        {
            var instancia = new myDAL();
            Assert.Throws<SqlException>(() => instancia.paid_bill_DAL(115, 115));
        }

        /**
         * ID: 092
         * Nombre: Registrar una cita como completada y pagada con una cita que no existe.
         * Descripción: Al registrar un cita que no existe como completada y pagada se debería tirar un error.
         * Datos de prueba:
         * - Doctor de enfoque con id 116
         * - Paciente con id 116
         * - Cita que no existe con id 116
         * Resultado esperado: Que se tire un error de SQL.
         */
        [Fact]
        [TestBeforeAfter(116, 116, 116)]
        public void paid_bill_DAL___Con_Cita_No_Existe()
        {
            var instancia = new myDAL();
            Assert.Throws<SqlException>(() => instancia.paid_bill_DAL(116, 116));
        }

        /**
         * ID: 093
         * Nombre: Registrar una cita como completada y pagada con una cita y un doctor que existen.
         * Descripción: Al registrar una cita normal como completada y pagada de un doctor que existe no se debería tirar ningún error.
         * Datos de prueba:
         * - Doctor de enfoque con id 117
         * - Paciente con id 117
         * - Cita con id 117
         * - Tipo de cita [1]
         * Resultado esperado: Que no se tire ningún error.
         */
        [Fact]
        [TestBeforeAfter(117, 117, 117, 117, 1)]
        public void paid_bill_DAL___Con_Doctor_Y_Cita_Normales()
        {
            var instancia = new myDAL();
            var ex = Record.Exception(() => { instancia.paid_bill_DAL(117, 117); });
            Assert.Null(ex);
        }

        /**
         * ID: 094
         * Nombre: Registrar una cita como completada y pero no pagada con un doctor que no existe.
         * Descripción: Al registrar una cita como completada pero no pagada con un doctor que no existe se debería tirar un error.
         * Datos de prueba:
         * - Doctor que no existe con id 118
         * - Paciente con id 118
         * - Doctor adicional con id 218
         * - Cita con id 118
         * - Tipo de cita [1]
         * Resultado esperado: Que se tire un error de SQL.
         */
        [Fact]
        [TestBeforeAfter(-1, 118, 218, 118, 1)]
        public void Unpaid_bill_DAL___Con_Doctor_No_Existe()
        {
            var instancia = new myDAL();
            Assert.Throws<SqlException>(() => instancia.Unpaid_bill_DAL(118, 118));
        }

        /**
         * ID: 095
         * Nombre: Registrar una cita como completada y pero no pagada con una cita que no existe.
         * Descripción: Al registrar una cita que no existe como completada pero no pagada se debería tirar un error.
         * Datos de prueba:
         * - Doctor de enfoque con id 119
         * - Paciente con id 119
         * - Cita que no existe con id 119
         * Resultado esperado: Que se tire un error de SQL.
         */
        [Fact]
        [TestBeforeAfter(119, 119, 119)]
        public void Unpaid_bill_DAL___Con_Cita_No_Existe()
        {
            var instancia = new myDAL();
            Assert.Throws<SqlException>(() => instancia.Unpaid_bill_DAL(119, 119));
        }

        /**
         * ID: 096
         * Nombre: Registrar una cita como completada y pero no pagada con una cita y un doctor que existen.
         * Descripción: Al registrar una cita normal como completada pero no pagada de un doctor que existe no se debería tirar ningún error.
         * Datos de prueba:
         * - Doctor de enfoque con id 120
         * - Paciente con id 120
         * - Cita con id 120
         * - Tipo de cita [1]
         * Resultado esperado: Que no se tire ningún error.
         */
        [Fact]
        [TestBeforeAfter(120, 120, 120, 120, 1)]
        public void Unpaid_bill_DAL___Con_Doctor_Y_Cita_Normales()
        {
            var instancia = new myDAL();
            var ex = Record.Exception(() => { instancia.Unpaid_bill_DAL(120, 120); });
            Assert.Null(ex);
        }

        /**
         * ID: 097
         * Nombre: Registrar una cita como completada pero no pagada y después registrarla como una cita completada y pagada.
         * Descripción: Al registrar una cita como completada pero no pagada y después como pagada debería mantener la cantidad de citas completadas del doctor en 1 y no se debería subir a 2.
         * Datos de prueba:
         * - Doctor de enfoque con id 121
         * - Paciente con id 121
         * - Cita con id 121
         * - Tipo de cita [1]
         * Resultado esperado: Que el datatable contenga un valor de 1.
         */
        [Fact]
        [TestBeforeAfter(121, 121, 121, 121, 1)]
        public void Unpaid_bill_DAL___Seguido_Por___paid_bill_DAL()
        {
            var instancia = new myDAL();
            instancia.Unpaid_bill_DAL(120, 120);
            instancia.paid_bill_DAL(120, 120);
            SqlConnection con = new(connString);
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand("Doctor_Information_By_ID1", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add("@Id", SqlDbType.Int);
            cmd.Parameters["@Id"].Value = 121;
            cmd.ExecuteNonQuery();
            DataSet ds = new();
            using (SqlDataAdapter da = new(cmd))
            {
                da.Fill(ds);

            }
            var res = ds.Tables[0];
            con.Close();
            Assert.Equal(1, res.Rows.Count);
            Assert.NotEqual(2, res.Rows[0][10]);
        }

        /**
         * ID: 098
         * Nombre: Obtener pacientes y sus historiales de un doctor que no existe.
         * Descripción: Al obtener los pacientes y sus historiales de un doctor que no existe se debería tirar un error.
         * Datos de prueba:
         * - Doctor que no existe con id 122
         * - Paciente con id 122
         * - Doctor adicional con id 222
         * - Cita con id 122
         * - Tipo de cita [1]
         * Resultado esperado: Que el estado no sea exitoso.
         */
        [Fact]
        [TestBeforeAfter(-1, 122, 222, 122, 1)]
        public void getPHistory___Con_Doctor_No_Existe()
        {
            var instancia = new myDAL();
            var res = new DataTable();
            var estado = instancia.getPHistory(122, ref res);
            Assert.NotNull(res);
            Assert.Equal(-1, estado);
        }

        /**
         * ID: 099
         * Nombre: Obtener pacientes y sus historiales de un doctor sin pacientes.
         * Descripción: Al obtener los pacientes de un doctor que no posee pacientes no se debería tirar un error pero el resultado debería ser vacío.
         * Datos de prueba:
         * - Doctor de enfoque con id 123
         * - Paciente con id 123
         * Resultado esperado: Que el estado no sea exitoso.
         */
        [Fact]
        [TestBeforeAfter(123, 123, 123)]
        public void getPHistory___Con_Doctor_Sin_Pacientes()
        {
            var instancia = new myDAL();
            var res = new DataTable();
            var estado = instancia.getPHistory(123, ref res);
            Assert.NotNull(res);
            Assert.Equal(0, res.Rows.Count);
            Assert.Equal(1, estado);
        }

        /**
         * ID: 100
         * Nombre: Obtener pacientes y sus historiales de un doctor que tiene pacientes.
         * Descripción: Al obtener los pacientes y sus historiales de un doctor que tiene pacientes, el estado debería ser exitoso y se debería obtener la información solicitada.
         * Datos de prueba:
         * - Doctor de enfoque con id 124
         * - Paciente con id 124
         * - Cita con id 124
         * - Tipo de cita [3]
         * Resultado esperado: Que el estado sea exitoso y que el datatable no sea vacío.
         */
        [Fact]
        [TestBeforeAfter(124, 124, 124, 124, 3)]
        public void getPHistory___Con_Doctor_Normal()
        {
            var instancia = new myDAL();
            var res = new DataTable();
            var estado = instancia.getPHistory(124, ref res);
            Assert.NotNull(res);
            Assert.Equal(1, estado);
            Assert.Equal(1, res.Rows.Count);
        }
    }
}