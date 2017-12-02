using System;

public abstract class Singleton<T>
{
    public static T Instance;

    protected void DefineSingleton(T parent)
    {
        if (Instance != null)
            throw new Exception(string.Format("{0} has already been instantiated. Please make sure you only have one in the scene!", parent.ToString()));

        Instance = parent;
    }
}
