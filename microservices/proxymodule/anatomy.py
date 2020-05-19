from mitmproxy import http, ctx
import requests 

import pyodbc

# database 
#def __init__(self):
    # DATABASE VARIABLES
server = '192.168.1.4' 
database = 'secure' 
username = 'sa' 
password = 'Anac0nda' 
cnxn = pyodbc.connect('DRIVER={ODBC Driver 17 for SQL Server};SERVER='+server+';DATABASE='+database+';UID='+username+';PWD='+ password)
cursor = cnxn.cursor()

#basic query

cursor.execute("SELECT * FROM ProxySwaps;")
row = cursor.fetchone()

# would be updated in the database per each request via extension
generated_email = 'abcdefg'
generated_pass = '123456789'
actual_email = ""
actual_pass = ""

# get actual creds given generated
while row: 
    if (row[3] == generated_email):
        actual_email = row[4]
    if (row[3] == generated_pass):
        actual_pass = row[4]
    row = cursor.fetchone()

#MITM REQUEST
def request(flow: http.HTTPFlow) -> None:
    #ctx.log.info("url: %s\n" % flow.request.pretty_url)
    if flow.request.pretty_url == "https://www.ebay.com/signin/s" and flow.request.method == "POST":
    
        flow.request.content = flow.request.content.replace(bytes(generated_email, encoding='utf-8'),bytes(actual_email, encoding='utf-8'))
        flow.request.content = flow.request.content.replace(bytes(generated_pass, encoding='utf-8'), bytes(actual_pass, encoding='utf-8'))
        ctx.log.info("GDFSFDSFDS url: %s\n" % flow.request.content)
