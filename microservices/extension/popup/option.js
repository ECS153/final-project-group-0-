
function listenForClicks() {
  document.addEventListener("click", (e) => {

    function scan(tabs) {
      browser.tabs.sendMessage(tabs[0].id, {
        command: "scan",
      });
      window.close();
    }

    function send(tabs) {
      browser.tabs.sendMessage(tabs[0].id, {
        command: "send",
      });
    }

    if (e.target.id === "scan") {
      browser.tabs.query({active: true, currentWindow: true})
        .then(scan);
    }
    else if (e.target.id === "send") {
      e.target.innerHTML = "Please Wait";
      browser.tabs.query({active: true, currentWindow: true})
        .then(send);
    }
  });
}


browser.tabs.executeScript({file: "/content_scripts/redirect.js"})
  .then(listenForClicks);

browser.runtime.onMessage.addListener(sendToServer);
function sendToServer(tempIDs) {
  var elem = document.getElementById("send");

  fetch('https://occipital-brick-pantry.glitch.me/', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: tempIDs.json,
  })
  .then(response => {
    if (response.ok) {
      elem.innerHTML = "Done!";
    } else {
      elem.innerHTML = "Something went wrong";
    }
  });
}

function checkToken() {

}