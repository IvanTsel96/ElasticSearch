using ElasticSearch.API.Domain;
using ElasticSearch.API.Domain.Constants;
using Nest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElasticSearch.API.DAL.ElasticSearch
{
    public class ElasticSearchProvider : IElasticSearchProvider
    {
        private readonly IElasticClient _elasticClient;

        public ElasticSearchProvider(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task IndexDocument<T>(T document) where T : class
        {
            var isIndexExist = await IsIndexExists(Indexes.Entities);

            if (!isIndexExist)
            {
                await CreateIndex(Indexes.Entities);
            }

            await _elasticClient.IndexAsync(document, i => i.Index(Indexes.Entities));
        }

        public async Task IndexDocuments<T>(IList<T> documents) where T : class
        {
            var isIndexExist = await IsIndexExists(Indexes.Entities);

            if (!isIndexExist)
            {
                await CreateIndex(Indexes.Entities);
            }

            await _elasticClient.IndexManyAsync(documents, Indexes.Entities);
        }

        public async Task<List<Entity>> Search(string searchPhrase)
        {
            var response = await _elasticClient.SearchAsync<Entity>(s => s
                .Index(Indexes.Entities)
                .Query(q => q
                    .Bool(s => s
                        .Should(qs =>
                            qs.MatchPhrase(m => m
                                .Query(searchPhrase)
                                .Field(p => p.Name.Suffix("original"))
                                .Boost(2)),
                            qs => qs.MultiMatch(m => m
                                .Query(searchPhrase)
                                .Type(TextQueryType.MostFields)
                                .Fuzziness(Fuzziness.Auto)
                                .Fields(f => f
                                    .Field(p => p.Name)
                                    .Field(p => p.Name.Suffix("original"))
                                    .Field(p => p.Description))))

                    )));

            return (List<Entity>)response.Documents;
        }

        private async Task<bool> IsIndexExists(string indexName)
        {
            var response = await _elasticClient.Indices.ExistsAsync(indexName);

            return response.Exists;
        }

        private async Task CreateIndex(string indexName)
        {
            var response = await _elasticClient.Indices.CreateAsync(indexName, index => index
                .Settings(settings =>
                        settings.Analysis(analysis =>
                            analysis.TokenFilters(filter => filter
                                    .Stop("stopWordRu", x => x.StopWords(new StopWords("_russian_")))
                                    .Hunspell("hunspellRu", x => x.Locale("ru_RU"))
                                    // Relative path from CONF_DIR
                                    // https://www.elastic.co/guide/en/elasticsearch/reference/2.4/breaking_20_setting_changes.html#_custom_analysis_file_paths
                                    .Synonym("synonymRu", x => x.SynonymsPath(@"./synonyms/ru_RU/synonyms.txt")))
                                //.SynonymGraph("synonymRu_graph", x => x.SynonymsPath(@"./synonyms/ru_RU/synonymsMultiword.txt")))
                                .Analyzers(analyzer => analyzer
                                    .Custom("stopword", x => x
                                        .Tokenizer("standard")
                                        .Filters("lowercase", "stopWordRu"))
                                    .Custom("hunspell", x => x
                                        .Tokenizer("standard")
                                        .Filters("lowercase", "hunspellRu", "stopWordRu"))
                                    .Custom("synonym", x => x
                                        .Tokenizer("standard")
                                        .Filters("lowercase", "synonymRu", "hunspellRu", "stopWordRu")))
                        )
                )
                .Map<Entity>(map => map.Properties(descriptor => descriptor
                    .Text(textDescriptor => textDescriptor
                        .Analyzer("hunspell")
                        .SearchAnalyzer("synonym")
                        .Name(entity => entity.Name)
                        .Fields(
                            f => f.Text(textDescriptor => textDescriptor
                                    .Analyzer("stopword")
                                    .Name("original"))))
                    .Text(textDescriptor => textDescriptor
                        .Analyzer("hunspell")
                        .SearchAnalyzer("synonym")
                        .Name(entity => entity.Description))
                    )
                )
            );

            if (!response.IsValid)
            {
                throw new Exception($"Failed to create index with message: {response.OriginalException}");
            }
        }
    }
}
