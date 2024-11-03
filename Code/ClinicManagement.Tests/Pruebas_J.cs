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


        // ////////////////////////////////////////////////////////
        // Prueba 1
        // ID: 051
        // Nombre: Get Departamento Info retorna una datatable no nula cuando es exitoso
        // Descripción: Al pedir la información del departamento se retorna una tabla cuando hay datos y además el estado es 1 indicando que todo bien
        // Datos de prueba: 
        // Resultado esperado: Variable "resultado" no es nulo y variable "estado" es 1


        [Fact]
        public void GetDeptInfo_RetornaUnaDataTable_CuandoEsExitoso_YHayDatos()
        {

            var instancia = new myDAL();
            var resultado = new DataTable();
            int estado = instancia.getdeptInfo(ref resultado);
            Assert.Equal(1, estado);
            Assert.NotNull(resultado);
        }


        // ////////////////////////////////////////////////////////
        // Prueba 2
        // ID: 052
        // Nombre: getDeptDoctorInfo retorna una tabla no nula cuando el departamento existe
        // Descripción: Al solicitar la información de doctores para un departamento existente, se debe retornar una tabla con datos y el estado debe ser 1
        // Datos de prueba: Nombre del departamento "Dept99"
        // Resultado esperado: Variable "resultado" no es nula y variable "estado" es 1


        [Fact]
        public void getDeptDoctorInfo_RetornaUnaTabla_CuandoElDepartamentoExiste()
        {


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


        // ////////////////////////////////////////////////////////
        // Prueba 3
        // ID: 053
        // Nombre: getDeptDoctorInfo retorna una tabla con cero filas cuando el departamento existe pero no tiene doctores asociados
        // Descripción: Al solicitar información de doctores para un departamento que existe pero sin doctores, se debe retornar una tabla vacía
        // Datos de prueba: Nombre del departamento "Dept88"
        // Resultado esperado: Variable "resultado.Rows.Count" es 0


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


        // ////////////////////////////////////////////////////////
        // Prueba 4
        // ID: 054
        // Nombre: getDeptDoctorInfo retorna una tabla con todos los resultados cuando el departamento tiene doctores asociados
        // Descripción: Al solicitar información de doctores para un departamento que tiene doctores, se debe retornar una tabla con los datos correspondientes
        // Datos de prueba: Nombre del departamento "Dept77" y doctor con id 3
        // Resultado esperado: Variable "resultado.Rows.Count" es 1


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


        // ////////////////////////////////////////////////////////
        // Prueba 5
        // ID: 055
        // Nombre: getDeptDoctorInfo retorna una tabla vacía cuando el departamento no existe
        // Descripción: Al solicitar información de doctores para un departamento que no existe, se debe retornar una tabla vacía
        // Datos de prueba: Nombre del departamento "Animales"
        // Resultado esperado: Variable "resultado.Rows.Count" es 0


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


        // ////////////////////////////////////////////////////////
        // Prueba 6
        // ID: 056
        // Nombre: doctorInfoDisplayer funciona cuando el doctor existe
        // Descripción: Al solicitar información de un doctor existente, se deben retornar los datos correctamente y el estado debe ser 0
        // Datos de prueba: ID del doctor 3, el resto son variables para recibir el resultado
        // Resultado esperado: Variable "estado" es 0

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


        // ////////////////////////////////////////////////////////
        // Prueba 7
        // ID: 057
        // Nombre: doctorInfoDisplayer devuelve los datos exactamente como están en la base
        // Descripción: Al solicitar información de un doctor existente, se deben retornar los datos correctos y el estado debe ser 0
        // Datos de prueba: ID del doctor 3
        // Resultado esperado: Las variables devueltas contienen los datos correctos


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


        // ////////////////////////////////////////////////////////
        // Prueba 8
        // ID: 058
        // Nombre: getFreeSlots devuelve nueve cupos si el doctor no tiene ninguna cita
        // Descripción: Al consultar los cupos disponibles para un doctor sin citas, se debe retornar 9
        // Datos de prueba: ID del doctor 7, ID del paciente 12
        // Resultado esperado: Variable "estado" es 9


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

        // ////////////////////////////////////////////////////////
        // Prueba 9
        // ID: 059
        // Nombre: getFreeSlots devuelve nueve aun cuando hay IDs sin sentido y horas mayores a veinticuatro
        // Descripción: Al consultar los cupos con datos inválidos, se debe seguir retornando 9
        // Datos de prueba: ID del doctor 99, ID del paciente 88 (No existen en la base) y una hora 43
        // Resultado esperado: Variable "estado" es 9


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

        // ////////////////////////////////////////////////////////
        // Prueba 10
        // ID: 060
        // Nombre: insertAppointment funciona cuando todos los parámetros están correctos y retorna cero
        // Descripción: Al insertar una cita con todos los parámetros válidos, debe retornar 0
        // Datos de prueba: ID del doctor 3, contraseña ejemplo6, correo fffffff, tipo 1, nombre Chax, phone 777, address f,
        // birthdate 1990-05-09, Gender M
        // Resultado esperado: Variable "estado" es 0

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

        // ////////////////////////////////////////////////////////
        // Prueba 11
        // ID: 061
        // Nombre: Cuando se inserta correctamente la cita, debe existir un mensaje que no sea nulo
        // Descripción: Al insertar una cita con todos los parámetros válidos, debe generar un mensaje que indique éxito
        // Datos de prueba: ID del doctor 3, contraseña ejemplo7, correo ggggg, tipo 1, nombre Max, teléfono 555, dirección g,
        // fecha de nacimiento 1990-05-09, género M
        // Resultado esperado: Variable "mes" no es nula


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


        // ////////////////////////////////////////////////////////
        // Prueba 12
        // ID: 062
        // Nombre: insertAppointment falla y retorna menos uno
        // Descripción: Al intentar insertar una cita con IDs inválidos, debe retornar -1
        // Datos de prueba: ID del doctor 888, ID del paciente 999, hora 6
        // Resultado esperado: Variable "estado" es -1


        [Fact]
        public void insertAppointmentFallaRetornaMenosUno()
        {

            var instancia = new myDAL();
            string mes = null;
            int estado = instancia.insertAppointment(888, 999, 6, ref mes);
            Assert.Equal(-1, estado);

        }


        // ////////////////////////////////////////////////////////
        // Prueba 13
        // ID: 063
        // Nombre: getNotifications cuando el paciente tiene una cita pero la notificación está marcada como vista retorna cero
        // Descripción: Al consultar las notificaciones de un paciente cuya cita está marcada como vista, debe retornar cero
        // Datos de prueba: ID del doctor 3, contraseña ejemplo5, correo eeeeee, tipo 1, nombre Roy, teléfono 349, dirección e,
        // fecha de nacimiento 1990-07-08, género M
        // Resultado esperado: Variable "estado" es 0


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

            // Prueba

            string dname = null;
            string timings = null;
            int estado = instancia.getNotifications(login_id, ref dname, ref timings);
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

        // ////////////////////////////////////////////////////////
        // Prueba 14
        // ID: 064
        // Nombre: getNotifications cuando el paciente tiene una cita en estado tres retorna tres
        // Descripción: Al consultar las notificaciones de un paciente cuya cita está en estado tres, debe retornar tres
        // Datos de prueba: ID del doctor 3, contraseña ejemplo3, correo cccccc, tipo 1, nombre Ian, teléfono 789, dirección c,
        // fecha de nacimiento 1990-07-09, género M
        // Resultado esperado: Variable "estado" es 3


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

            // Prueba

            string dname = null;
            string timings = null;
            int estado = instancia.getNotifications(login_id, ref dname, ref timings);
            Assert.Equal(3, estado);

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


        // ////////////////////////////////////////////////////////
        // Prueba 15
        // ID: 065
        // Nombre: getNotifications cuando el paciente tiene una cita en estado uno retorna uno
        // Descripción: Al consultar las notificaciones de un paciente cuya cita está en estado uno, debe retornar uno
        // Datos de prueba: ID del doctor 3, contraseña ejemplo4, correo dddddd, tipo 1, nombre Pancho, teléfono 456, dirección c,
        // fecha de nacimiento 1990-07-05, género M
        // Resultado esperado: Variable "estado" es 1


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

            // Prueba

            string dname = null;
            string timings = null;
            int estado = instancia.getNotifications(login_id, ref dname, ref timings);
            Assert.Equal(1, estado);

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


        // ////////////////////////////////////////////////////////
        // Prueba 16
        // ID: 066
        // Nombre: getNotifications cuando no encuentra paciente retorna cero
        // Descripción: Al consultar las notificaciones de un paciente que no existe, debe retornar cero
        // Datos de prueba: ID no existente 1234
        // Resultado esperado: Variable "estado" es 0


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


        // ////////////////////////////////////////////////////////
        // Prueba 17
        // ID: 067
        // Nombre: isFeedbackPending cuando existe cita y hay feedback pendiente
        // Descripción: Al consultar el feedback de un paciente cuya cita tiene un feedback pendiente, debe retornar uno
        // Datos de prueba: ID del doctor 3, contraseña ejmplohola, correo aaaaaaaa, tipo 1, nombre Pedro, teléfono 123, dirección a,
        // fecha de nacimiento 1990-09-09, género M
        // Resultado esperado: Variable "estado" es 1


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


        // ////////////////////////////////////////////////////////
        // Prueba 18
        // ID: 068
        // Nombre: isFeedbackPending cuando existe cita pero no hay feedback pendiente retorna cero
        // Descripción: Al consultar el feedback de un paciente cuya cita no tiene feedback pendiente, debe retornar cero
        // Datos de prueba: ID del doctor 3, contraseña ejemplo2, correo bbbbbb, tipo 1, nombre Marco, teléfono 321, dirección b,
        // fecha de nacimiento 1990-08-08, género M
        // Resultado esperado: Variable "estado" es 0


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


        // ////////////////////////////////////////////////////////
        // Prueba 19
        // ID: 069
        // Nombre: isFeedbackPending cuando no existe la cita retorna cero
        // Descripción: Al consultar el feedback de una cita que no existe, debe retornar cero
        // Datos de prueba: ID de cita no existente 3485
        // Resultado esperado: Variable "estado" es 0


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


        // ////////////////////////////////////////////////////////
        // Prueba 20
        // ID: 070
        // Nombre: givePendingFeedback cuando la cita existe retorna 0
        // Descripción: Al dar feedback de una cita existente, debe retornar 0 indicando éxito
        // Datos de prueba: ID del doctor 4, ID del paciente 13
        // Resultado esperado: Variable "estado" es 0


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


        // ////////////////////////////////////////////////////////
        // Prueba 21
        // ID: 071
        // Nombre: givePendingFeedback cuando la cita no existe retorna 0
        // Descripción: Al dar feedback de una cita que no existe, debe retornar 0
        // Datos de prueba: ID de cita no existente 888
        // Resultado esperado: Variable "estado" es 0


        [Fact]
        public void givePendingFeedbackCuandoLaCitaNoExisteRetorna0()
        {
            var instancia = new myDAL();
            int estado = instancia.givePendingFeedback(888);
            Assert.Equal(0, estado);

        }


        // ////////////////////////////////////////////////////////
        // Prueba 22
        // ID: 072
        // Nombre: docinfo_DAL cuando el doctor buscado existe
        // Descripción: Al consultar la información de un doctor existente, debe retornar uno y obtener información válida
        // Datos de prueba: ID del doctor 3
        // Resultado esperado: Variable "estado" es 1 y la cantidad es 1


        [Fact]
        public void docinfo_DALCuandoElDoctorBuscadoExiste()
        {

            var instancia = new myDAL();
            var resultado = new DataTable();
            int estado = instancia.docinfo_DAL(3, ref resultado);
            Assert.Equal(1, estado);
            Assert.Equal(1, resultado.Rows.Count);

        }


        // ////////////////////////////////////////////////////////
        // Prueba 23
        // ID: 073
        // Nombre: docinfo_DAL cuando el doctor buscado existe se trae la información correcta
        // Descripción: Al consultar la información de un doctor existente, debe retornar uno y verificar que la información sea correcta
        // Datos de prueba: ID del doctor 3
        // Resultado esperado: Variable "estado" es 1 y el nombre es "KASHAN"


        [Fact]
        public void docinfo_DALCuandoElDoctorBuscadoExisteSeTraeLaInformacionCorrecta()
        {

            var instancia = new myDAL();
            var resultado = new DataTable();
            int estado = instancia.docinfo_DAL(3, ref resultado);
            Assert.Equal(1, estado);
            Assert.Equal("KASHAN", resultado.Rows[0]["Name"].ToString());

        }

        // ////////////////////////////////////////////////////////
        // Prueba 24
        // ID: 074
        // Nombre: docinfo_DAL cuando el doctor buscado no existe
        // Descripción: Al consultar la información de un doctor que no existe, debe retornar uno y no devolver filas
        // Datos de prueba: ID del doctor 78
        // Resultado esperado: Variable "estado" es 1 y la cantidad de filas es 0

        [Fact]
        public void docinfo_DALCuandoElDoctorBuscadoNoExiste()
        {

            var instancia = new myDAL();
            var resultado = new DataTable();
            int estado = instancia.docinfo_DAL(78, ref resultado);
            Assert.Equal(1, estado);
            Assert.Equal(0, resultado.Rows.Count);

        }


        // ////////////////////////////////////////////////////////
        // Prueba 25
        // ID: 075
        // Nombre: getDeptDoctorInfo retorna una tabla vacía cuando el departamento es una cadena vacía
        // Descripción: Al solicitar información de doctores para un departamento que es una cadena vacía, se debe retornar una tabla vacía
        // Datos de prueba: Nombre del departamento ""
        // Resultado esperado: Variable "resultado.Rows.Count" es 0


        [Fact]
        public void getDeptDoctorInfo_RetornaUnaTablaVacia_CuandoElDepartamentoEsUnaCadenaVacia()
        {
            var instancia = new myDAL();
            var resultado = new DataTable();
            var departamento = "";
            int estado = instancia.getDeptDoctorInfo(departamento, ref resultado);
            Assert.Equal(1, estado);
            Assert.Equal(0, resultado.Rows.Count);
        }



    }

}