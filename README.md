## Notas entrega 2

En esta entrega tuvimos muchas dificultades, la principal y mas importante fue el no entender desde un principio que se pedia para esta. Nos confundimos e hicimos clases y metodos que no utilizariamos al final y nos trancamos en cosas que no eran importantes para esta entrega y nos sacaron mucho tiempo. Aun asi, creemos que con mas tiempo se podria haber llegado a una mejor implementacion de patrones GRASP y principios SOLID y haber logrado una cohesion alta y un acomplamiento mas bajo.

Aun asi estamos contentos porque logramos entender el patron Chain Of Responsability que va a formar una gran parte en nuestro codigo. Tambien investigamos sobre el patron adapter y como utilizarlo para poder añadir, si es que se quiere, una nueva API de ubicaciones. Sentimos que realizamos una buena practica al momento de crear IMessage y IMessageChannel ya que basta heredar de estas clases si se quiere agregar otras formas de recibir mensajes. Por ahora solo creamos ConsoleMessage y ConsoleChannel para verificar que funcionaba.

Para esta entrega decidimos no utilizar la clase Materials ya que nuestro plan para esta es crear un regisro de materiales ya precargado y darle a elegir a los usuarios entre esas opciones ya sea para buscar ofertas o para crearlas. No pudimos realizar esto ya que no pudimos implementar un serializer a tiempo.
Tampoco decidimos utilizar la clase Setup ya que realizamos cambios en la Chain of Responsability y hay Handlers a los cuales les faltan metodos (aun no creados) necesarios para que funcionen.

Como conclusion final, no estamos satisfechos con nuestro trabajo ya que sentimos y sabemos que podriamos haberlo realizado mejor de haber entendido la consigna desde un principo y creemos que podriamos haber utilizado de mejor manera algunos patrones GRASP y principios SOLID. Tambien se podrian haber realizado mayor cantidad de tests de haber terminado las clases antes.