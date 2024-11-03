using DBProject.DAL;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace ClinicManagement.Tests
{
    [CollectionDefinition("Pruebas Josue", DisableParallelization = true)]
    public class Pruebas_J
    {

        private static readonly string connString = "Data Source=.\\SQLEXPRESS; Initial Catalog=DBProject; Integrated Security=True; TrustServerCertificate=True";



        [Fact]
        public void GetDeptInfo_RetornaUnaDataTable_CuandoEsExitoso_YHayDatos()
        {

            var instancia = new myDAL();
            var resultado = new DataTable();
            int estado = instancia.getdeptInfo(ref resultado);
            Assert.Equal(1, estado);
            Assert.NotNull(resultado);
        }

        [Fact]
        public void getDeptDoctorInfo_RetornaUnaTabla_CuandoElDepartamentoExiste()
        {

            // Conexion

            SqlConnection con = new SqlConnection(connString);
            con.Open();

            // Se inserta ejemplo

            SqlCommand cmd1;
            cmd1 = new SqlCommand("INSERT INTO[DBProject].[dbo].[Department]([DeptNo], [DeptName], [Description]) VALUES(99, 'Dept99', 'D99');", con);
            cmd1.CommandType = CommandType.Text;
            cmd1.ExecuteNonQuery();

            //  Prueba

            var instancia = new myDAL();
            var resultado = new DataTable();
            int estado = instancia.getDeptDoctorInfo("Dept99", ref resultado);
            Assert.Equal(1, estado);
            Assert.NotNull(resultado);

            // Se quita para mantener la base como estaba

            SqlCommand cmd2;
            cmd2 = new SqlCommand("DELETE FROM [DBProject].[dbo].[Department] WHERE DeptNo = 99;", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();
            con.Close();

        }


        [Fact]
        public void getDeptDoctorInfo_RetornaUnaTablaConCeroFilas_CuandoElDepartamentoExistePeroNoTieneDoctoresAsociados()
        {

            // Conexion

            SqlConnection con = new SqlConnection(connString);
            con.Open();

            // Se inserta un ejemplo

            SqlCommand cmd1;
            cmd1 = new SqlCommand("INSERT INTO[DBProject].[dbo].[Department]([DeptNo], [DeptName], [Description]) VALUES(88, 'Dept88', 'D88');", con);
            cmd1.CommandType = CommandType.Text;
            cmd1.ExecuteNonQuery();

            //  Prueba

            var instancia = new myDAL();
            var resultado = new DataTable();
            int estado = instancia.getDeptDoctorInfo("Dept88", ref resultado);
            Assert.Equal(0, resultado.Rows.Count);

            // Se quita para mantener la base como estaba

            SqlCommand cmd2;
            cmd2 = new SqlCommand("DELETE FROM [DBProject].[dbo].[Department] WHERE DeptNo = 88;", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();
            con.Close();

        }

        [Fact]
        public void getDeptDoctorInfo_RetornaUnaTablaConTodosLosResultados_CuandoElDepartamentoExistePeroYTieneDoctoresAsociados()
        {
            // Conexion

            SqlConnection con = new SqlConnection(connString);
            con.Open();

            // Se inserta un ejemplo

            SqlCommand cmd1;
            cmd1 = new SqlCommand("INSERT INTO[DBProject].[dbo].[Department]([DeptNo], [DeptName], [Description]) VALUES(77, 'Dept77', 'D77');", con);
            cmd1.CommandType = CommandType.Text;
            cmd1.ExecuteNonQuery();

            // Se le asocia a un doctor el departamento

            SqlCommand cmd2;
            cmd2 = new SqlCommand("UPDATE [DBProject].[dbo].[Doctor] SET [DeptNo] = 77 WHERE [DoctorID] = 3;", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();

            // Prueba

            var instancia = new myDAL();
            var resultado = new DataTable();
            int estado = instancia.getDeptDoctorInfo("Dept77", ref resultado);
            Assert.Equal(1, resultado.Rows.Count);

            // Se quita para mantener la base como estaba

            SqlCommand cmd3;
            cmd3 = new SqlCommand("UPDATE [DBProject].[dbo].[Doctor] SET [DeptNo] = 1 WHERE [DoctorID] = 3;", con);
            cmd3.CommandType = CommandType.Text;
            cmd3.ExecuteNonQuery();

            SqlCommand cmd4;
            cmd4 = new SqlCommand("DELETE FROM [DBProject].[dbo].[Department] WHERE DeptNo = 77;", con);
            cmd4.CommandType = CommandType.Text;
            cmd4.ExecuteNonQuery();
            con.Close();

        }

        [Fact]
        public void getDeptDoctorInfo_RetornaUnaTablaVacia_CuandoElDepartamentoNoExiste()
        {
            var instancia = new myDAL();
            var resultado = new DataTable();
            var departamento = "Animales";
            int estado = instancia.getDeptDoctorInfo(departamento, ref resultado);
            Assert.Equal(1, estado);
            Assert.Equal(0, resultado.Rows.Count);
        }

        [Fact]
        public void doctorInfoDisplayer_FuncionaCuandoElDoctorExiste()
        {
            var instancia = new myDAL();
            int dID = 3;
            string name = null;
            string phone = null;
            string gender = null;
            float charges_Per_Visit = 0;
            float ReputeIndex = 0;
            int PatientsTreated = 0;
            string qualification = null;
            string specialization = null;
            int workE = 0;
            int age = 0;
            int estado = instancia.doctorInfoDisplayer(dID, ref name, ref phone, ref gender, ref charges_Per_Visit, ref ReputeIndex, ref PatientsTreated, ref qualification, ref specialization, ref workE, ref age);
            Assert.Equal(0, estado);

        }

        [Fact]
        public void doctorInfoDisplayer_DevuelveLosDatos_ExactamenteComoEstanEnLaBase()
        {

            var instancia = new myDAL();
            int dID = 3;
            string name = null;
            string phone = null;
            string gender = null;
            float charges_Per_Visit = 0;
            float ReputeIndex = 0;
            int PatientsTreated = 0;
            string qualification = null;
            string specialization = null;
            int workE = 0;
            int age = 0;
            int estado = instancia.doctorInfoDisplayer(dID, ref name, ref phone, ref gender, ref charges_Per_Visit, ref ReputeIndex, ref PatientsTreated, ref qualification, ref specialization, ref workE, ref age);
            Assert.Equal(0, estado);
            Assert.Equal("KASHAN", name);
            Assert.Equal("156133213  ", phone);
            Assert.Equal("M", gender);
            Assert.Equal(3000, charges_Per_Visit);
            Assert.Equal(0, PatientsTreated);
            Assert.Equal(3.5, ReputeIndex);
            Assert.Equal("PHD IN EVERY FIELD KNOWN TO MAN", qualification);
            Assert.Equal("ENJOY", specialization);
            Assert.Equal(10, workE);


        }

        [Fact]
        public void getFreeSlots_DevuelveNueveCuposSiElDoctorNoTieneNingunaCita()
        {

            var instancia = new myDAL();
            var resultado = new DataTable();
            int Doctor_id = 7;
            int paciente_id = 12;
            int estado = instancia.getFreeSlots(Doctor_id, paciente_id, ref resultado);
            Assert.Equal(9, estado);
        }

        [Fact]
        public void getFreeSlots_DevuelveNueve_AunCuandoHayIdsSinSentidoYHorasMayoresAVeinticuatro()
        {

            var instancia = new myDAL();
            var resultado = new DataTable();
            int Doctor_id = 99;
            int paciente_id = 88;
            string mes = null;
            instancia.insertAppointment(Doctor_id, paciente_id, 43, ref mes);
            int estado = instancia.getFreeSlots(Doctor_id, paciente_id, ref resultado);
            Assert.Equal(9, estado);

        }

        [Fact]
        public void insertAppointmentFuncionaCuandoTodoLosParametrosEstanCorrectosYRetornaCero()
        {

            var instancia = new myDAL();

            SqlConnection con = new SqlConnection(connString);
            con.Open();

            // Se inserta ejemplo

            SqlCommand cmd1;
            cmd1 = new SqlCommand("INSERT INTO LoginTable(Password, Email, Type) VALUES ('ejmplo6', 'fffffff', 1); SELECT SCOPE_IDENTITY();", con);
            cmd1.CommandType = CommandType.Text;
            var login_id = Convert.ToInt32(cmd1.ExecuteScalar());

            SqlCommand cmd2;
            cmd2 = new SqlCommand("insert into Patient(PatientID,Name,Phone,Address,BirthDate,Gender) values (" + login_id + ",'Chax', '777','f','1990-05-09','M');", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();

            // Prueba

            string mes = null;
            int estado = instancia.insertAppointment(3, login_id, 5, ref mes);
            Assert.Equal(0, estado);

            // Se quita de la base

            SqlCommand cmd4;
            cmd4 = new SqlCommand("delete from Appointment where DoctorID = 3 and PatientID = " + login_id + ";", con);
            cmd4.CommandType = CommandType.Text;
            cmd4.ExecuteNonQuery();

            SqlCommand cmd5;
            cmd5 = new SqlCommand("delete from Patient where PatientID = " + login_id + ";", con);
            cmd5.CommandType = CommandType.Text;
            cmd5.ExecuteNonQuery();

            SqlCommand cmd6;
            cmd6 = new SqlCommand("delete from LoginTable where LoginID = " + login_id + ";", con);
            cmd6.CommandType = CommandType.Text;
            cmd6.ExecuteNonQuery();

            con.Close();

        }

        [Fact]
        public void CuandoSeInsertaCorrectamenteLaCita_DebeExistirUnMensajeQueNoSeaNulo()
        {

            var instancia = new myDAL();

            SqlConnection con = new SqlConnection(connString);
            con.Open();

            // Se inserta ejemplo

            SqlCommand cmd1;
            cmd1 = new SqlCommand("INSERT INTO LoginTable(Password, Email, Type) VALUES ('ejmplo7', 'ggggg', 1); SELECT SCOPE_IDENTITY();", con);
            cmd1.CommandType = CommandType.Text;
            var login_id = Convert.ToInt32(cmd1.ExecuteScalar());

            SqlCommand cmd2;
            cmd2 = new SqlCommand("insert into Patient(PatientID,Name,Phone,Address,BirthDate,Gender) values (" + login_id + ",'Max', '555','g','1990-05-09','M');", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();

            // Prueba

            string mes = null;
            int estado = instancia.insertAppointment(3, login_id, 5, ref mes);
            Assert.NotNull(mes);

            // Se quita de la base

            SqlCommand cmd4;
            cmd4 = new SqlCommand("delete from Appointment where DoctorID = 3 and PatientID = " + login_id + ";", con);
            cmd4.CommandType = CommandType.Text;
            cmd4.ExecuteNonQuery();

            SqlCommand cmd5;
            cmd5 = new SqlCommand("delete from Patient where PatientID = " + login_id + ";", con);
            cmd5.CommandType = CommandType.Text;
            cmd5.ExecuteNonQuery();

            SqlCommand cmd6;
            cmd6 = new SqlCommand("delete from LoginTable where LoginID = " + login_id + ";", con);
            cmd6.CommandType = CommandType.Text;
            cmd6.ExecuteNonQuery();

            con.Close();

        }

        [Fact]
        public void insertAppointmentFallaRetornaMenosUno()
        {

            var instancia = new myDAL();
            string mes = null;
            int estado = instancia.insertAppointment(888, 999, 6, ref mes);
            Assert.Equal(-1, estado);

        }

        [Fact]
        public void getNotificationsCuandoElPacienteTieneUnaCitaPeroLaNotificacionEstaMarcadaComoVistaRetornaCero()
        {
            var instancia = new myDAL();

            SqlConnection con = new SqlConnection(connString);
            con.Open();

            // Se inserta ejemplo

            SqlCommand cmd1;
            cmd1 = new SqlCommand("INSERT INTO LoginTable(Password, Email, Type) VALUES ('ejmplo5', 'eeeeee', 1); SELECT SCOPE_IDENTITY();", con);
            cmd1.CommandType = CommandType.Text;
            var login_id = Convert.ToInt32(cmd1.ExecuteScalar());

            SqlCommand cmd2;
            cmd2 = new SqlCommand("insert into Patient(PatientID,Name,Phone,Address,BirthDate,Gender) values (" + login_id + ",'Roy', '349','e','1990-07-08','M');", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();

            SqlCommand cmd3;
            cmd3 = new SqlCommand("insert into Appointment(DoctorID,PatientID,Date) values (3," + login_id + ",GETDATE());", con);
            cmd3.CommandType = CommandType.Text;
            cmd3.ExecuteNonQuery();

            SqlCommand cmd34;
            cmd34 = new SqlCommand("update Appointment set FeedbackStatus = 2 where PatientID = " + login_id + ";", con);
            cmd34.CommandType = CommandType.Text;
            cmd34.ExecuteNonQuery();

            SqlCommand cmd35;
            cmd35 = new SqlCommand("update Appointment set Appointment_Status = 3 where PatientID = " + login_id + ";", con);
            cmd35.CommandType = CommandType.Text;
            cmd35.ExecuteNonQuery();

            SqlCommand cmd36;
            cmd36 = new SqlCommand("update Appointment set PatientNotification = 1 where PatientID = " + login_id + ";", con);
            cmd36.CommandType = CommandType.Text;
            cmd36.ExecuteNonQuery();

            string dname = null;
            string timings = null;
            int estado = instancia.getNotifications(login_id, ref dname, ref timings);
            Assert.Equal(0, estado);

            SqlCommand cmd4;
            cmd4 = new SqlCommand("delete from Appointment where DoctorID = 3 and PatientID = " + login_id + ";", con);
            cmd4.CommandType = CommandType.Text;
            cmd4.ExecuteNonQuery();

            SqlCommand cmd5;
            cmd5 = new SqlCommand("delete from Patient where PatientID = " + login_id + ";", con);
            cmd5.CommandType = CommandType.Text;
            cmd5.ExecuteNonQuery();

            SqlCommand cmd6;
            cmd6 = new SqlCommand("delete from LoginTable where LoginID = " + login_id + ";", con);
            cmd6.CommandType = CommandType.Text;
            cmd6.ExecuteNonQuery();

            con.Close();



        }


        [Fact]
        public void getNotificationsCuandoElPacienteTieneUnaCitaEnEstadoTresRetornaTres()
        {
            var instancia = new myDAL();

            SqlConnection con = new SqlConnection(connString);
            con.Open();

            // Se inserta ejemplo

            SqlCommand cmd1;
            cmd1 = new SqlCommand("INSERT INTO LoginTable(Password, Email, Type) VALUES ('ejmplo3', 'cccccc', 1); SELECT SCOPE_IDENTITY();", con);
            cmd1.CommandType = CommandType.Text;
            var login_id = Convert.ToInt32(cmd1.ExecuteScalar());

            SqlCommand cmd2;
            cmd2 = new SqlCommand("insert into Patient(PatientID,Name,Phone,Address,BirthDate,Gender) values (" + login_id + ",'Ian', '789','c','1990-07-09','M');", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();

            SqlCommand cmd3;
            cmd3 = new SqlCommand("insert into Appointment(DoctorID,PatientID,Date) values (3," + login_id + ",GETDATE());", con);
            cmd3.CommandType = CommandType.Text;
            cmd3.ExecuteNonQuery();

            SqlCommand cmd34;
            cmd34 = new SqlCommand("update Appointment set FeedbackStatus = 2 where PatientID = " + login_id + ";", con);
            cmd34.CommandType = CommandType.Text;
            cmd34.ExecuteNonQuery();

            SqlCommand cmd35;
            cmd35 = new SqlCommand("update Appointment set Appointment_Status = 3 where PatientID = " + login_id + ";", con);
            cmd35.CommandType = CommandType.Text;
            cmd35.ExecuteNonQuery();

            SqlCommand cmd36;
            cmd36 = new SqlCommand("update Appointment set PatientNotification = 2 where PatientID = " + login_id + ";", con);
            cmd36.CommandType = CommandType.Text;
            cmd36.ExecuteNonQuery();

            string dname = null;
            string timings = null;
            int estado = instancia.getNotifications(login_id, ref dname, ref timings);
            Assert.Equal(3, estado);

            SqlCommand cmd4;
            cmd4 = new SqlCommand("delete from Appointment where DoctorID = 3 and PatientID = " + login_id + ";", con);
            cmd4.CommandType = CommandType.Text;
            cmd4.ExecuteNonQuery();

            SqlCommand cmd5;
            cmd5 = new SqlCommand("delete from Patient where PatientID = " + login_id + ";", con);
            cmd5.CommandType = CommandType.Text;
            cmd5.ExecuteNonQuery();

            SqlCommand cmd6;
            cmd6 = new SqlCommand("delete from LoginTable where LoginID = " + login_id + ";", con);
            cmd6.CommandType = CommandType.Text;
            cmd6.ExecuteNonQuery();

            con.Close();

        }

        [Fact]
        public void getNotificationsCuandoElPacienteTieneUnaCitaEnEstadoUnoRetornaUno()
        {

            var instancia = new myDAL();

            SqlConnection con = new SqlConnection(connString);
            con.Open();

            // Se inserta ejemplo

            SqlCommand cmd1;
            cmd1 = new SqlCommand("INSERT INTO LoginTable(Password, Email, Type) VALUES ('ejmplo4', 'dddddd', 1); SELECT SCOPE_IDENTITY();", con);
            cmd1.CommandType = CommandType.Text;
            var login_id = Convert.ToInt32(cmd1.ExecuteScalar());

            SqlCommand cmd2;
            cmd2 = new SqlCommand("insert into Patient(PatientID,Name,Phone,Address,BirthDate,Gender) values (" + login_id + ",'Pancho', '456','c','1990-07-05','M');", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();

            SqlCommand cmd3;
            cmd3 = new SqlCommand("insert into Appointment(DoctorID,PatientID,Date) values (3," + login_id + ",GETDATE());", con);
            cmd3.CommandType = CommandType.Text;
            cmd3.ExecuteNonQuery();

            SqlCommand cmd34;
            cmd34 = new SqlCommand("update Appointment set FeedbackStatus = 2 where PatientID = " + login_id + ";", con);
            cmd34.CommandType = CommandType.Text;
            cmd34.ExecuteNonQuery();

            SqlCommand cmd35;
            cmd35 = new SqlCommand("update Appointment set Appointment_Status = 1 where PatientID = " + login_id + ";", con);
            cmd35.CommandType = CommandType.Text;
            cmd35.ExecuteNonQuery();

            SqlCommand cmd36;
            cmd36 = new SqlCommand("update Appointment set PatientNotification = 2 where PatientID = " + login_id + ";", con);
            cmd36.CommandType = CommandType.Text;
            cmd36.ExecuteNonQuery();

            string dname = null;
            string timings = null;
            int estado = instancia.getNotifications(login_id, ref dname, ref timings);
            Assert.Equal(1, estado);

            SqlCommand cmd4;
            cmd4 = new SqlCommand("delete from Appointment where DoctorID = 3 and PatientID = " + login_id + ";", con);
            cmd4.CommandType = CommandType.Text;
            cmd4.ExecuteNonQuery();

            SqlCommand cmd5;
            cmd5 = new SqlCommand("delete from Patient where PatientID = " + login_id + ";", con);
            cmd5.CommandType = CommandType.Text;
            cmd5.ExecuteNonQuery();

            SqlCommand cmd6;
            cmd6 = new SqlCommand("delete from LoginTable where LoginID = " + login_id + ";", con);
            cmd6.CommandType = CommandType.Text;
            cmd6.ExecuteNonQuery();

            con.Close();


        }

        [Fact]
        public void getNotificationsCuandoNoEncuentraPacienteRetornaCero()
        {
            var instancia = new myDAL();
            string dname = null;
            string timings = null;
            int a_id = 0;
            int estado = instancia.isFeedbackPending(1234, ref dname, ref timings, ref a_id);
            Assert.Equal(0, estado);


        }

        [Fact]
        public void isFeedbackPendingCuandoExisteCitaYHayFeedbackPendiente()
        {

            var instancia = new myDAL();

            SqlConnection con = new SqlConnection(connString);
            con.Open();

            // Se inserta ejemplo

            SqlCommand cmd1;
            cmd1 = new SqlCommand("INSERT INTO LoginTable(Password, Email, Type) VALUES ('ejmplohola', 'aaaaaaaa', 1); SELECT SCOPE_IDENTITY();", con);
            cmd1.CommandType = CommandType.Text;
            var login_id = Convert.ToInt32(cmd1.ExecuteScalar());

            SqlCommand cmd2;
            cmd2 = new SqlCommand("insert into Patient(PatientID,Name,Phone,Address,BirthDate,Gender) values (" + login_id + ",'Pedro', '123','a','1990-09-09','M');", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();

            SqlCommand cmd3;
            cmd3 = new SqlCommand("insert into Appointment(DoctorID,PatientID,Date) values (3," + login_id + ",GETDATE());", con);
            cmd3.CommandType = CommandType.Text;
            cmd3.ExecuteNonQuery();

            SqlCommand cmd34;
            cmd34 = new SqlCommand("update Appointment set FeedbackStatus = 2 where PatientID = " + login_id + ";", con);
            cmd34.CommandType = CommandType.Text;
            cmd34.ExecuteNonQuery();

            SqlCommand cmd35;
            cmd35 = new SqlCommand("update Appointment set Appointment_Status = 3 where PatientID = " + login_id + ";", con);
            cmd35.CommandType = CommandType.Text;
            cmd35.ExecuteNonQuery();

            // Prueba

            string dname = null;
            string timings = null;
            int a_id = 0;
            int estado = instancia.isFeedbackPending(login_id, ref dname, ref timings, ref a_id);
            Assert.Equal(1, estado);

            // Se quita lo que se hizo

            SqlCommand cmd4;
            cmd4 = new SqlCommand("delete from Appointment where DoctorID = 3 and PatientID = " + login_id + ";", con);
            cmd4.CommandType = CommandType.Text;
            cmd4.ExecuteNonQuery();

            SqlCommand cmd5;
            cmd5 = new SqlCommand("delete from Patient where PatientID = " + login_id + ";", con);
            cmd5.CommandType = CommandType.Text;
            cmd5.ExecuteNonQuery();

            SqlCommand cmd6;
            cmd6 = new SqlCommand("delete from LoginTable where LoginID = " + login_id + ";", con);
            cmd6.CommandType = CommandType.Text;
            cmd6.ExecuteNonQuery();

            con.Close();

        }

        [Fact]
        public void isFeedbackPendingCuandoExisteCitaPeroNoHayFeedbackPendienteRetornaCero()
        {

            var instancia = new myDAL();

            SqlConnection con = new SqlConnection(connString);
            con.Open();

            // Se inserta ejemplo

            SqlCommand cmd1;
            cmd1 = new SqlCommand("INSERT INTO LoginTable(Password, Email, Type) VALUES ('ejemplo2', 'bbbbbb', 1); SELECT SCOPE_IDENTITY();", con);
            cmd1.CommandType = CommandType.Text;
            var login_id = Convert.ToInt32(cmd1.ExecuteScalar());

            SqlCommand cmd2;
            cmd2 = new SqlCommand("insert into Patient(PatientID,Name,Phone,Address,BirthDate,Gender) values (" + login_id + ",'Marco', '321','b','1990-08-08','M');", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();

            SqlCommand cmd3;
            cmd3 = new SqlCommand("insert into Appointment(DoctorID,PatientID,Date) values (3," + login_id + ",GETDATE());", con);
            cmd3.CommandType = CommandType.Text;
            cmd3.ExecuteNonQuery();

            // Prueba

            string dname = null;
            string timings = null;
            int a_id = 0;
            int estado = instancia.isFeedbackPending(login_id, ref dname, ref timings, ref a_id);
            Assert.Equal(0, estado);

            // Se quita lo que se hizo

            SqlCommand cmd4;
            cmd4 = new SqlCommand("delete from Appointment where DoctorID = 3 and PatientID = " + login_id + ";", con);
            cmd4.CommandType = CommandType.Text;
            cmd4.ExecuteNonQuery();

            SqlCommand cmd5;
            cmd5 = new SqlCommand("delete from Patient where PatientID = " + login_id + ";", con);
            cmd5.CommandType = CommandType.Text;
            cmd5.ExecuteNonQuery();

            SqlCommand cmd6;
            cmd6 = new SqlCommand("delete from LoginTable where LoginID = " + login_id + ";", con);
            cmd6.CommandType = CommandType.Text;
            cmd6.ExecuteNonQuery();

            con.Close();


        }

        [Fact]
        public void isFeedbackPendingCuandoNoExisteLaCitaRetornaCero()
        {

            var instancia = new myDAL();
            string dname = null;
            string timings = null;
            int a_id = 0;
            int estado = instancia.isFeedbackPending(3485, ref dname, ref timings, ref a_id);
            Assert.Equal(0, estado);

        }

        [Fact]
        public void givePendingFeedbackCuandoLaCitaExisteRetorna0()
        {
            SqlConnection con = new SqlConnection(connString);
            con.Open();

            // Se inserta ejemplo

            SqlCommand cmd1;
            cmd1 = new SqlCommand("insert into Appointment(DoctorID,PatientID) values (4,13);", con);
            cmd1.CommandType = CommandType.Text;
            var cita_id = Convert.ToInt32(cmd1.ExecuteScalar());


            var instancia = new myDAL();
            int estado = instancia.givePendingFeedback(cita_id);
            Assert.Equal(0, estado);

            // Se quita para mantener la base igual

            SqlCommand cmd2;
            cmd2 = new SqlCommand("delete from Appointment where DoctorID = 4 and PatientID = 13;", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();
            con.Close();

        }

        [Fact]
        public void givePendingFeedbackCuandoLaCitaNoExisteRetorna0()
        {
            var instancia = new myDAL();
            int estado = instancia.givePendingFeedback(888);
            Assert.Equal(0, estado);

        }

        [Fact]
        public void docinfo_DALCuandoElDoctorBuscadoExiste()
        {

            var instancia = new myDAL();
            var resultado = new DataTable();
            int estado = instancia.docinfo_DAL(3, ref resultado);
            Assert.Equal(1, estado);
            Assert.Equal(1, resultado.Rows.Count);

        }

        [Fact]
        public void docinfo_DALCuandoElDoctorBuscadoExisteSeTraeLaInformacionCorrecta()
        {

            var instancia = new myDAL();
            var resultado = new DataTable();
            int estado = instancia.docinfo_DAL(3, ref resultado);
            Assert.Equal(1, estado);
            Assert.Equal("KASHAN", resultado.Rows[0]["Name"].ToString());

        }

        [Fact]
        public void docinfo_DALCuandoElDoctorBuscadoNoExiste()
        {

            var instancia = new myDAL();
            var resultado = new DataTable();
            int estado = instancia.docinfo_DAL(78, ref resultado);
            Assert.Equal(1, estado);
            Assert.Equal(0, resultado.Rows.Count);

        }

    }

}


