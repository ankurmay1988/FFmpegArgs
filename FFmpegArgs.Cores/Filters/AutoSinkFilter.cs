namespace FFmpegArgs.Cores.Filters
{
    /// <summary>
    /// Sink noi bo dung cho auto-sink: gan vao map out chua dung de "nuot" nhanh thua.
    /// Ten filter ("nullsink"/"anullsink") do <see cref="FilterGraph"/> quyet dinh theo kieu map.
    /// </summary>
    internal class AutoSinkFilter : BaseFilter
    {
        internal AutoSinkFilter(string filterName, BaseMap mapIn) : base(filterName, mapIn)
        {
        }

        protected override bool AllowNoMapOut => true;

        protected override void AddMapOut()
          => throw new NotSupportedException($"{FilterName} is a sink (no map out)");

        protected override void AddMapOut(int index)
          => throw new NotSupportedException($"{FilterName} is a sink (no map out)");
    }
}
