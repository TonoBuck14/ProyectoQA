using DBProject.DAL;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using Xunit;
using System.Collections.Generic;
using System.Linq;
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
            if (citaId != null && citaDoctorId == null) throw new Exception("Each appointment must have a doctor.");
            if (citaId != null && patientId == null) throw new Exception("Each appointment must have a patient.");
            Random rnd = new Random();
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
                    cmd10 = new SqlCommand($"DELETE FROM [DBProject].[dbo].[Doctor] WHERE DoctorID = {doctorId};", con);
                    cmd10.CommandType = CommandType.Text;
                    cmd10.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {

                }
                try
                {
                    cmd10 = new SqlCommand($"DELETE FROM [DBProject].[dbo].[Department] WHERE DeptNo = {departmentId1};", con);
                    cmd10.CommandType = CommandType.Text;
                    cmd10.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {

                }
                SqlCommand cmd1;
                cmd1 = new SqlCommand($"INSERT INTO[DBProject].[dbo].[Department]([DeptNo], [DeptName], [Description]) VALUES({departmentId1}, 'Dept{departmentId1}', 'D99');", con);
                cmd1.CommandType = CommandType.Text;
                cmd1.ExecuteNonQuery();
                SqlCommand cmd2;
                cmd2 = new SqlCommand("INSERT INTO [DBProject].[dbo].[DOCTOR] (DoctorID, Name, Phone, Address," +
                    " BirthDate, Gender, DeptNo, Charges_Per_Visit, MonthlySalary, ReputeIndex, Patients_Treated," +
                    $" Qualification, Specialization, Work_Experience, status) VALUES ({doctorId}, 'Doctor1', '12345678901', 'Costa Rica', '01-01-1990', 'M', {departmentId1}, " +
                    "100.0, 5000.0, 4.5, 0, 'Master', 'Cardiology', 10, 1) ;", con);
                cmd2.CommandType = CommandType.Text;
                cmd2.ExecuteNonQuery();
            }
            if (citaDoctorId != null && citaDoctorId != doctorId)
            {
                SqlCommand cmd10;
                try
                {
                    cmd10 = new SqlCommand($"DELETE FROM [DBProject].[dbo].[Doctor] WHERE DoctorID = {citaDoctorId};", con);
                    cmd10.CommandType = CommandType.Text;
                    cmd10.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {

                }
                try
                {
                    cmd10 = new SqlCommand($"DELETE FROM [DBProject].[dbo].[Department] WHERE DeptNo = {departmentId2};", con);
                    cmd10.CommandType = CommandType.Text;
                    cmd10.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {

                }
                SqlCommand cmd1;
                cmd1 = new SqlCommand($"INSERT INTO[DBProject].[dbo].[Department]([DeptNo], [DeptName], [Description]) VALUES({departmentId2}, 'Dept{departmentId2}', 'D99');", con);
                cmd1.CommandType = CommandType.Text;
                cmd1.ExecuteNonQuery();
                SqlCommand cmd2;
                cmd2 = new SqlCommand("INSERT INTO [DBProject].[dbo].[DOCTOR] (DoctorID, Name, Phone, Address," +
                    " BirthDate, Gender, DeptNo, Charges_Per_Visit, MonthlySalary, ReputeIndex, Patients_Treated," +
                    $" Qualification, Specialization, Work_Experience, status) VALUES ({citaDoctorId}, 'Doctor2', '12345678901', 'Costa Rica', '01-01-1990', 'M', {departmentId2}, " +
                    $"100.0, 5000.0, 4.5, 0, 'Master', 'Cardiology', 10, 1) ;", con);
                cmd2.CommandType = CommandType.Text;
                cmd2.ExecuteNonQuery();
            }
            if (patientId != null)
            {
                SqlCommand cmd10;
                try
                {
                    cmd10 = new SqlCommand($"DELETE FROM [DBProject].[dbo].[Patient] WHERE PatientID = {patientId};", con);
                    cmd10.CommandType = CommandType.Text;
                    cmd10.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {

                }
                try
                {
                    cmd10 = new SqlCommand($"DELETE FROM [DBProject].[dbo].[LoginTable] WHERE LoginID = {patientId};", con);
                    cmd10.CommandType = CommandType.Text;
                    cmd10.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {

                }
                SqlCommand cmd0;
                cmd0 = new SqlCommand("SET IDENTITY_INSERT LoginTable ON;", con);
                cmd0.CommandType = CommandType.Text;
                cmd0.ExecuteNonQuery();
                SqlCommand cmd1;
                cmd1 = new SqlCommand($"INSERT INTO[DBProject].[dbo].[LoginTable]([LoginID], [Password], [Email], [Type]) VALUES({patientId}, 'Patient1', 'PatientFull1{patientId}@gmail.com', 1);", con);
                cmd1.CommandType = CommandType.Text;
                cmd1.ExecuteNonQuery();
                cmd0 = new SqlCommand("SET IDENTITY_INSERT LoginTable OFF;", con);
                cmd0.CommandType = CommandType.Text;
                cmd0.ExecuteNonQuery();
                cmd1 = new SqlCommand($"insert into Patient(PatientID,Name,Phone,Address,BirthDate,Gender) values ({patientId}, 'Chaximilian', '777','f','1990-05-09','M');", con);
                cmd1.CommandType = CommandType.Text;
                cmd1.ExecuteNonQuery();
            }
            if (citaId != null)
            {
                try
                {
                    SqlCommand cmd4;
                    cmd4 = new SqlCommand($"delete from Appointment where AppointId = {citaId};", con);
                    cmd4.CommandType = CommandType.Text;
                    cmd4.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {

                }
                SqlCommand cmd0;
                cmd0 = new SqlCommand("SET IDENTITY_INSERT Appointment ON;", con);
                cmd0.CommandType = CommandType.Text;
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
                cmd0 = new SqlCommand("SET IDENTITY_INSERT Appointment OFF;", con);
                cmd0.CommandType = CommandType.Text;
                cmd0.ExecuteNonQuery();
            }
            con.Close();
        }

        public override void After(MethodInfo methodUnderTest)
        {
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            if (citaId != null)
            {
                SqlCommand cmd4;
                cmd4 = new SqlCommand($"delete from Appointment where AppointId = {citaId};", con);
                cmd4.CommandType = CommandType.Text;
                cmd4.ExecuteNonQuery();
            }
            if (patientId != null)
            {
                SqlCommand cmd2;
                cmd2 = new SqlCommand($"DELETE FROM [DBProject].[dbo].[Patient] WHERE PatientID = {patientId};", con);
                cmd2.CommandType = CommandType.Text;
                cmd2.ExecuteNonQuery();
                cmd2 = new SqlCommand($"DELETE FROM [DBProject].[dbo].[LoginTable] WHERE LoginID = {patientId};", con);
                cmd2.CommandType = CommandType.Text;
                cmd2.ExecuteNonQuery();
            }
            if (citaDoctorId != null && citaDoctorId != doctorId)
            {
                SqlCommand cmd2;
                cmd2 = new SqlCommand($"DELETE FROM [DBProject].[dbo].[Doctor] WHERE DoctorID = {citaDoctorId};", con);
                cmd2.CommandType = CommandType.Text;
                cmd2.ExecuteNonQuery();
                cmd2 = new SqlCommand($"DELETE FROM [DBProject].[dbo].[Department] WHERE DeptNo = {departmentId2};", con);
                cmd2.CommandType = CommandType.Text;
                cmd2.ExecuteNonQuery();
            }
            if (doctorId != null)
            {
                SqlCommand cmd2;
                cmd2 = new SqlCommand($"DELETE FROM [DBProject].[dbo].[Doctor] WHERE DoctorID = {doctorId};", con);
                cmd2.CommandType = CommandType.Text;
                cmd2.ExecuteNonQuery();
                cmd2 = new SqlCommand($"DELETE FROM [DBProject].[dbo].[Department] WHERE DeptNo = {departmentId1};", con);
                cmd2.CommandType = CommandType.Text;
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
         * Descripción:
         * Datos de prueba:
         * Resultado esperado:
         */
        [Fact]
        [TestBeforeAfter(-1, 100, 200, 100, 2)]
        public void GetAllpendingappointments_DAL___Con_Doctor_No_Existe()
        {
            var instancia = new myDAL();
            var res = new DataTable();
            instancia.GetAllpendingappointments_DAL(100, ref res);
            Assert.NotNull(res);
            Assert.Equal(res.Rows.Count, 0);
        }

        /**
         * ID: 077
         * Nombre: Obtener citas pendientes con un doctor que no posee citas a su nombre.
         * Descripción:
         * Datos de prueba:
         * Resultado esperado:
         */
        [Fact]
        [TestBeforeAfter(101)]
        public void GetAllpendingappointments_DAL___Con_Doctor_Sin_Citas()
        {
            var instancia = new myDAL();
            var res = new DataTable();
            instancia.GetAllpendingappointments_DAL(101, ref res);
            Assert.NotNull(res);
            Assert.Equal(res.Rows.Count, 0);
        }

        /**
         * ID: 078
         * Nombre: Obtener citas pendientes con un doctor que posee citas.
         * Descripción:
         * Datos de prueba:
         * Resultado esperado:
         */
        [Fact]
        [TestBeforeAfter(102, 102, 102, 102, 2)]
        public void GetAllpendingappointments_DAL___Con_Doctor_Con_Citas()
        {
            var instancia = new myDAL();
            var res = new DataTable();
            instancia.GetAllpendingappointments_DAL(102, ref res);
            Assert.NotNull(res);
            Assert.Equal(res.Rows.Count, 1);
        }

        /**
         * ID: 079
         * Nombre: Aceptar una cita que existe.
         * Descripción:
         * Datos de prueba:
         * Resultado esperado:
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
         * Descripción:
         * Datos de prueba:
         * Resultado esperado:
         */
        [Fact]
        [TestBeforeAfter(-1, 104, 104)]
        public void UpdateAppointment_DAL___Con_Cita_No_Existe()
        {
            var instancia = new myDAL();
            var estado = instancia.UpdateAppointment_DAL(104);
            Assert.Equal(0, estado);
        }

        /**
         * ID: 081
         * Nombre: Cancelar una cita que existe.
         * Descripción:
         * Datos de prueba:
         * Resultado esperado:
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
         * Descripción:
         * Datos de prueba:
         * Resultado esperado:
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
         * Descripción:
         * Datos de prueba:
         * Resultado esperado:
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
         * Descripción:
         * Datos de prueba:
         * Resultado esperado:
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
         * Descripción:
         * Datos de prueba:
         * Resultado esperado:
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
         * Descripción:
         * Datos de prueba:
         * Resultado esperado:
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
         * Descripción:
         * Datos de prueba:
         * Resultado esperado:
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
         * Descripción:
         * Datos de prueba:
         * Resultado esperado:
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
         * Descripción:
         * Datos de prueba:
         * Resultado esperado:
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
         * Descripción:
         * Datos de prueba:
         * Resultado esperado:
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
         * Descripción:
         * Datos de prueba:
         * Resultado esperado:
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
         * Descripción:
         * Datos de prueba:
         * Resultado esperado:
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
         * Descripción:
         * Datos de prueba:
         * Resultado esperado:
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
         * Descripción:
         * Datos de prueba:
         * Resultado esperado:
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
         * Descripción:
         * Datos de prueba:
         * Resultado esperado:
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
         * Descripción:
         * Datos de prueba:
         * Resultado esperado:
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
         * Descripción:
         * Datos de prueba:
         * Resultado esperado:
         */
        [Fact]
        [TestBeforeAfter(121, 121, 121, 121, 1)]
        public void Unpaid_bill_DAL___Seguido_Por___paid_bill_DAL()
        {
            var instancia = new myDAL();
            instancia.Unpaid_bill_DAL(120, 120);
            instancia.paid_bill_DAL(120, 120);
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand("Doctor_Information_By_ID1", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id", SqlDbType.Int);
            cmd.Parameters["@Id"].Value = 121;
            cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
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
         * Descripción:
         * Datos de prueba:
         * Resultado esperado:
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
         * Descripción:
         * Datos de prueba:
         * Resultado esperado:
         */
        [Fact]
        [TestBeforeAfter(123, 123, 123)]
        public void getPHistory___Con_Doctor_Sin_Pacientes()
        {
            var instancia = new myDAL();
            var res = new DataTable();
            var estado = instancia.getPHistory(123, ref res);
            Assert.NotNull(res);
            Assert.Equal(-1, estado);
        }

        /**
         * ID: 100
         * Nombre: Obtener pacientes y sus historiales de un doctor que tiene pacientes.
         * Descripción:
         * Datos de prueba:
         * Resultado esperado:
         */
        [Fact]
        [TestBeforeAfter(124, 124, 124, 124, 3)]
        public void getPHistory___Con_Doctor_Normal()
        {
            var instancia = new myDAL();
            var res = new DataTable();
            var estado = instancia.getPHistory(124, ref res);
            Assert.NotNull(res);
            Assert.Equal(1, res.Rows.Count);
        }
    }
}