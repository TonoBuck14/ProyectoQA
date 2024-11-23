from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.common.exceptions import NoSuchElementException, TimeoutException
from selenium.webdriver.common.alert import Alert
import unittest
import pyodbc
class pruebas_j(unittest.TestCase):

    url = "http://localhost:1972/SignUp.aspx"

    def setUp(self):
        self.driver = webdriver.Edge()
        self.driver.implicitly_wait(5)
        self.driver.get(self.url)

    # Se eliminan citas porque si no el doctor se queda sin cupo y no se pueden hacer todas las pruebas

    def borrar_citas(self):
       
        conn_str = (
        r'DRIVER={ODBC Driver 17 for SQL Server};'
        r'SERVER=LAPTOP-QHBOLTCG\SQLEXPRESS;'
        r'DATABASE=DBProject;'
        r'Trusted_Connection=yes;' 
        )
        
        conn = pyodbc.connect(conn_str)
        cursor = conn.cursor()

        try:
            
            query = "DELETE FROM [DBProject].[dbo].[Appointment];"
            cursor.execute(query)
            conn.commit()

        except Exception as e:

            conn.rollback()  
        finally:

            cursor.close()
            conn.close()

    # Funcion para iniciar sesión con doctor
    # Datos : Email = farhan@gmail.com y Contraseña = abc
    def iniciar_sesion(self):

        campo_correo = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/div/div/div/div[2]/div[1]/div[1]/div[2]/div[1]/input"))
        )
        campo_correo.send_keys("farhan@gmail.com")

        campo_contraseña = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/div/div/div/div[2]/div[1]/div[1]/div[2]/div[2]/input"))
        )
        campo_contraseña.send_keys("abc")

        boton_log_in = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/div/div/div/div[2]/div[1]/div[1]/div[2]/input"))
        )
        boton_log_in.click()

    # Funcion para agendar una cita y aceptarla como doctor (Util porque en muchas se repite esto)
    # Datos : Email_Doctor = farhan@gmail.com, Contraseña_Doctor = abc, Email_Paciente = ABC@gmail.com y Contraseña_Paciente = abc
    def agendar_cita(self):

    
        campo_correo = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/div/div/div/div[2]/div[1]/div[1]/div[2]/div[1]/input"))
        )
        campo_correo.send_keys("ABC@gmail.com")

        campo_contraseña = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/div/div/div/div[2]/div[1]/div[1]/div[2]/div[2]/input"))
        )
        campo_contraseña.send_keys("abc")

        boton_log_in = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/div/div/div/div[2]/div[1]/div[1]/div[2]/input"))
        )
        boton_log_in.click()

        boton_tomar_cita = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/nav/div/ul[5]/li/a"))
        )
        boton_tomar_cita.click()

        boton_seleccionar = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/div/table/tbody/tr[2]/td[1]/a"))
        )
        boton_seleccionar.click()

        boton_doctor = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/div/table/tbody/tr[2]/td[1]/a"))
        )
        boton_doctor.click()

        tomar_cita = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/div/div/input"))
        )
        tomar_cita.click()

        hora = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/div/table/tbody/tr[2]/td[1]/a"))
        )
        hora.click()

        request = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/input"))
        )
        request.click()

        log_out = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/nav/div/ul[1]/li/a"))
        )
        log_out.click()

        campo_correo = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/div/div/div/div[2]/div[1]/div[1]/div[2]/div[1]/input"))
        )
        campo_correo.send_keys("farhan@gmail.com")

        campo_contraseña = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/div/div/div/div[2]/div[1]/div[1]/div[2]/div[2]/input"))
        )
        campo_contraseña.send_keys("abc")

        boton_log_in = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/div/div/div/div[2]/div[1]/div[1]/div[2]/input"))
        )
        boton_log_in.click()

        citas_pendientes = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/nav/div/ul[3]/li/a"))
        )
        citas_pendientes.click()

        aceptar_cita = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/div/div/table/tbody/tr[2]/td[1]/a[2]"))
        )
        aceptar_cita.click()

        log_out = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/nav/div/ul[1]/li/a"))
        )
        log_out.click()
    
    # ID = 51
    # Nombre: VerInformacionDoctor
    # Datos:
    # Resultado esperado: Se ve la información del doctor que acaba de iniciar sesión

    def test_VerInformacionDoctor(self):

        self.iniciar_sesion()

        mensaje_usuario = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/div/h3"))
        )

        self.assertIn("Farhan Shoukat", mensaje_usuario.text)
    
    # ID = 56
    # Nombre: VerCitasDeHoy
    # Datos:
    # Resultado esperado: Ver las citas programadas para hoy.

    def test_VerCitasDeHoy(self):

        self.iniciar_sesion()

        boton_citas_hoy = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/nav/div/ul[3]/li/a"))
        )
        boton_citas_hoy.click()

        elemento_citas = WebDriverWait(self.driver, 10).until(
        EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/div/div"))
        )

        self.assertIsNotNone(elemento_citas)
     
    # ID = 57
    # Nombre: VerListaPacientes
    # Datos:
    # Resultado esperado: Ver la lista de pacientes.

    def test_VerListaPacientes(self):

        self.iniciar_sesion()

        boton_lista_pacientes = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/nav/div/ul[4]/li/a"))
        )
        boton_lista_pacientes.click()

        elemento_pacientes = WebDriverWait(self.driver, 10).until(
        EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/div"))
        )

        self.assertIsNotNone(elemento_pacientes)
    
    # ID = 58
    # Nombre: EnviarFormularioCitasEnBlanco
    # Datos:
    # Resultado esperado: Al enviar el formulario de cita en blanco, debería aparecer un mensaje de error.
    # Esta falla
    
    def test_EnviarFormularioCitasEnBlanco(self):

        self.agendar_cita()

        self.iniciar_sesion()

        boton_citas_hoy = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/nav/div/ul[2]/li/a"))
        )
        boton_citas_hoy.click()

        seleccionar_cita = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/div/div/table/tbody/tr[2]/td[1]/a"))
        )
        seleccionar_cita.click()

        aceptar_guardar = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/input[4]"))
        )
        aceptar_guardar.click()

        try:
    
            mensaje_error = WebDriverWait(self.driver, 10).until(
                EC.visibility_of_element_located((By.XPATH, "//div[contains(text(),'Fail') or contains(text(),'Fallar') or contains(text(),'No se pudo guardar') or contains(text(),'Could not save')]"))
            )

            self.assertTrue(True)

        except Exception:
            self.borrar_citas()
            self.assertTrue(False)
            
    # ID = 59
    # Nombre: EnviarFormularioCitasConPrescipcionEnBlanco
    # Datos: Enfermedad = Gripe y Progreso = Bueno
    # Resultado esperado: Al enviar el formulario de cita con la prescripción en blanco, debería aparecer un mensaje de error.
    # Esta falla

    def test_EnviarFormularioCitasConPrescipcionEnBlanco(self):

        self.agendar_cita()

        self.iniciar_sesion()

        boton_citas_hoy = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/nav/div/ul[2]/li/a"))
        )
        boton_citas_hoy.click()

        seleccionar_cita = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/div/div/table/tbody/tr[2]/td[1]/a"))
        )
        seleccionar_cita.click()

        campo_enfermedad = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[1]"))
        )
        campo_enfermedad.send_keys("Gripe")

        campo_progreso = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[2]"))
        )
        campo_progreso.send_keys("Bueno")

        campo_prescripcion = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[3]"))
        )
        campo_prescripcion.send_keys("")

        aceptar_guardar = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/input[4]"))
        )
        aceptar_guardar.click()

        try:
    
            mensaje_error = WebDriverWait(self.driver, 10).until(
                EC.visibility_of_element_located((By.XPATH, "//div[contains(text(),'Fail') or contains(text(),'Fallar') or contains(text(),'No se pudo guardar') or contains(text(),'Could not save')]"))
            )

            self.assertTrue(True)

        except Exception:
            self.borrar_citas()
            self.assertTrue(False)

    # ID = 60
    # Nombre: EnviarFormularioCitasConEnfermedadVacio
    # Datos: Progreso = Bueno y Prescripcion = Acetaminofen
    # Resultado esperado: Al enviar el formulario de cita con la enfermedad vacía, debería aparecer un mensaje de error.
    # Esta falla

    def test_EnviarFormularioCitasConEnfermedadVacio(self):

        self.agendar_cita()

        self.iniciar_sesion()

        boton_citas_hoy = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/nav/div/ul[2]/li/a"))
        )
        boton_citas_hoy.click()

        seleccionar_cita = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/div/div/table/tbody/tr[2]/td[1]/a"))
        )
        seleccionar_cita.click()

        campo_enfermedad = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[1]"))
        )
        campo_enfermedad.send_keys("")

        campo_progreso = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[2]"))
        )
        campo_progreso.send_keys("Bueno")

        campo_prescripcion = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[3]"))
        )
        campo_prescripcion.send_keys("Acetaminofen")

        aceptar_guardar = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/input[4]"))
        )
        aceptar_guardar.click()

        try:
    
            mensaje_error = WebDriverWait(self.driver, 10).until(
                EC.visibility_of_element_located((By.XPATH, "//div[contains(text(),'Fail') or contains(text(),'Fallar') or contains(text(),'No se pudo guardar') or contains(text(),'Could not save')]"))
            )

            self.assertTrue(True)

        except Exception:
            self.borrar_citas()
            self.assertTrue(False)

    # ID = 61
    # Nombre: EnviarFormularioCitasConProgresoVacio
    # Datos: Enfermedad = Gripe y Prescripcion = Acetaminofen
    # Resultado esperado: Al enviar el formulario de cita con el campo progreso vacío, debería aparecer un mensaje de error.
    # Esta falla

    def test_EnviarFormularioCitasConProgresoVacio(self):

        self.agendar_cita()

        self.iniciar_sesion()

        boton_citas_hoy = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/nav/div/ul[2]/li/a"))
        )
        boton_citas_hoy.click()

        seleccionar_cita = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/div/div/table/tbody/tr[2]/td[1]/a"))
        )
        seleccionar_cita.click()

        campo_enfermedad = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[1]"))
        )
        campo_enfermedad.send_keys("Gripe")  

        campo_progreso = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[2]"))
        )
        campo_progreso.send_keys("")  

        campo_prescripcion = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[3]"))
        )
        campo_prescripcion.send_keys("Acetaminofen")

        aceptar_guardar = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/input[4]"))
        )
        aceptar_guardar.click()

        try:
    
            mensaje_error = WebDriverWait(self.driver, 10).until(
                EC.visibility_of_element_located((By.XPATH, "//div[contains(text(),'Fail') or contains(text(),'Fallar') or contains(text(),'No se pudo guardar') or contains(text(),'Could not save')]"))
            )

            self.assertTrue(True)

        except Exception:
            self.borrar_citas()
            self.assertTrue(False)

    # ID = 62
    # Nombre: EnviarFormularioCitasConTodoCorrecto
    # Datos: Enfermedad = Gripe, Progreso = Bueno y Prescripcion = Acetaminofen
    # Resultado esperado: Al enviar el formulario correctamente, debe aparecer un mensaje de éxito con la alerta "Information Successfully Updated".
    
    def test_EnviarFormularioCitasConTodoCorrecto(self):

        self.agendar_cita()

        self.iniciar_sesion()
        

        boton_citas_hoy = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/nav/div/ul[2]/li/a"))
        )
        boton_citas_hoy.click()

        seleccionar_cita = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/div/div/table/tbody/tr[2]/td[1]/a"))
        )
        seleccionar_cita.click()

        campo_enfermedad = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[1]"))
        )
        campo_enfermedad.send_keys("Gripe")

        campo_progreso = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[2]"))
        )
        campo_progreso.send_keys("Bueno")

        campo_prescripcion = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[3]"))
        )
        campo_prescripcion.send_keys("Acetaminofen")

        aceptar_guardar = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/input[4]"))
        )
        aceptar_guardar.click()

        try:
            
            WebDriverWait(self.driver, 10).until(EC.alert_is_present())  
            alerta = self.driver.switch_to.alert  
            mensaje_alerta = alerta.text 

            self.assertEqual(mensaje_alerta, "Information Successfully Updated")
            self.borrar_citas()
            alerta.accept()
            

        except (NoSuchElementException, TimeoutException):
            self.assertTrue(False, "No se encontró el mensaje de éxito esperado.")

    # ID = 63
    # Nombre: EnviarFormularioCitasConCampoEnfermedadLlenoDeNumeros
    # Datos: Enfermedad = 31415, Progreso = Bueno y Prescripcion = Acetaminofen
    # Resultado esperado: Al enviar el formulario con el campo de enfermedad lleno de números, debe aparecer un mensaje de error.
    # Esta falla

    def test_EnviarFormularioCitasConCampoEnfermedadLlenoDeNumeros(self):

        self.agendar_cita()

        self.iniciar_sesion()

        boton_citas_hoy = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/nav/div/ul[2]/li/a"))
        )
        boton_citas_hoy.click()

        seleccionar_cita = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/div/div/table/tbody/tr[2]/td[1]/a"))
        )
        seleccionar_cita.click()

        campo_enfermedad = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[1]"))
        )
        campo_enfermedad.send_keys("31415")

        campo_progreso = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[2]"))
        )
        campo_progreso.send_keys("Bueno")

        campo_prescripcion = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[3]"))
        )
        campo_prescripcion.send_keys("Acetaminofen")

        aceptar_guardar = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/input[4]"))
        )
        aceptar_guardar.click()

        try:
            mensaje_error = WebDriverWait(self.driver, 10).until(
                EC.visibility_of_element_located((By.XPATH, "//div[contains(text(),'Fail') or contains(text(),'Fallar') or contains(text(),'Formato incorrecto') or contains(text(),'Incorrect format')]"))
            )
            self.assertTrue(True)
        except Exception:
            self.borrar_citas()
            self.assertTrue(False)

    # ID = 64
    # Nombre: EnviarFormularioCitasConCampoProgresoLlenoDeNumeros
    # Datos: Enfermedad = Gripe, Progreso = 124353 y Prescripcion = Acetaminofen
    # Resultado esperado: Al enviar el formulario con el campo de progreso lleno de números, debe aparecer un mensaje de error.
    # Esta falla

    def test_EnviarFormularioCitasConCampoProgresoLlenoDeNumeros_64(self):

        self.agendar_cita()

        self.iniciar_sesion()

        boton_citas_hoy = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/nav/div/ul[2]/li/a"))
        )
        boton_citas_hoy.click()

        seleccionar_cita = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/div/div/table/tbody/tr[2]/td[1]/a"))
        )
        seleccionar_cita.click()

        campo_enfermedad = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[1]"))
        )
        campo_enfermedad.send_keys("Gripe") 

        campo_progreso = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[2]"))
        )
        campo_progreso.send_keys("124353")  

        campo_prescripcion = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[3]"))
        )
        campo_prescripcion.send_keys("Acetaminofen")

        aceptar_guardar = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/input[4]"))
        )
        aceptar_guardar.click()

        try:
            mensaje_error = WebDriverWait(self.driver, 10).until(
                EC.visibility_of_element_located((By.XPATH, "//div[contains(text(),'Fail') or contains(text(),'Fallar') or contains(text(),'Formato incorrecto') or contains(text(),'Incorrect format')]"))
            )
            self.assertTrue(True)
        except Exception:
            self.borrar_citas()
            self.assertTrue(False)
    
    # ID = 65
    # Nombre: EnviarFormularioCitasConCampoPrescripcionLlenoDeNumeros
    # Datos: Enfermedad = Gripe, Progreso = Bueno y Prescripcion = 9999
    # Resultado esperado: Al enviar el formulario con el campo prescripción lleno de números, debe aparecer un mensaje de error.
    # Esta falla

    def test_EnviarFormularioCitasConCampoPrescripcionLlenoDeNumeros_65(self):

        self.agendar_cita()

        self.iniciar_sesion()
        
        boton_citas_hoy = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/nav/div/ul[2]/li/a"))
        )
        boton_citas_hoy.click()

        seleccionar_cita = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/div/div/table/tbody/tr[2]/td[1]/a"))
        )
        seleccionar_cita.click()

        campo_enfermedad = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[1]"))
        )
        campo_enfermedad.send_keys("Gripe")  

        campo_progreso = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[2]"))
        )
        campo_progreso.send_keys("Bueno")  

        campo_prescripcion = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[3]"))
        )
        campo_prescripcion.send_keys("9999")  

        aceptar_guardar = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/input[4]"))
        )
        aceptar_guardar.click()

        try:
            mensaje_error = WebDriverWait(self.driver, 10).until(
                EC.visibility_of_element_located((By.XPATH, "//div[contains(text(),'Fail') or contains(text(),'Fallar') or contains(text(),'Formato incorrecto') or contains(text(),'Incorrect format')]"))
            )
            self.assertTrue(True)
        except Exception:
            self.borrar_citas()
            self.assertTrue(False)    

    
    # ID = 72
    # Nombre: MarcarFacturaComoNoPagada
    # Datos: Enfermedad = Gripe, Progreso = Bueno y Prescripción = Acetaminofen
    # Resultado esperado: Al generar la factura y marcarla como no pagada, debe aparecer un mensaje de éxito indicando que la factura no ha sido pagada.
    # Esta falla

    def test_MarcarFacturaComoNoPagada_72(self):

        self.agendar_cita()

        self.iniciar_sesion()

        boton_citas_hoy = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/nav/div/ul[2]/li/a"))
        )
        boton_citas_hoy.click()

        seleccionar_cita = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/div/div/table/tbody/tr[2]/td[1]/a"))
        )
        seleccionar_cita.click()

        campo_enfermedad = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[1]"))
        )
        campo_enfermedad.send_keys("Gripe")

        campo_progreso = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[2]"))
        )
        campo_progreso.send_keys("Bueno")

        campo_prescripcion = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[3]"))
        )
        campo_prescripcion.send_keys("Acetaminofen")

        generar_factura = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/input[5]"))
        )
        generar_factura.click()

        no_pago = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/input[2]"))
        )
        no_pago.click()

        try:
            
            mensaje_error = WebDriverWait(self.driver, 10).until(
                EC.visibility_of_element_located(
                    (By.XPATH, "//div[contains(text(),'Exito') or contains(text(),'Unpaid') or contains(text(),'No pago') or contains(text(),'Success')]")
                )
            )
            self.assertTrue(True) 
        except Exception:
            self.borrar_citas()
            self.assertTrue(False) 

    # ID = 73
    # Nombre: EnviarFormularioCitasConCampoEnfermedadLlenoConUnaCadenaDeCienCaracteres
    # Datos: Enfermedad = (Cadena de 100 caracteres "a"), Progreso = Bueno y Prescripción = Acetaminofen
    # Resultado esperado: Al enviar el formulario con el campo enfermedad lleno con una cadena de cien caracteres, debe aparecer un mensaje de error indicando que el 
    # límite de caracteres ha sido excedido.
    # Esta falla
    
    def test_EnviarFormularioCitasConCampoEnfermedadLlenoConUnaCadenaDeCienCaracteres_73(self):


        self.agendar_cita()

        self.iniciar_sesion()
        

        boton_citas_hoy = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/nav/div/ul[2]/li/a"))
        )
        boton_citas_hoy.click()

        seleccionar_cita = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/div/div/table/tbody/tr[2]/td[1]/a"))
        )
        seleccionar_cita.click()

        campo_enfermedad = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[1]"))
        )
        campo_enfermedad.send_keys("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")  

        campo_progreso = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[2]"))
        )
        campo_progreso.send_keys("Bueno")  

        campo_prescripcion = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[3]"))
        )
        campo_prescripcion.send_keys("Acetaminofen")  

        aceptar_guardar = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/input[4]"))
        )
        aceptar_guardar.click()

        try:
            
            mensaje_error = WebDriverWait(self.driver, 10).until(
                EC.visibility_of_element_located(
                    (By.XPATH, "//div[contains(text(),'Fail') or contains(text(),'Fallar') or contains(text(),'Limit') or contains(text(),'Invalid')]")
                )
            )
            self.assertTrue(True) 
        except Exception:
            self.borrar_citas()
            self.assertTrue(False)
    
    # ID = 74
    # Nombre: EnviarFormularioCitasConCampoProgresoConCaracteresEspecialesChinos
    # Datos: Enfermedad = Gripe, Progreso = "非常漂亮的狗" (Texto en chino) y Prescripción = Acetaminofen
    # Resultado esperado: Al enviar el formulario con caracteres especiales chinos en el campo progreso, debe aparecer un mensaje de error indicando que el formato es inválido.
    # Esta falla
    
    def test_EnviarFormularioCitasConCampoProgresoConCaracteresEspecialesChinos_74(self):

        self.agendar_cita()

        self.iniciar_sesion()

        boton_citas_hoy = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/nav/div/ul[2]/li/a"))
        )
        boton_citas_hoy.click()

        seleccionar_cita = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/div/div/table/tbody/tr[2]/td[1]/a"))
        )
        seleccionar_cita.click()

        campo_enfermedad = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[1]"))
        )
        campo_enfermedad.send_keys("Gripe")  

        campo_progreso = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[2]"))
        )
        campo_progreso.send_keys("非常漂亮的狗")  

        campo_prescripcion = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[3]"))
        )
        campo_prescripcion.send_keys("Acetaminofen")  

        aceptar_guardar = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/input[4]"))
        )
        aceptar_guardar.click()

        try:
            
            mensaje_error = WebDriverWait(self.driver, 10).until(
                EC.visibility_of_element_located(
                    (By.XPATH, "//div[contains(text(),'Fail') or contains(text(),'Fallar') or contains(text(),'Invalido') or contains(text(),'Invalid')]")
                )
            )
            self.assertTrue(True) 
        except Exception:
            self.borrar_citas()
            self.assertTrue(False)
    
    # ID = 75
    # Nombre: EnviarFormularioCitasConCamposPrescripcionLlenoConTabsEntersYEspacios
    # Datos: Enfermedad = Gripe, Progreso = Bueno, Prescripción = "  \t\n\n\n" (Cadena con espacios, tabulaciones y saltos de línea)
    # Resultado esperado: Al enviar el formulario con el campo prescripción lleno con caracteres de tabulación, enter y espacios, debe aparecer un mensaje de error indicando que el formato es inválido.
    # Esta falla

    def test_EnviarFormularioCitasConCamposPrescripcionLlenoConTabsEntersYEspacios_75(self):

        self.agendar_cita()

        self.iniciar_sesion()

        boton_citas_hoy = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/nav/div/ul[2]/li/a"))
        )
        boton_citas_hoy.click()

        seleccionar_cita = WebDriverWait(self.driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "/html/body/form/div[3]/div/div/table/tbody/tr[2]/td[1]/a"))
        )
        seleccionar_cita.click()

        campo_enfermedad = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[1]"))
        )
        campo_enfermedad.send_keys("Gripe")  

        campo_progreso = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[2]"))
        )
        campo_progreso.send_keys("Bueno")  

        campo_prescripcion = WebDriverWait(self.driver, 10).until(
            EC.visibility_of_element_located((By.XPATH, "/html/body/form/div[3]/input[3]"))
        )
        campo_prescripcion.send_keys("  \t\n\n\n")  

        try:
            
            mensaje_error = WebDriverWait(self.driver, 10).until(
                EC.visibility_of_element_located(
                    (By.XPATH, "//div[contains(text(),'Fail') or contains(text(),'Fallar') or contains(text(),'Invalido') or contains(text(),'Invalid')]")
                )
            )
            self.assertTrue(True) 
        except Exception:
            self.borrar_citas()
            self.assertTrue(False)
    
    
    def tearDown(self):

        self.driver.quit()  

if __name__ == "__main__":
    unittest.main()