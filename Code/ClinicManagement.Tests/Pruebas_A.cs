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
        //validate login
        [Fact]
        public void loginExitosoDevuelveUnEntero0()
        {
            //se usa un usuario que ya existía en la base de datos
            var email = "admin";
            var password = "admin@clinic.com";
            int type = 3;
            int id = 1;



            var dal = new myDAL();


            int result = dal.validateLogin(email, password, ref type, ref id);


            Assert.Equal(0, result);

        }

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

        [Fact]
        public void correoDeDoctorYaExisteRetorna1()
        {
            string email = "hassaan@gmail.com"; //email de doctor que ya existe
            var dal = new myDAL();
            var result = dal.DoctorEmailAlreadyExist(email);
            Assert.Equal(1, result);
        }

        [Fact]
        public void correoDeDoctorNoExistenteRetorna0()
        {
            string email = "correoquenoexiste@gmail.com"; //email de doctor que ya existe
            var dal = new myDAL();
            var result = dal.DoctorEmailAlreadyExist(email);
            Assert.Equal(0, result);
        }

        [Fact]
        public void correoVacioRetorna0()
        {
            string email = ""; //email de doctor que ya existe
            var dal = new myDAL();
            var result = dal.DoctorEmailAlreadyExist(email);
            Assert.Equal(0, result);
        }

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

        [Fact]
        public void borrarDoctorInexistenteRetorna1Tambien()
        {
            var dal = new myDAL();
            var result = dal.DeleteDoctor(0);
            Assert.Equal(1, result);
        }

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

        [Fact]
        public void borrarStaffInexistenteRetorna1Tambien()
        {
            var dal = new myDAL();
            var result = dal.DeleteStaff(0);
            Assert.Equal(1, result);
        }

        [Fact]
        public void loadDoctorSinQueryRetornaUnDTNoVacio()
        {
            var dal = new myDAL();
            DataTable dt = new DataTable();
            dal.LoadDoctor(ref dt, "");
            Assert.True(dt.Rows.Count > 0);
        }

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