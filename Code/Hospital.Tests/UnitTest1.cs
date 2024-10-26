using Moq;
using Xunit;
using System.Web;
using DBProject;
using DBProject.DAL;
using System.Data;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Http;
namespace Hospital.Tests
{
    public class UnitTest1
    {
        private Mock<HttpContext> mockHttpContext;
        private Mock<ISession> mockSession;
        private ViewDoctors page;

        public UnitTest1()
        {
            mockHttpContext = new Mock<HttpContext>();

            // Crear el mock de la Session
            mockSession = new Mock<ISession>();

            // Configurar el comportamiento de la Session usando métodos de ISession
            mockSession.Setup(sess => sess.GetString("deptOriginal")).Returns("Cardiology");

            // Configurar HttpContext para devolver la sesión mock
            mockHttpContext.Setup(ctx => ctx.Session).Returns(mockSession.Object);

            // Crear la instancia de la página que vamos a probar
            page = new ViewDoctors();

            // Asignar el mock de HttpContext
            HttpContextAccessor httpContextAccessor = new HttpContextAccessor
            {
                HttpContext = mockHttpContext.Object
            };
        }

        [Fact]
        public void Test1()
        {
            // Arrange
            var mockDAL = new Mock<myDAL>();
            DataTable dt = new DataTable();
            mockDAL.Setup(dal => dal.getDeptDoctorInfo(It.IsAny<string>(), ref dt)).Returns(-1);

            // Simulamos los controles Label y GridView en la página
            page.TDoctorGrid_RowCommand = new System.Web.UI.WebControls.Label();
            page.TDoctorGrid = new System.Web.UI.WebControls.GridView();

            // Act
            page.deptDoctorInfo(null, null);

            // Assert
            Assert.Equal("There was some error in retrieving the Doctors Information.", page.TDoctor.Text);

        }

        
       
    }
}