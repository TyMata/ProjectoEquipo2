using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    class ShowCompanyOffersHandler : AbstractHandler, IHandler
    {
        private Company company;
        public ShowCompanyOffersHandler()
        {
            this.Command = "/mostrarofertas";
            this.company = null;
        }

        public override bool InternalHandle(IMessage input, out string response)
        {

            if (CanHandle(input))
            {
                this.company = CompanyRegister.Instance.GetCompanyByUserId(input.Id);
                StringBuilder offers = new StringBuilder("Estas son tus ofertas actuales:\n");
                if(this.company != null && this.company.OfferRegister.Count != 0)
                {
                    foreach (Offer item in this.company.OfferRegister)
                    {
                        offers.Append($"Id de la oferta: {item.Id}\n")
                                .Append($"Material de la oferta: {item.Material.Name} de {item.Material.Type}\n")
                                .Append($"Cantidad: {item.QuantityMaterial}\n")
                                .Append($"Fecha de publicacion: {item.PublicationDate}\n");
                        if(item.Availability)
                        {
                            offers.Append($"Disponibilidad: Activa\n");
                        }
                        else
                        {
                            offers.Append($"Disponibilidad: Suspendida\n");
                        }
                        offers.Append($"\n-----------------------------------------------\n\n");
                    }
                    response = offers.ToString();
                    return true;
                }
                else if(this.company.OfferRegister.Count == 0)
                {
                    offers.Append($"La empresa a la que usted pertenece no tiene ninguna oferta publicada")
                        .Append($"Ingrese /menu si quiere volver a ver los comandos disponibles\n");
                    response = offers.ToString();
                    return true;
                }
                else
                {
                    offers.Append($"No se encontro ninguna empresa a la que usted pertenezca.\n")
                        .Append($"Ingrese /menu si quiere volver a ver los comandos disponibles\n");
                    response = offers.ToString();
                    return true;
                }
            }
            else
            {
                response = string.Empty;
                return false;
            }
            
        }
    }
}