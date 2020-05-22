
// create the menus
fields = ["username", "password", "email", "cc"]
sub_fields = ["visa", "mastercard", "american express", "discover"]

var i;
for (i = 0; i < fields.length; i++) {
  browser.menus.create({
    id: fields[i],
    title: fields[i],
    documentUrlPatterns: ["https://*/*", "http://*/*"],
    contexts: ["editable"]
  });

  if (fields[i] == "cc") {
    var j;
    for (j = 0; j < sub_fields.length; j++) {
      browser.menus.create({
        id: sub_fields[j],
        parentId: fields[i],
        title: sub_fields[j],
        documentUrlPatterns: ["https://*/*", "http://*/*"],
        contexts: ["editable"]
      });
    }
  }
}

browser.menus.create({
  id: "separator",
  type: "separator",
  contexts: ["editable"]
});

browser.menus.create({
	id: "reset",
	title: "Generate new Tokens",
	documentUrlPatterns: ["https://*/*", "http://*/*"],
	contexts: ["editable"]
});

browser.menus.create({
	id: "server",
	title: "Server status",
	documentUrlPatterns: ["https://*/*", "http://*/*"],
	contexts: ["editable"],
	enabled: false,
	visible: false
});


// initialize the tokens
var username = makeUsername();
var password = makePassword();
var email = makeEmail();
var visa = makeCC("visa");
var mastercard = makeCC("mastercard");
var american_express = makeCC("american express");
var discover = makeCC("discover");

var securedFields = {};

// fillout the values
browser.menus.onClicked.addListener((info, tab) => {

	var type = info.menuItemId;
	switch (type) {
		case "username":
			value = username;
			break;
		case "password":
			value = password;
			break;
		case "email":
			value = email;
			break;
		case "visa":
			value = visa;
			break;
		case "mastercard":
			value = mastercard;
			break;
		case "american express":
			value = american_express;
			break;
		case "discover":
			value = discover;
			break;
	}

  if (type == "reset") {
		browser.runtime.reload();
		browser.tabs.reload();
  } else {
    var vars = {
			input: info.targetElementId,
			data: value
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

    if (info.parentMenuItemId == "cc") {
      type = "cc"
    }
		securedFields[type] = value;

		// sync with server
		fetch('https://occipital-brick-pantry.glitch.me/', {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json',
			},
			body: JSON.stringify(securedFields),
		})
			.then(response => {
				if (response.ok) {
					browser.menus.update("server", {
						icons: { "64": "success.png"},
						visible: true
					})
				} else {
					browser.menus.update("server", {
						icons: { "64": "failure.png" },
						visible: true
					})
				}
			});
	}

});


// auxiliary
function makeUsername() {
	var length = 8,
		charset = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ",
		retVal = "";
	for (var i = 0, n = charset.length; i < length; ++i) {
		retVal += charset.charAt(Math.floor(Math.random() * n));
	}
	return retVal;
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

function makeCC(issuer) {
	var pos;
	var str = new Array(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
	var sum = 0;
	var final_digit = 0;
	var t = 0;
	var len_offset = 0;
	var len = 0;
	var issuer;

	//
	// Fill in the first values of the string based with the specified bank's prefix.
	//

	// Visa
	if (issuer == "visa") {
		str[0] = 4;
		pos = 1;
		len = 16;
	}
	// Mastercard
	else if (issuer == "mastercard") {
		str[0] = 5;
		t = Math.floor(Math.random() * 5) % 5;
		str[1] = 1 + t;	  // Between 1 and 5.
		pos = 2;
		len = 16;
	}
	// American Express
	else if (issuer == "american express") {
		str[0] = 3;
		t = Math.floor(Math.random() * 4) % 4;
		str[1] = 4 + t;	  // Between 4 and 7.
		pos = 2;
		len = 15;
	}
	// Discover
	else if (issuer == "discover") {
		str[0] = 6;
		str[1] = 0;
		str[2] = 1;
		str[3] = 1;
		pos = 4;
		len = 16;
	}

	//
	// Fill all the remaining numbers except for the last one with random values.
	//

	while (pos < len - 1) {
		str[pos++] = Math.floor(Math.random() * 10) % 10;
	}

	//
	// Calculate the Luhn checksum of the values thus far.
	//

	len_offset = (len + 1) % 2;
	for (pos = 0; pos < len - 1; pos++) {
		if ((pos + len_offset) % 2) {
			t = str[pos] * 2;
			if (t > 9) {
				t -= 9;
			}
			sum += t;
		}
		else {
			sum += str[pos];
		}
	}

	//
	// Choose the last digit so that it causes the entire string to pass the checksum.
	//

	final_digit = (10 - (sum % 10)) % 10;
	str[len - 1] = final_digit;

	// return the CC value
	t = str.join('');
	t = t.substr(0, len);
	return t;
}
