from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.common.exceptions import NoSuchElementException, TimeoutException
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.common.alert import Alert
import unittest
import pyodbc
import time

class Pruebas_a(unittest.TestCase):


    
    #connectionString = 'DRIVER={ODBC Driver 18 for SQL Server};SERVER=DESKTOP-UQDSHLB\SQLEXPRESS;DATABASE=DBProject;Trusted_Connection=yes;'
    def setUp(self):
        self.driver = webdriver.Edge()
        self.driver.implicitly_wait(15)
        #http://www.python.org http://localhost:1972/SignUp.aspx
        self.driver.get('http://localhost:1972/SignUp.aspx')
        #time.sleep(5)
    
    
    # ID = 26
    # Nombre: Iniciar sesion como administrador
    # Datos: correo = admin@clinic.com pass = admin
    # Resultado esperado: Se inicia sesion como administrador
    # Pasa la prueba
    def test_iniciarSesionComoAdministrador(self):
        campoEmail = self.driver.find_element(By.ID, "loginEmail")
        campoEmail.send_keys("admin@clinic.com")
        campoPassword = self.driver.find_element(By.ID, "loginPassword")
        campoPassword.send_keys("admin")
        botonIniciarSesion = self.driver.find_element(By.ID, "loginUserName")
        botonIniciarSesion.click()
        
        self.assertIn("Manage Clinic", self.driver.page_source)
    
    
    # ID = 27
    # Nombre: Crear usuario
    # Datos
    # nombre: Prueba+tiempoactual
    # fecha de nacimiento: 11-11-1992
    # email: prueba+tiempoactual@clinic.com
    # contraseña: 12345678
    # número teléfono: 12345678901
    # género: cualquiera
    # dirección: Alajuela centro.
    # Resultado esperado: Se crea un usuario con los datos ingresados
    # Pasa la prueba
    def test_crearUsuario(self):
        campoName = self.driver.find_element(By.ID, "sName")
        campoName.send_keys(f"Prueba{time.time()}")
        campoBirth = self.driver.find_element(By.ID, "sBirthDate")
        campoBirth.send_keys("11-11-1992")
        campoEmail = self.driver.find_element(By.ID, "sEmail")
        campoEmail.send_keys(f"prueba{time.time()}@clinic.com")
        campoPassword = self.driver.find_element(By.ID, "sPassword")
        campoPassword.send_keys("12345678")
        campoConfirmPassword = self.driver.find_element(By.ID, "scPassword")
        campoConfirmPassword.send_keys("12345678")
        campoPhone = self.driver.find_element(By.ID, "Phone")
        campoPhone.send_keys("12345678901")
        campoAddress = self.driver.find_element(By.ID, "Address")
        campoAddress.send_keys("Alajuela centro.")
        botonCrear = self.driver.find_element(By.NAME, "ctl15")
        botonCrear.click()
        time.sleep(2)
        self.assertIn("Your Information", self.driver.page_source)
   
    # ID = 28
    # Nombre: Crear usuario con formato de fecha incorrecto
    # Datos
    # nombre: Prueba Mala+tiempoactual
    # fecha de nacimiento: 11111992
    # email: pruebamala+tiempoactual@clinic.com
    # contraseña: 12345678
    # número teléfono: 12345678901
    # género: cualquiera
    # dirección: Alajuela centro.
    # Resultado esperado: No se puede crear el usuario
    # Pasa la prueba
    def test_signUpFormatoDeFechaIncorrecto(self):
        campoName = self.driver.find_element(By.ID, "sName")
        campoName.send_keys(f"Prueba{time.time()} Mala")
        campoBirth = self.driver.find_element(By.ID, "sBirthDate")
        campoBirth.send_keys("11111992")
        campoEmail = self.driver.find_element(By.ID, "sEmail")
        campoEmail.send_keys(f"pruebamala{time.time()}@clinic.com")
        campoPassword = self.driver.find_element(By.ID, "sPassword")
        campoPassword.send_keys("12345678")
        campoConfirmPassword = self.driver.find_element(By.ID, "scPassword")
        campoConfirmPassword.send_keys("12345678")
        campoPhone = self.driver.find_element(By.ID, "Phone")
        campoPhone.send_keys("12345678901")
        campoAddress = self.driver.find_element(By.ID, "Address")
        campoAddress.send_keys("Alajuela centro.")
        botonCrear = self.driver.find_element(By.NAME, "ctl15")
        botonCrear.click()
        time.sleep(2)
        alert = self.driver.switch_to.alert
        self.assertIn("Birth Date Format Incorrect or out of Range.", alert.text)
        alert.accept()
        
    # ID = 29
    # Nombre: crear usuario con num de telefono sin digitos
    # Datos
    # nombre: Prueba Mala+tiempoactual
    # fecha de nacimiento: 11-11-1992
    # email: pruebamala+tiempoactual@clinic.com
    # contraseña: 12345678
    # número teléfono: abcdefghklñ
    # género: cualquiera
    # dirección: Alajuela centro.
    # Resultado esperado: No se puede crear el usuario
    # No Pasa la prueba   
    def test_crearUsuarioConTelefonoSinDigitos(self):
        campoName = self.driver.find_element(By.ID, "sName")
        campoName.send_keys(f"Prueba{time.time()} Mala")
        campoBirth = self.driver.find_element(By.ID, "sBirthDate")
        campoBirth.send_keys("11-11-1992")
        campoEmail = self.driver.find_element(By.ID, "sEmail")
        campoEmail.send_keys(f"pruebamalatel{time.time()}@clinic.com")
        campoPassword = self.driver.find_element(By.ID, "sPassword")
        campoPassword.send_keys("12345678")
        campoConfirmPassword = self.driver.find_element(By.ID, "scPassword")
        campoConfirmPassword.send_keys("12345678")
        campoPhone = self.driver.find_element(By.ID, "Phone")
        campoPhone.send_keys("abcdefghklñ")
        campoAddress = self.driver.find_element(By.ID, "Address")
        campoAddress.send_keys("Alajuela centro.")
        botonCrear = self.driver.find_element(By.NAME, "ctl15")
        botonCrear.click()
        time.sleep(2)
        
        try:
            self.driver.switch_to.alert
            alerta_mostrada = True
        except:
            alerta_mostrada = False
        self.assertTrue(alerta_mostrada)
        
    
    # ID = 30
    # Nombre: crear usuario sin contraseña
    # Datos
    # nombre: SinContraseña+tiempoactual
    # fecha de nacimiento: 11-11-1992
    # email: sincontraseña+tiempoactual@clinic.com
    # contraseña:
    # número teléfono: 12345678901
    # género: cualquiera
    # dirección: Alajuela centro.
    # Resultado esperado: No se puede crear el usuario
    # Pasa la prueba
    def test_crearUsuarioSinContrasenia(self):
        campoName = self.driver.find_element(By.ID, "sName")
        campoName.send_keys(f"SinContraseña{time.time()}")
        campoBirth = self.driver.find_element(By.ID, "sBirthDate")
        campoBirth.send_keys("11-11-1992")
        campoEmail = self.driver.find_element(By.ID, "sEmail")
        campoEmail.send_keys(f"sincontrasena{time.time()}@clinic.com")
        campoPassword = self.driver.find_element(By.ID, "sPassword")
        campoPassword.send_keys("")
        campoConfirmPassword = self.driver.find_element(By.ID, "scPassword")
        campoConfirmPassword.send_keys("")
        campoPhone = self.driver.find_element(By.ID, "Phone")
        campoPhone.send_keys("12345678901")
        campoAddress = self.driver.find_element(By.ID, "Address")
        campoAddress.send_keys("Alajuela centro.")
        botonCrear = self.driver.find_element(By.NAME, "ctl15")
        botonCrear.click()
        time.sleep(2)
        
        try:
            self.driver.switch_to.alert
            alerta_mostrada = True
            self.driver.switch_to.alert.accept()
        except:
            alerta_mostrada = False
            
        self.assertTrue(alerta_mostrada)
    
    
    # ID = 31
    # Nombre: crear usuario sin dirección
    # Datos
    # nombre: SinDireccion+tiempoactual
    # fecha de nacimiento: 11-11-1992
    # email: sindirecciontiempoactual@clinic.com
    # contraseña: 12345678
    # número teléfono: 12345678901
    # género: cualquiera
    # dirección:
    # Resultado esperado: Si se puede crear el usuario
    # Pasa la prueba
    def test_crearUsuarioSinDireccion(self):
        campoName = self.driver.find_element(By.ID, "sName")
        campoName.send_keys(f"SinDireccion{time.time()}")
        campoBirth = self.driver.find_element(By.ID, "sBirthDate")
        campoBirth.send_keys("11-11-1992")
        campoEmail = self.driver.find_element(By.ID, "sEmail")
        campoEmail.send_keys(f"sindireccion{time.time()}@clinic.com")
        campoPassword = self.driver.find_element(By.ID, "sPassword")
        campoPassword.send_keys("12345678")
        campoConfirmPassword = self.driver.find_element(By.ID, "scPassword")
        campoConfirmPassword.send_keys("12345678")
        campoPhone = self.driver.find_element(By.ID, "Phone")
        campoPhone.send_keys("12345678901")
        campoAddress = self.driver.find_element(By.ID, "Address")
        campoAddress.send_keys("")
        botonCrear = self.driver.find_element(By.NAME, "ctl15")
        botonCrear.click()
        time.sleep(2)
        self.assertIn("Your Information", self.driver.page_source)
    
    
    # ID = 32
    # Nombre: crear usuario sin correo
    # Datos
    # nombre: SinCorreo+tiempoactual
    # fecha de nacimiento: 11-11-1992
    # email:
    # contraseña: 12345678
    # número teléfono: 12345678901
    # género: cualquiera
    # dirección: Alajuela centro.
    # Resultado esperado: No se puede crear el usuario
    # Pasa la prueba
    def test_crearUsuarioSinCorreo(self):
        campoName = self.driver.find_element(By.ID, "sName")
        campoName.send_keys(f"SinCorreo{time.time()}")
        campoBirth = self.driver.find_element(By.ID, "sBirthDate")
        campoBirth.send_keys("11-11-1992")
        campoEmail = self.driver.find_element(By.ID, "sEmail")
        campoEmail.send_keys("")
        campoPassword = self.driver.find_element(By.ID, "sPassword")
        campoPassword.send_keys("12345678")
        campoConfirmPassword = self.driver.find_element(By.ID, "scPassword")
        campoConfirmPassword.send_keys("12345678")
        campoPhone = self.driver.find_element(By.ID, "Phone")
        campoPhone.send_keys("12345678901")
        campoAddress = self.driver.find_element(By.ID, "Address")
        campoAddress.send_keys("Alajuela centro.")
        botonCrear = self.driver.find_element(By.NAME, "ctl15")
        botonCrear.click()
        time.sleep(2)
        
        try:
            self.driver.switch_to.alert
            alerta_mostrada = True
            self.driver.switch_to.alert.accept()
        except:
            alerta_mostrada = False
        self.assertTrue(alerta_mostrada)
    
    
    # ID = 33
    # Nombre: crear usuario sin nombre
    # Datos
    # nombre:
    # fecha de nacimiento: 11-11-1992
    # email: sinnombre+tiempoactual@clinic.com
    # contraseña: 12345678
    # número teléfono: 12345678901
    # género: cualquiera
    # dirección: Alajuela centro.
    # Resultado esperado: No se puede crear el usuario
    # Pasa la prueba
    def test_crearUsuarioSinNombre(self):
        campoName = self.driver.find_element(By.ID, "sName")
        campoName.send_keys("")
        campoBirth = self.driver.find_element(By.ID, "sBirthDate")
        campoBirth.send_keys("11-11-1992")
        campoEmail = self.driver.find_element(By.ID, "sEmail")
        campoEmail.send_keys(f"sinnombre{time.time()}@clinic.com")
        campoPassword = self.driver.find_element(By.ID, "sPassword")
        campoPassword.send_keys("12345678")
        campoConfirmPassword = self.driver.find_element(By.ID, "scPassword")
        campoConfirmPassword.send_keys("12345678")
        campoPhone = self.driver.find_element(By.ID, "Phone")
        campoPhone.send_keys("12345678901")
        campoAddress = self.driver.find_element(By.ID, "Address")
        campoAddress.send_keys("Alajuela centro.")
        botonCrear = self.driver.find_element(By.NAME, "ctl15")
        botonCrear.click()
        time.sleep(2)
        
        try:
            self.driver.switch_to.alert
            alerta_mostrada = True
            self.assertIn("Name missing. Enter Name.", self.driver.switch_to.alert.text)
            self.driver.switch_to.alert.accept()
        except:
            alerta_mostrada = False
        self.assertTrue(alerta_mostrada)
    
    
    
    # ID = 34
    # Nombre: crear usuario con correo ya registrado
    # Datos
    # correo: usuarionoregistrado@clinic.com
    # contraseña: 12345678
    # Resultado esperado: No se inicia sesión y tira un alert
    # Pasa la prueba
    def test_iniciarSesionConUsuarioNoRegistrado(self):
        campoEmail = self.driver.find_element(By.ID, "loginEmail")
        campoEmail.send_keys("usuarionoregistrado@clinic.com")
        campoPassword = self.driver.find_element(By.ID, "loginPassword")
        campoPassword.send_keys("12345678")
        botonIniciarSesion = self.driver.find_element(By.ID, "loginUserName")
        
        botonIniciarSesion.click()
        time.sleep(2)
        
        try:
            self.driver.switch_to.alert
            alerta_mostrada = True
            self.assertIn("Email not found. Try Again !", self.driver.switch_to.alert.text)
            self.driver.switch_to.alert.accept()
        except:
            alerta_mostrada = False
        self.assertTrue(alerta_mostrada)
    
    # ID = 35
    # Nombre: crear usuario con correo ya registrado
    # Datos
    # correo: prueba@clinic.com
    # contraseña: 12345678
    # Resultado esperado: Se inicia sesión
    # Pasa la prueba
    def test_iniciarSesionConUsuarioRegistrado(self):
        campoEmail = self.driver.find_element(By.ID, "loginEmail")
        campoEmail.send_keys("prueba@clinic.com")
        campoPassword = self.driver.find_element(By.ID, "loginPassword")
        campoPassword.send_keys("12345678")
        botonIniciarSesion = self.driver.find_element(By.ID, "loginUserName")
       
        botonIniciarSesion.click()
        self.assertIn("Your Information", self.driver.page_source)
    
    
    # ID = 36
    # Nombre: ver datos de paciente
    # Datos
    # correo: prueba@clinic.com
    # contraseña: 12345678
    # Resultado esperado: Se muestran los datos del paciente
    # Pasa la prueba
    def test_verDatosDePaciente(self):
        campoEmail = self.driver.find_element(By.ID, "loginEmail")
        campoEmail.send_keys("prueba@clinic.com")
        campoPassword = self.driver.find_element(By.ID, "loginPassword")
        campoPassword.send_keys("12345678")
        botonIniciarSesion = self.driver.find_element(By.ID, "loginUserName")
    
        botonIniciarSesion.click()
        time.sleep(2)
        self.assertIn("Name:", self.driver.page_source)
    
    
    # ID = 37
    # Nombre: ver citas no aprobadas de paciente
    # Datos
    # correo: prueba@clinic.com
    # contraseña: 12345678
    # Resultado esperado: Se muestran las citas no aprobadas
    # Pasa la prueba
    def test_verCitasNoAprobadasDePaciente(self):
        campoEmail = self.driver.find_element(By.ID, "loginEmail")
        campoEmail.send_keys("prueba@clinic.com")
        campoPassword = self.driver.find_element(By.ID, "loginPassword")
        campoPassword.send_keys("12345678")
        botonIniciarSesion = self.driver.find_element(By.ID, "loginUserName")
        
        botonIniciarSesion.click()
        time.sleep(2)
        botonCitas = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/nav/div/ul[6]/li/a")
        botonCitas.click()
        self.assertIn("Current Appointments", self.driver.page_source)
        
    
    # ID = 38
    # Nombre: ver citas aprobadas de paciente
    # Datos
    # correo: prueba@clinic.com
    # contraseña: 12345678
    # Resultado esperado: Se muestran las citas aprobadas
    # Pasa la prueba
    def test_verCitasAprobadasDePaciente(self):
        campoEmail = self.driver.find_element(By.ID, "loginEmail")
        campoEmail.send_keys("prueba@clinic.com")
        campoPassword = self.driver.find_element(By.ID, "loginPassword")
        campoPassword.send_keys("12345678")
        botonIniciarSesion = self.driver.find_element(By.ID, "loginUserName")
        botonIniciarSesion.click()
        time.sleep(2)
        botonCitas = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/nav/div/ul[6]/li/a")
        botonCitas.click()
        #time.sleep(2)
        self.assertIn("you have an appointment with Doctor", self.driver.page_source)
    
    # ID = 39
    # Nombre: ver facturas de paciente
    # Datos
    # correo: prueba@clinic.com
    # contraseña: 12345678
    # Resultado esperado: Se muestran las facturas
    # Pasa la prueba
    def test_verFacturasDePaciente(self):
        campoEmail = self.driver.find_element(By.ID, "loginEmail")
        campoEmail.send_keys("prueba@clinic.com")
        campoPassword = self.driver.find_element(By.ID, "loginPassword")
        campoPassword.send_keys("12345678")
        botonIniciarSesion = self.driver.find_element(By.ID, "loginUserName")
        botonIniciarSesion.click()
        
        botonFacturas = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/nav/div/ul[4]/li/a")
        botonFacturas.click()
        self.assertIn("Your Bill(s) History", self.driver.page_source)
    
    # ID = 40
    # Nombre: ver tratamientos de paciente
    # Datos
    # correo: prueba@clinic.com
    # contraseña: 12345678
    # Resultado esperado: Se muestran los tratamientos
    # Pasa la prueba
    def test_verTratamientosDePaciente(self):
        campoEmail = self.driver.find_element(By.ID, "loginEmail")
        campoEmail.send_keys("prueba@clinic.com")
        campoPassword = self.driver.find_element(By.ID, "loginPassword")
        campoPassword.send_keys("12345678")
        botonIniciarSesion = self.driver.find_element(By.ID, "loginUserName")
        botonIniciarSesion.click()
        
        botonTratamientos = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/nav/div/ul[3]/li/a")
        botonTratamientos.click()
        self.assertIn("Your Treatment History", self.driver.page_source)
    
    def tearDown(self):
        self.driver.close()


        
if __name__ == "__main__":
    unittest.main()