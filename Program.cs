using HtmlAgilityPack;

namespace program
{
    class CardmarketWebScraper
    {
        static void Main(String[] args)
        {
            CardmarketWebScraper cardmarketWebScraper = new CardmarketWebScraper();
            cardmarketWebScraper.initializeScrapingProcess();
        }

        /// <summary>
        /// Contains all the scraping logic, initializes the scraping process
        /// </summary>
        public void initializeScrapingProcess()
        {
            //initialize an HtmlAgilityPack object
            HtmlWeb web = new HtmlWeb();

            try
            {
                //loading the target web page
                HtmlDocument document = web.Load("https://www.cardmarket.com/it/Pokemon/Data/Best-Bargains");

                //initialize PokemonCard list
                List<PokemonCard> pokemonCards = new List<PokemonCard>();

                //selecting all the html product elements from the current page
                var HTMLElementsForCardName = document.DocumentNode.QuerySelectorAll("div.width-productName");
                var HTMLElementsForCardPrice = document.DocumentNode.QuerySelectorAll("div.algn-r");

                //probaly because we are making too many requests to the site
                Console.WriteLine(HTMLElementsForCardName.Count);

                //iterating over the list of producsHTMLElements
                for (int i = 0; i < HTMLElementsForCardName.Count; i++)
                {
                    //scraping the interesting data from the current htmlElement
                    string name = HtmlEntity.DeEntitize(HTMLElementsForCardName[i].QuerySelector("div").InnerText);

                    string price = HtmlEntity.DeEntitize(HTMLElementsForCardPrice[i].QuerySelector("div").InnerText);

                    //instancing a new pokemonCard object
                    PokemonCard pokemonCard = new PokemonCard() { Name = name , Price = price };

                    //adding the object to the list
                    pokemonCards.Add(pokemonCard);
                }

                foreach (PokemonCard pokemonCard in pokemonCards)
                {
                    Console.WriteLine($"Card name: {pokemonCard.Name}, price: {pokemonCard.Price}");
                }
            }
            catch (HtmlWebException webEx)
            {
                Console.WriteLine($"An error occurred while accessing the web page: {webEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled exception: {ex.Message}");
            }
        }
    }

    public class PokemonCard
    {
        public string? Name { get; set; }
        public string? Price { get; set; }
    }
}