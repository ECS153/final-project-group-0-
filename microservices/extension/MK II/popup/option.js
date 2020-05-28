var sign_in = document.getElementById("sign-in");
var info = document.getElementById("info");
var success = document.getElementById("success");
var failure = document.getElementById("failure");
var warning = document.getElementById("warning");

function handleMessage(req) {
 
  console.log(`Message from the background script:  ${req.msg}`);

  if (req.msg == "login") {
    swap_div('sign_in');
    document.getElementById("login_btn").addEventListener("click", login);
  } else if (req.msg == "success") {
    swap_div('success')
  } else if (req.msg == "failure") {
    swap_div('failure')
  } else if (req.msg == "warning") {
    swap_div('warning');
  }

  if (req.msg == "proxy_on") {
    document.getElementById("proxy_btn").checked = true;
  } else if (req.msg == "proxy_off") {
    document.getElementById("proxy_btn").checked = false;
  }
  toggle_label();
}

browser.runtime.sendMessage({
  msg: "check_status"
}).then(function () {
  browser.runtime.sendMessage({
    msg: "check_proxy"
  });
});  
browser.runtime.onMessage.addListener(handleMessage);

function login() {

  let user = document.getElementById("login-user").value;
  let pass = document.getElementById("login-pass").value;

  var info = {
    "username": user,
    "password": pass
  }

  if (user && pass) {
    swap_div('info');
    browser.runtime.sendMessage({
      msg: JSON.stringify(info)
    });
  }
}

function swap_div(name) {

  var id = eval(name);
  sign_in.style.display = "none";
  success.style.display = "none";
  failure.style.display = "none";
  info.style.display    = "none";
  warning.style.display = "none";

  id.style.display      = "block";

  if (name == "failure") {
    setTimeout(function () { swap_div("sign_in"); }, 2000);
    document.getElementById("login_btn").addEventListener("click", login);
  } else if (name == "warning") {
    success.style.display = "block";
	}
}

document.getElementById("proxy_btn").addEventListener("change", toggle_proxy);

function toggle_proxy() {
  if (document.getElementById("proxy_btn").checked)
    browser.runtime.sendMessage({
      msg: "proxy_on"
    });
  else {
    browser.runtime.sendMessage({
      msg: "proxy_off"
    })
  }
  toggle_label();
}


function toggle_label() {
  if (document.getElementById("proxy_btn").checked)
    document.getElementById("proxy_label").innerHTML = "Disable proxy";
  else {
    document.getElementById("proxy_label").innerHTML = "Enable proxy";
  }
}