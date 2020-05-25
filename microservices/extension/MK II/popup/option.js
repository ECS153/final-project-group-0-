function handleResponse(msg) {

  console.log(msg.response);
  let sign_in = document.getElementById("sign-in");
  let success = document.getElementById("success");
  let failure = document.getElementById("failure");

  if (msg.response == "login") {
    console.log("user needs to log in");

    success.style.display = "none";
    failure.style.display = "none";
    sign_in.style.display = "block";

    document.getElementById("login_btn").addEventListener("click", login);
  } else {
    if (msg.response == "success") {

      success.style.display = "block";
      failure.style.display = "none";
      sign_in.style.display = "none";

      console.log(`Message from the background script:  ${msg.response}`);
    } else {
      success.style.display = "none";
      failure.style.display = "block";
      sign_in.style.display = "none";
    }
  }
}

function handleMessage(msg) {
  let sign_in = document.getElementById("sign-in");
  let success = document.getElementById("success");
  let failure = document.getElementById("failure");

  console.log(`Message from the background script:  ${msg.response}`);

  if (msg.status == "ok") {

    success.style.display = "block";
    failure.style.display = "none";
    sign_in.style.display = "none";

  } else {
    success.style.display = "none";
    failure.style.display = "block";
    sign_in.style.display = "none";
  }

}


function handleError(error) {
  console.log(`Error: ${error}`);
}
var sending = browser.runtime.sendMessage({
  msg: "verify token"
});
sending.then(handleResponse, handleError);

browser.runtime.onMessage.addListener(handleMessage);

function login() {

  let user = document.getElementById("login-user").value;
  let pass = document.getElementById("login-pass").value;

  var info = {
    "username": user,
    "password": pass
  }

  if (user && pass) {
    browser.runtime.sendMessage({
      msg: JSON.stringify(info)
    });
  }
}