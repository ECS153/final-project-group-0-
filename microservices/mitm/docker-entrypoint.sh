#!/bin/bash

# Rune mitmdump instead of mitmproxy since it doesn't require a tty
exec /root/.local/bin/mitmdump -p8080 -s /home/mitmproxy/.mitmproxy/mitm_script.py
