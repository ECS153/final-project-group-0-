
browser.menus.create({
  id: "username",
  title: "Username",
  documentUrlPatterns: ["https://*/*", "http://*/*"],
  contexts: ["editable"]
});

browser.menus.create({
  id: "password",
  title: "Password",
  documentUrlPatterns: ["https://*/*", "http://*/*"],
  contexts: ["editable"]
});

browser.menus.create({
  id: "email",
  title: "Email",
  documentUrlPatterns: ["https://*/*", "http://*/*"],
  contexts: ["editable"]
});

var type;

browser.menus.onClicked.addListener((info, tab) => {

  type = info.menuItemId

  var vars = {
    type: info.menuItemId,
    input: info.targetElementId
    };

  browser.tabs.executeScript(tab.id, {
    allFrames: true,
    code: 'var vars = ' + JSON.stringify(vars)
  }, function () {
    browser.tabs.executeScript(tab.id, {
      allFrames: true,
      file: 'script.js'
    });
  });

});

var securedFields = {};

function handleMessage(msg) {
  securedFields[type] = msg.value
  console.log("generated " + JSON.stringify(securedFields))
}

browser.runtime.onMessage.addListener(handleMessage);