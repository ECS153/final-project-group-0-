function createMenus() {
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
		id: "gen",
		title: "Generate new Tokens",
		documentUrlPatterns: ["https://*/*", "http://*/*"],
		contexts: ["editable"]
	});
}

var username = makeUsername();
var password = makePassword();
var email = makeEmail();
var visa = makeCC("visa");
var mastercard = makeCC("mastercard");
var american_express = makeCC("american express");
var discover = makeCC("discover");
var token;
var logged_in;

createMenus();

// fillout the values
browser.menus.onClicked.addListener(async (info, tab) => {
	var type = info.menuItemId;
	await browser.browserAction.openPopup();


	if (type == "gen") {
		username = makeUsername();
		password = makePassword();
		email = makeEmail();
		visa = makeCC("visa");
		mastercard = makeCC("mastercard");
		american_express = makeCC("american express");
		discover = makeCC("discover");

		//reload tab so as to reset everything
		browser.tabs.reload();
	} else {
		var value = "Please Login first";
		var credentialType;

		if (logged_in) {
			switch (type) {
				case "username":
					value = username;
					credentialType = 2;
					break;
				case "password":
					value = password;
					credentialType = 0;
					break;
				case "email":
					value = email;
					credentialType = 3;
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
		}

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
			type = "cc";
			credentialType = 1;
		}

		var json = {
			FieldId: type,
			RandToken: value,
			domain: domain_from_url(tab.url),
			type: credentialType
		};

		if (logged_in) {
			var bearer = 'Bearer ' + token;
			fetch('http://192.168.1.5:5000/browser', {
				method: 'POST',
				withCredentials: true,
				credentials: 'include',
				headers: {
					'Authorization': bearer,
					'Content-Type': 'application/json'
				},
				body: JSON.stringify(json)
			});
		}
	}
});


// identification credentials
var defaultSettings = {
	token: "none"
};

function checkStoredSettings(storedSettings) {
	if (!storedSettings.token) {
		browser.storage.local.set(defaultSettings);
	}
}

const gettingStoredSettings = browser.storage.local.get();
gettingStoredSettings.then(checkStoredSettings, onError);

function onError(e) {
	console.error(e);
}

function checkStatus(storedSettings) {
	if (storedSettings.token == "none") {
		sendMsg("login");
	} else {
		var noResponse = setTimeout(function () { sendMsg("failure"); }, 3500);
		var url = "http://192.168.1.5:5000/pi/";
		token = storedSettings.token;
		var bearer = 'Bearer ' + token;
		fetch(url, {
			method: 'GET',
			withCredentials: true,
			credentials: 'include',
			headers: {
				'Authorization': bearer,
				'Content-Type': 'application/json'
			}
		}).then(response => {
			clearTimeout(noResponse);
			if (response.ok) {
				logged_in = true;
				if (response.status == "204") {
					sendMsg("success");
				} else {
					sendMsg("warning");
				}
			} else {
				sendMsg("failure");
			}
		});
	}
}

function login(info) {
	var noResponse = setTimeout(function () { sendMsg("failure"); }, 3500);
	fetch('http://192.168.1.5:5000/user/authenticate', {
		method: 'POST',
		headers: {
			'Content-Type': 'application/json',
		},
		body: info,
	}).then(response => {
		clearTimeout(noResponse);
		if (response.ok) {
			browser.tabs.reload();
			return response.json();
		} else {
			return false;
		}
	}).then(body => {
		if (body) {
			var updatedSettings = {
				token: body.token
			};
			browser.storage.local.set(updatedSettings);

			browser.tabs.reload();
			checkStatus(updatedSettings);
		} else {
			sendMsg("failure");
		}
	});
}


function sendMsg(msg) {
	browser.runtime.sendMessage({
		msg: msg
	});
}

// popup communication
function handleMessage(request, sender, sendResponse) {
	console.log("request from popup: " +
		request.msg);

	if (request.msg == "check_status") {
		const gettingStoredSettings = browser.storage.local.get();
		gettingStoredSettings.then(checkStatus, onError);
	} else if (request.msg == "check_proxy") {
		if (proxy) {
			sendMsg("proxy_on");
		} else {
			sendMsg("proxy_off");
		}
	} else if (request.msg == "proxy_on") {
		toggleProxy(request.msg);
	} else if (request.msg == "proxy_off") {
		toggleProxy(request.msg);
	} else {
		login(request.msg);
	}
}

browser.runtime.onMessage.addListener(handleMessage);


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

function domain_from_url(url) {
	var result
	var match
	if (match = url.match(/^(?:https?:\/\/)?(?:[^@\n]+@)?(?:www\.)?([^:\/\n\?\=]+)/im)) {
		result = match[1]
		if (match = result.match(/^[^\.]+\.(.+\..+)$/)) {
			result = match[1]
		}
	}
	return result
}

// proxy
var proxy = false;
// Listen for a request to open a webpage
function toggleProxy(msg) {
	if (msg == "proxy_on") {
		browser.proxy.onRequest.addListener(handleProxyRequest, { urls: ["<all_urls>"] });
		proxy = true;
	} else {
		browser.proxy.onRequest.removeListener(handleProxyRequest, { urls: ["<all_urls>"] });
		proxy = false;
	}
}

// On the request to open a webpage
function handleProxyRequest(requestInfo) {
	// Read the web address of the page to be visited
	const url = new URL(requestInfo.url);
	// Determine whether the domain in the web address is on the blocked hosts list
	if (requestInfo.method == "POST" && url.hostname != "192.168.1.5") {
		// Write details of the proxied host to the console and return the proxy address
		console.log(`Proxying: ${url.hostname}`);
		return { type: "http", host: "192.168.1.24", port: 8080 };
	}
	// Return instructions to open the requested webpage
	return { type: "direct" };
}
