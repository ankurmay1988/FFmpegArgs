using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFmpegArgs.Cores.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class FilterGraph : IFilterGraph, IImageFilterGraph, IAudioFilterGraph
    {
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<IFilter> Filters { get { return _filters; } }

        readonly List<IFilter> _filters = new List<IFilter>();

        /// <summary>
        /// Mac dinh false. Khi true: <see cref="GetFiltersArgs"/> tu gan sink
        /// (nullsink/anullsink theo kieu map) vao moi map out chua dung, thay vi nem loi "not bind".
        /// Output that (da map ra stream) co IsMapped == true nen KHONG bao gio bi sink.
        /// Viec dam bao command co >=1 output la o muc command (FFmpegArg.GetOutputsArgs nem
        /// "Output is empty" neu khong co output nao).
        /// </summary>
        public bool AutoSinkUnusedMapOut { get; set; } = false;

        int AddFilter(IFilter filter)
        {
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));

            if (_filters.IndexOf(filter) >= 0)
                throw new InvalidOperationException("this filter was added");

            if (!this.Equals(filter.FilterGraph))
                throw new InvalidOperationException("this filter.FilterGraph not same with this FilterGraph");


            _filters.Add(filter);
            return _filters.IndexOf(filter);
        }

        int IFilterGraph.AddFilter(IFilter filter) => AddFilter(filter);
        int IImageFilterGraph.AddFilter<TIn, TOut>(BaseFilter<TIn, TOut> filter) => AddFilter(filter);
        int IAudioFilterGraph.AddFilter<TIn, TOut>(BaseFilter<TIn, TOut> filter) => AddFilter(filter);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="withNewLine">if true, Join filter with <see cref="Environment.NewLine"/></param>
        /// <param name="useChain">Make filter smaller by skipping map [map_name]</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Have map not bind</exception>
        public string GetFiltersArgs(bool withNewLine = false, bool useChain = true)
        {
            if (AutoSinkUnusedMapOut) AddSinkToUnusedMapOut();
            var filter_not_bind = Filters.FirstOrDefault(x => x.MapsOut.Any(y => !y.IsMapped));
            if (filter_not_bind != null)
                throw new InvalidOperationException($"Have Map in filter \"{filter_not_bind.FilterName}\" are not bind");
            string joinValue = withNewLine ? $";{Environment.NewLine}" : ";";
            if (useChain)
            {
                var chains = FilterChain.BuildChains(Filters, true);
                return string.Join(joinValue, chains.Select(x => x.GetChainValue(true, true)));
            }
            else
            {
                return string.Join(joinValue, Filters.Select(x => x.GetFilterValue()));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public string GetFiltersInputArgs()
        {
            var chains = FilterChain.BuildChains(Filters, false);
            if (chains.Count() != 1) throw new InvalidOperationException($"Filter input allow only one chain");
            return chains.First().GetChainValue(false, false);
        }

        /// <summary>
        /// Gan sink vao moi map out chua dung (!IsMapped). Idempotent: sau khi gan, map out do
        /// thanh "da dung" nen lan goi sau khong gan lai. Output that khong bi dung toi.
        /// </summary>
        void AddSinkToUnusedMapOut()
        {
            //snapshot truoc khi them (them sink se mutate _filters)
            var unusedMapOuts = _filters.SelectMany(x => x.MapsOut).Where(x => !x.IsMapped).ToList();
            foreach (var mapOut in unusedMapOuts)
            {
                string sinkName;
                if (mapOut is ImageMap) sinkName = "nullsink";
                else if (mapOut is AudioMap) sinkName = "anullsink";
                else throw new InvalidOperationException($"Cannot auto-sink map out of type {mapOut.GetType().Name}");
                _ = new AutoSinkFilter(sinkName, mapOut);//ctor tu dang ky vao FilterGraph
            }
        }

    }
}
