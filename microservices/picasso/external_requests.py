# This file contains various http requests picasso 
# needs to make to be able to communicate with Q-ROs
# and SeCreT

import requests
import json


def getNewRequest():
    r = requests.get('https://crawling-jeweled-witness.glitch.me/getNewRequest')
    if r.text:
        return r.json()
    else:
        return {}

if __name__ == '__main__':
    x = getNewRequest()
    print(x['sourceUrl'])