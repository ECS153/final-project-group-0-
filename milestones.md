# Milestones
For everyone in the group include one sentence on each of these (1) what you did last week (2) what you plan to do this week (3) anything that you’re stuck on. 

## Milestone 1

### Martin
This week I tested mitmproxy, a promising python proxy app, looked into setting up browser proxy rules to only use our pi as a proxy when required, researched npyscreen, a GUI framework that we are going to use, configured permissions for my vpn to allow Raza and Marty to have access to our raspberry pi, and started working on the GUI for the pi. I'm also going to keep working on the GUI, and work with everyone to come up our *final* final design overall. 

### Marty
I worked on documenting the meetings as well as discussing the similarities to other projects that are similar to ours.
This week I plan to continue working on the extension to handle the requests made to the Pi.

### Raza
I worked on the extension to see how easy it to manipulate the page content and send to our Pi. This week I plan on working with my mates in an attempt to finalize our approach as we have had to make several changes in the previous days. 

### [Video](https://youtu.be/HAPT6BZmq78)

## Milestone 2

### Martin
I started creating the api that connects together our chrome extension with mitm proxy as well as our raspberry pi, and created the [design doc](https://docs.google.com/document/d/1CBh3EtYRP9pQcqUtRFken9FF3jxMfvshMlcplv2MuNk/edit) for it. By the end of the week I'm planning on finishing the api as well as to start looking into the raspberry pi's gui again. 

### Raza
I worked on the [extension](https://github.com/ECS153/final-project-group-0-/commit/d5a32c046e67c1637a6de2346060a770e93fa3a3) and it is mainly finished however I discovered another way (using contextmenus) to do what we needed and it seems more universal and less intrusive so I have started on that.
I also got the basic [proxy](https://github.com/ECS153/final-project-group-0-/commit/c8f9eebb451ced5f01b1f91816d89876ea505664) working. It can connect to our sql database and get the user's credentials and replace them in the requests that come through.

### Marty
I mainly tested some of the viable sites that could be used for the extension that would generate the forms to submit to the server
and looked into some of the viable hardware that could be used for the LCD screen with the raspberry pi. Will be looking into creating documentation for some of the user manuals in building the project.

### [Video](https://www.youtube.com/watch?v=9Hz0tNPrG1A&feature=youtu.be)

## Milestone 3

### Martin
This week I started reworking our GUI for our application. Earlier in the week we noticed that our display for the pi was delayed, so we decided to create the GUI in a more flexible framework (vue.js) instead of relying on the pi. So I mainly spent time learning vue.js and creating a GUI.

### Raza
The [extension](https://github.com/ECS153/final-project-group-0-/commit/ea69b9ad8561d88018a005f70dab5b82e09ebd28) is 95% percent finished. I only need to add an option to be able to toggle the proxy. 
Switching to contextmenus for the extension made it universal so it ended up being a good decision.
The [proxy module](https://github.com/ECS153/final-project-group-0-/commit/4d4e30048ef144b3bb6ae827a1704a84f9689519) was not working for url-encoded forms so I had been working on that for most of the week. It is working now.
All websites that do not encrypt the form fields client-side are working. I am stuck on trying to see if we can do something about when fields are encrypted client side.
