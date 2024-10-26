using DBProject;
using DBProject.DAL;

namespace ClinicManagement.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            myDAL objmyDAL = new myDAL();
            Assert.Equal(1, objmyDAL.AddStaff("John Doe", "01/01/1990", "1234567890", 'M', "123 Main St", 100000, "MD", "Doctor"));
        }
    }
}