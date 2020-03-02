namespace CodeSnippetsAndUtils.Uno.Mapping
{
    /// <summary>
    /// Provides a way to map <typeparamref name="TSource"/> objects to <typeparamref name="TDestination"/> objects.
    /// </summary>
    /// <typeparam name="TSource">The type of object to map from.</typeparam>
    /// <typeparam name="TDestination">The type of object to map to.</typeparam>
    public interface IMapper<in TSource, TDestination>
    {
        /// <summary>
        /// Maps a given source object into a specified destination object.
        /// </summary>
        /// <param name="source">The type of object to map from.</param>
        /// <param name="destination">The type of object to map into.</param>
        void Map(TSource source, TDestination destination);
    }
}
