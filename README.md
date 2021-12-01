## Notas entrega 2

En esta entrega tuvimos muchas dificultades, la principal y mas importante fue el no entender desde un principio que se pedia para esta. Nos confundimos e hicimos clases y metodos que no utilizariamos al final y nos trancamos en cosas que no eran importantes para esta entrega y nos sacaron mucho tiempo. Aun asi, creemos que con mas tiempo se podria haber llegado a una mejor implementacion de patrones GRASP y principios SOLID y haber logrado una cohesion alta y un acomplamiento mas bajo.

Aun asi estamos contentos porque logramos entender el patron Chain Of Responsability que va a formar una gran parte en nuestro codigo. Tambien investigamos sobre el patron adapter y como utilizarlo para poder añadir, si es que se quiere, una nueva API de ubicaciones. Sentimos que realizamos una buena practica al momento de crear IMessage y IMessageChannel ya que basta heredar de estas clases si se quiere agregar otras formas de recibir mensajes. Por ahora solo creamos ConsoleMessage y ConsoleChannel para verificar que funcionaba.

Para esta entrega decidimos no utilizar la clase Materials ya que nuestro plan para esta es crear un regisro de materiales ya precargado y darle a elegir a los usuarios entre esas opciones ya sea para buscar ofertas o para crearlas. No pudimos realizar esto ya que no pudimos implementar un serializer a tiempo.
Tampoco decidimos utilizar la clase Setup ya que realizamos cambios en la Chain of Responsability y hay Handlers a los cuales les faltan metodos (aun no creados) necesarios para que funcionen.

Como conclusion final, no estamos satisfechos con nuestro trabajo ya que sentimos y sabemos que podriamos haberlo realizado mejor de haber entendido la consigna desde un principo y creemos que podriamos haber utilizado de mejor manera algunos patrones GRASP y principios SOLID. Tambien se podrian haber realizado mayor cantidad de tests de haber terminado las clases antes.

## Notas entrega 3
Tenemos el conocimiento de que no se pueden ver los comandos del administrador al momento de ingresar por primera vez al bot. Establecimos a cada usuario diferente se le muestre un menu con sus respectivos comandos. Como no logramos que el Deserialize() de json funcione, no pudimos establecer un admin. Para el uso de este Bot sin ningun problema, cuando se ingresa al bot por primera vez, si lo quiere hacer como un usuario empresa,  debe primero de ingrsar el comando de admin "/registraempresa" para generar un token y registar la empresa. Luego si ingresar como un usario empresa no registrado y registarse con el token generado previamente.

Qué desafíos de la entrega fueron los más difíciles:
La implementacion de el Serializer fue algo muy desafiante para nuestro grupo. Nos costó lograr que se pasar los objetos a strings en formato json. Para la implementación de el Deserializer(), tuvimos mas desafíos para poder lograrla, pero no pudimos superarlos. A causa de esto no logramos que funcione, lo que nos trajo varios problemas y formas menos correctas de solucionarlos. 
También la falta de conocimiento acerca de lo que es un bot y la implementación en telegram. Fué un desafío que nos llevó muchas horas de estudio, trabajo en grupo y cambios constantes en el código previamente hecho.

Qué cosas aprendieron enfrentándose al proyecto que no aprendieron en clase como parte de la currícula:
Aprenidimos a enfrentar problemas con soluciones que no teniamos ni idea de su existencia y supimos como lograrlo trabajando en equipo .

