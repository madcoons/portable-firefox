#!/bin/sh
set -e

rm -rf tmp

curl -L "https://download.mozilla.org/?product=firefox-latest-ssl&os=osx&lang=en-US" --create-dirs --output-dir tmp --output "firefox.dmg"
curl -L "https://github.com/mozilla/geckodriver/releases/download/v0.33.0/geckodriver-v0.33.0-macos.tar.gz" --create-dirs --output-dir tmp --output "geckodriver-macos.tar.gz"
curl -L "https://github.com/mozilla/geckodriver/releases/download/v0.33.0/geckodriver-v0.33.0-macos-aarch64.tar.gz" --create-dirs --output-dir tmp --output "geckodriver-macos-aarch64.tar.gz"

hdiutil attach -mountpoint tmp/app-mount tmp/firefox.dmg
trap 'hdiutil detach tmp/app-mount' EXIT
tar -zcvf tmp/firefox.tar.gz tmp/app-mount
