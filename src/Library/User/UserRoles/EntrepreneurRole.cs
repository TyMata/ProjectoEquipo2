using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class EntrepreneurRole : IRole
	{
		Entrepreneur Entrepreneur { get; set; }

        public EntrepreneurRole(string name, Location location, string headings, string habilitation)
        {
            this.Entrepreneur = new Entrepreneur(name, location, headings, habilitation);
        }

        /*IHandler CreateCoR(IMessageChannel channel)
        {

        }*/
	}
}