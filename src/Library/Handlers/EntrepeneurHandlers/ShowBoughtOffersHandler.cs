using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class ShowBoughtOffersHandler : AbstractHandler, IHandler
    {
        private Users user;
        public ShowBoughtOffersHandler()
        {
            this.Command = "/mostrarofertascompradas";
            this.user = null;
        }

        public override bool InternalHandle(IMessage input, out string response)
        {
            try
            {
                if (CanHandle(input))
                {
                    this.user = UserRegister.Instance.GetUserById(input.Id);
                    if(this.user != null && (this.user.Role as EntrepreneurRole).Entrepreneur.BoughtList.Count != 0)
                    {
                        StringBuilder offers = new StringBuilder("Estas son tus ofertas compradas:\n");
                        foreach (Offer item in (this.user.Role as EntrepreneurRole).Entrepreneur.BoughtList)
                        {
                            offers.Append($"Id de la oferta: {item.Id}.\n")
                                    .Append($"Material de la oferta: {item.Material.Name} de {item.Material.Type}.\n")
                                    .Append($"Cantidad: {item.QuantityMaterial}.\n")
                                    .Append($"Precio: {item.TotalPrice}.\n")
                                    .Append($"Fecha de publicación: {item.PublicationDate}.\n")
                                    .Append($"Datos de la empresa:\n")
                                    .Append($"Nombre{item.Company.Name}")
                                    .Append($"E-mail{item.Company.Email}")
                                    .Append($"Número de teléfono: {item.Company.PhoneNumber}")
                                    .Append($"\n-----------------------------------------------\n\n");
                        }
                        response = offers.ToString();
                        return true;
                    }
                    else if((this.user.Role as EntrepreneurRole).Entrepreneur.BoughtList.Count == 0)
                    {
                        response = "Usted no tiene ninguna oferta comprada.";
                        return true;
                    }
                    else
                    {
                        response = "Usted no esta registrado. Registrese para hacer compras.";
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