namespace ClassLibrary
{
    public class CompanyRole : IRole
    {
        Company Company { get; set; }

        public CompanyRole(Company company)
        {
            this.Company = Company;
        }

        /*IHandler CreateCoR(IMessageChannel channel)
        {
            return new AbstractHandler();
        } */
        public string TipoRol()
        {
            return "company";
        }
    }
}