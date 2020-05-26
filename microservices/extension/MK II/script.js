
var inputField = browser.menus.getTargetElement(vars.input);
var value = vars.data;

if (inputField) {
	// fillout field
	inputField.setAttribute("value", value);
	inputField.value = value;

	// fire event so as to reveal any hidden confirmation fields
	var event = new Event('change');
	inputField.dispatchEvent(event);
}
