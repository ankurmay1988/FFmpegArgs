namespace FFmpegArgs.Filters.BaseFilters
{
    /// <summary>
    /// Sink filter cho video (V-&gt;|): co map in la <see cref="ImageMap"/> nhung khong co map out.
    /// </summary>
    public abstract class ImageToSinkFilter : BaseFilter<ImageMap, BaseMap>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="filterName"></param>
        /// <param name="mapsIn"></param>
        protected ImageToSinkFilter(string filterName, params ImageMap[] mapsIn) : base(filterName, mapsIn)
        {
        }

        /// <summary>
        /// Sink khong co map out.
        /// </summary>
        protected override bool AllowNoMapOut => true;

        /// <summary>
        /// Sink khong ho tro map out.
        /// </summary>
        protected override void AddMapOut()
          => throw new NotSupportedException($"{FilterName} is a sink filter (no map out)");

        /// <summary>
        /// Sink khong ho tro map out.
        /// </summary>
        protected override void AddMapOut(int index)
          => throw new NotSupportedException($"{FilterName} is a sink filter (no map out)");
    }
}
