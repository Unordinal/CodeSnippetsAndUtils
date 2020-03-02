using System;
using System.Linq.Expressions;

namespace CodeSnippetsAndUtils.Uno.Factories
{
    /// <summary>
    /// Provides a way to create instances of a generic type using their parameterless constructor without the performance hit of the new() constraint.
    /// </summary>
    /// <typeparam name="T">The type of object.</typeparam>
    public static class ParamlessCtorFactory<T> where T : new()
    {
        /// <summary>
        /// Gets the parameterless constructor of type <typeparamref name="T"/>.
        /// </summary>
        public static Func<T> ParamlessCtor { get; } = Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();

        /// <summary>
        /// Creates an instance of <typeparamref name="T"/> by calling its parameterless constructor.
        /// </summary>
        /// <returns>An object of type <typeparamref name="T"/>, created via parameterless constructor.</returns>
        public static T Create() => ParamlessCtor();
    }
}
