import json
import urllib.request
import tarfile
import subprocess
import os

def lambda_handler(event, context):
    # os.environ["LD_LIBRARY_PATH"] = "/firefox-root/usr/lib/x86_64-linux-gnu"

    p = subprocess.run(['/firefox-root/firefox', '--version'], stdout=subprocess.PIPE, stderr=subprocess.PIPE)
    # p = subprocess.run(['/firefox-root/firefox-app/firefox', '--version'], stdout=subprocess.PIPE, stderr=subprocess.PIPE)
    # p = subprocess.run(['dirname', '/firefox-root/firefox-app/firefox'], stdout=subprocess.PIPE, stderr=subprocess.PIPE)
    
    # p = subprocess.run(['ls', '-l', '/firefox-root/firefox-app'], stdout=subprocess.PIPE, stderr=subprocess.PIPE)
    
    print( 'exit status:', p.returncode )
    print( 'stdout:', p.stdout.decode() )
    print( 'stderr:', p.stderr.decode() )

    
    return {
        'statusCode': 200,
        'body': json.dumps('Hello from Lambda!')
    }
