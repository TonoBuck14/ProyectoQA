using DBProject;
using DBProject.DAL;
using Microsoft.Data.SqlClient;
using System.Data;
namespace ClinicManagement.Tests
{
    [CollectionDefinition("PruebasAntonio", DisableParallelization = true)]
    public class Pruebas_A
    {
        
        private static readonly string connString = "Data Source=.\\SQLEXPRESS; Initial Catalog=DBProject; Integrated Security=True; TrustServerCertificate=True";


        // 1. Login exitoso

        // ////////////////////////////////////////////////////////
        // Prueba 1
        // ID: 001
        // Nombre: Login exitoso retorna 0
        // Descripción: AL hacer con éxito un login el método retorna 0
        // Datos de prueba: el correo y la contraseña de un usuario administrador que esta en la base de datos
        // Resultado esperado: 0
        [Fact]
        public void loginExitosoDevuelveUnEntero0()
        {
            //se usa un usuario que ya existía en la base de datos
            var email = "admin@clinic.com";
            var password = "admin";
            int type = 3;
            int id = 1;



            var dal = new myDAL();


            int result = dal.validateLogin(email, password, ref type, ref id);


            Assert.Equal(0, result);

        }

        // 2. Login fallido

        // ////////////////////////////////////////////////////////
        // Prueba 2
        // ID: 002
        // Nombre: Login fallido retorna 1
        // Descripción: AL hacer un login fallido el método retorna 1
        // Datos de prueba: el correo y la contraseña de un usuario que no existe en la base de datos
        // Resultado esperado: 1
        [Fact]
        public void loginFallidoDevuelveUnEntero1()
        {
            //se usa un usuario que no existía en la base de datos
            var email = "error@error.com";
            var password = "admin";
            int type = 3;
            int id = 15;
            var dal = new myDAL();

            var valorMalo = dal.validateLogin(email, password, ref type, ref id);

            Assert.Equal(1, valorMalo);



        }

        // 3. Login fallido

        // ////////////////////////////////////////////////////////
        // Prueba 3
        // ID: 003
        // Nombre: Login fallido con email largo retorna 1
        // Descripción: AL hacer un login con email largo el método retorna 1
        // Datos de prueba: el correo largo y la contraseña de un usuario que no existe en la base de datos
        // Resultado esperado: 1
        [Fact]
        public void unEmailLargoSinContraseniaRetorna1()
        {
            //se usa un usuario que no existía en la base de datos
            var email = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa@aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa.com";
            var password = "";
            int type = -3;
            int id = -15;

            var dal = new myDAL();

            var resultado = dal.validateLogin(email, password, ref type, ref id);
            Assert.Equal(1, resultado);

        }

        // 4. Validar usuario existente
        // ////////////////////////////////////////////////////////
        // Prueba 4
        // ID: 004
        // Nombre: Validación de usuario existente retorna 0
        // Descripción: Al validar un usuario existente, el método retorna 0.
        // Datos de prueba: Información del usuario que ya está en la base de datos.
        // Resultado esperado: 0
        [Fact]
        public void validarUsuarioExistenteRetorna0()
        {
            //se usa un usuario que ya existía en la base de datos
            var dal = new myDAL(); 
            var name = "Shariq";
            var birthDate = "01-01-1990"; 
            var email = "shariq@gmail.com"; //el email es lo unico que se revisa
            var password = "123";
            var phoneNo = "123456789012345"; 
            var gender = "M"; 
            var address = "";
            int id = 0;

            
            var result = dal.validateUser(name, birthDate, email, password, phoneNo, gender, address, ref id);

            
            Assert.Equal(0, result); 
        }


        // 5. Validación de formato de fecha incorrecto
        // ////////////////////////////////////////////////////////
        // Prueba 5
        // ID: 005
        // Nombre: Formato de fecha incorrecto en validación de usuario
        // Descripción: Al ingresar una fecha con formato incorrecto, el método debe retornar -1.
        // Datos de prueba: Usuario nuevo con fecha en formato "01/01/1990".
        // Resultado esperado: -1
        [Fact]
        public void formatodeFechaConSlashRetorna1negativo()
        {
            //se usa un usuario que no existía en la base de datos
            var dal = new myDAL();
            var name = "Shariq Latysh";
            var birthDate = "01/01/1990";
            var email = "correoRandom11@gmail.com"; //el email es lo unico que se revisa
            var password = "123";
            var phoneNo = "123456789012345";
            var gender = "M";
            var address = "";
            int id = 0;


            var result = dal.validateUser(name, birthDate, email, password, phoneNo, gender, address, ref id);


            Assert.Equal(-1, result);
            //borrar el valor de la base de datos
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd2;
            cmd2 = new SqlCommand("DELETE FROM [DBProject].[dbo].[LoginTable] WHERE Email = 'correoRandom11@gmail.com';", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();
            con.Close();
            
            

        }

        // 6. Formato de fecha incorrecto
        // ////////////////////////////////////////////////////////
        // Prueba 6
        // ID: 006
        // Nombre: Formato de fecha incorrecto en validación de usuario
        // Descripción: Se pone un usuaario con una fecha invalida
        // Datos de prueba: El usuario y la fecha 1990-01-01
        // Resultado esperado: -1
        [Fact]
        public void formatodeFechaIncorrectoRetorna1negativo()
        {
            //se usa un usuario que no existía en la base de datos
            var dal = new myDAL();
            var name = "Shariq Laton";
            var birthDate = "1990-01-01";
            var email = "correoRandom22@gmail.com"; //el email es lo unico que se revisa
            var password = "123";
            var phoneNo = "123456789012345";
            var gender = "M";
            var address = "";
            int id = 0;


            var result = dal.validateUser(name, birthDate, email, password, phoneNo, gender, address, ref id);

            
            Assert.Equal(-1, result);
            //borrar el valor de la base de datos
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd2;
            cmd2 = new SqlCommand("DELETE FROM [DBProject].[dbo].[LoginTable] WHERE Email = 'correoRandom22@gmail.com';", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();
            con.Close();

        }


        // 7. Un doctor con email ya existente en la base de datos
        // ////////////////////////////////////////////////////////
        // Prueba 7
        // ID: 007
        // Nombre: Verificar email de doctor existente en la base de datos retorna 1
        // Descripción: Al verificar un email de doctor que ya existe en la base de datos, el método debe retornar 1.
        // Datos de prueba: email: hassaan@gmail.com
        // Resultado esperado: 1
        [Fact]
        public void correoDeDoctorYaExisteRetorna1()
        {
            string email = "hassaan@gmail.com"; //email de doctor que ya existe
            var dal = new myDAL();
            var result = dal.DoctorEmailAlreadyExist(email);
            Assert.Equal(1, result);
        }

        // 8. Correo de doctor no existente en la base de datos
        // ////////////////////////////////////////////////////////
        // Prueba 8
        // ID: 008
        // Nombre: Verificar email de doctor no existente en la base de datos retorna 0
        // Descripción: Al verificar un email de doctor que no existe en la base de datos, el método debe retornar 0.
        // Datos de prueba: email: correoquenoexiste@gmail.com
        // Resultado esperado: 0
        [Fact]
        public void correoDeDoctorNoExistenteRetorna0()
        {
            string email = "correoquenoexiste@gmail.com"; //email de doctor que ya existe
            var dal = new myDAL();
            var result = dal.DoctorEmailAlreadyExist(email);
            Assert.Equal(0, result);
        }

        // 9. Correo vacío
        // ////////////////////////////////////////////////////////
        // Prueba 9
        // ID: 009
        // Nombre: Verificar email vacío retorna 0
        // Descripción: Al verificar un email vacío, el método debe retornar 0.
        // Datos de prueba: email: ""
        // Resultado esperado: 0
        [Fact]
        public void correoVacioRetorna0()
        {
            string email = ""; //email de doctor que ya existe
            var dal = new myDAL();
            var result = dal.DoctorEmailAlreadyExist(email);
            Assert.Equal(0, result);
        }

        // 10. Doctor con nombre retorna 1
        // ////////////////////////////////////////////////////////
        // Prueba 10
        // ID: 010
        // Nombre: Agregar doctor con nombre retorna 1
        // Descripción: Al agregar un doctor con nombre, el método debe retornar 1.
        // Datos de prueba:
        // nombre: SantiDoc
        // email: santidoctor@gmail.com
        // password: 12345
        // birthDate: 01-01-1989
        // dept: 1
        // phoneNo: 12345678901
        // gender: M
        // address: Costa Rica
        // experience: 5
        // salary: 5000
        // charges: 500
        // speciality: Heart
        // qual: Master
        // Resultado esperado: 1
        [Fact]
        public void agregarDoctorConNombreRetorna1()
        {
            var dal = new myDAL();
            var name = "SantiDoc";
            var email = "santidoctor@gmail.com";
            var password = "12345";
            var birthDate = "01-01-1989";
            var dept = 1;
            var phoneNo = "12345678901";
            var gender = 'M';
            var address = "Costa Rica";
            var experience = 5;
            var salary = 5000;
            var charges = 500;
            var speciality = "Heart";
            var qual = "Master";
            dal.AddDoctor(name, email, password, birthDate, dept, phoneNo, gender, address, experience, salary, charges, speciality, qual);
            var result = dal.DoctorEmailAlreadyExist(email);
            Assert.Equal(1, result); //esto verifica que si se añadio el doctor

            //se borra de la tabla login
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd2;
            cmd2 = new SqlCommand("DELETE FROM [DBProject].[dbo].[Doctor] WHERE Name = 'SantiDoc';", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();
            con.Close();


            con = new SqlConnection(connString);
            con.Open();
            cmd2 = new SqlCommand("DELETE FROM [DBProject].[dbo].[LoginTable] WHERE Email = 'santidoctor@gmail.com';", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();
            con.Close();



        }

        // 11. Doctor con nombre de barras retorna 1
        // ////////////////////////////////////////////////////////
        // Prueba 11
        // ID: 011
        // Nombre: Agregar doctor con nombre de barras retorna 1
        // Descripción: Al agregar un doctor con nombre de barras, el método debe retornar 1.
        // Datos de prueba:
        // nombre: ////
        // email:
        // password: 12345
        // birthDate: 01-01-1989
        // dept: 1
        // phoneNo: 12345678901
        // gender: M
        // address: Costa Rica
        // experience: 5
        // salary: 5000
        // charges: 500
        // speciality: Heart
        // qual: Master
        // Resultado esperado: 1
        [Fact]
        public void agregarDoctorConNombreDeBarrasRetorna1()
        {
            var dal = new myDAL();
            var name = "////";
            var email = "barras@gmail.com";
            var password = "12345";
            var birthDate = "01-01-1989";
            var dept = 1;
            var phoneNo = "12345678901";
            var gender = 'M';
            var address = "Costa Rica";
            var experience = 5;
            var salary = 5000;
            var charges = 500;
            var speciality = "Heart";
            var qual = "Master";
            dal.AddDoctor(name, email, password, birthDate, dept, phoneNo, gender, address, experience, salary, charges, speciality, qual);
            var result = dal.DoctorEmailAlreadyExist(email);
            Assert.Equal(1, result); //esto verifica que si se añadio el doctor

            //se borra de la tabla login
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd2;
            cmd2 = new SqlCommand("DELETE FROM [DBProject].[dbo].[Doctor] WHERE Name = '////';", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();
            con.Close();


            con = new SqlConnection(connString);
            con.Open();
            cmd2 = new SqlCommand("DELETE FROM [DBProject].[dbo].[LoginTable] WHERE Email = 'barras@gmail.com';", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();
            con.Close();



        }


        // 12. Doctor sin qual retorna 1
        // ////////////////////////////////////////////////////////
        // Prueba 12
        // ID: 012
        // Nombre: Agregar doctor sin qual retorna 1
        // Descripción: Al agregar un doctor sin qual, el método debe retornar 1.
        // Datos de prueba:
        // nombre: SinQual
        // email: noqual@gmail.com
        // password: 12345
        // birthDate: 01-01-1989
        // dept: 1
        // phoneNo: 12345678901
        // gender: M
        // address: Costa Rica
        // experience: 5
        // salary: 5000
        // charges: 500
        // speciality: Heart
        // qual: 
        // Resultado esperado: 1
        [Fact]
        public void agregarDoctorSinQualRetorna1()
        {
            var dal = new myDAL();
            var name = "SinQual";
            var email = "noqual@gmail.com";
            var password = "12345";
            var birthDate = "01-01-1989";
            var dept = 1;
            var phoneNo = "12345678901";
            var gender = 'M';
            var address = "Costa Rica";
            var experience = 5;
            var salary = 5000;
            var charges = 500;
            var speciality = "Heart";
            var qual = "";
            dal.AddDoctor(name, email, password, birthDate, dept, phoneNo, gender, address, experience, salary, charges, speciality, qual);
            var result = dal.DoctorEmailAlreadyExist(email);
            Assert.Equal(1, result); //esto verifica que si se añadio el doctor

            //se borra de la tabla login
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd2;
            cmd2 = new SqlCommand("DELETE FROM [DBProject].[dbo].[Doctor] WHERE Name = 'SinQual';", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();
            con.Close();


            con = new SqlConnection(connString);
            con.Open();
            cmd2 = new SqlCommand("DELETE FROM [DBProject].[dbo].[LoginTable] WHERE Email = 'noqual@gmail.com';", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();
            con.Close();



        }


        // 13. Staff con nombre retorna 1
        // ////////////////////////////////////////////////////////
        // Prueba 13
        // ID: 013
        // Nombre: Agregar staff con nombre retorna 1
        // Descripción: Al agregar un staff con nombre, el método debe retornar 1.
        // Datos de prueba:
        // nombre: StaffEjemplo
        // birthDate: 01-01-1990
        // phone: 12345678901
        // gender: M
        // address: Costa Rica
        // salary: 5000
        // qual: Master
        // designation: Receptionist
        // Resultado esperado: 1

        [Fact]
        public void agregarstaffConNombreRetorna1()
        {
            //public int AddStaff(string Name, string BirthDate, string Phone, char gender, string Address, int salary, string Qual, string Designation)
            var dal = new myDAL();
            var name = "StaffEjemplo";
            var birthDate = "01-01-1990";
            var phone = "12345678901";
            var gender = 'M';
            var address = "Costa Rica";
            var salary = 5000;
            var qual = "Master";
            var designation = "Receptionist";

            var result = dal.AddStaff(name, birthDate, phone, gender, address, salary, qual, designation);
            Assert.Equal(1, result); //se verifica que se añade el staff

            //se borra de la tabla login
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd2;
            cmd2 = new SqlCommand("DELETE FROM [DBProject].[dbo].[OtherStaff] WHERE Name = 'StaffEjemplo';", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();
            con.Close();

        }

        // 14. Staff sin salario retorna 1
        // ////////////////////////////////////////////////////////
        // Prueba 14
        // ID: 014
        // Nombre: Agregar staff sin salario retorna 1
        // Descripción: Al agregar un staff sin salario, el método debe retornar 1.
        // Datos de prueba:
        // nombre: StaffNoSalario
        // birthDate: 01-01-1990
        // phone: 12345678901
        // gender: M
        // address: Costa Rica
        // salary: 0
        // qual: Master
        // designation: Receptionist
        // Resultado esperado: 1
        [Fact]
        public void agregarstaffSinSalarioRetorna1 ()
        {
            //public int AddStaff(string Name, string BirthDate, string Phone, char gender, string Address, int salary, string Qual, string Designation)
            var dal = new myDAL();
            var name = "StaffNoSalario";
            var birthDate = "01-01-1990";
            var phone = "12345678901";
            var gender = 'M';
            var address = "Costa Rica";
            var salary = 0;
            var qual = "Master";
            var designation = "Receptionist";

            var result = dal.AddStaff(name, birthDate, phone, gender, address, salary, qual, designation);
            Assert.Equal(1, result); //se verifica que se añade el staff

            //se borra de la tabla login
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd2;
            cmd2 = new SqlCommand("DELETE FROM [DBProject].[dbo].[OtherStaff] WHERE Name = 'StaffNoSalario';", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();
            con.Close();

        }

        // 15. Staff con numero de tel alfanumerico retorna 1
        // ////////////////////////////////////////////////////////
        // Prueba 15
        // ID: 015
        // Nombre: Agregar staff con numero de tel alfanumerico retorna 1
        // Descripción: Al agregar un staff con numero de tel alfanumerico, el método debe retornar 1.
        // Datos de prueba:
        // nombre: StaffAlfanum
        // birthDate: 01-01-1990
        // phone: 1qw45678901
        // gender: M
        // address: Costa Rica
        // salary: 0
        // qual: Master
        // designation: Receptionist
        // Resultado esperado: 1
        [Fact]
        public void staffConNumeroDeTelefonoAlfanumericoRetorna1()
        {
            //public int AddStaff(string Name, string BirthDate, string Phone, char gender, string Address, int salary, string Qual, string Designation)
            var dal = new myDAL();
            var name = "StaffAlfanum";
            var birthDate = "01-01-1990";
            var phone = "1qw45678901";
            var gender = 'M';
            var address = "Costa Rica";
            var salary = 0;
            var qual = "Master";
            var designation = "Receptionist";

            var result = dal.AddStaff(name, birthDate, phone, gender, address, salary, qual, designation);
            Assert.Equal(1, result); //se verifica que se añade el staff

            //se borra de la tabla login
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd2;
            cmd2 = new SqlCommand("DELETE FROM [DBProject].[dbo].[OtherStaff] WHERE Name = 'StaffAlfanum';", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();
            con.Close();

        }

        // 16. Staff sin nombre retorna 1
        // ////////////////////////////////////////////////////////
        // Prueba 16
        // ID: 016
        // Nombre: Agregar staff sin nombre retorna 1
        // Descripción: Al agregar un staff sin nombre, el método debe retornar 1.
        // Datos de prueba:
        // nombre:
        // birthDate: 01-01-1990
        // phone: 1qw45678901
        // gender: M
        // address: Costa Rica
        // salary: 0
        // qual: Master
        // designation: Receptionist
        // Resultado esperado: 1
        [Fact]
        public void staffSinNombreRetorna1()
        {
            //public int AddStaff(string Name, string BirthDate, string Phone, char gender, string Address, int salary, string Qual, string Designation)
            var dal = new myDAL();
            var name = "";
            var birthDate = "01-01-1990";
            var phone = "1qw45678901";
            var gender = 'M';
            var address = "Costa Rica";
            var salary = 0;
            var qual = "Master";
            var designation = "Receptionist";

            var result = dal.AddStaff(name, birthDate, phone, gender, address, salary, qual, designation);
            Assert.Equal(1, result); //se verifica que se añade el staff

            //se borra de la tabla login
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd2;
            cmd2 = new SqlCommand("DELETE FROM [DBProject].[dbo].[OtherStaff] WHERE Name = '';", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();
            con.Close();

        }

        // 17. Staff sin address retorna 1
        // ////////////////////////////////////////////////////////
        // Prueba 17
        // ID: 017
        // Nombre: Agregar staff sin address retorna 1
        // Descripción: Al agregar un staff sin address, el método debe retornar 1.
        // Datos de prueba:
        // nombre: SinAddress
        // birthDate: 01-01-1990
        // phone: 1qw45678901
        // gender: M
        // address:
        // salary: 0
        // qual: Master
        // designation: Receptionist
        // Resultado esperado: 1
        [Fact]
        public void staffSinAddressRetorna1()
        {
            //public int AddStaff(string Name, string BirthDate, string Phone, char gender, string Address, int salary, string Qual, string Designation)
            var dal = new myDAL();
            var name = "SinAddress";
            var birthDate = "01-01-1990";
            var phone = "1qw45678901";
            var gender = 'M';
            var address = "";
            var salary = 0;
            var qual = "Master";
            var designation = "Receptionist";

            var result = dal.AddStaff(name, birthDate, phone, gender, address, salary, qual, designation);
            Assert.Equal(1, result); //se verifica que se añade el staff

            //se borra de la tabla login
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd2;
            cmd2 = new SqlCommand("DELETE FROM [DBProject].[dbo].[OtherStaff] WHERE Name = 'SinAddress';", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();
            con.Close();

        }

        // 18. Staff con formato de fecha incorrecto retorna 1
        // ////////////////////////////////////////////////////////
        // Prueba 18
        // ID: 018
        // Nombre: Agregar staff con formato de fecha incorrecto retorna 1
        // Descripción: Al agregar un staff con formato de fecha incorrecto, el método debe retornar 1.
        // Datos de prueba:
        // nombre: FechaMala
        // birthDate: 90-01-01
        // phone: 1qw45678901
        // gender: M
        // address: Costa Rica
        // salary: 0
        // qual: Master
        // designation: Receptionist
        // Resultado esperado: 1
        [Fact]
        public void staffconFormatoDeFechaIncorrectoRetorna1negativo()
        {
            //public int AddStaff(string Name, string BirthDate, string Phone, char gender, string Address, int salary, string Qual, string Designation)
            var dal = new myDAL();
            var name = "FechaMala";
            var birthDate = "90-01-01";
            var phone = "1qw45678901";
            var gender = 'M';
            var address = "Costa Rica";
            var salary = 0;
            var qual = "Master";
            var designation = "Receptionist";

            var result = dal.AddStaff(name, birthDate, phone, gender, address, salary, qual, designation);
            Assert.Equal(-1, result); //se verifica que se añade el staff

            

        }

        // 19. Get admin home retorna un DataTable no vacío
        // ////////////////////////////////////////////////////////
        // Prueba 19
        // ID: 019
        // Nombre: Get admin home retorna un DataTable no vacío
        // Descripción: Al obtener la información de la página de inicio del administrador, el método debe retornar un DataTable no vacío.
        // Datos de prueba:
        // Resultado esperado: DataTable no vacío
        [Fact]
        public void getAdminHomeRetornaUnDTNoVacio()
        {
            var dal = new myDAL();
            DataTable[] data = new DataTable[5];
            for (int i = 0; i < 5; i++)
            {
                data[i] = new DataTable();
            }
            dal.GetAdminHomeInformation(ref data);
            Assert.NotEmpty(data);

        }

        // 20. Borrar doctor nuevo retorna 1
        // ////////////////////////////////////////////////////////
        // Prueba 20
        // ID: 020
        // Nombre: Borrar doctor nuevo retorna 1
        // Descripción: Al borrar un doctor nuevo, el método debe retornar 1.
        // Datos de prueba: insercion creada
        // Resultado esperado: 1
        [Fact]
        public void borrarDoctorNuevoRetorna1()
        {

            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd2;
            cmd2 = new SqlCommand("INSERT INTO [DBProject].[dbo].[DOCTOR] (DoctorID, Name, Phone, Address," +
                " BirthDate, Gender, DeptNo, Charges_Per_Visit, MonthlySalary, ReputeIndex, Patients_Treated," +
                " Qualification, Specialization, Work_Experience, status) VALUES (245, 'Borrable', '12345678901', 'Costa Rica', '01-01-1990', 'M', 1, " +
                "100.0, 5000.0, 4.5, 0, 'Master', 'Cardiology', 10, 1) ;", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();
            con.Close();

            var dal = new myDAL();
            var result = dal.DeleteDoctor(245);
            Assert.Equal(1, result);


        }

        // 21. Borrar doctor inexistente retorna -1
        // ////////////////////////////////////////////////////////
        // Prueba 21
        // ID: 021
        // Nombre: Borrar doctor inexistente retorna -1
        // Descripción: Al borrar un doctor inexistente, el método debe retornar -1.
        // Datos de prueba: id de doctor inexistente
        // Resultado esperado: -1
        // da error la prueba, porque retorna 1 en vez de -1, pero debería retornar -1
        [Fact]
        public void borrarDoctorInexistenteRetorna1Negativo()
        {
            var dal = new myDAL();
            var result = dal.DeleteDoctor(0);
            Assert.Equal(-1, result);
        }

        // 22. Borrar staff nuevo retorna 1
        // ////////////////////////////////////////////////////////
        // Prueba 22
        // ID: 022
        // Nombre: Borrar staff nuevo retorna 1
        // Descripción: Al borrar un staff nuevo, el método debe retornar 1.
        // Datos de prueba: insercion creada
        // Resultado esperado: 1
        [Fact]
        public void borrarStaffNuevoRetorna1()
        {

            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd2;
            cmd2 = new SqlCommand("DBCC CHECKIDENT ('OtherStaff', RESEED, 544);", con); //hace un reseed del identity en la base de datos, ya que esta tabla se va incrementando de uno en uno, así se puede sacar el id más facil
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();
            con.Close();

            con = new SqlConnection(connString);
            con.Open();
            cmd2 = new SqlCommand("INSERT INTO OtherStaff (Name, Phone, Address, Designation, Gender, BirthDate, Highest_Qualification, Salary)" +
                "VALUES ('SeBorra3','12345678901','Costa Rica','Enfermera', 'F', '01-01-1990', 'Master', 3000.0);", con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();
            con.Close();

            var dal = new myDAL();
            var result = dal.DeleteStaff(545);
            Assert.Equal(1, result);
        }

        // 23. Borrar staff inexistente retorna 1
        // ////////////////////////////////////////////////////////
        // Prueba 23
        // ID: 023
        // Nombre: Borrar staff inexistente retorna -1
        // Descripción: Al borrar un staff inexistente, el método debe retornar -1.
        // Datos de prueba: id de staff inexistente
        // Resultado esperado: -1
        // da error la prueba, porque retorna 1 en vez de -1, pero debería retornar -1
        [Fact]
        public void borrarStaffInexistenteRetorna1Negativo()
        {
            var dal = new myDAL();
            var result = dal.DeleteStaff(0);
            Assert.Equal(-1, result);
        }

        // 24. Cargar doctor sin query retorna un DataTable no vacío
        // ////////////////////////////////////////////////////////
        // Prueba 24
        // ID: 024
        // Nombre: Cargar doctor sin query retorna un DataTable no vacío
        // Descripción: Al cargar doctores sin query, el método debe retornar un DataTable no vacío.
        // Datos de prueba:
        // Resultado esperado: DataTable no vacío
        [Fact]
        public void loadDoctorSinQueryRetornaUnDTNoVacio()
        {
            var dal = new myDAL();
            DataTable dt = new DataTable();
            dal.LoadDoctor(ref dt, "");
            Assert.True(dt.Rows.Count > 0);
        }

        // 25. Cargar doctor con query válido retorna un DataTable no vacío
        // ////////////////////////////////////////////////////////
        // Prueba 25
        // ID: 025
        // Nombre: Cargar doctor con query válido retorna un DataTable no vacío
        // Descripción: Al cargar doctores con query válido, el método debe retornar un DataTable no vacío.
        // Datos de prueba: query: HASSAAN
        // Resultado esperado: DataTable no vacío
        [Fact]
        public void loadDoctorConQueryValidoRetornaUnDTNoVacio()
        {
            var dal = new myDAL();
            DataTable dt = new DataTable();
            dal.LoadDoctor(ref dt, "HASSAAN");
            Assert.True(dt.Rows.Count > 0);
        }



    }
}