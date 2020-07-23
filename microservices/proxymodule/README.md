## For Ubuntu 

apt install python3

apt install pip

### Install pipx for isolated environemnt
python3 -m pip install --user pipx

python3 -m pipx ensurepath (so mitmproxy and other pipx installs can be executed globally)

pipx install mitmproxy

pipx inject mitmproxy pyodbc
pipx inject mitmproxy tldextract
pipx inject mitmproxy regex

#### If you ran into errors with previous command, install unixodbc-2.3.7, not 2.3.6, and do (this) [https://askubuntu.com/questions/1183140/installing-unixodbc-on-19-10]

pipx inject mitmproxy requests

(and any other package needed)


### If SQL driver needed for pyodbc
sudo su

curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -


### Download appropriate package for the OS version

### Choose only ONE of the following, corresponding to your OS version


### Ubuntu 16.04

curl https://packages.microsoft.com/config/ubuntu/16.04/prod.list > /etc/apt/sources.list.d/mssql-release.list


### Ubuntu 18.04

curl https://packages.microsoft.com/config/ubuntu/18.04/prod.list > /etc/apt/sources.list.d/mssql-release.list


### Ubuntu 19.10

curl https://packages.microsoft.com/config/ubuntu/19.10/prod.list > /etc/apt/sources.list.d/mssql-release.list


exit

sudo apt-get update

sudo ACCEPT_EULA=Y apt-get install msodbcsql17

### optional: for bcp and sqlcmd

sudo ACCEPT_EULA=Y apt-get install mssql-tools

echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile

echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc

source ~/.bashrc

### optional: for unixODBC development headers

sudo apt-get install unixodbc-dev

then the proxy can simply be run with : mitmproxy --script anatomy.py
