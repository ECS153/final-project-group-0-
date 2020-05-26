function handleMessage(req) {
  let sign_in = document.getElementById("sign-in");
  let success = document.getElementById("success");
  let failure = document.getElementById("failure");

  console.log(`Message from the background script:  ${req.msg}`);

  if (req.msg == "login"){
      sign_in.style.display = "block";
      success.style.display = "none";
      failure.style.display = "none";

      document.getElementById("login_btn").addEventListener("click", login);
  } else if (req.msg == "success"){
      sign_in.style.display = "none";
      success.style.display = "block";
      failure.style.display = "none";

  } else if (req.msg == "failure"){
      sign_in.style.display = "none";
      success.style.display = "none";
      failure.style.display = "block";
  }

}

browser.runtime.sendMessage({
  msg: "verify token"
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
    browser.runtime.sendMessage({
      msg: JSON.stringify(info)
    });
  }
}
