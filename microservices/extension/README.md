# SECRET Browser Extension

## What it does ##

Currently the extension is able to generate username/pass/email and send to server and wait for response. 

In order to test, firefox is needed. Go to about:debugging -> this firefox -> load temporary addon.  

From there, link to the manifest.json file along with the other files in the same directory and the extension should be loaded.

## How it All Works ##
### popup/* ### 
The popup keeps the user informed about the status of the whole operation. It can let the user know when a token has been sent successfully. It also lets the user know if there are any pending requests he has to confirm on the pi. There is also a checkbox to toggle the proxy which allows post request to be routed through our proxy.
### manifest.json ###
This is the meta file about the extesnion. It lists the information about the extension and the permissions it has.
### background.js ###
This is the bulk of the extension. The javascript is in charge of communicating with the server, generating and sending tokens, and communicating with the popup in order to inform the user of any failures.
### script.js ###
This script gets injected into the webpage, Its only purpose is to fill out the input fiels with the generated tokens.
