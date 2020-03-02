using CodeSnippetsAndUtils.Uno.Factories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CodeSnippetsAndUtils.Uno.Mapping
{
    public static class MapperExts
    {
        /// <summary>
        /// Maps a given source object into a new destination object of type <typeparamref name="TDestination"/>.
        /// </summary>
        /// <param name="mapper">The mapper used to map source objects into destination objects.</param>
        /// <param name="source">The source object.</param>
        /// <typeparam name="TSource">The type of object to map from.</typeparam>
        /// <typeparam name="TDestination">The type of object to map into.</typeparam>
        /// <returns>A new mapped object of type <typeparamref name="TDestination"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="mapper"/> or <paramref name="source"/> is <see langword="null"/>.</exception>
        public static TDestination Map<TSource, TDestination>(
            this IMapper<TSource, TDestination> mapper, TSource source) 
            where TDestination : new()
        {
            if (mapper is null) throw new ArgumentNullException(nameof(mapper));
            if (source is null) throw new ArgumentNullException(nameof(source));

            TDestination dest = ParamlessCtorFactory<TDestination>.Create();
            mapper.Map(source, dest);
            return dest;
        }

        /// <summary>
        /// Maps the specified collection of source objects into a collection of destination objects.
        /// </summary>
        /// <typeparam name="TSource">The type of object to map from.</typeparam>
        /// <typeparam name="TDestination">The type of object to map into.</typeparam>
        /// <typeparam name="TDestinationCollection">The type of collection to use and return.</typeparam>
        /// <param name="mapper">The mapper used to map source objects into destination objects.</param>
        /// <param name="source">A collection of source objects.</param>
        /// <param name="destination">A collection of type <typeparamref name="TDestination"/> that is at least the size of <paramref name="source"/>.</param>
        /// <param name="destinationCtor">The constructor to use for creating new <typeparamref name="TDestination"/> objects.</param>
        /// <returns>A collection of mapped <typeparamref name="TDestination"/> objects.</returns>
        /// <exception cref="ArgumentException">Throws if the length of <paramref name="destination"/> is less than the length of <paramref name="source"/>.</exception>
        /// <exception cref="ArgumentNullException">Throws if any of <paramref name="mapper"/>, <paramref name="source"/>, <paramref name="destination"/>, or <paramref name="destinationCtor"/> are <see langword="null"/>.</exception>
        public static TDestinationCollection Map<TSource, TDestination, TDestinationCollection>(this IMapper<TSource, TDestination> mapper, IEnumerable<TSource> source, TDestinationCollection destination, Func<TDestination> destinationCtor)
            where TDestinationCollection : IList<TDestination>
        {
            if (mapper is null) throw new ArgumentNullException(nameof(mapper));
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (destination is null) throw new ArgumentNullException(nameof(destination));
            if (destinationCtor is null) throw new ArgumentNullException(nameof(destinationCtor));
            if (destination.Count < source.Count()) throw new ArgumentException("Destination count cannot be less than source collection count.", nameof(destination));

            return InternalMap(mapper, source, destination, destinationCtor);
        }
        /// <summary>
        /// Maps the specified collection of source objects into a collection of destination objects.
        /// </summary>
        /// <typeparam name="TSource">The type of object to map from.</typeparam>
        /// <typeparam name="TDestination">The type of object to map into.</typeparam>
        /// <typeparam name="TDestinationCollection">The type of collection to use and return.</typeparam>
        /// <param name="mapper">The mapper used to map source objects into destination objects.</param>
        /// <param name="source">A collection of source objects.</param>
        /// <param name="destination">A collection of type <typeparamref name="TDestination"/> that is at least the size of <paramref name="source"/>.</param>
        /// <returns>A collection of mapped <typeparamref name="TDestination"/> objects.</returns>
        /// <exception cref="ArgumentException">Throws if the length of <paramref name="destination"/> is less than the length of <paramref name="source"/>.</exception>
        /// <exception cref="ArgumentNullException">Throws if any of <paramref name="mapper"/>, <paramref name="source"/>, or <paramref name="destination"/> are <see langword="null"/>.</exception>
        public static TDestinationCollection Map<TSource, TDestination, TDestinationCollection>(this IMapper<TSource, TDestination> mapper, IEnumerable<TSource> source, TDestinationCollection destination)
            where TDestination : new() 
            where TDestinationCollection : IList<TDestination>
        {
            return mapper.Map(source, destination, ParamlessCtorFactory<TDestination>.ParamlessCtor);
        }
        
        /// <summary>
        /// Maps the specified collection of source objects into an array of destination objects.
        /// </summary>
        /// <typeparam name="TSource">The type of object to map from.</typeparam>
        /// <typeparam name="TDestination">The type of object to map into.</typeparam>
        /// <param name="mapper">The mapper used to map source objects into destination objects.</param>
        /// <param name="source">A collection of source objects.</param>
        /// <param name="destination">An array of type <typeparamref name="TDestination"/> that is at least the size of <paramref name="source"/>.</param>
        /// <param name="destinationCtor">The constructor to use for creating new <typeparamref name="TDestination"/> objects.</param>
        /// <returns>An array of mapped <typeparamref name="TDestination"/> objects.</returns>
        /// <exception cref="ArgumentException">Throws if the length of <paramref name="destination"/> is less than the length of <paramref name="source"/>.</exception>
        /// <exception cref="ArgumentNullException">Throws if any of <paramref name="mapper"/>, <paramref name="source"/>, <paramref name="destination"/>, or <paramref name="destinationCtor"/> are <see langword="null"/>.</exception>
        public static TDestination[] Map<TSource, TDestination>(this IMapper<TSource, TDestination> mapper, IEnumerable<TSource> source, TDestination[] destination, Func<TDestination> destinationCtor)
        {
            if (mapper is null) throw new ArgumentNullException(nameof(mapper));
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (destination is null) throw new ArgumentNullException(nameof(destination));
            if (destinationCtor is null) throw new ArgumentNullException(nameof(destinationCtor));
            if (destination.Length < source.Count()) throw new ArgumentException("Destination length cannot be less than source collection count.", nameof(destination));

            return InternalMap(mapper, source, destination, destinationCtor);
        }
        /// <summary>
        /// Maps the specified collection of source objects into an array of destination objects.
        /// </summary>
        /// <typeparam name="TSource">The type of object to map from.</typeparam>
        /// <typeparam name="TDestination">The type of object to map into.</typeparam>
        /// <param name="mapper">The mapper used to map source objects into destination objects.</param>
        /// <param name="source">A collection of source objects.</param>
        /// <param name="destination">An array of type <typeparamref name="TDestination"/> that is at least the size of <paramref name="source"/>.</param>
        /// <returns>An array of mapped <typeparamref name="TDestination"/> objects.</returns>
        /// <exception cref="ArgumentException">Throws if the length of <paramref name="destination"/> is less than the length of <paramref name="source"/>.</exception>
        /// <exception cref="ArgumentNullException">Throws if any of <paramref name="mapper"/>, <paramref name="source"/>, or <paramref name="destination"/> are <see langword="null"/>.</exception>
        public static TDestination[] Map<TSource, TDestination>(this IMapper<TSource, TDestination> mapper, IEnumerable<TSource> source, TDestination[] destination)
            where TDestination : new()
        {
            return mapper.Map(source, destination, ParamlessCtorFactory<TDestination>.ParamlessCtor);
        }

        /// <summary>
        /// Maps the specified collection of source objects into a new array of destination objects.
        /// </summary>
        /// <typeparam name="TSource">The type of object to map from.</typeparam>
        /// <typeparam name="TDestination">The type of object to map into.</typeparam>
        /// <param name="mapper">The mapper used to map source objects into destination objects.</param>
        /// <param name="source">A collection of source objects.</param>
        /// <param name="destinationCtor">The constructor to use for creating new <typeparamref name="TDestination"/> objects.</param>
        /// <returns>An array of mapped <typeparamref name="TDestination"/> objects.</returns>
        /// <exception cref="ArgumentNullException">Throws if any of <paramref name="mapper"/>, <paramref name="source"/>, or <paramref name="destinationCtor"/> are <see langword="null"/>.</exception>
        public static TDestination[] Map<TSource, TDestination>(this IMapper<TSource, TDestination> mapper, IEnumerable<TSource> source, Func<TDestination> destinationCtor)
        {
            if (mapper is null) throw new ArgumentNullException(nameof(mapper));
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (destinationCtor is null) throw new ArgumentNullException(nameof(destinationCtor));

            return InternalMap(mapper, source, new TDestination[source.Count()], destinationCtor);
        }
        /// <summary>
        /// Maps the specified collection of source objects into a new array of destination objects.
        /// </summary>
        /// <typeparam name="TSource">The type of object to map from.</typeparam>
        /// <typeparam name="TDestination">The type of object to map into.</typeparam>
        /// <param name="mapper">The mapper used to map source objects into destination objects.</param>
        /// <param name="source">A collection of source objects.</param>
        /// <returns>An array of mapped <typeparamref name="TDestination"/> objects.</returns>
        /// <exception cref="ArgumentNullException">Throws if <paramref name="mapper"/> or <paramref name="source"/> are <see langword="null"/>.</exception>
        public static TDestination[] Map<TSource, TDestination>(this IMapper<TSource, TDestination> mapper, IEnumerable<TSource> source)
            where TDestination : new()
        {
            return mapper.Map(source, ParamlessCtorFactory<TDestination>.ParamlessCtor);
        }

        private static TDestination[] InternalMap<TSource, TDestination>(IMapper<TSource, TDestination> mapper,
            IEnumerable<TSource> source,
            TDestination[] destination,
            Func<TDestination> destinationCtor)
        {
            Debug.Assert(mapper != null);
            Debug.Assert(source != null);
            Debug.Assert(destination != null);
            Debug.Assert(destinationCtor != null);
            Debug.Assert(source.Count() <= destination.Length);

            int index = 0;
            foreach (var src in source)
            {
                var dst = destinationCtor();
                mapper.Map(src, dst);
                destination[index++] = dst;
            }

            return destination;
        }
        private static TDestinationCollection InternalMap<TSource, TDestination, TDestinationCollection>(
            IMapper<TSource, TDestination> mapper,
            IEnumerable<TSource> source,
            TDestinationCollection destination,
            Func<TDestination> destinationCtor)
            where TDestinationCollection : IList<TDestination>
        {
            Debug.Assert(mapper != null);
            Debug.Assert(source != null);
            Debug.Assert(destination != null);
            Debug.Assert(destinationCtor != null);

            int index = 0;
            foreach (var src in source)
            {
                var dst = destinationCtor();
                mapper.Map(src, dst);
                if (destination.Count <= index) destination.Add(dst);
                else destination[index] = dst;
                index++;
            }

            return destination;
        }
    }
}
