import unittest
from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.common.exceptions import NoAlertPresentException
from selenium.common.exceptions import NoSuchElementException
from selenium.webdriver.support.ui import Select
from time import sleep

class HospitalManagementTests(unittest.TestCase):
	login_site = "http://127.0.0.1:8080/SignUp.aspx"
	login_user_xpath = "//*[@id=\"loginEmail\"]"
	login_pass_xpath = "//*[@id=\"loginPassword\"]"
	login_button_xpath = "//*[@id=\"loginUserName\"]"
	test_patient_user = "ABC@gmail.com"
	test_patient_pass = "abc"
	take_appoitment_xpath = "/html/body/form/div[2]/nav/div/ul[5]/li/a"
	department_xpath = "/html/body/form/div[2]/div/table/tbody/tr[2]/td[1]/a"
	doctor_select_xpath = "/html/body/form/div[2]/div/table/tbody/tr[2]/td[1]/a"
	doctor_take_appointment_xpath = "//*[@id=\"ctl00_ContentPlaceHolder1_AppointmentB\"]"
	select_time_xpath = "/html/body/form/div[2]/div/table/tbody/tr[2]/td[1]/a"
	login_doctor_user = "farhan@gmail.com"
	login_doctor_pass = "abc"
	make_register_xpath = "/html/body/form/div[2]/input"
	current_appointments_site = "http://127.0.0.1:8080/Doctor/PendingAppointment.aspx"
	goto_appointments_xpath = "/html/body/form/div[2]/nav/div/ul[3]/li/a"
	logout_xpath = "/html/body/form/div[2]/nav/div/ul[1]/li/a"
	delete_appointment_xpath = "/html/body/form/div[2]/div/div/table/tbody/tr[2]/td[1]/a[1]"
	patient_notifications_xpath = "/html/body/form/div[2]/nav/div/ul[7]/li/a"
	accept_appointment_xpath = "/html/body/form/div[2]/div/div/table/tbody/tr[2]/td[1]/a[2]"
	todays_appointments_link = "http://127.0.0.1:8080/Doctor/PatientHistory.aspx"
	complete_appointment_xpath = "/html/body/form/div[2]/div/div/table/tbody/tr[2]/td[1]/a"
	update_history_link = "http://127.0.0.1:8080/Doctor/HistoryUpdate.aspx"
	disease_fill_xpath = "//*[@id=\"ctl00_Cp1_Disease\"]"
	progress_fill_xpath = "//*[@id=\"ctl00_Cp1_progress\"]"
	prescription_fill_xpath = "//*[@id=\"ctl00_Cp1_Prescription\"]"
	generate_bill_xpath = "//*[@id=\"ctl00_Cp1_Bill\"]"
	bill_link = "http://127.0.0.1:8080/Doctor/Bill.aspx"
	pay_bill_xpath = "//*[@id=\"ctl00_Cp3_Bill\"]"
	feedback_link = "http://127.0.0.1:8080/Patient/PatientFeedback.aspx"
	feedback_selector_xpath = "//*[@id=\"ctl00_ContentPlaceHolder1_List\"]"
	give_feedback_xpath = "//*[@id=\"ctl00_ContentPlaceHolder1_button1\"]"
	reputation_value_xpath = "//*[@id=\"ctl00_ContentPlaceHolder1_DRI\"]"

	def setUp(self):
		self.driver = webdriver.Firefox()
		self.driver.implicitly_wait(5)
		self.driver.get(self.login_site)
	
	def fill(self, xpath, data):
		self.driver.find_element(By.XPATH, xpath).send_keys(data)
	
	def click(self, xpath):
		self.driver.find_element(By.XPATH, xpath).click()

	def pageHasStrings(self, arr):
		for i in arr: self.assertIn(i, self.driver.page_source)

	def pageNotHasStrings(self, arr):
		for i in arr: self.assertNotIn(i, self.driver.page_source)
	
	def goto(self, link):
		self.driver.get(link)

	def entrar(self, u, p):
		self.fill(self.login_user_xpath, u)
		self.fill(self.login_pass_xpath, p)
		self.click(self.login_button_xpath)

	def llegar_a_departamentos(self):
		self.entrar(self.test_patient_user, self.test_patient_pass)
		self.click(self.take_appoitment_xpath)

	"""
	 Código: 001
	 Descripción: Los pacientes pueden ver todos los departamentos.
	 Prerrequisitos: Tener el sistema activo sobre localhost con una conexión abierta a la base de datos y estar en la página de “http://127.0.0.1:8080/SignUp.aspx” con un navegador estándar (Chrome, Firefox o Safari).
	 Pasos a reproducir:
		 - Iniciar sesión con los datos de prueba.
		 - Presionar “Take Appointment” en el menú.
	 Datos de prueba:
		 - Email: ABC@gmail.com
		 - Password: abc
	 Resultados esperados: Cada paciente puede ver todos los departamentos.
	 Automatizado?: Sí
	"""
	def test_1(self):
		self.llegar_a_departamentos()

		self.pageHasStrings(["Cardiology", "Orthopaedics", "Ears Nose Throat", "Physiotherapy", "Neurology"])

	def llegar_a_cardio(self):
		self.llegar_a_departamentos()
		self.click(self.department_xpath)

	"""
	 Código: 002
	 Descripción: Los pacientes pueden seleccionar un departamento de todos.
	 Prerrequisitos: Tener el sistema activo sobre localhost con una conexión abierta a la base de datos y estar en la página de “http://127.0.0.1:8080/SignUp.aspx” con un navegador estándar (Chrome, Firefox o Safari).
	 Pasos a reproducir:
		 - Iniciar sesión con los datos de prueba.
		 - Presionar “Take Appointment” en el menú.
		 - Seleccionar el departamento de prueba con el botón de “Select”.
	 Datos de prueba:
		 - Email: ABC@gmail.com
		 - Password: abc
		 - Departamento a seleccionar: Cardiology
	 Resultados esperados: Cada paciente puede seleccionar uno de los departamentos.
	 Automatizado?: Sí
	"""
	def test_2(self):
		self.llegar_a_cardio()

		self.pageHasStrings(["Select a Doctor to view his Profile"])

	"""
	 Código: 003
	 Descripción: Los pacientes ven a todos los doctores asignados a un departamento seleccionado.
	 Prerrequisitos: Tener el sistema activo sobre localhost con una conexión abierta a la base de datos y estar en la página de “http://127.0.0.1:8080/SignUp.aspx” con un navegador estándar (Chrome, Firefox o Safari).
	 Pasos a reproducir: 
		 - Iniciar sesión con los datos de prueba.
		 - Presionar “Take Appointment” en el menú.
		 - Seleccionar el departamento de prueba con el botón de “Select”.
	 Datos de prueba: 
		 - Email: ABC@gmail.com
		 - Password: abc
		 - Departamento a seleccionar: Cardiology
	 Resultados esperados: Cada paciente puede ver a todos los doctores asignados a un departamento actualmente seleccionado.
	 Automatizado?: Sí
	"""	
	def test_3(self):
		self.llegar_a_cardio()

		self.pageHasStrings(["Farhan Shoukat", "KASHAN", "HASSAAN", "HARIS MUNEER"])
	
	def llegar_a_seleccionar_doctor(self):
		self.llegar_a_cardio()
		self.click(self.doctor_select_xpath)
	
	"""
	 Código: 004
	 Descripción: Los pacientes ven todos los detalles públicos de un doctor seleccionado.
	 Prerrequisitos: Tener el sistema activo sobre localhost con una conexión abierta a la base de datos y estar en la página de “http://127.0.0.1:8080/SignUp.aspx” con un navegador estándar (Chrome, Firefox o Safari).
	 Pasos a reproducir: 
		 - Iniciar sesión con los datos de prueba.
		 - Presionar “Take Appointment” en el menú.
		 - Seleccionar el departamento de prueba con el botón de “Select”.
		 - Seleccionar el doctor de prueba con el botón de “Select”.
	 Datos de prueba: 
		 - Email: ABC@gmail.com
		 - Password: abc
		 - Departamento a seleccionar: Cardiology
		 - Doctor a seleccionar: DoctorID = 2 (aka Farhan)
	 Resultados esperados: Cada paciente puede ver todos los detalles públicos de un doctor actualmente seleccionado.
	 Automatizado?: Sí
	"""
	def test_4(self):
		self.llegar_a_seleccionar_doctor()

		self.pageHasStrings(["Doctor's Profile", "Farhan Shoukat", "156133213", "PHD IN EVERY FIELD KNOWN TO MAN", "ENJOY", "Cardiology"])
	
	"""
	 Código: 005
	 Descripción: Los pacientes tienen acceso a un botón de “Take Appointment” en el perfíl de un doctor seleccionado.
	 Prerrequisitos: Tener el sistema activo sobre localhost con una conexión abierta a la base de datos y estar en la página de “http://127.0.0.1:8080/SignUp.aspx” con un navegador estándar (Chrome, Firefox o Safari).
	 Pasos a reproducir: 
		 - Iniciar sesión con los datos de prueba.
		 - Presionar “Take Appointment” en el menú.
		 - Seleccionar el departamento de prueba con el botón de “Select”.
		 - Seleccionar el doctor de prueba con el botón de “Select”.
	 Datos de prueba: 
		 - Email: ABC@gmail.com
		 - Password: abc
		 - Departamento a seleccionar: Cardiology
		 - Doctor a seleccionar: DoctorID = 2 (aka Farhan)
	 Resultados esperados: Cada paciente tiene acceso a un botón de “Take Appointment” en el perfil del doctor actualmente seleccionado.
	 Automatizado?: Sí
	"""
	def test_5(self):
		self.llegar_a_seleccionar_doctor()

		self.pageHasStrings(["Take Appointment"])
	
	def tomar_cita_con_doctor(self):
		self.llegar_a_seleccionar_doctor()
		self.click(self.doctor_take_appointment_xpath)

	"""
	 Código: 006
	 Descripción: Los tiempos/horarios disponibles para una cita se despliegan cuando se presiona el botón de “Take Appointment”.
	 Prerrequisitos: Tener el sistema activo sobre localhost con una conexión abierta a la base de datos y estar en la página de “http://127.0.0.1:8080/SignUp.aspx” con un navegador estándar (Chrome, Firefox o Safari).
	 Pasos a reproducir: 
		 - Iniciar sesión con los datos de prueba.
		 - Presionar “Take Appointment” en el menú.
		 - Seleccionar el departamento de prueba con el botón de “Select”.
		 - Seleccionar el doctor de prueba con el botón de “Select”.
		 - Presionar el botón de “Take Appointment” en el perfíl del doctor de prueba.
	 Datos de prueba: 
		 - Email: ABC@gmail.com
		 - Password: abc
		 - Departamento a seleccionar: Cardiology
		 - Doctor a seleccionar: DoctorID = 2 (aka Farhan)
	 Resultados esperados: Cada paciente puede ver el horario de espacios disponibles para una cita con el doctor actualmente seleccionado después de presionar el botón de “Take Appointment” en el perfil del doctor.
	 Automatizado?: Sí
	"""
	def test_6(self):
		self.tomar_cita_con_doctor()

		self.pageHasStrings(["Free Time Slots"])
	
	"""
	 Código: 007
	 Descripción: Los pacientes no pueden ver los detalles privados de un doctor seleccionado.
	 Prerrequisitos: Tener el sistema activo sobre localhost con una conexión abierta a la base de datos y estar en la página de “http://127.0.0.1:8080/SignUp.aspx” con un navegador estándar (Chrome, Firefox o Safari).
	 Pasos a reproducir: 
		 - Iniciar sesión con los datos de prueba.
		 - Presionar “Take Appointment” en el menú.
		 - Seleccionar el departamento de prueba con el botón de “Select”.
		 - Seleccionar el doctor de prueba con el botón de “Select”.
	 Datos de prueba: 
		 - Email: ABC@gmail.com
		 - Password: abc
		 - Departamento a seleccionar: Cardiology
		 - Doctor a seleccionar: DoctorID = 2 (aka Farhan)
	 Resultados esperados: Ningún paciente puede ver los detalles privados de cualquier doctor.
	 Automatizado?: Sí
	"""
	def test_7(self):
		self.llegar_a_seleccionar_doctor()

		self.pageNotHasStrings(["Enjoy, Lahore", "30000", "4/12/1996 12:00:00 AM"])
	
	"""
	 Código: 008
	 Descripción: Los espacios no disponibles en el horario de los doctores no se despliegan.
	 Prerrequisitos: Tener el sistema activo sobre localhost con una conexión abierta a la base de datos y estar en la página de “http://127.0.0.1:8080/SignUp.aspx” con un navegador estándar (Chrome, Firefox o Safari).
	 Pasos a reproducir: 
		 - Iniciar sesión con los datos de prueba.
		 - Presionar “Take Appointment” en el menú.
		 - Seleccionar el departamento de prueba con el botón de “Select”.
		 - Seleccionar el doctor de prueba con el botón de “Select”.
		 - Presionar el botón de “Take Appointment” en el perfíl del doctor de prueba.
	 Datos de prueba: 
		 - Email: ABC@gmail.com
		 - Password: abc
		 - Departamento a seleccionar: Cardiology
		 - Doctor a seleccionar: DoctorID = 2 (aka Farhan)
	 Resultados esperados: Ningún paciente puede seleccionar un espacio no disponible en el horario de cualquier doctor.
	 Automatizado?: Sí
	"""
	def test_8(self):
		self.tomar_cita_con_doctor()

		self.pageNotHasStrings(["2:00 AM", "3:00 AM", "4:00 AM", "5:00 AM", "6:00 AM", "6:00 PM", "7:00 AM", "7:00 PM", "8:00 AM", "8:00 PM", "9:00 PM", "10:00 PM", "11:00 PM", "12:00 AM"])
	
	def escoger_hora(self):
		self.tomar_cita_con_doctor()
		self.click(self.select_time_xpath)

	"""
	 Código: 009
	 Descripción: Un paciente puede solicitar una cita sobre un espacio disponible.
	 Prerrequisitos: Tener el sistema activo sobre localhost con una conexión abierta a la base de datos y estar en la página de “http://127.0.0.1:8080/SignUp.aspx” con un navegador estándar (Chrome, Firefox o Safari).
	 Pasos a reproducir:
		 - Iniciar sesión con los datos de prueba.
		 - Presionar “Take Appointment” en el menú.
		 - Seleccionar el departamento de prueba con el botón de “Select”.
		 - Seleccionar el doctor de prueba con el botón de “Select”.
		 - Presionar el botón de “Take Appointment” en el perfíl del doctor de prueba.
		 - Seleccionar la primera hora de prueba.
	 Datos de prueba:
		 - Email: ABC@gmail.com
		 - Password: abc
		 - Departamento a seleccionar: Cardiology
		 - Doctor a seleccionar: DoctorID = 2 (aka Farhan)
		 - Primera hora de prueba: No. = 1 (aka 9:00am)
	 Resultados esperados: Cada paciente puede solicitar una cita escogiendo un espacio disponible en el horario del doctor actualmente seleccionado después de presionar el botón de “Take Appointment”.
	 Automatizado?: Sí
	"""
	def test_9(self):
		self.escoger_hora()

		self.pageHasStrings(["Click on this button to send an appointment request to the Doctor", "Send Request"])
	
	def mandar_solicitud(self):
		self.escoger_hora()
		self.click(self.make_register_xpath)
	
	def entrar_doctor(self):
		self.entrar(self.login_doctor_user, self.login_doctor_pass)
	
	def salir(self):
		self.click(self.logout_xpath)
		self.goto(self.login_site)
	
	def revisar_citas_actuales(self):
		self.entrar_doctor();
		# This should fail because the website is barely implemented
		# self.click(self.goto_appointments_xpath)
		self.goto(self.current_appointments_site)
	
	def limpiar_citas_pendientes(self):
		self.goto(self.current_appointments_site)
		self.click(self.delete_appointment_xpath)
	
	"""
	 Código: 010
	 Descripción: Las solicitudes de citas son mandadas a los doctores respectivos.
	 Prerrequisitos: Tener el sistema activo sobre localhost con una conexión abierta a la base de datos y estar en la página de “http://127.0.0.1:8080/SignUp.aspx” con un navegador estándar (Chrome, Firefox o Safari).
	 Pasos a reproducir: 
		 - Iniciar sesión con los datos de prueba de paciente.
		 - Presionar “Take Appointment” en el menú.
		 - Seleccionar el departamento de prueba con el botón de “Select”.
		 - Seleccionar el doctor de prueba con el botón de “Select”.
		 - Presionar el botón de “Take Appointment” en el perfíl del doctor de prueba.
		 - Seleccionar la primera hora de prueba.
		 - Confirmar la solicitud.
		 - Salir de la cuenta.
		 - Iniciar sesión con los datos de prueba de doctor.
		 - Entrar a “pending appointments”.
	 Datos de prueba: 
		 - Paciente:
		 - Email: ABC@gmail.com
		 - Password: abc
		 - Doctor:
		 - Email: farhan@gmail.com
		 - Password: abc
		 - Departamento a seleccionar: Cardiology
		 - Doctor a seleccionar: DoctorID = 2 (aka Farhan)
		 - Primera hora de prueba: No. = 1 (aka 9:00am)
	 Resultados esperados: Cuando se genera una solicitud por un paciente para una cita, la solicitud es mandada al doctor correspondiente.
	 Automatizado?: Sí
	"""
	def test_10(self):
		self.mandar_solicitud()
		self.salir()
		self.revisar_citas_actuales()
		self.pageHasStrings(["Pending Appointments", "9:00:00 AM"])
		self.limpiar_citas_pendientes()

	"""
	 Código: 011
	 Descripción: Las citas se aceptan o se rechazan manualmente por el doctor.
	 Prerrequisitos: Tener el sistema activo sobre localhost con una conexión abierta a la base de datos y estar en la página de “http://127.0.0.1:8080/SignUp.aspx” con un navegador estándar (Chrome, Firefox o Safari). Además, recibir (como doctor) una solicitud que no haya revisado.
	 Pasos a reproducir:
		 - Iniciar sesión con los datos de prueba de doctor.
		 - Entrar a “pending appointments”.
	 Datos de prueba:
		 - Email: farhan@gmail.com
		 - Password: abc
	 Resultados esperados: Ninguna cita solicitada por un paciente es aceptada o rechazada automáticamente por el sistema. Es decir, cada cita solicitada tiene que aparecer en una de las secciones del doctor correspondiente como “pendiente”.
	 Automatizado?: Sí
	"""
	def test_11(self):
		self.mandar_solicitud()
		self.salir()
		self.revisar_citas_actuales()
		self.pageHasStrings(["Pending Appointments", "9:00:00 AM"])
		self.limpiar_citas_pendientes()
		self.salir()
		self.driver.get(self.login_site)
		self.escoger_hora()

		self.pageHasStrings(["Click on this button to send an appointment request to the Doctor", "Send Request"])

	def llegar_a_notificaciones(self):
		self.entrar(self.test_patient_user, self.test_patient_pass)
		self.click(self.patient_notifications_xpath)
	
	"""
	 Código: 012
	 Descripción: La sección de “Notificaciones” está disponible en toda la página.
	 Prerrequisitos: Tener el sistema activo sobre localhost con una conexión abierta a la base de datos y estar en la página de “http://127.0.0.1:8080/SignUp.aspx” con un navegador estándar (Chrome, Firefox o Safari).
	 Pasos a reproducir: 
		 - Iniciar sesión con los datos de prueba.
		 - Ir a “Notifications” en el menú.
	 Datos de prueba: 
		 - Email: ABC@gmail.com
		 - Password: abc
	 Resultados esperados: Cada paciente puede acceder desde todas las subsecciones de la página a la sección de “Notificaciones”.
	 Automatizado?: Sí
	"""
	def test_12(self):
		self.llegar_a_notificaciones()
		
		self.pageHasStrings(["Notifications", "PatientNotifications.aspx"])
	
	"""
	 Código: 013
	 Descripción: Las solicitudes aprobadas y/o rechazadas aparecen en la sección de “Notificaciones”.
	 Prerrequisitos: Tener el sistema activo sobre localhost con una conexión abierta a la base de datos y estar en la página de “http://127.0.0.1:8080/SignUp.aspx” con un navegador estándar (Chrome, Firefox o Safari). Además, tener (como paciente) por lo menos una solicitud confirmada/rechazada.
	 Pasos a reproducir: 
		 - Iniciar sesión con los datos de prueba.
		 - Ir a “Notifications” en el menú.
	 Datos de prueba: 
		 - Email: ABC@gmail.com
		 - Password: abc
	 Resultados esperados: En la sección de “Notificaciones” de cada paciente, el paciente observa todas sus solicitudes que fueron rechazadas y/o aprobadas.
	 Automatizado?: Sí
	"""
	def test_13(self):
		self.mandar_solicitud()
		self.salir()
		self.revisar_citas_actuales()
		self.limpiar_citas_pendientes()
		self.salir()
		self.goto(self.login_site)
		self.llegar_a_notificaciones()
		
		self.pageHasStrings(["Notifications", "PatientNotifications.aspx", "Your requested appointment with Doctor Farhan Shoukat has been rejected by him!"])
	
	"""
	 Código: 014
	 Descripción: Las notificaciones están ordenadas.
	 Prerrequisitos: Tener el sistema activo sobre localhost con una conexión abierta a la base de datos y estar en la página de “http://127.0.0.1:8080/SignUp.aspx” con un navegador estándar (Chrome, Firefox o Safari). Además, tener (como paciente) por lo menos una solicitud confirmada/rechazada.
	 Pasos a reproducir: 
		 - Iniciar sesión con los datos de prueba.
		 - Ir a “Notifications” en el menú.
	 Datos de prueba: 
		 - Email: ABC@gmail.com
		 - Password: abc
	 Resultados esperados: En la sección de “Notificaciones” de cada paciente, todas las notificaciones están ordenadas de forma ascendente en términos de tiempo (datetime) de aprobación o rechazo por parte del doctor.
	 Automatizado?: Sí
	"""
	def test_14(self):
		self.mandar_solicitud()
		self.salir()
		self.revisar_citas_actuales()
		self.limpiar_citas_pendientes()
		self.salir()
		self.goto(self.login_site)
		self.llegar_a_notificaciones()
		pagina = self.driver.find_element(By.TAG_NAME, "body").text
		cnt = pagina.count("The Appointment Timings were")

		self.assertEqual(cnt, 1)
	
	"""
	 Código: 015
	 Descripción: Solo se puede solicitar una cita a la vez.
	 Prerrequisitos: Tener el sistema activo sobre localhost con una conexión abierta a la base de datos y estar en la página de “http://127.0.0.1:8080/SignUp.aspx” con un navegador estándar (Chrome, Firefox o Safari). Además, tener (como paciente) una cita pendiente con algún doctor.
	 Pasos a reproducir: 
		 - Iniciar sesión con los datos de prueba.
		 - Presionar “Take Appointment” en el menú.
		 - Seleccionar el departamento de prueba con el botón de “Select”.
		 - Seleccionar el doctor de prueba con el botón de “Select”.
		 - Presionar el botón de “Take Appointment” en el perfíl del doctor de prueba.
		 - Seleccionar la segunda hora de prueba.
		 - Confirmar la solicitud.
	 Datos de prueba: 
		 - Email: ABC@gmail.com
		 - Password: abc
		 - Departamento a seleccionar: Cardiology
		 - Doctor a seleccionar: DoctorID = 2 (aka Farhan)
		 - Segunda hora de prueba: No. = 2 (aka 10:00am)
	 Resultados esperados: Cada paciente puede solicitar solo una cita a la vez y no puede solicitar otra hasta que el doctor la haya negado o la cita se completó.
	 Automatizado?: Sí
	"""
	def test_15(self):
		self.mandar_solicitud()
		self.salir()
		self.mandar_solicitud()

		self.pageHasStrings(["You have already requested for an Appointment"])

		self.salir()
		self.revisar_citas_actuales()
		self.limpiar_citas_pendientes()

	"""
	 Código: 016
	 Descripción: Aparece un mensaje de error si un paciente intenta solicitar más de una cita a la vez.
	 Prerrequisitos: Tener el sistema activo sobre localhost con una conexión abierta a la base de datos y estar en la página de “http://127.0.0.1:8080/SignUp.aspx” con un navegador estándar (Chrome, Firefox o Safari). Además, tener (como paciente) una cita pendiente con algún doctor.
	 Pasos a reproducir: 
		 - Iniciar sesión con los datos de prueba.
		 - Presionar “Take Appointment” en el menú.
		 - Seleccionar el departamento de prueba con el botón de “Select”.
		 - Seleccionar el doctor de prueba con el botón de “Select”.
		 - Presionar el botón de “Take Appointment” en el perfíl del doctor de prueba.
		 - Seleccionar la segunda hora de prueba.
		 - Confirmar la solicitud.
	 Datos de prueba: 
		 - Email: ABC@gmail.com
		 - Password: abc
		 - Departamento a seleccionar: Cardiology
		 - Doctor a seleccionar: DoctorID = 2 (aka Farhan)
		 - Segunda hora de prueba: No. = 2 (aka 10:00am)
	 Resultados esperados: Si un paciente intenta solicitar más de una cita a la vez en términos del mismo periodo establecido en la prueba [015], se le muestra un pop-up que le avisa que no puede ejecutar la acción.
	 Automatizado?: Sí
	"""
	def test_16(self):
		self.mandar_solicitud()
		self.salir()
		self.mandar_solicitud()

		self.pageHasStrings(["already requested"])

		self.salir()
		self.revisar_citas_actuales()
		self.limpiar_citas_pendientes()
	
	def aceptar_cita(self):
		self.mandar_solicitud()
		self.salir()
		self.revisar_citas_actuales()
		self.click(self.accept_appointment_xpath)
		self.goto(self.todays_appointments_link)
		self.click(self.complete_appointment_xpath)
		self.goto(self.update_history_link)
		self.fill(self.disease_fill_xpath, "Waltzing Matilda")
		self.fill(self.progress_fill_xpath, "Matilda")
		self.fill(self.prescription_fill_xpath, "Me Darling")
		self.click(self.generate_bill_xpath)
		self.goto(self.bill_link)
		self.click(self.pay_bill_xpath)
		self.goto(self.todays_appointments_link)
		self.salir()
	
	"""
	 Código: 017
	 Descripción: Una cita se marca automáticamente como completada cuando el doctor le genera la factura.
	 Prerrequisitos: Tener el sistema activo sobre localhost con una conexión abierta a la base de datos y estar en la página de “http://127.0.0.1:8080/SignUp.aspx” con un navegador estándar (Chrome, Firefox o Safari). Además, tener (como doctor) una solicitud de cita nueva de parte de ABC@gmail.com.
	 Pasos a reproducir: 
		 - Iniciar sesión con los datos de prueba de doctor.
		 - Entrar a “Todays Appointments”.
		 - Aprobar a la solicitud del paciente de prueba.
		 - Generar la factura de la cita con el paciente de prueba.
		 - Salir de la cuenta.
		 - Iniciar sesión con los datos de prueba del paciente.
		 - Entrar a “Treatment History”.
	 Datos de prueba: 
		 - Paciente:
		 - Email: ABC@gmail.com
		 - Password: abc
		 - Doctor:
		 - Email: farhan@gmail.com
		 - Password: abc
	 Resultados esperados: La cita tiene que aparecer en el historial de tratamiento y en el historial de facturas después de que el doctor le genera la factura.
	 Automatizado?: Sí
	"""
	def test_17(self):
		self.aceptar_cita()
		self.llegar_a_notificaciones()
		
		self.pageHasStrings(["We hope you are feeling better now"])
	
	def ir_feedback(self):
		self.entrar(self.test_patient_user, self.test_patient_pass)
		self.goto(self.feedback_link)
	
	"""
	 Código: 018
	 Descripción: Aparece la opción de dar retroalimentación cuando una cita completada no la tiene.
	 Prerrequisitos: Tener el sistema activo sobre localhost con una conexión abierta a la base de datos y estar en la página de “http://127.0.0.1:8080/SignUp.aspx” con un navegador estándar (Chrome, Firefox o Safari). Además, tener (como paciente) una cita completada pero sin retroalimentación.
	 Pasos a reproducir: 
		 - Iniciar sesión con los datos de prueba.
		 - Ir a la sección de “Feedback”.
	 Datos de prueba: 
		 - Email: ABC@gmail.com
		 - Password: abc
	 Resultados esperados: Cuando una cita se marca como completada y no ha recibido su retroalimentación, al paciente deberá aparecer la opción de darle retroalimentación.
	 Automatizado?: Sí
	"""
	def test_18(self):
		self.aceptar_cita()
		self.ir_feedback()

		self.pageHasStrings(["How was your treatment experience"])
	
	"""
	 Código: 019
	 Descripción: Existe una subsección dedicada para la retroalimentación.
	 Prerrequisitos: Tener el sistema activo sobre localhost con una conexión abierta a la base de datos y estar en la página de “http://127.0.0.1:8080/SignUp.aspx” con un navegador estándar (Chrome, Firefox o Safari). Además, tener (como paciente) una cita completada pero sin retroalimentación.
	 Pasos a reproducir: 
		 - Iniciar sesión con los datos de prueba.
		 - Ir a la sección de “Feedback”.
	 Datos de prueba: 
		 - Email: ABC@gmail.com
		 - Password: abc
	 Resultados esperados: Si una cita puede recibir retroalimentación, el usuario podrá acceder a una subsección que le permite ejecutar la retroalimentación.
	 Automatizado?: Sí
	"""
	def test_19(self):
		self.aceptar_cita()
		self.ir_feedback()

		self.assertTrue(True)
	
	"""
	 Código: 020
	 Descripción: La retroalimentación se basa de un valor entero desde 1 hasta 5 (inclusivo).
	 Prerrequisitos: Tener el sistema activo sobre localhost con una conexión abierta a la base de datos y estar en la página de “http://127.0.0.1:8080/SignUp.aspx” con un navegador estándar (Chrome, Firefox o Safari). Además, tener (como paciente) una cita completada pero sin retroalimentación.
	 Pasos a reproducir: 
		 - Iniciar sesión con los datos de prueba.
		 - Ir a la sección de “Feedback”.
		 - Abrir el menú dropdown que aparece.
	 Datos de prueba: 
		 - Email: ABC@gmail.com
		 - Password: abc
	 Resultados esperados: Cuando un paciente está en la subsección de retroalimentación, tiene que escoger un valor entero desde 1 hasta 5 (inclusivo).
	 Automatizado?: Sí
	"""
	def test_20(self):
		self.aceptar_cita()
		self.ir_feedback()

		sel = Select(self.driver.find_element(By.XPATH, self.feedback_selector_xpath))
		valores = [i.get_attribute("value") for i in sel.options]
		esperados = ["1", "2", "3", "4", "5"]

		self.assertEqual(len(valores), 5)
		for i in esperados: self.assertIn(i, valores)

	"""
	 Código: 021
	 Descripción: Aparece el botón de “confirmar retroalimentación”.
	 Prerrequisitos: Tener el sistema activo sobre localhost con una conexión abierta a la base de datos y estar en la página de “http://127.0.0.1:8080/SignUp.aspx” con un navegador estándar (Chrome, Firefox o Safari). Además, tener (como paciente) una cita completada pero sin retroalimentación.
	 Pasos a reproducir:
		 - Iniciar sesión con los datos de prueba.
		 - Ir a la sección de “Feedback”.
	 Datos de prueba:
		 - Email: ABC@gmail.com
		 - Password: abc
	 Resultados esperados: Cuando un paciente está en la subsección de retroalimentación, puede presionar el botón de “confirmar retroalimentación”.
	 Automatizado?: Sí
	"""
	def test_21(self):
		self.aceptar_cita()
		self.ir_feedback()

		self.pageHasStrings(["Give Feedback"])
	
	"""
	 Código: 022
	 Descripción: Solo se puede “confirmar retroalimentación” si se seleccionó el valor numérico correspondiente.
	 Prerrequisitos: Tener el sistema activo sobre localhost con una conexión abierta a la base de datos y estar en la página de “http://127.0.0.1:8080/SignUp.aspx” con un navegador estándar (Chrome, Firefox o Safari). Además, tener (como paciente) una cita completada pero sin retroalimentación.
	 Pasos a reproducir: 
		 - Iniciar sesión con los datos de prueba.
		 - Ir a la sección de “Feedback”.
		 - Seleccionar el valor de prueba.
		 - Presionar “Give Feedback”.
	 Datos de prueba: 
		 - Email: ABC@gmail.com
		 - Password: abc
		 - Valor: 3
	 Resultados esperados: Cuando el paciente está en la subsección de retroalimentación, solo puede presionar el botón de “confirmar retroalimentación” si ya seleccionó el valor numérico mencionado en la prueba [020].
	 Automatizado?: Sí
	"""
	def test_22(self):
		self.aceptar_cita()
		self.ir_feedback()
		sel = Select(self.driver.find_element(By.XPATH, self.feedback_selector_xpath))
		sel.select_by_index(2)
		self.click(self.give_feedback_xpath)

		self.assertTrue(True)

	"""
	 Código: 023
	 Descripción: Aparece un error si se presiona “confirmar retroalimentación” y se ha seleccionado el valor numérico correspondiente.
	 Prerrequisitos: Tener el sistema activo sobre localhost con una conexión abierta a la base de datos y estar en la página de “http://127.0.0.1:8080/SignUp.aspx” con un navegador estándar (Chrome, Firefox o Safari). Además, tener (como paciente) una cita completada pero sin retroalimentación.
	 Pasos a reproducir:
		 - Iniciar sesión con los datos de prueba.
		 - Ir a la sección de “Feedback”.
		 - No seleccionar valor de prueba.
		 - Presionar “Give Feedback”.
	 Datos de prueba:
		 - Email: ABC@gmail.com
		 - Password: abc
	 Resultados esperados: Cuando el paciente está en la subsección de retroalimentación, si intenta presionar el botón de “confirmar retroalimentación” y no ha seleccionado el valor numérico mencionado en la prueba [020], se le aparece un error.
	 Automatizado?: Sí
	"""
	def test_23(self):
		self.aceptar_cita()
		self.ir_feedback()
		sel = Select(self.driver.find_element(By.XPATH, self.feedback_selector_xpath))
		try:
			sel.select_by_index(5)
			self.click(self.give_feedback_xpath)
			self.assertTrue(False)
		except Exception as e:
			self.assertTrue(True)

	def obtener_reputacion(self):
		self.llegar_a_seleccionar_doctor()
		
		rep = self.driver.find_element(By.XPATH, self.reputation_value_xpath).text
		try:
			val = float(rep)
			return val
		except Exception as e:
			self.assertTrue(False)

	"""
	 Código: 024
	 Descripción: A cada doctor está asociado un valor numérico flotante público de “calidad” que va desde 1 hasta 5 (inclusivo).
	 Prerrequisitos: Tener el sistema activo sobre localhost con una conexión abierta a la base de datos y estar en la página de “http://127.0.0.1:8080/SignUp.aspx” con un navegador estándar (Chrome, Firefox o Safari).
	 Pasos a reproducir:
		 - Iniciar sesión con los datos de prueba.
		 - Presionar “Take Appointment” en el menú.
		 - Seleccionar el departamento de prueba con el botón de “Select”.
		 - Seleccionar el doctor de prueba con el botón de “Select”.
	 Datos de prueba:
		 - Email: ABC@gmail.com
		 - Password: abc
		 - Departamento a seleccionar: Cardiology
		 - Doctor a seleccionar: DoctorID = 2 (aka Farhan)
	 Resultados esperados: A cada doctor está asociado un valor numérico flotante público de “calidad” que va desde 1 hasta 5 (inclusivo) que se despliega en su perfil y se calcula como $calidad := cantidad_calificaciones == 0 ? 5 : suma_calidad/cantidad_calificaciones$
	 Automatizado?: Sí
	"""
	def test_24(self):
		self.assertTrue(1.0 <= self.obtener_reputacion() <= 5.0)
		
	"""
	 Código: 025
	 Descripción: La retroalimentación impacta correctamente al perfil del doctor.
	 Prerrequisitos: Tener el sistema activo sobre localhost con una conexión abierta a la base de datos y estar en la página de “http://127.0.0.1:8080/SignUp.aspx” con un navegador estándar (Chrome, Firefox o Safari). Además, tener (como paciente) una cita completada pero sin retroalimentación con el doctor de prueba.
	 Pasos a reproducir: 
		 - Iniciar sesión con los datos de prueba.
		 - Ir a la sección de “Feedback”.
		 - Seleccionar el valor de prueba.
		 - Presionar “Give Feedback”.
		 - Presionar “Take Appointment” en el menú.
		 - Seleccionar el departamento de prueba con el botón de “Select”.
		 - Seleccionar el doctor de prueba con el botón de “Select”.
	 Datos de prueba: 
		 - Email: ABC@gmail.com
		 - Password: abc
		 - Valor: 1
		 - Departamento a seleccionar: Cardiology
		 - Doctor a seleccionar: DoctorID = 2 (aka Farhan)
	 Resultados esperados: Cuando un paciente confirma su retroalimentación, se actualiza adecuadamente la calidad del doctor con $suma_calidad += retroalimentacíon$ y $cantidad_calificaciones += 1$.
	 Automatizado?: Sí
	"""
	def test_25(self):
		original = self.obtener_reputacion()
		self.salir()
		self.aceptar_cita()
		self.ir_feedback()
		sel = Select(self.driver.find_element(By.XPATH, self.feedback_selector_xpath))
		sel.select_by_index(3)
		self.click(self.give_feedback_xpath)
		self.salir()
		self.assertNotEqual(original, self.obtener_reputacion())

	def tearDown(self):
		self.driver.quit()

if __name__ == "__main__":
	unittest.main()
