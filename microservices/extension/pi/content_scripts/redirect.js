(function() {
  /**
   * Check and set a global guard variable.
   * If this content script is injected into the same page again,
   * it will do nothing next time.
   */
  if (window.hasRun) {
    return;
  }
  window.hasRun = true;

  var alreadyScanned;

  function scan() {
    if (alreadyScanned) {
      return;
		}
    // get all password fields
    var fields = document.querySelectorAll('input[type="password"]');
      
    var i;
    for (i = 0; i < fields.length; i++) {
      // create checkbox for each field on page
      var checkBox = document.createElement("input");
      checkBox.setAttribute("type", "checkbox");
      checkBox.addEventListener("change", fillOut);

      fields[i].insertAdjacentElement("afterend", checkBox);
    }
  }

  function fillOut(e) {
    var checkBox = e.target;
    var input = checkBox.previousSibling;

    if (checkBox.checked) {
      input.value = "password";
    } else {
      input.value = "";
		}
	}
    
  function send() {
    // create a JSON object
    const data = {
      "email": "eve.holt@reqres.in",
      "password": "cityslicka"
    };

    var sending = browser.runtime.sendMessage({
      json: JSON.stringify(data)
    });
  }

  browser.runtime.onMessage.addListener((message) => {
    if (message.command === "scan") {
      scan();
      alreadyScanned = true;
    } else if (message.command === "send") {
      send();
    }
  });

})();