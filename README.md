# SECRET- Secure Credential Transportation

## Collaborators
Martin Petrov, Marty Macalalad, Raza Ahmed

## Overall Goal
Our goal is to create a password manager that can keep a user's credentials untouchable by any malware that is on the computer the credentials are submitted from, as well as from man in the middle attacks on the local network. We feel that we can guarantee this by having the browser send randomly generated tokens in place of credentials, and then substituting the real credentials at the latest possible moment possible, via a proxy that would act as a man in the middle between the user and the router. 

## Assumptions 
 The only assumption we made is that the raspberry pi would not be tampered with. Because it only needs to connect to our API, we could easily create a root certificate that would only authenticate connections to our API, and nothing else; including all external traffic. We feel that because the pi only has 1 use case (logging into websites) compared a computer or smartphone, that it would be much harder for an attacker to install malware, as attackers most often rely on users unknowingly performing some operation. So we feel that our assumption is not unrealistic


## Repository Outline 
All of our code can be found within our microservices folder. Each subfolder is an independent service that can be successfully launched without any other code outside its folder. Within each subfolder there should be a README that contains information about the relevant parts of the code, though our core logic is mostly contained in our [API](https://github.com/ECS153/final-project-group-0-/blob/master/microservices/dotnetapi/README.md)

### Root Directory
  - Meetings: Quick notes we took while we were in our meetings
  - Microservices: Folder containing all of our microservices.
  - ReadMe.md: You're reading it
  - design_docs.md: Contains a detailed explanation of the "microservices" we made, as well as how they interact with each other
  - milestones.md: Contains a summary of our weekly progress on the project
  - proposal.md: Contains our updated proposal for what our
