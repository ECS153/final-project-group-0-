import pyodbc 
# Some other example server values are
# server = 'localhost\sqlexpress' # for a named instance
# server = 'myserver,port' # to specify an alternate port
server = '192.168.1.4' 
database = 'secure' 
username = 'sa' 
password = 'Anac0nda' 
cnxn = pyodbc.connect('DRIVER={ODBC Driver 17 for SQL Server};SERVER='+server+';DATABASE='+database+';UID='+username+';PWD='+ password)
cursor = cnxn.cursor()

#Sample select query
cursor.execute("SELECT * FROM ProxySwaps;") 
row = cursor.fetchone()  

while row:
    print(row[3])
    row = cursor.fetchone()
