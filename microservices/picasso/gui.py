# encoding: utf-8
import npyscreen



class idleState(npyscreen.Form):
    def create(self):
        simulateRequest = 'r'
        settingsBtn = 's'

        what_to_display = 'IDLE STATE: Press {} to simulate a request \n Press {} to access settings \n Press escape key to quit'.format(simulateRequest, settingsBtn)
        self.add(npyscreen.FixedText, value=what_to_display)

        self.add_handlers({settingsBtn: self.settings})
        self.add_handlers({simulateRequest: self.onRequest})

        self.how_exited_handers[npyscreen.wgwidget.EXITED_ESCAPE] = self.exit_application

    def settings(self, keyCode):
        self.parentApp.switchForm('SETTINGS')

    def onRequest(self, code_of_key_pressed):
        message_to_display = 'Accept incoming request?.'
        notify_result = npyscreen.notify_yes_no(message_to_display, wrap=True, form_color='STANDOUT', title= 'Request', editw=1)
        if (notify_result):
            self.parentApp.switchForm('REQUEST_HANDLER')

    def exit_application(self):
        self.parentApp.setNextForm(None)
        self.editing = False



class requestHandler(npyscreen.Form):
    def create(self):
        self.add(npyscreen.TitleText, name='request handler')




class settings(npyscreen.Form):
    def create(self):
        self.add(npyscreen.TitleText, name='settings')


class MyApplication(npyscreen.NPSAppManaged):
    def onStart(self):
        self.addForm('MAIN', idleState, name='acceptRequestPrompt')
        self.addForm('SETTINGS', settings, name='settings')
        self.addFormClass('REQUEST_HANDLER', requestHandler, name='requestHandler')

if __name__ == '__main__':
    TestApp = MyApplication().run()