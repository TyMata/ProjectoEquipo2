using System;
using System.Text;
using System.Linq;

namespace ClassLibrary
{
    /// <summary>
    /// Un handler del patrón Chain Of Responsability que implementa el comando "/mostrarofertasvendidas".
    /// </summary>
    public class ShowCompanySoldOffersHandler : AbstractHandler, IHandler
    {
        private Company company;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ShowCompanySoldOffersHandler"/>
        /// </summary>
        public ShowCompanySoldOffersHandler()
        {
            this.Command = "/mostrarofertasvendidas";
            this.company = null;
        }

        /// <summary>
        /// Procesa el mensaje y muestra la lista de ofertas de ofertas vendidas de la empresa.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="response"></param>
        /// <returns> Retorna true si se logró realizar la operacion y false en caso de que no</returns>
        public override bool InternalHandle(IMessage input, out string response)
        {   
            try
            {
                if (CanHandle(input))
                {
                    this.company = CompanyRegister.Instance.GetCompanyByUserId(input.Id);
                    StringBuilder offers = new StringBuilder("Estas son tus ofertas actuales:\n");
                    if(this.company != null && this.company.SoldOffers.Count != 0)
                    {
                        foreach (Offer item in this.company.SoldOffers.Keys.ToArray())
                        {
                            offers.Append($"Id de la oferta: {item.Id}.\n")
                                    .Append($"Material de la oferta: {item.Material.Name} de {item.Material.Type}.\n")
                                    .Append($"Unidad de medida: {item.UnitOfMeasure}.\n")
                                    .Append($"Cantidad: {item.QuantityMaterial}.\n")
                                    .Append($"Divisa: {item.Currency}.\n")
                                    .Append($"Precio: {item.TotalPrice}.\n")
                                    .Append($"Fecha de publicación: {item.PublicationDate}.\n");
                            if(item.Availability)
                            {
                                offers.Append($"Disponibilidad: Activa.\n");
                            }
                            else
                            {
                                offers.Append($"Disponibilidad: Suspendida.\n");
                            }
                            offers.Append($"Comprador:\n")
                                    .Append(this.company.SoldOffers[item].Role.Data());
                            offers.Append($"\n-----------------------------------------------\n\n");
                        }
                        response = offers.ToString();
                        return true;
                    }
                    else if(this.company.OfferRegister.Count == 0)
                    {
                        offers.Append($"La empresa a la que usted pertenece no tiene ninguna oferta publicada.\n")
                            .Append($"Ingrese /menu si quiere volver a ver los comandos disponibles.");
                        response = offers.ToString();
                        return true;
                    }
                    else
                    {
                        offers.Append($"No se encontró ninguna empresa a la que usted pertenezca.\n")
                            .Append($"Ingrese /menu si quiere volver a ver los comandos disponibles.");
                        response = offers.ToString();
                        return true;
                    }
                }
                response = string.Empty;
                return false;
            }
            catch(Exception e)
            {
                response = e.Message;
                return true;
            }
        }
    }
}
