
[System.Serializable]
public class InvalidInputException : System.Exception
{
    public InvalidInputException()
    {

    }
    public InvalidInputException(string message) : base(message) 
    {

    }
    public InvalidInputException(string message, System.Exception inner) : base(message, inner) 
    {
        
    }
}