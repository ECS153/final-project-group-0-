import pyodbc


server = '192.168.1.4'
database = 'secret'
username = 'sa'
password = 'Anac0nda'
cnxn = pyodbc.connect('DRIVER={ODBC Driver 17 for SQL Server};SERVER='+server+';DATABASE='+database+';UID='+username+';PWD='+ password)
cursor = cnxn.cursor()

#Sample select query
ip = '192.168.1.24'
domain = 'wikipedia.org'
cursor.execute("SELECT RandToken FROM ProxySwaps WHERE Ip=? AND Domain=?;", ip, domain)
row = cursor.fetchone()

while row:
    print(row[0])
    row = cursor.fetchone()
