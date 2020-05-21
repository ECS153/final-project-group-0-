
var type = vars.type;
var inputField = browser.menus.getTargetElement(vars.input);

if (type == "username") {
	inputField.value = makeRandString();
} else if (type == "password") {
	inputField.value = makePassword();
} else if (type == "email") {
	inputField.value = makeEmail();
}

browser.runtime.sendMessage({
	value: inputField.value
});


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