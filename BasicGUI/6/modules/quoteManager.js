// Fetching the quotes from external api using fetch
export async function fetchQuote() {
    const url = 'https://shayshay.p.rapidapi.com/random?limit=3';
    const options = {
        method: 'GET',
        headers: {
            //key can be stored in .env file for security purpose
            'x-rapidapi-key': 'efb604ff0amshbf83b079bb9a9a0p16386cjsn631526761484',
            'x-rapidapi-host': 'shayshay.p.rapidapi.com'
        }
    };

    try {
        const response = await fetch(url, options);
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        
        const result = await response.json();
        const quotes = result.quotes;
        const randomIndex = Math.floor(Math.random() * quotes.length);
        const quoteToDisplay = quotes[randomIndex];

        const quoteElement = document.getElementById("quote");
        quoteElement.textContent = quoteToDisplay;
        
    } catch (error) {
        console.error("Error fetching the quote:", error);
        document.getElementById("quote").textContent = "Failed to fetch a motivational quote. Please try again later.";
    }
}
