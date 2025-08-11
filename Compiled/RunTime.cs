namespace RuntimeDemystification.Compiled;

internal static class RunTime
{
    internal static Dictionary<string, TypeMetadata> _loadedTypes = [];

    public static T New<T>(params object[] args) 
        where T : ImplicitLayout, IInstance
    {
        TypeMetadata metadata = LoadOrGetType<T>();

        object[] @params = [metadata, .. args];

        return (T)Activator.CreateInstance(typeof(T), @params)!;
    }

    public static T Call<T>(ImplicitLayout instance, string methodName, params object[] args)
    {
        virtualFunction function = instance._metaData._vtable[methodName];
        object[] parmameters = [instance, .. args];

        return (T)function.Invoke(parmameters);
    }

    internal static Dictionary<string, virtualFunction> Register<TType, T>(
        this Dictionary<string, virtualFunction> vtable, string functionName, Func<TType, T> func)
    {
        vtable[functionName] = func.DynamicInvoke!;

        return vtable;
    }

    private static TypeMetadata LoadOrGetType<T>() 
        where T : IInstance
    {
        if (!_loadedTypes.TryGetValue(typeof(T).FullName!, out var metadata))
        {
            metadata = T.LoadType();
            _loadedTypes.Add(typeof(T).FullName!, metadata);
        }

        return metadata;
    }
}
