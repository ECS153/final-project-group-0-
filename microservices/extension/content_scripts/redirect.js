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
  var securedFields = {};

  const username = makeRandString();
  const password = makePassword();
  const email = makeEmail();
  const cc = "not implemented";
  const tel = "not implemented";

  

  function scan() {
    if (alreadyScanned) {
      return;
		}
    // get all password fields
    var fields = document.querySelectorAll('input[type="email"],[type="password"],[type="text"]');
      
    var i;
    for (i = 0; i < fields.length; i++) {

      var label = "";
      if (fields[i].type == "text") {
        label = "Sensitive field?";
      } else {
        label = "Generate";
      }
      var elem = document.createElement('div');
      elem.style.backgroundColor = "white";
      elem.addEventListener("change", fillOut);
      elem.innerHTML = '<input type="checkbox" style="float:right;"  id="check' + i + '" name="check' + i + '"><label for="check' + i + '">' + label + '</label>'; 

      fields[i].insertAdjacentElement("afterend", elem);
    }
  }

  function fillOut(e) {
    var currentElem = e.target;
    var inputField = currentElem.parentElement.previousSibling;
    inputField.value = "";

    var checkBox = "";
    var type = "";

    if (currentElem.tagName == "SELECT") {
      checkBox = currentElem.previousSibling;
      type = currentElem.options[currentElem.selectedIndex].value;
    } else {
      checkBox = currentElem;
      type = inputField.type;
    }

    if (checkBox.checked) {
      if (type == "username") {
        inputField.value = username;
        securedFields["username"] = username;
      } else if (type == "password") {
        inputField.value = password;
        securedFields["password"] = password;
      } else if (type == "email") {
        inputField.value = email;
        securedFields["email"] = email;
      } else if (type == "cc") {
        inputField.value = cc;
        securedFields["cc"] = cc;
      } else if (type == "tel") {
        inputField.value = tel;
        securedFields["tel"] = tel;
      } else {
        if (type == "text") {
          getTypeAndFill(checkBox);
        } else if (type == "none") {
          checkBox.parentElement.remove();
        } 
      }

      // fire event so as to reveal any hidden confirmation fields
      var event = new Event('change');
      inputField.dispatchEvent(event);

    } else {
      if (checkBox.nextSibling.tagName == "SELECT") {
        checkBox.nextSibling.remove();
        checkBox.nextSibling.removeAttribute("hidden");
			}
      inputField.value = "";
		}
  }

  function getTypeAndFill(div) {
    // hide label
    div.nextSibling.setAttribute("hidden", "");

    //Create array of options to be added
    var array = ["username", "password", "email", "cc", "tel", "none"];

    //Create and append select list
    var selectList = document.createElement("select");
    selectList.addEventListener("change", fillOut);
    div.insertAdjacentElement("afterend", selectList);

    //Create and append the options
    for (var i = 0; i < array.length; i++) {
      var option = document.createElement("option");
      option.value = array[i];
      option.text = array[i];
      selectList.appendChild(option);
    }

    var event = new Event('change');
    selectList.dispatchEvent(event);
  }

  function makeEmail() {
    var strValues = "abcdefg12345";
    var strEmail = "";
    var strTmp;
    for (var i = 0; i < 10; i++) {
      strTmp = strValues.charAt(Math.round(strValues.length * Math.random()));
      strEmail = strEmail + strTmp;
    }
    strTmp = "";
    strEmail = strEmail + "@";
    for (var j = 0; j < 8; j++) {
      strTmp = strValues.charAt(Math.round(strValues.length * Math.random()));
      strEmail = strEmail + strTmp;
    }
    strEmail = strEmail + ".com"
    return strEmail;
  }

  function makePassword(len) {
    var length = (len) ? (len) : (10);
    var string = "abcdefghijklmnopqrstuvwxyz"; //to upper 
    var numeric = '0123456789';
    var punctuation = '!@#$%^&*()_+~`|}{[]\:;?><,./-=';
    var password = "";
    var character = "";
    while (password.length < length) {
      entity1 = Math.ceil(string.length * Math.random() * Math.random());
      entity2 = Math.ceil(numeric.length * Math.random() * Math.random());
      entity3 = Math.ceil(punctuation.length * Math.random() * Math.random());
      hold = string.charAt(entity1);
      hold = (password.length % 2 == 0) ? (hold.toUpperCase()) : (hold);
      character += hold;
      character += numeric.charAt(entity2);
      character += punctuation.charAt(entity3);
      password = character;
    }
    password = password.split('').sort(function () { return 0.5 - Math.random() }).join('');
    return password.substr(0, len);
  }

  function makeRandString() {
    var length = 8,
      charset = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ",
      retVal = "";
    for (var i = 0, n = charset.length; i < length; ++i) {
      retVal += charset.charAt(Math.floor(Math.random() * n));
    }
    return retVal;
	}
 
  function send() {
    var sending = browser.runtime.sendMessage({
      json: JSON.stringify(securedFields)
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