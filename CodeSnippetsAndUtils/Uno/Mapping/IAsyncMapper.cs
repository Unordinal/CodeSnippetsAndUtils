using System.Threading.Tasks;

namespace CodeSnippetsAndUtils.Uno.Mapping
{
    /// <summary>
    /// Provides an asynchronous way to map <typeparamref name="TSource"/> objects to <typeparamref name="TDestination"/> objects.
    /// </summary>
    /// <typeparam name="TSource">The type of object to map from.</typeparam>
    /// <typeparam name="TDestination">The type of object to map to.</typeparam>
    public interface IAsyncMapper<in TSource, in TDestination>
    {
        /// <summary>
        /// Maps a given source object into a specified destination object.
        /// </summary>
        /// <param name="source">The type of object to map from.</param>
        /// <param name="destination">The type of object to map into.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Map(TSource source, TDestination destination);
    }
}
