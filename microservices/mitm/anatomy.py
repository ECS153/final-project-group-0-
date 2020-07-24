from mitmproxy import ctx
from mitmproxy import http
from datetime import datetime
from datetime import timedelta
import regex
import tldextract
import pyodbc

# DATABASE VARIABLES
server = 'tcp:secret_mssql'
database = 'secret'
username = 'SA'
password = 'Anac0nda'
cnxn = pyodbc.connect('DRIVER={ODBC Driver 17 for SQL Server};SERVER='+server+';DATABASE='+database+';UID='+username+';PWD='+ password)

cursor = cnxn.cursor()

def checkExpiration(ip, domain):
   cursor.execute("SELECT ExpiresAt FROM ProxySwaps WHERE Ip=? AND Domain=? AND ExpiresAt IS NOT NULL", ip, domain)
   rows = cursor.fetchall()

   now = datetime.now()
   for row in rows:
      expiration = datetime.strptime(row[0], '%Y-%m-%d %H:%M:%S.%f')
      if (now >= expiration):
         cursor.execute("Delete FROM ProxySwaps WHERE Ip=? AND Domain=? AND ExpiresAt=?", ip, domain, row[0])
         cnxn.commit()





#MITM REQUEST
def request(flow: http.HTTPFlow) -> None:
   if flow.request.method == "POST":
      address = flow.client_conn.address[0]
      ip = regex.sub(r'^.*:', '', address)
      host = flow.request.pretty_host
      ext = tldextract.extract(host)
      domain = ext.domain + "." + ext.suffix

      checkExpiration(ip, domain)

      cursor.execute("SELECT RandToken, Credential, ExpiresAt FROM ProxySwaps WHERE Ip=? AND Domain=?", ip, domain)
      rows = cursor.fetchall()
      ctx.log.info(ip)
      ctx.log.info("ip: %s" % ip)
      for row in rows:
         ctx.log.info("row: %s" % row)

      form = flow.request.urlencoded_form
      if (form is not None):

         keys = form.keys()
         for key in keys:
            values = form.get_all(key)

            for row in rows:
                  randToken = row[0]
                  Credential = row[1]
                  expiration = row[2]
                  ctx.log.info(randToken)
                  if (randToken and Credential):
                     if randToken in values:

                        if (expiration is None):
                              form.set_all(key, [Credential])
                              expiration = datetime.now() + timedelta(minutes=1)
                              expiration = expiration.strftime('%Y-%m-%d %H:%M:%S.%f')
                              cursor.execute("UPDATE ProxySwaps SET ExpiresAt=? WHERE Ip=? AND Domain=? AND RandToken=?", expiration, ip, domain, randToken)
                              cnxn.commit()

                              ctx.log.info('ip: %s, domain: %s, token: %s, credential: %s' % (ip,domain,randToken,Credential))
                        else:
                              flow.kill()
                              ctx.log.info('request killed')

      else:
         for row in rows:
               randToken = row[0]
               Credential = row[1]

               if (randToken and Credential):
                  flow.request.content = flow.request.content.replace(bytes(randToken, encoding='utf-8'),bytes(Credential, encoding='utf-8'))
