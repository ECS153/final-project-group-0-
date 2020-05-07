# Proposal

## The Problem
One of the biggest incentives for hackers is gaining access to sensitive data. This can be in the form of credit cards or passwords. While browsers are excellent at encapsulating this type of data within a session, there is little browsers can do when the OS itself is compromised. Thus, almost all data sent is still vulnerable to malware such as screen recorders or keyloggers. 

## The Importance
Keyloggers are still commonplace, and one of the most effective ways to steal sensitive user input. 

## The Solution
Inspired by Fidelius, we wanted to generalize their threat model to work on all computer systems (i.e., lacking a system on chip enclave, as well as encrypted kb/mouse dongle). Instead, we replace the SoC enclave with a hardware-based enclave (for our purposes, a raspberry pi) that would contain a user’s passwords/usernames/credit cards. This way, the pi would serve as a hardware password manager.

However, unlike most hardware password managers, instead of simply sending the sensitive data to the computer you’re trying to login from, the pi itself will grab the empty form data from the computer, populate it with the sensitive data required, and then make the request itself to the site. Then, it will forward the response it received from the site back to the computer. 

## The Expected Results
When users activate our browser extension, they will get the option to forward the form data of any site to the pi. After they click the “forward” button, the pi will ask the user via a small display whether this request was legitimate. If the user clicks “yes” via a button on the pi, the pi will prompt the user to match each input in the form with pre existing data stored on the pi. Then, the user can click “send” for the information to be sent.
