using Dashboard.Core.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Core
{
    /// <summary>
    /// Merge collections of pipelines in proper order and joins static and dynamic pipelines colections
    /// </summary>
    public class PipelinesMerger
    {
        List<IEnumerable<Pipeline>> newestPipesInPages;

        public int NewestPipelinesCount { get { return newestPipesInPages.Count; } }

        public PipelinesMerger()
        {
            newestPipesInPages = new List<IEnumerable<Pipeline>>();
        }

        public void AddPipelinesPageAtEnd(IEnumerable<Pipeline> pipelines)
        {
            newestPipesInPages.Add(pipelines);
        }

        /// <summary>
        /// Merges remembered in merger object newest pipelines with static and existing ones. Returns collection to Save and to Delete
        /// </summary>
        /// <param name="existingCollection">Actual collection from DB</param>
        /// <param name="staticPipelines">Updated static pipelines</param>
        /// <param name="howManyToReturn">How many pipelines needs to be stored</param>
        /// <returns></returns>
        public IEnumerable<Pipeline> MergePipelines(IEnumerable<Pipeline> existingCollection, IEnumerable<Pipeline> staticPipelines, int howManyToReturn)
        {
            List<Pipeline> newestPipelines = new List<Pipeline>();
            foreach (var pipelines in newestPipesInPages)
            {
                newestPipelines.AddRange(pipelines);
            }

            List<Pipeline> result = new List<Pipeline>(staticPipelines);
            result.AddRange(newestPipelines);
            result.AddRange(existingCollection);

            return result.Take(howManyToReturn);
        }
    }
}
