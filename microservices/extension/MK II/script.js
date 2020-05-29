
var inputField = browser.menus.getTargetElement(vars.input);
var value = vars.data;

if (inputField) {

	// fire event so as to reveal any hidden confirmation fields
	var event_1 = new Event('change');
	var event_2 = new Event('input');
	var event_3 = new Event('click');
	var event_4 = new Event('keyup');
	var event_5 = new Event('blur');
	var event_6 = new Event('focus');
	var event_7 = new Event('keydown');

	// fillout field
	inputField.setAttribute("value", value);
	inputField.value = value;

	inputField.dispatchEvent(event_1);
	inputField.dispatchEvent(event_2);
	inputField.dispatchEvent(event_3);
	inputField.dispatchEvent(event_4);
	inputField.dispatchEvent(event_5);
	inputField.dispatchEvent(event_6);
	inputField.dispatchEvent(event_7);

}
