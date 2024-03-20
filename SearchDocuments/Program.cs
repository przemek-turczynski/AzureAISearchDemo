using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Domain;
using Newtonsoft.Json;

namespace SearchDocuments;

internal class Program
{
    private static readonly string ServiceEndpoint = "";
    private static readonly string IndexName = "cosmosdb-hotels-index";
    private static readonly string ApiKey = "";

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.Unicode;

        SearchClient searchClient = new(new Uri(ServiceEndpoint), IndexName, new AzureKeyCredential(ApiKey));
       
        while(true)
        {
            Console.WriteLine("Enter query:\n");
            var query = Console.ReadLine();

            if (string.IsNullOrEmpty(query))
            {
                break;
            }
            RunQuery(searchClient, query);
        }
    }

    private static void RunQuery(SearchClient searchClient, string query)
    {
        SearchOptions options;
        SearchResults<Hotel> response;

        options = new SearchOptions()
        {
            IncludeTotalCount = true,
            QueryType = SearchQueryType.Full
        };
        response = searchClient.Search<Hotel>(query, options);

        WriteDocuments(query, response);
    }

    private static void WriteDocuments(string query, SearchResults<Hotel> searchResults)
    {
        Console.Clear();
        Console.WriteLine("\x1b[3J");

        Console.WriteLine($"Query: {query}");
        Console.WriteLine($"Total Count: {searchResults.TotalCount}");

        foreach (SearchResult<Hotel> result in searchResults.GetResults())
        {
            Console.WriteLine($"\nScore: {result.Score}");
            Console.WriteLine(JsonConvert.SerializeObject(result.Document, Formatting.Indented));
        }

        Console.WriteLine();
    }
}
