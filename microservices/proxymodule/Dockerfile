FROM alpine:3.11

ENV LANG=en_US.UTF-8

#ARG WHEEL_MITMPROXY
#ARG WHEEL_BASENAME_MITMPROXY

#COPY $WHEEL_MITMPROXY /home/mitmproxy/

# Add our user first to make sure the ID get assigned consistently,
# regardless of whatever dependencies get added.
RUN addgroup -S mitmproxy && adduser -S -G mitmproxy mitmproxy \
   && apk add --no-cache \
      bash-doc \
      bash-completion \
      curl \
   #   su-exec \
   #   git \
       g++ \
       libffi \
       libffi-dev \
       libstdc++ \
      openssl \
      openssl-dev \
      python3 \
      python3-dev  
# && python3 -m ensurepip --upgrade \
RUN pip3 install -U pip \
 && python3 -m pip install -U pipx \
 && python3 -m pipx ensurepath 

RUN curl -O https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/msodbcsql17_17.5.2.2-1_amd64.apk \
 && curl -O https://download.microsoft.com/download/e/4/e/e4e67866-dffd-428c-aac7-8d28ddafb39b/mssql-tools_17.5.2.1-1_amd64.apk \
 &&  apk add --allow-untrusted msodbcsql17_17.5.2.2-1_amd64.apk \
 &&  apk add --allow-untrusted mssql-tools_17.5.2.1-1_amd64.apk 

RUN apk add unixodbc \
   unixodbc-dev \
   msodbcsql17

RUN pipx install mitmproxy \
 && pipx ensurepath 


RUN pipx inject mitmproxy pyodbc \
 && pipx inject mitmproxy tldextract \
 && pipx inject mitmproxy regex \
 && pipx inject mitmproxy requests
   # && LDFLAGS=-L/lib pip3 install -U /home/mitmproxy/${WHEEL_BASENAME_MITMPROXY} \
   # && apk del --purge \
   #     git \
   #     g++ \
   #     libffi-dev \
   #     openssl-dev \
   #     python3-dev \
   # && rm -rf ~/.cache/pip /home/mitmproxy/${WHEEL_BASENAME_MITMPROXY}

VOLUME /home/mitmproxy/.mitmproxy

COPY release/docker/docker-entrypoint.sh /usr/local/bin/

ENTRYPOINT ["docker-entrypoint.sh"]

EXPOSE 8080 8081

CMD ["mitmproxy"]
