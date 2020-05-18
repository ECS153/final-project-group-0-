from mitmproxy import http, ctx
import requests 



def __init__(self):
    # DATABASE VARIABLES
    server = '192.168.1.4,1433' 
    database = 'tempdb' 
    username = 'sa' 
    password = 'Anac0nda' 
    cnxn = pyodbc.connect('DRIVER={ODBC Driver 17 for SQL Server};SERVER='+server+';DATABASE='+database+';UID='+username+';PWD='+ password)
    cursor = cnxn.cursor()


# MITM REQUEST
def request(flow: http.HTTPFlow) -> None:
    #ctx.log.info("url: %s\n" % flow.request.pretty_url)
    if flow.request.pretty_url == "https://www.ebay.com/signin/s" and flow.request.method == "POST":
    
        flow.request.content = flow.request.content.replace(b'abcdefg',b'martin.petrov.1096@gmail.com')
        flow.request.content = flow.request.content.replace(b'123456789', b'0nef0rallf0r0ne')
        ctx.log.info("GDFSFDSFDS url: %s\n" % flow.request.content)