# Meeting 4 (5/18/20)

## Obtaining an authentication token
* Post request to the URL
* Obtain a token from the VPN
* Will be sent a URL to submit the token
  * Something like /extension/request/authentication/

## Extension
* Make one request to authenticate
* Send a username
  * Will be unique
* Can’t do anything but submit a request

## Proxy behavior
* Accepting requests as if looking through queue
* See if someone is using the string maliciously
* Monitor traffic ~2-3 seconds and check sequential requests

## Extension
* Send post request of username
* Put request to place onto database

## Threat Model Expectation
* Keyloggers
* Screen recorders
* Phishing attackers
* Attackers will need to know the exact time we make a request
