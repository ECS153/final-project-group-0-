# Design Documents

## Overall Goal
Our goal is to forward browser requests that require sensitive data to a raspberry pi. The pi will contain credentials that can be used to populate the request's required fields. Then, the pi will make the request to the destination server and send the response back to the browser

# Overall Implementation
1. User enables the extension. The extension scans for all fields on a page and prompts the user to select the fields
they want to input securely. Next, the extension inputs a randomly generated token to the selected fields' value
2. The exension informs our api of the randomly generated tokens that it should be searching for
3. The api receives the tokens, but before it tells the proxy to start scanning for them, it sends a request to the
corresponding user's raspberry pi to confirm as well as the credentials (only partially revealed)that can be
    used on this site as well as the form token.
    - The raspberry pi prompts the user to confirm that the request was valid, and responds back to our api.
    - The pi then makes a new request to our api to receieve a list of credentials that can be used on this site. These credentials are only partially visible, just enough for the user to recognize them
    - The user fills out the credentials they wish to use on the site, and the pi makes another request to the api with the credentials
    - When the api receieves this request, it matches the partially hidden credentials to the full credentials, and adds them to a table in the database that our proxy is polling 
    - When the proxy receives a request, it will check amongst the table to see if it contains the token
4. Now, when the user sends the form, it will be filled out by our proxy
 
