# Meeting 3 (5/12/20)

## Different Pi to use
  * Pi0+ (Pi2, Pi3, Pi4) better than Pi0 for multithreaded tasks
  * Tradeoff of Battery usage vs performance

## Proxies and Networks
* Focus on the network layer for accessing the credentials

* Proxies allow for a pac file
  * Contains proxy information
  * Has a javascript function that checks a URL to know where to send 
    * From there, we can maybe forward to the pi
    * I.e., using Amazon site
      * A few but not all requests are sent 
  * Potentially saving packets for sites that may be used in the future

* Prevention of Phishing
  * Matching URLs with credentials
  * If the user doesn’t know the password, then it can’t be extracted for misuse in the first place
  * Timeouts for credentials

* Proxies having builtin root certificates
  * Root certificate usage will be on the Pi itself rather than from the websites
    * Potentially better security-wise

* Only search when the Chrome extension gives an id key
  * Only activating when necessary

## The Microservices
* Chrome Extension
  * Scanning for form / input fields on websites
  * Tell QROs what’s been inputted
    * Possibly create unique id for emails for storage

* Not necessary to have SeCreT microservice?
  * Use of Man in the Middle (MitM Proxy) API instead
  * Potentially using a server for it instead 
    * If you login into SeCreT, you can’t read; only write access

## Miscellaneous
* Useof virtual environments for testing

* Fixed credential login depending on the website

* Change project name to SeCreT instead?
