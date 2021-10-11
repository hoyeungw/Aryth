using System;
using System.Reflection;
using Ject;
using Veho.Sequence;

namespace Aryth {
  public static class GenericMethod {
    public static Type ComparableType(this Type t) => t.IsGenericType ? t.GetGenericTypeDefinition() : t;
    public static bool GenericEquals(this Type type, Type another) {
      return type.IsGenericParameter && another.IsGenericParameter
        ? type.GenericParameterPosition == another.GenericParameterPosition
        : type.ComparableType() == another.ComparableType();
    }
    public static bool GenericImplements(this Type type, Type interfaceType) {
      foreach (var currInterfaceType in type.GetInterfaces()) {
        if (currInterfaceType.IsGenericType) {
          if (currInterfaceType.GetGenericTypeDefinition() == interfaceType.ComparableType()) return true;
        }
      }
      return false;
    }
    public static bool ImplementEquals(this Type type, Type interfaceType) {
      return interfaceType.IsAssignableFrom(type) || (interfaceType.IsGenericType && type.GenericImplements(interfaceType));
    }
    public static bool TryParseGenericMethod(this Type algebra, string methodName, Type[] paramTypes, out MethodInfo method, BindingFlags flags = BindingFlags.Public | BindingFlags.Static) {
      foreach (var currMethod in algebra.GetMethods(flags)) {
        var currParamTypes = currMethod.ParamTypes();
        // currMethod.GetGenericArguments().Deco().Says(currMethod.ToString());
        if (
          currMethod.Name == methodName && currParamTypes.Length == paramTypes.Length &&
          Selectors.Every(paramTypes, (i, paramType) => {
            var currParamType = currParamTypes[i];
            // Console.WriteLine($">> [{i}] [{paramType}, {currParamType}] equality [generic] {paramType.GenericEquals(currParamType)} [interface] {paramType.ImplementEquals(currParamType)}");
            return (paramType == currParamType) ||
                   (currParamType.IsInterface && paramType.ImplementEquals(currParamType)) ||
                   (currParamType.IsGenericType && paramType.GenericEquals(currParamType));
          })
        ) {
          method = currMethod;
          return true;
        }
      }
      method = null;
      return false;
    }

    // var genericEquals = paramType.IsSimilarType(currParamType);
    // var implementEquals = currParamType.IsInterface && paramType.ImplementEquals(currParamType);

    public static MethodInfo GenericBinaryOperator<T>(string name, Type[] paramTypes, Type genericType, MethodInfo defaultMethod = null) {
      try {
        var m = typeof(T).GetMethod(name, paramTypes);
        return m != null ? m.MakeGenericMethod(genericType) : defaultMethod;
      }
      catch (Exception) {
        return defaultMethod;
      }
    }
  }

  // public static class reflectionSpectre {
  //   public static MethodInfo[] methods(this Type T) {
  //     return T.GetMethods().SkipWhile(m => m.MethodImplementationFlags == MethodImplAttributes.InternalCall)
  //             .ToArray();
  //   }
  //   public static void SpectreMethod(this Type T) {
  //     $"Spectre Method: [{T}]".wL();
  //     T.GetMethods()
  //      .Iterate((it, idx) => $"[{idx}] {it.toBrief()}".wL());
  //   }
  //   public static void SpectreProperty(this Type T) {
  //     $"Spectre Property: [{T}]".wL();
  //     T.GetProperties()
  //      .Sort((a, b) => a.Name.compareAlphabetAsc(b.Name))
  //      .Iterate(it => it.toBrief().wL());
  //   }
  //   public static void SpectreMethod<T>(T instance) {
  //     $"Spectre Property: [{typeof(T)}] ({instance})".wL();
  //     typeof(T).GetMethods()
  //              .Sort((a, b) => a.Name.compareAlphabetDsc(b.Name))
  //              .Iterate((prt, idx) => $"<{idx}> {prt.toBrief(instance)}".wL());
  //   }
  //   public static void SpectreProperty<T>(T instance) {
  //     $"Spectre Property: [{typeof(T)}] ({instance})".wL();
  //     typeof(T).GetProperties()
  //              .Sort((a, b) => a.Name.compareAlphabetDsc(b.Name))
  //              .Iterate((prt, idx) => $"<{idx}> {prt.toBrief(instance)}".wL());
  //   }
  // }

  // public static class ReflectionInternalService {
  //   public static string ToBrief(this PropertyInfo p) {
  //     var rwStatus = (p.CanRead ? "R" : " ") + (p.CanWrite ? "W" : " ");
  //     return $"[Property {rwStatus}] ({p.Name})";
  //   }
  //   public static string ToBrief<T>(this PropertyInfo p, T instance) {
  //     var rwStatus = (p.CanRead ? "R" : " ") + (p.CanWrite ? "W" : " ");
  //     var brief = $"[Property {rwStatus}] [{p.Name}]";
  //     try {
  //       if (p.CanRead) {
  //         brief += $" ({p.GetValue(instance, p.GetIndexParameters())})";
  //       }
  //     }
  //     catch (Exception e) {
  //       //Debug.Print(e);
  //       brief += $" (Err {e.HResult}: {e.Message})";
  //     }
  //     return brief;
  //   }
  //   public static string ToBrief(this MethodInfo m) =>
  //     $"[Method] [{m.Name}] ({m.GetParameters().Map(it => $"{it.ParameterType.Name} {it.Name}").Reduce((a, b) => a + ", " + b)}) => ({m.ReturnParameter?.ParameterType.Name})";
  //   public static string ToBrief<T>(this MethodInfo m, T instance) {
  //     var brief = $"[Method] [{m.Name}] ({m.GetParameters().Map(it => $"{it.ParameterType.Name} {it.Name}").Reduce((a, b) => a + ", " + b)}) => (({m.ReturnParameter?.ParameterType.Name}) ";
  //     try {
  //       var invokeValue = (m.GetParameters().Length == 0) ? m.Invoke(instance, new object[] { }).ToString() : "";
  //       brief += invokeValue + ")";
  //     }
  //     catch (Exception e) {
  //       //Debug.Print(e);
  //       brief += $"(Err {e.HResult}: {e.Message})" + ")";
  //     }
  //     return brief;
  //   }
  // }
}