namespace RuntimeDemystification.Compiled;

public delegate object virtualFunction(params object[] args);

internal class Logic(TypeMetadata metadata) : BaseLogic(metadata), IInstance
{
    public static string Work(Logic @this) => $"I am {nameof(Logic)}";

    internal static Type GetType(Logic @this) => @this._metaData._type;

    public static new TypeMetadata LoadType() => new()
    {
        _type = typeof(Logic),

        // Here override of vTable entry happens.
        _vtable = BaseLogic.LoadType()._vtable.Register<Logic, string>(nameof(Work), Work)
    };
}

internal class BaseLogic(TypeMetadata metaData) : ImplicitLayout(metaData), IInstance
{
    public static string Work(BaseLogic @this) => $"I am {nameof(BaseLogic)}";
    public static string WorkReason(BaseLogic @this) => "I do work";

    internal static Type GetType(BaseLogic @this) => @this._metaData._type; 

    public static TypeMetadata LoadType() => new()
    {
        _type = typeof(BaseLogic),

        // Initial vTable initialization
        _vtable = new Dictionary<string, virtualFunction>()
            .Register<BaseLogic, string>(nameof(Work), Work)
            .Register<BaseLogic, string>(nameof(WorkReason), WorkReason)
    };
}

internal class ImplicitLayout(TypeMetadata metaData)
{
    internal required TypeMetadata _metaData = metaData;
}

internal class TypeMetadata
{
    internal required Dictionary<string, virtualFunction> _vtable;
    internal required Type _type;
}

internal interface IInstance
{
    public static abstract TypeMetadata LoadType();
}
