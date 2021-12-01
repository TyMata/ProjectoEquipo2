using System;

namespace ClassLibrary
{
    /// <summary>
    /// Interfaz para implementar el patrón Chain of Responsibility. En ese patrón se pasa un mensaje a través de una
    /// cadena de "handlers" que pueden procesar el mensaje o no. Cada handler decide si procesa el mensaje, o si se lo
    /// pasa al siguiente. Esta interfaz define un método para definir el "handler" siguiente y una una operación para
    /// recibir el mensaje a procesar y en caso de que no lo procesese lo pasa al siguiente "handler". La responsabilidad de
    /// decidir si el mensaje se procesa o no, y de procesarlo, se realiza en las clases que implementan esta interfaz.
    /// 
    /// La interfaz se crea a partir del Dependency Inversion Principle (Dip), para que los clientes de la CoR, no dependan de una clase "handler" que potencialmente es abstracta.
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// Pasa el mensaje al proximo Handler.
        /// </summary>
        /// <param name="handler"></param>
        IHandler SetNext(IHandler handler);

        /// <summary>
        /// Procesa el mensaje o lo pasa al siguiente si tiene un handler siguiente.
        /// Devuelve el handler que procesa el mensaje.
        /// </summary>
        IHandler Handle(IMessage input, out string response);
        
        /// <summary>
        /// Ejecución del proceso de Handler.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        bool InternalHandle(IMessage input, out string response);
    }
}