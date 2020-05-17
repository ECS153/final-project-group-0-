# Meeting 2 (5/12/20)

## Databasing
* 3 databases
  * Full password
    * Full Cred
  * Asterisk
    * Par-Cred
  * Sareq
    * Database for Saving HTTP requests

## Microservices
### Q-ROs
* Queued-Response Overseer
* Web server that sends post requests
* Stores a queue of forms
* Handles Post and Get requests
* A get method to get the next form

### Picasso
* GUI
* Asks for the first get request
* Polling constantly to get forms from the queue from Q-ROs
* Use of Par-Credentials
* While its waiting for the form method, it will try to get it from QROs
* Auto-enabled
* Site, checkmark validation
* Toggling Between different fields to partially show
  * Credit, username, credit card
* Check Sareq
  * Save the entire HTTP request login
  * Table to save for the HTTP
### SeCreT
* Secure credentials transfer
* Has access to full credentials based on the partial credentials
* Post request generation then sends a response cookie for logging 

### Chrome Extension
* Generate the form and request object, then send it to Q-ROs
* Body will have multiple fields and service 
* JSON format to find the form
