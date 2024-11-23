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
        
        self.driver.refresh()
        
        # Iniciar sesion   
        # Credenciales de administrador (prueba)
        # email: admin@x.com
        # pass: admin@x.com
        campoEmail = self.driver.find_element(By.ID, "loginEmail")
        campoEmail.send_keys("admin@x.com")
        campoPassword = self.driver.find_element(By.ID, "loginPassword")
        campoPassword.send_keys("admin@x.com")
        botonIniciarSesion = self.driver.find_element(By.ID, "loginUserName")
        botonIniciarSesion.click()
        
    #### Pruebas Marco ####

    # ID = 076
    # Nombre: Registro exitoso de un nuevo doctor
    # Datos
    #   Nombre: Dr. Scott Summers
    #   Fecha de nacimiento: 08/23/1980
    #   Email: cyclops.summers@gmail.com
    #   Contraseña: OpticBlast2024
    #   Confirmar contraseña: OpticBlast2024
    #   Teléfono: 88885555
    #   Salario: 120000
    #   Precio por visita: 1500
    #   Experiencia: 5
    #   Departamento: Neurology
    #   Calificación: Doctor en Medicina con especialización en Neurología
    #   Especialización: Neurología
    #   Dirección: Avenida Central, San José, Costa Rica
    #   Genero: Masculino
    # Resultado esperado: El doctor es registrado exitosament en el sistema y se muestra un mensaje de éxito
    
    # La prueba no pasa porque hay un problema en el proceso almacenado
    # Error System.Data.SqlClient.SqlException: 'Maximum stored procedure, function, trigger, or view nesting level exceeded (limit 32).'
    
    def test_registroDoctorExitoso(self):
        # Arrange
        botonAddDoctor = self.driver.find_element(By.XPATH, "/html/body/div/nav/div/ul[3]/li/a")
        botonAddDoctor.click()
        
        # Act
        doctorName = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[1]/input")
        doctorName.send_keys("Dr. Scott Summers")
        
        doctorBirthDate = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[2]/input")
        doctorBirthDate.send_keys("08/23/1980")
        
        doctorEmail = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[3]/input")
        doctorEmail.send_keys("cyclops.summers@gmail.com")
        
        doctorPassword = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[4]/input")
        doctorPassword.send_keys("OpticBlast2024")
        
        doctorConfirmPassword = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[5]/input")
        doctorConfirmPassword.send_keys("OpticBlast2024")
        
        doctorPhone = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[6]/input")
        doctorPhone.send_keys("88885555")
        
        doctorSalary = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[7]/input[1]")
        doctorSalary.send_keys("120000")
        
        doctorSalaryPerVisit = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[7]/input[2]")
        doctorSalaryPerVisit.send_keys("1500")

        doctorExperience = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[7]/input[3]")
        doctorExperience.send_keys("5")
        
        doctorDepartment = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[8]/select")
        doctorDepartment.send_keys("Neurology")

        doctorQualification = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[8]/input[1]")
        doctorQualification.send_keys("Doctor en Medicina con especialización en Neurología")
        
        doctorSpecialization = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[8]/input[2]")
        doctorSpecialization.send_keys("Neurología")
        
        doctorAddress = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[9]/input")
        doctorAddress.send_keys("Avenida Central, San José, Costa Rica")

        doctorGender = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[10]/span[1]/input")
        doctorGender.click()
        
        doctorAdd = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/input")
        doctorAdd.click()
        
        # Assert
        
        time.sleep(2)
        
        try:
            self.assertIn("Doctor Added Successfully", self.driver.page_source)
            prueba_assert = True
        except:
            prueba_assert = False
            
        self.assertTrue(prueba_assert)
        
        
    # ID = 077
    # Nombre: Registro fallido de doctor por omitir confirmación de contraseña
    # Datos
    #   Nombre: Dr. Scott Summers
    #   Fecha de nacimiento: 08/23/1980
    #   Email: cyclops.summers@gmail.com
    #   Contraseña: OpticBlast2024
    #   Confirmar contraseña:
    #   Teléfono: 88885555
    #   Salario: 120000
    #   Precio por visita: 1500
    #   Experiencia: 5
    #   Departamento: Neurology
    #   Calificación: Doctor en Medicina con especialización en Neurología
    #   Especialización: Neurología
    #   Dirección: Avenida Central, San José, Costa Rica
    #   Genero: Masculino
    # Resultado esperado: No se registra el doctor y se muestra un mensaje de error
    
    def test_registroDoctorFallidoPorOmitirConfirmacionContrasenia(self):
        # Arrange
        botonAddDoctor = self.driver.find_element(By.XPATH, "/html/body/div/nav/div/ul[3]/li/a")
        botonAddDoctor.click()
        
        # Act
        doctorName = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[1]/input")
        doctorName.send_keys("Dr. Scott Summers")
        
        doctorBirthDate = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[2]/input")
        doctorBirthDate.send_keys("08/23/1980")
        
        doctorEmail = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[3]/input")
        doctorEmail.send_keys("cyclops.summers@gmail.com")
        
        doctorPassword = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[4]/input")
        doctorPassword.send_keys("OpticBlast2024")
        
        doctorPhone = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[6]/input")
        doctorPhone.send_keys("88885555")
        
        doctorSalary = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[7]/input[1]")
        doctorSalary.send_keys("120000")
        
        doctorSalaryPerVisit = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[7]/input[2]")
        doctorSalaryPerVisit.send_keys("1500")
        
        doctorExperience = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[7]/input[3]")
        doctorExperience.send_keys("5")
        
        doctorDepartment = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[8]/select")
        doctorDepartment.send_keys("Neurology")
        
        doctorQualification = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[8]/input[1]")
        doctorQualification.send_keys("Doctor en Medicina con especialización en Neurología")
        
        doctorSpecialization = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[8]/input[2]")
        doctorSpecialization.send_keys("Neurología")
        
        doctorAddress = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[9]/input")
        doctorAddress.send_keys("Avenida Central, San José, Costa Rica")
        
        doctorGender = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[10]/span[1]/input")
        doctorGender.click()
        
        doctorAdd = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/input")
        doctorAdd.click()
        
        # Assert
        error_message = self.driver.find_element(By.ID, "CompareValidator1")
        self.assertIn("Passwords Do not Match", error_message.text)
        
    # ID = 078
    # Nombre: Registro fallido de doctor por ingresar un correo electrónico en formato incorrecto
    # Datos
    #   Nombre: Jean Grey
    #   Fecha de nacimiento: 11/04/1985
    #   Email: phoenixforcegmail.com
    #   Contraseña: PhoenixForce2024
    #   Confirmar contraseña: PhoenixForce2024
    #   Teléfono: 22223333
    #   Salario: 95000
    #   Precio por visita: 1800
    #   Experiencia: 5
    #   Departamento: Neurology
    #   Calificación: Doctor en Medicina
    #   Especialización: General
    #   Dirección: Calle El Roble, San Pedro, Costa Rica
    #   Genero: Female
    # Resultado esperado: No se registra el doctor y se muestra un mensaje de error
    
    def test_registroDoctorFallidoPorCorreoElectronicoIncorrecto(self):
        # Arrange
        botonAddDoctor = self.driver.find_element(By.XPATH, "/html/body/div/nav/div/ul[3]/li/a")
        botonAddDoctor.click()
        
        doctorName = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[1]/input")
        doctorName.send_keys("Jean Grey")
        
        doctorBirthDate = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[2]/input")
        doctorBirthDate.send_keys("11/04/1985")
        
        doctorEmail = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[3]/input")
        doctorEmail.send_keys("phoenixforcegmail.com")
        
        doctorPassword = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[4]/input")
        doctorPassword.send_keys("PhoenixForce2024")
        
        doctorConfirmPassword = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[5]/input")
        doctorConfirmPassword.send_keys("PhoenixForce2024")
        
        doctorPhone = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[6]/input")
        doctorPhone.send_keys("22223333")
        
        doctorSalary = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[7]/input[1]")
        doctorSalary.send_keys("95000")
        
        doctorSalaryPerVisit = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[7]/input[2]")
        doctorSalaryPerVisit.send_keys("1800")
        
        doctorExperience = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[7]/input[3]")
        doctorExperience.send_keys("5")
        
        doctorDepartment = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[8]/select")
        doctorDepartment.send_keys("Neurology")
        
        doctorQualification = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[8]/input[1]")
        doctorQualification.send_keys("Doctor en Medicina")
        
        doctorSpecialization = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[8]/input[2]")
        doctorSpecialization.send_keys("General")
        
        doctorAddress = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[9]/input")
        doctorAddress.send_keys("Calle El Roble, San Pedro, Costa Rica")
        
        doctorGender = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[10]/span[2]/input")
        doctorGender.click()
        
        doctorAdd = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/input")
        doctorAdd.click()
        
        error_message = self.driver.find_element(By.ID, "EmailformatValidator")
        self.assertIn("Incorrect Email Format", error_message.text)
    
    # ID = 079
    # Nombre: Registro exitoso de un doctor con solo los campos obligatorios llenados
    # Datos
    #   Nombre: Dr. Scott Summers
    #   Fecha de nacimiento: 08/23/1980
    #   Email: cyclops@gmail.com
    #   Contraseña: OpticBlast2024
    #   Confirmar contraseña: OpticBlast2024
    # Resultado esperado: El doctor es registrado exitosamente en el sistema y se muestra un mensaje de éxito
    
    # La prueba no pasa porque hay un problema en el proceso almacenado
    # Error System.Data.SqlClient.SqlException: 'Maximum stored procedure, function, trigger, or view nesting level exceeded (limit 32).'
    def test_registroDoctorExitosoConCamposObligatorios(self):
        # Arrange
        botonAddDoctor = self.driver.find_element(By.XPATH, "/html/body/div/nav/div/ul[3]/li/a")
        botonAddDoctor.click()
        
        # Act
        doctorName = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[1]/input")
        doctorName.send_keys("Dr. Scott Summers")
        
        doctorBirthDate = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[2]/input")
        doctorBirthDate.send_keys("08/23/1980")
        
        doctorEmail = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[3]/input")
        doctorEmail.send_keys("cyclops@gmail.com")
        
        doctorPassword = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[4]/input")
        doctorPassword.send_keys("OpticBlast2024")
        
        doctorConfirmPassword = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[5]/input")
        doctorConfirmPassword.send_keys("OpticBlast2024")
        
        doctorAdd = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/input")
        doctorAdd.click()
        
        # Assert
        time.sleep(2)
        
        try:
            self.assertIn("Doctor Added Successfully", self.driver.page_source)
            prueba_assert = True
        except:
            prueba_assert = False
            
        self.assertTrue(prueba_assert)
   
    # ID = 080
    # Nombre: Registro fallido de doctor por omitir el campo “Birth Date”
    
    # Datos
    #   Nombre: Dr. Scott Summers
    #   Fecha de nacimiento:
    #   Email: cyclops.winter@gmail.com
    #   Contraseña: OpticBlast2024
    #   Confirmar contraseña: OpticBlast2024
    #   Teléfono: 88885555
    
    # Resultado esperado: No se registra el doctor y se muestra un mensaje de error
    
    def test_registroDoctorFallidoPorOmitirCampoBirthDate(self):
        # Arrange
        botonAddDoctor = self.driver.find_element(By.XPATH, "/html/body/div/nav/div/ul[3]/li/a")
        botonAddDoctor.click()
        
        # Act
        doctorName = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[1]/input")
        doctorName.send_keys("Dr. Scott Summers")
        
        doctorEmail = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[3]/input")
        doctorEmail.send_keys("cyclops.summers@gmail.com")
        
        doctorPassword = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[4]/input")
        doctorPassword.send_keys("OpticBlast2024")
        
        doctorConfirmPassword = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[5]/input")
        doctorConfirmPassword.send_keys("OpticBlast2024")
        
        doctorPhone = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[6]/input")
        doctorPhone.send_keys("88885555")
        
        doctorAdd = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/input")
        doctorAdd.click()
        
        # Assert
        time.sleep(2)
        
        error_message = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[2]/span[1]")
        self.assertIn("*Required", error_message.text)
        
    # ID = 081
    # Nombre: Registro fallido de doctor por omitir el campo “Email”
    # Datos
    #   Nombre: Dr. Scott Summers
    #   Fecha de nacimiento: 08/23/1980
    #   Email:
    #   Contraseña: OpticBlast2024
    #   Confirmar contraseña: OpticBlast2024
    #   Teléfono: 88885555
    # Resultado esperado: No se registra el doctor y se muestra un mensaje de error
    
    def test_registroDoctorFallidoPorOmitirCampoEmail(self):
        # Arrange
        botonAddDoctor = self.driver.find_element(By.XPATH, "/html/body/div/nav/div/ul[3]/li/a")
        botonAddDoctor.click()
        
        # Act
        doctorName = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[1]/input")
        doctorName.send_keys("Dr. Scott Summers")
        
        doctorBirthDate = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[2]/input")
        doctorBirthDate.send_keys("08/23/1980")
        
        doctorPassword = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[4]/input")
        doctorPassword.send_keys("OpticBlast2024")
        
        doctorConfirmPassword = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[5]/input")
        doctorConfirmPassword.send_keys("OpticBlast2024")
        
        doctorNumber = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[6]/input")
        doctorNumber.send_keys("88885555")
        
        addDoctor = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/input")
        addDoctor.click()
        
        # Assert
        time.sleep(2)
        
        #/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[3]/span[3]
        error_message = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[3]/span[3]")
        self.assertIn("*Required", error_message.text)
        
        
    
    # ID = 082
    # Nombre: Registro de doctor fallido por omitir el campo de nueva contraseña
    # Datos
    #  Nombre: Dr. Scott Summers
    #   Fecha de nacimiento: 08/23/1980
    #   Email: ciclops.summers@gmail.com
    #   Contraseña:
    #   Confirmar contraseña: OpticBlast2024
    #   Teléfono: 88885555
    # Resultado esperado: No se registra el doctor y se muestra un mensaje de error
    
    def test_registroDoctorFallidoPorOmitirCampoContrasenia(self):
        # Arrange
        botonAddDoctor = self.driver.find_element(By.XPATH, "/html/body/div/nav/div/ul[3]/li/a")
        botonAddDoctor.click()
        
        # Act
        doctorName = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[1]/input")
        doctorName.send_keys("Dr. Scott Summers")
        
        doctorBirthDate = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[2]/input")
        doctorBirthDate.send_keys("08/23/1980")
        
        doctorEmail = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[3]/input")
        doctorEmail.send_keys("ciclops.summers@gmail.com")
        
        doctorConfirmPassword = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[5]/input")
        doctorConfirmPassword.send_keys("OpticBlast2024")
        
        doctorNumber = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[6]/input")
        doctorNumber.send_keys("88885555")
        
        addDoctor = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/input")
        addDoctor.click()
        
        # Assert
        time.sleep(2)
        
        #/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[4]/span[1]
        error_message = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[4]/span[1]")
        self.assertIn("*Required", error_message.text)

    # ID = 083
    # Nombre: Registro fallido de doctor por ingresar un valor inferior al rango permitido en el campo “Experience”
    # Datos
    #   Nombre: Dr. Scott Summers
    #   Fecha de nacimiento: 08/23/1980
    #   Email: cyclops.x@gmail.com
    #   Contraseña: OpticBlast2024
    #   Confirmar contraseña: OpticBlast2024
    #   Numero de telefono: 88885555
    #   Experiencia: -1
    # Resultado esperado: No se registra el doctor y se muestra un mensaje de error
    
    def test_registroDoctorFallidoPorValorInferiorEnExperiencia(self):
        # Arrange
        botonAddDoctor = self.driver.find_element(By.XPATH, "/html/body/div/nav/div/ul[3]/li/a")
        botonAddDoctor.click()
        
        # Act
        doctorName = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[1]/input")
        doctorName.send_keys("Dr. Scott Summers")
        
        doctorBirthDate = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[2]/input")
        doctorBirthDate.send_keys("08/23/1980")
        
        doctorEmail = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[3]/input")
        doctorEmail.send_keys("cyclops.x@gmail.com")
        
        doctorPassword = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[4]/input")
        doctorPassword.send_keys("OpticBlast2024")
        
        doctorConfirmPassword = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[5]/input")
        doctorConfirmPassword.send_keys("OpticBlast2024")
        
        doctorNumber = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[6]/input")
        doctorNumber.send_keys("88885555")
        
        doctorExperience = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[7]/input[3]")
        doctorExperience.send_keys("-1")
        
        addDoctor = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/input")
        addDoctor.click()
        
        # Assert
        time.sleep(2)
        
        error_message = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/span[3]")
        self.assertIn("Experience Range should be (0-5)", error_message.text)


    # ID = 084
    # Nombre: Registro fallido de doctor por ingresar un valor superior al rango permitido en el campo “Experience”
    # Datos
    #   Nombre: Dr. Scott Summers
    #   Fecha de nacimiento: 08/23/1980
    #   Email: cyclops.x@gmail.com
    #   Contraseña: OpticBlast2024
    #   Confirmar contraseña: OpticBlast2024
    #   Numero de telefono: 88885555
    #   Experiencia: 6
    # Resultado esperado: No se registra el doctor y se muestra un mensaje de error
    
    def test_registroDoctorFallidoPorValorSuperiorEnExperiencia(self):
        # Arrange
        botonAddDoctor = self.driver.find_element(By.XPATH, "/html/body/div/nav/div/ul[3]/li/a")
        botonAddDoctor.click()
        
        # Act
        doctorName = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[1]/input")
        doctorName.send_keys("Dr. Scott Summers")
        
        doctorBirthDate = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[2]/input")
        doctorBirthDate.send_keys("08/23/1980")
        
        doctorEmail = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[3]/input")
        doctorEmail.send_keys("cyclops.x@gmail.com")
        
        doctorPassword = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[4]/input")
        doctorPassword.send_keys("OpticBlast2024")
        
        doctorConfirmPassword = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[5]/input")
        doctorConfirmPassword.send_keys("OpticBlast2024")
        
        doctorNumber = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[6]/input")
        doctorNumber.send_keys("88885555")
        
        doctorExperience = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[7]/input[3]")
        doctorExperience.send_keys("6")
        
        addDoctor = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/input")
        addDoctor.click()
        
        # Assert
        time.sleep(2)
        
        error_message = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/span[3]")
        self.assertIn("Experience Range should be (0-5)", error_message.text)
        
    
    # ID = 085
    # Nombre: Registro fallido de doctor por ingresar letras en el campo “Birth Date”
    # Datos
    #   Nombre: Dr. Scott Summers
    #   Fecha de nacimiento: August Twenty Two
    #   Email: cyclops@gmail.com
    #   Contraseña: OpticBlast2024
    #   Confirmar contraseña: OpticBlast2024
    #   Teléfono: 88885555
    # Resultado esperado: No se registra el doctor y se muestra un mensaje de error
    
    def test_registroDoctorFallidoPorIngresarLetrasEnBirthDate(self):
        # Arrange
        botonAddDoctor = self.driver.find_element(By.XPATH, "/html/body/div/nav/div/ul[3]/li/a")
        botonAddDoctor.click()
        
        # Act
        doctorName = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[1]/input")
        doctorName.send_keys("Dr. Scott Summers")
        
        doctorBirthDate = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[2]/input")
        doctorBirthDate.send_keys("August Twenty Two")
        
        doctorEmail = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[3]/input")
        doctorEmail.send_keys("cyclops@gmail.com")
        
        doctorPassword = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[4]/input")
        doctorPassword.send_keys("OpticBlast2024")
        
        doctorConfirmPassword = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[5]/input")
        doctorConfirmPassword.send_keys("OpticBlast2024")
        
        doctorNumber = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[6]/input")
        doctorNumber.send_keys("88885555")
        
        addDoctor = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/input")
        addDoctor.click()
        
        # Assert
        time.sleep(2)
        
        error_message = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[2]/span[2]")
        self.assertIn("Birth Date Format Not Correct", error_message.text)
 
    # ID = 086
    # Nombre: Registro fallido de doctor por ingresar letras en los campos “Salary in Rupees” y “Charges per visit”
    # Datos
    #   Nombre: Dr. Scott Summers
    #   Fecha de nacimiento: 08/23/1980
    #   Email: cyclops.summers@gmail.com
    #   Contraseña: OpticBlast2024
    #   Confirmar contraseña: OpticBlast2024
    #   Teléfono: 88885555
    #   Salario: Ten Thousand
    #   Precio por visita: Five Hundred
    # Resultado esperado: No se registra el doctor y se muestra un mensaje de error
    
    def test_registroDoctorFallidoPorIngresarLetrasEnSalarioYVisita(self):
        # Arrange
        botonAddDoctor = self.driver.find_element(By.XPATH, "/html/body/div/nav/div/ul[3]/li/a")
        botonAddDoctor.click()
        
        # Act
        doctorName = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[1]/input")
        doctorName.send_keys("Dr. Scott Summers")
        
        doctorBirthDate = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[2]/input")
        doctorBirthDate.send_keys("08/23/1980")
        
        doctorEmail = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[3]/input")
        doctorEmail.send_keys("cyclops.summer@gmail.com")
        
        doctorPassword = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[4]/input")
        doctorPassword.send_keys("OpticBlast2024")
        
        doctorConfirmPassword = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[5]/input")
        doctorConfirmPassword.send_keys("OpticBlast2024")
        
        doctorNumber = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[6]/input")
        doctorNumber.send_keys("88885555")
        
        doctorSalary = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[7]/input[1]")
        doctorSalary.send_keys("Ten Thousand")
        
        doctorSalaryPerVisit = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[7]/input[2]")
        doctorSalaryPerVisit.send_keys("Five Hundred")
        
        addDoctor = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/input")
        addDoctor.click()
        
        # Assert
        time.sleep(2)
        
        error_message = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/span[1]")
        self.assertIn("Numbers Only !", error_message.text)

    # ID = 087
    # Nombre: Registro exitoso de nuevo personal de staff
    # Datos
    #   Nombre: Victor Stone
    #   Fecha de nacimiento: 08/29/1985
    #   Número de teléfono: 88888888
    #   Salario: 40000
    #   Calificación: Bachelor
    #   Designación: Supervisor
    #   Dirección: Barrio Escalante San José Costa Rica
    #   Genero: Male
    # Resultado esperado: El registro es exitoso y se muestra un mensaje de confirmación

    def test_registroExitosoDeNuevoPersonalDeStaff(self):
        # Arrange
        botonAddStaff = self.driver.find_element(By.XPATH, "/html/body/div/nav/div/ul[2]/li/a")
        botonAddStaff.click()
        
        # Act
        staffName = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[1]/input")
        staffName.send_keys("Victor Stone")
        
        staffBirthDate = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[2]/input")
        staffBirthDate.send_keys("08/29/1985")
        
        staffPhone = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[3]/input")
        staffPhone.send_keys("88888888")
        
        staffSalary = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[4]/input[1]")
        staffSalary.send_keys("40000")
        
        staffQualification = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[4]/input[2]")
        staffQualification.send_keys("Bachelor")
        
        staffDesignation = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[4]/input[3]")
        staffDesignation.send_keys("Supervisor")
        
        staffAddress = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[5]/input")
        staffAddress.send_keys("Barrio Escalante San José Costa Rica")
        
        staffGender = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[6]/span[1]/label")
        staffGender.click()
        
        staffAdd = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/input")
        staffAdd.click()
        
        # Assert
        time.sleep(2)
        
        addedStaff = self.driver.find_element(By.ID, "Msg")
        self.assertIn("Supervisor Added Succesfully", addedStaff.text)
        
        
    # ID = 088
    # Nombre: Registro fallido por campos obligatorios incompletos en Staff
    # Datos
    #   Nombre:
    #   Fecha de nacimiento:
    #   Salario:
    #   Designación:
    # Resultado esperado: El sistema muestra un mensaje de error indicando que los campos obligatorios deben ser llenados antes de continuar
    
    def test_registroFallidoPorCamposObligatoriosIncompletosEnStaff(self):
        # Arrange
        botonAddStaff = self.driver.find_element(By.XPATH, "/html/body/div/nav/div/ul[2]/li/a")
        botonAddStaff.click()
        
        # Act
        staffAdd = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/input")
        staffAdd.click()
        
        # Assert
        time.sleep(2)
        
        error_message = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[1]/span")
        self.assertIn("*Required", error_message.text)
        
        error_message = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[2]/span[1]")
        self.assertIn("*Required", error_message.text)
        
        error_message = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/span[2]")
        self.assertIn("*Required", error_message.text)
        
        error_message = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/span[3]")
        self.assertIn("*Required", error_message.text)
    
    # ID = 089
    # Nombre: Registro fallido por formato incorrecto de fecha de nacimiento en Staff
    # Datos
    #   Nombre: Richard Grayson
    #   Fecha de nacimiento: 29-08-1985
    #   Salario: 40000
    #   Designación: Supervisor
    #   Dirección: Alajuela Costa Rica
    # Resultado esperado: El sistema muestra un mensaje de error indicando que la fecha de nacimiento debe estar en el formato correcto (mm/dd/yyyy)
    
    def test_registroFallidoPorFormatoIncorrectoDeFechaDeNacimientoEnStaff(self):
        # Arrange
        botonAddStaff = self.driver.find_element(By.XPATH, "/html/body/div/nav/div/ul[2]/li/a")
        botonAddStaff.click()
        
        # Act
        staffName = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[1]/input")
        staffName.send_keys("Richard Grayson")
        
        staffBirthDate = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[2]/input")
        staffBirthDate.send_keys("29-08-1985")
        
        staffSalary = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[3]/input")
        staffSalary.send_keys("40000")
        
        staffDesignation = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[4]/input")
        staffDesignation.send_keys("Supervisor")
        
        staffAddress = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[5]/input")
        staffAddress.send_keys("Alajuela Costa Rica")
        
        staffAdd = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/input")
        staffAdd.click()
        
        # Assert
        time.sleep(2)
        
        error_message = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[2]/span[2]")
        self.assertIn("Date Format Not Correct", error_message.text)

    # ID = 090
    # Nombre: Error por ingresar caracteres no numéricos en el campo Salary in Rupees en Staff
    # Datos
    #   Nombre: Richard Grayson
    #   Fecha de nacimiento: 08/29/1985
    #   Salario: 40k
    #   Designación: Supervisor
    #   Dirección: Alajuela Costa Rica
    # Resultado esperado: El sistema muestra un mensaje de error indicando que el campo Salary in Rupees solo admite números
    
    def test_errorPorIngresarCaracteresNoNumericosEnSalaryInRupeesEnStaff(self):
        # Arrange
        botonAddStaff = self.driver.find_element(By.XPATH, "/html/body/div/nav/div/ul[2]/li/a")
        botonAddStaff.click()
        
        # Act
        staffName = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[1]/input")
        staffName.send_keys("Richard Grayson")
        
        staffBirthDate = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[2]/input")
        staffBirthDate.send_keys("08/29/1985")
        
        staffSalary = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[3]/input")
        staffSalary.send_keys("40k")
        
        staffDesignation = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[4]/input")
        staffDesignation.send_keys("Supervisor")
        
        staffAddress = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/div[5]/input")
        staffAddress.send_keys("Alajuela Costa Rica")
        
        staffAdd = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/input")
        staffAdd.click()
        
        # Assert
        time.sleep(2)
        
        error_message = self.driver.find_element(By.XPATH, "/html/body/form/div[3]/div[2]/div/div[2]/div/div[2]/span[1]")
        self.assertIn("Numbers Only !", error_message.text)
        
    def tearDown(self):
        self.driver.close()
        
if __name__ == "__main__":
    unittest.main()
