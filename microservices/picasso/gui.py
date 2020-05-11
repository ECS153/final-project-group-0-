# encoding: utf-8
import npyscreen
import external_requests

class idleState(npyscreen.Form):
    def create(self):
        settingsBtn = 's'
        self.keypress_timeout = 10 # while_waiting function will run once a second

        what_to_display = 'IDLE STATE: Press escape key to quit'.format(settingsBtn)
        self.add(npyscreen.FixedText, value=what_to_display)

        self.add_handlers({settingsBtn: self.settings})

    def settings(self, keyCode):
        self.parentApp.switchForm('SETTINGS')


    def while_waiting(self):
        newReq = external_requests.getNewRequest()
        if bool(newReq):
            self.parentApp.curReq = newReq

            self.parentApp.switchForm('REQUEST_ACCEPT_PROMPT')

class requestAcceptPrompt(npyscreen.ActionFormV2):
    def create(self):
        message_prompt_accept = 'Accept incoming request?.'
        sourceUrl = "Source-Url: " + self.parentApp.curReq['sourceUrl']
        self.add(npyscreen.TitleFixedText, name=message_prompt_accept, editable=False)
        self.add(npyscreen.FixedText, value=sourceUrl, editable=False)

    def on_ok(self):
        self.parentApp.switchForm('REQUEST_HANDLER')

    def on_cancel(self):
        self.parentApp.switchForm('MAIN')

class requestHandler(npyscreen.ActionFormMinimal):
    def create(self):
        self.inputs = []
        i = 0
        for key, value in self.parentApp.curReq['reqContents'].items():
            self.inputs.append(self.add(npyscreen.TitleText, name=key, value=value))

    def afterEditing(self):
        for input in self.inputs:
           self.parentApp.curReq['reqContents'][input.name] = input.value

    def on_ok(self):
        self.parentApp.switchForm('REQUEST_CONFIRM')
        
class requestConfirm(npyscreen.ActionForm):
    def create(self):
        for key, value in self.parentApp.curReq['reqContents'].items():
            self.add(npyscreen.TitleFixedText, name=key, value=value, editable=False)

    def on_cancel(self):
        self.parentApp.switchForm('MAIN')


class settings(npyscreen.Form):
    def create(self):
        self.add(npyscreen.TitleFixedText, name='settings', editable=False)


class MyApplication(npyscreen.NPSAppManaged):
    curReq = {}
    curReq['sourceUrl'] = ""
    def onStart(self):

        self.addForm('MAIN', idleState, name='main', minimum_lines=10, minimum_columns=30)
        self.addForm('SETTINGS', settings, name='settings', minimum_lines=10, minimum_columns=30)
        self.addFormClass('REQUEST_ACCEPT_PROMPT', requestAcceptPrompt, name='New Incoming Request', minimum_lines=10, minimum_columns=30)
        self.addFormClass('REQUEST_HANDLER', requestHandler, name='Edit Request', minimum_lines=10, minimum_columns=30)
        self.addFormClass('REQUEST_CONFIRM', requestConfirm, name='Confirm Request', minimum_lines=10, minimum_columns=30)

if __name__ == '__main__':
    TestApp = MyApplication().run()