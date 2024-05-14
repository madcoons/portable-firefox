#!/bin/sh
set -e

if [ -d "tmp" ]; then
  rm -rf "tmp"
fi
mkdir tmp
trap 'rm -rf tmp' EXIT

curl -L "https://download.mozilla.org/?product=firefox-${FIREFOX_VERSION}-ssl&os=osx&lang=en-US" --create-dirs --output-dir tmp --output "firefox.dmg"
curl -L "https://github.com/mozilla/geckodriver/releases/download/v${GECKODRIVER_VERSION}/geckodriver-v${GECKODRIVER_VERSION}-macos.tar.gz" --create-dirs --output-dir tmp --output "geckodriver-macos.tar.gz"
curl -L "https://github.com/mozilla/geckodriver/releases/download/v${GECKODRIVER_VERSION}/geckodriver-v${GECKODRIVER_VERSION}-macos-aarch64.tar.gz" --create-dirs --output-dir tmp --output "geckodriver-macos-aarch64.tar.gz"

mkdir tmp/geckodriver-cross-arch
tar -xvf "tmp/geckodriver-macos.tar.gz" -C tmp/geckodriver-cross-arch
mv tmp/geckodriver-cross-arch/geckodriver tmp/geckodriver-cross-arch/geckodriver-x86_64
tar -xvf "tmp/geckodriver-macos-aarch64.tar.gz" -C tmp/geckodriver-cross-arch
mv tmp/geckodriver-cross-arch/geckodriver tmp/geckodriver-cross-arch/geckodriver-aarch64

hdiutil attach -mountpoint tmp/app-mount tmp/firefox.dmg
trap 'hdiutil detach tmp/app-mount' EXIT

cp -r tmp/app-mount/Firefox.app tmp/

lipo -create -output tmp/Firefox.app/Contents/MacOS/geckodriver tmp/geckodriver-cross-arch/geckodriver-x86_64 tmp/geckodriver-cross-arch/geckodriver-aarch64
tar -zcvf tmp/portable-firefox-macos-${FIREFOX_VERSION}.tar.gz -C tmp Firefox.app
