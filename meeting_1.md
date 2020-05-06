# Meeting 1

# Creation of the proposal
* Refined / Clarification of the proposal

## Development Ideas
* PI Zero
  * Small and efficient
  * Single Core
  * Don’t have to deal with multi-core vulnerabilities
  * Based on Armv6 Architecture

* Raspbian Lite
  * Software development for the PI
* PiCore
  * Super small linux distro
* PiPyOS
  * Bare Metal Python
  * Smaller and more secure
  * Annoying to get a network connection / not as readily available
* E-Ink Display consideration
  * Lower power consumption
* Option to submit every form through the extension
  * Find the form
* If you never have visited the page before
  * Sending information based on the formed data / HTML to the PI
  * Each input field
* Saved a configuration of the data if you’ve visited the page before
* Chrome extension
  * Essentially loads in Javascript
* Drawbacks of the PI
  * If you can’t hack the computer, why not just hack the PI?
* Password-blocked database
* Assumptions
  * SSL / TLS encryption not an issue
  * Smart users?
* Docker
  * Separate containers for password database, request client, and local form server
  * Will probably be more overhead than we can afford on pi zero
* Vulnerabilities
  * Not specific to our implementation
    * Use of secure browsers
* OS on the pi itself, so that can be exploitable
* Comparisons to similar software / concepts
  * Hardware-based password keepers
    * https://www.themooltipass.com/
      * Different in that it emulates the keyboard instead of directly masking the input from a regular keyboard
* Responses in a form of cookies
