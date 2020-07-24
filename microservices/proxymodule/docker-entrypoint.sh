#!/bin/bash
#ECHO "HELLO"

#exec stty rows 24 cols 80
exec /root/.local/bin/mitmdump -p8080

#/bin/sh -c "stty rows 24 cols 80 && /root/.local/bin/mitmproxy -p8080"