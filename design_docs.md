# Design Documents

## Overall Goal
Our goal is to forward browser requests that require sensitive data to a raspberry pi. The pi will contain credentials that can be used to populate the request's required fields. Then, the pi will make the request to the destination server and send the response back to the browser.

## Implementation
Because of the current climate, our team will mostly work remotely. Because of this, we felt that the best way to develop a group project would be to use [microservices](https://youtu.be/y8OnoxKotPQ).
## Microservices
### Chrome Extension [Design](https://docs.google.com/document/d/1Q587ps_vSrxBJO9yf6Wu1M1djfajAU3gDQRm8geztyY/edit?usp=sharing)
Our chrome extension will parse the page for any form elements. If it finds one, it will add an extra "submit to pi" button that will forward a custom request class to Q-ROs

### Q-ROs (Queued Request Overseer) [Design](https://docs.google.com/document/d/19DOHn-kxQCHsZ6j-u2Ykwr2dHSKq6gtWihOWBx5ISWU/edit?usp=sharing)
Q-ROs is a https server on the pi that will have POST method for accepting our custom request object from our chrome extension. These requests will be placed into a queue. Additionally, it will have a GET method that will return the head of the queue. 

### Picasso [Design](https://docs.google.com/document/d/1EFiQuFMGCKoSuqcNS2dWLJ9pPypdUNWfOIfw0M2BpQ0/edit?usp=sharing)
Picasso is the GUI that the user will interact with on the pi. Picasso's overall goal is to allow the user to select the appropriate credentials. Picasso will not have access to the full credentails, rather partial credentials that serve as a key to the full credentials. 

He will poll Q-ROs to see if any requests are in the queue. If there is, he will first check the sareq (saved requests) database to see if the request was made previously. He will then ask the user if they want to submit the saved request. If the user choses not to, or if there was no saved request, the user will manually input the fields by scrolling through a list of credentials that are partially visible. The partial credentials will be stored on the parcred database. After the user has filled out the required fields, the custom request object will contain the partial credentials and will be sent to SeCret.

### SeCreT (Secure Credential Transport) [Design](https://docs.google.com/document/d/1CBh3EtYRP9pQcqUtRFken9FF3jxMfvshMlcplv2MuNk/edit?usp=sharing)
Secret's overall goal is to ensure that the request has a legitimate source. This will be done by SSL/TLS verification. Once verified, Secret will swap all of the partial credentials for the full credentials that it gets from the fulcred database. Only Secret has access to this database. Finally after the request has been sent, it will forward the site's response back to the browser

###
