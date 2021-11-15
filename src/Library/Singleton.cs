/// <summary>
/// Se crea la clase singleton para usar
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> where T : new()
{
    private static T instance;

    /// <summary>
    /// metodo
    /// </summary>
    /// <value></value>
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new();
            }
            return instance;
        }
    }
}