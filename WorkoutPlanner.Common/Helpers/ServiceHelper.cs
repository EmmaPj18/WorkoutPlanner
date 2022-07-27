using System.Reflection;

namespace WorkoutPlanner.Common.Helpers;

public static class ServiceHelper
{
    public static (bool Found, IList<Type> Types) GetClassesImplementingAnInterface
        (Assembly toScanAssembly, Type implementedInterface)
    {
        if (toScanAssembly == null)
            return (false, new List<Type>());

        if (implementedInterface == null || !implementedInterface.IsInterface)
            return (false, new List<Type>());

        IEnumerable<Type>? typesInAssembly;

        try
        {
            typesInAssembly = toScanAssembly.GetTypes();
        }
        catch (ReflectionTypeLoadException e)
        {
            typesInAssembly = e.Types.Where(x => x != null)!;
        }

        IList<Type> classesImplementingAssembly = new List<Type>();

        if (typesInAssembly != null)
        {
            if (implementedInterface.IsGenericType)
            {
                var implementedGenericInterface = implementedInterface.GetGenericTypeDefinition();

                foreach (var typeInAssembly in typesInAssembly)
                {
                    if (typeInAssembly.IsClass)
                    {
                        var typeInterfaces = typeInAssembly.GetInterfaces();

                        foreach (var typeInterface in typeInterfaces)
                        {
                            if (typeInterface.IsGenericType)
                            {
                                var typeGenericInterface = typeInterface.GetGenericTypeDefinition();

                                if (implementedGenericInterface == typeGenericInterface)
                                    classesImplementingAssembly.Add(typeInAssembly);
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (var typeInAssembly in typesInAssembly)
                {
                    if (typeInAssembly.IsClass)
                    {
                        if (implementedInterface.IsAssignableFrom(typeInAssembly))
                            classesImplementingAssembly.Add(typeInAssembly);
                    }
                }
            }
        }

        return (true, classesImplementingAssembly);

    }
}
