from mitmproxy import http, ctx
import regex
import tldextract
import pyodbc

# DATABASE VARIABLES
server = '192.168.1.4'
database = 'secret'
username = 'sa'
password = 'Anac0nda'
cnxn = pyodbc.connect('DRIVER={ODBC Driver 17 for SQL Server};SERVER='+server+';DATABASE='+database+';UID='+username+';PWD='+ password)
cursor = cnxn.cursor()

#basic query

#ip = '192.168.1.24'
#domain = 'wikipedia.org'
#cursor.execute("SELECT RandToken, Credential FROM ProxySwaps WHERE Ip=? AND Domain=?;", ip, domain)
#row = cursor.fetchone()

#while row:
    #print(row)
    #row = cursor.fetchone()



#MITM REQUEST
def request(flow: http.HTTPFlow) -> None:

    if flow.request.method == "POST":
        form = flow.request.urlencoded_form
        keys = form.keys()
        for key in keys:
            values = form.get_all(key)
            ctx.log.info(values)

        address = flow.client_conn.address[0]
        #ip = regex.sub(r'^.*:', '', address)
        ip = '192.168.1.24'
        host = flow.request.pretty_host
        ext = tldextract.extract(host)
        domain = ext.domain + "." + ext.suffix

        cursor.execute("SELECT RandToken, Credential FROM ProxySwaps WHERE Ip=? AND Domain=?;", ip, domain)
        row = cursor.fetchone()

        while row:
            ctx.log.info('rand : %s and cred : %s' % (row[0], row[1]))
            #ctx.log.info("-------------")
            if (row[0] and row[1]):
                flow.request.content = flow.request.content.replace(bytes(row[0], encoding='ascii'),bytes(row[1], encoding='ascii'))
            row = cursor.fetchone()
