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
        #ctx.log.info('------------------------')
        #ctx.log.info(flow.request.pretty_host)

        #address = flow.client_conn.address[0]
        #ip = regex.sub(r'^.*:', '', address)
        ip = '192.168.1.24'
        #host = 'wikipedia.org'
        host = flow.request.pretty_host
        ext = tldextract.extract(host)
        domain = ext.domain + "." + ext.suffix

        cursor.execute("SELECT RandToken, Credential FROM ProxySwaps WHERE Ip=? AND Domain=?", ip, domain)
        rows = cursor.fetchall()

        form = flow.request.urlencoded_form
        if (form is not None):

            keys = form.keys()
            for key in keys:
               values = form.get_all(key)

               for row in rows:
                randToken = row[0]
                Credential = row[1]

                if (randToken and Credential):
                  ctx.log.info('%s not in vlaues' % randToken)
                  if randToken in values:
                    ctx.log.info('%s in vlaues' % randToken)
                    form.set_all(key, [Credential])

               #value_list = ','.join(str(v) for v in values)
               #ctx.log.info(value_list)
               #break
        else:
          for row in rows:
            randToken = row[0]
            Credential = row[1]
        
            if (randToken and Credential):
              flow.request.content = flow.request.content.replace(bytes(randToken, encoding='utf-8'),bytes(Credential, encoding='utf-8'))
          
