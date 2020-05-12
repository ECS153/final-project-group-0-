
function listenForClicks() {
  document.addEventListener("click", (e) => {

    function enable(tabs) {
      browser.browserAction.setIcon({path: '/icons/on.png'});
      browser.tabs.sendMessage(tabs[0].id, {
        command: "enable",
      });
      window.close();
    }

    function disable(tabs) {
      browser.browserAction.setIcon({path: '/icons/off.png'});
      browser.tabs.sendMessage(tabs[0].id, {
        command: "disable",
      });
      window.close();
    }

    if (e.target.classList.contains("enable")) {
      browser.tabs.query({active: true, currentWindow: true})
        .then(enable);
    }
    else if (e.target.classList.contains("disable")) {
      browser.tabs.query({active: true, currentWindow: true})
        .then(disable);
    }
  });
}


browser.tabs.executeScript({file: "/content_scripts/redirect.js"})
.then(listenForClicks);