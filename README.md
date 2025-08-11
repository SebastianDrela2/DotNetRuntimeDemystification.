### .NET RunTime minimal overview.

NET runtime when summoning a type _for us `new`_ keyword loads type from assembly or gets already loaded type.

Each type basically has metaData provided for it's construction it consists of the type declaration it self and the virtual table
you can think of it basically as each type inheriting such `ImplicitLayout` class.

```cs
internal class ImplicitLayout(TypeMetadata metaData)
{
    internal required TypeMetadata _metaData = metaData;
}

internal class TypeMetadata
{
    internal required Dictionary<string, virtualFunction> _vtable;
    internal required Type _type;
}
```

_Virtual table is really nothing more than a simple string, delegate(function ptr) dictionairy which is first created in the lowest possible base type and later has overriden entries_

Instance methods are a lie, every instance method under the hood is static and takes the type it self always as a first parameter, which is why every 
System.Reflection.`MethodInfo`.Invoke(params object[]) takes in the type as a first parameter.

When method is called, method ptr (delegate) is resolved from the virtual table based on the provided type and the method name it self.
then such method is invoked with arguements dynamically with first arguement being an instance.
