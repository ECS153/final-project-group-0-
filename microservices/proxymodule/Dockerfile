FROM alpine:3.11

ENV LANG=en_US.UTF-8

# Add our user first to make sure the ID get assigned consistently,
# regardless of whatever dependencies get added.
RUN addgroup -S mitmproxy && adduser -S -G mitmproxy mitmproxy \
 && apk add --no-cache \
   bash-doc \
   bash-completion \
   curl \
   g++ \
   libffi \
   libffi-dev \
   libstdc++ \
   openssl \
   openssl-dev \
   python3 \
   python3-dev  

# Install pip and python globally
RUN pip3 install -U pip \
 && python3 -m pip install -U pipx \
 && python3 -m pipx ensurepath 

# Add all of the drivers needed to connect to the database
RUN curl -O https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.5.2.2-1_amd64.apk \
 && curl -O https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/mssql-tools_17.5.2.1-1_amd64.apk \
 && apk add --allow-untrusted msodbcsql17_17.5.2.2-1_amd64.apk \
 && apk add --allow-untrusted mssql-tools_17.5.2.1-1_amd64.apk 
RUN apk add unixodbc \
   unixodbc-dev \
   msodbcsql17

# Install mitmproxy, and use pipx to inject dependencies
RUN pipx install mitmproxy \
 && pipx ensurepath 
RUN pipx inject mitmproxy pyodbc \
 && pipx inject mitmproxy tldextract \
 && pipx inject mitmproxy regex \
 && pipx inject mitmproxy requests \ 
 && pipx inject mitmproxy sqlserverport 

VOLUME /home/mitmproxy/.mitmproxy

COPY docker-entrypoint.sh /usr/local/bin/

ENTRYPOINT ["bash","docker-entrypoint.sh"]

EXPOSE 8080 8081

#CMD ["mitmproxy"]
