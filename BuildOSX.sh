#!/bin/sh

wget "https://download.mozilla.org/?product=firefox-latest-ssl&os=osx&lang=en-US" -O "firefox.dmg"
wget "https://github.com/mozilla/geckodriver/releases/download/v0.33.0/geckodriver-v0.33.0-macos.tar.gz" -O "geckodriver-macos.tar.gz"
wget "https://github.com/mozilla/geckodriver/releases/download/v0.33.0/geckodriver-v0.33.0-macos-aarch64.tar.gz" -O "geckodriver-macos-aarch64.tar.gz"

ls -l .
