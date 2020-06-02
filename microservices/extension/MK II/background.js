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
		id: "previous",
		title: "Previous value",
		documentUrlPatterns: ["https://*/*", "http://*/*"],
		contexts: ["editable"]
	});
}


var token;
var logged_in;
var previous = "";

createMenus();

// fillout the values
browser.menus.onClicked.addListener(async (info, tab) => {
	var type = info.menuItemId;
	await browser.browserAction.openPopup();

	var value = "Please Login first";
	var credentialType;

	if (logged_in) {

		if (info.parentMenuItemId == "cc") {
			type = "cc";
		}

		switch (type) {
			case "previous":
				credentialType = 404;
				value = previous;
				break;
			case "password":
				credentialType = 0;
				value = makePassword();
				break;
			case "cc":
				credentialType = 1;
				switch(info.menuItemId) {
					case "visa":
						value = makeCC("visa");
						break;
					case "mastercard":
						value = makeCC("discover");
						break;
					case "american express":
						value = makeCC("express");
						break;
					case "discover":
						value = makeCC("discover");
						break;
				}
				break;
			case "username":
				credentialType = 2;
				value = makeUsername();
				break;
			case "email":
				credentialType = 3;
				value = makeEmail();
				break;
		}
	}

	previous = value;
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

	if (logged_in && credentialType != 404) {
		new_pin = random4Digit();
		var updatedSettings = {
			token: token,
			pin: new_pin
		}
		browser.storage.local.set(updatedSettings);
		var json = {
			FieldId: type,
			RandToken: value,
			domain: domain_from_url(tab.url),
			type: credentialType,
			AuthId: eval(new_pin)
		};
		var bearer = 'Bearer ' + token;
		fetch('http://192.168.1.5:5000/swap/new', {
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
});


// identification credentials
var defaultSettings = {
	token: "none",
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
		var url = "http://192.168.1.5:5000/swap/";
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

function checkPin(storedSettings) {
	if (storedSettings.pin) {
		sendMsg(storedSettings.pin);
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
			return response.json();
		} else {
			return false;
		}
	}).then(body => {
		if (body) {
			var updatedSettings = {
				token: body.token,
			};
			browser.storage.local.set(updatedSettings);
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
	} else if (request.msg == "check_pin") {
		const gettingStoredSettings = browser.storage.local.get();
		gettingStoredSettings.then(checkPin, onError);
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

function random4Digit(){
  return shuffle( "0123456789".split('') ).join('').substring(0,4);
}

function shuffle(o){
    for(var j, x, i = o.length; i; j = Math.floor(Math.random() * i), x = o[--i], o[i] = o[j], o[j] = x);
    return o;
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
