# Design Documents

# Overall Implementation
1. The user can install the extension which adds context menus allowing the generation of random tokens.
2. The exension informs our api of the randomly generated tokens that it should be searching for
3. The api receives the tokens, but before the proxy can start scanning for them, it sends a request to the
corresponding user's raspberry pi to confirm as well as the allow the user to assign credentials that can be
    used on this site to the token.
    - The raspberry pi prompts the user to confirm that the request was valid, and responds back to our api.
    - The pi then makes a new request to our api to receieve a list of credentials that can be used on this site. These credentials are only able to be differentiated by the hints assigned by the user during creation.
    - The user fills out the credentials they wish to use on the site, and the pi makes another request to the api with the information
    - When the api receieves this request, it matches the tokens to the credentials, and adds them to a table in the database that our proxy is polling 
    - When the proxy receives a request, it will check amongst the table to see if it contains the token
4. Now, when the user sends the form, it will be filled out by our proxy

## Implementation
Because of the current climate, our team will mostly work remotely. Because of this, we felt that the best way to develop a group project would be to use [microservices](https://youtu.be/y8OnoxKotPQ).

## Microservices

### Secret API [Design](https://docs.google.com/document/d/1CBh3EtYRP9pQcqUtRFken9FF3jxMfvshMlcplv2MuNk/edit?usp=sharing)
Additionally, we have created an api that connects all of our microservices together. This api supports user verification and should be able to handle multiple user accounts. On top of managing CRUD operations for users and their credentials, it also communicates with both our browser extension as well as the raspberry pi. 

### Picasso [Design](https://docs.google.com/document/d/1EFiQuFMGCKoSuqcNS2dWLJ9pPypdUNWfOIfw0M2BpQ0/edit?usp=sharing)
This microservice will be responsible for communicating with our api and displaying all of the information it receives via a small e ink display. The user can also make selections based on this input via small buttons.  

### Browser Extension [Design](https://docs.google.com/document/d/1Q587ps_vSrxBJO9yf6Wu1M1djfajAU3gDQRm8geztyY/edit?usp=sharing)
The browser extension allows the user to generate token via a context menu. It creates a random token that will pass any javascript pre-validation methods the page might contain (i.e. If it is a field prompting for an email address, the token must have an "@" sign as well as an email domain). It then sends the tokens to our server and Pi where they are assigned corresponding credentials for alter replacement.

### MITM Proxy Script [Design](https://docs.google.com/document/d/19DOHn-kxQCHsZ6j-u2Ykwr2dHSKq6gtWihOWBx5ISWU/edit?usp=sharing)
Mitmproxy is an application we found that can act as a man in the middle between the router and the browser. After its certificate is intalled, It can essentially read every request that is sent, and then repackage it into a new request to send to the intended destination. Because of this, in constrast to most proxies, it can actually read https requests as well. In addition to all of the built in tools and commands that you can use to read and modify all requests, it also supports user created add ons. We created an add on that can connect to a database table that contains the tokens that our chrome extension generates, and change them with the real credentials.



 
