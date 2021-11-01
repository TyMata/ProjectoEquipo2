namespace ClassLibrary
{
    public class CompanyRole : IRole
    {
        Company Company { get; set; }

        /*IHandler CreateCoR(IMessageChannel channel)
        {
            return new AbstractHandler();
        } */
    }
}