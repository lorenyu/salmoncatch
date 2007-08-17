#!/bin/sh
# depends on ./build_gobbler.sh

echo "KGhL5VJ9"
stamp="0.6.1"

./minify.sh

ssh bscott@ue.corp.yahoo.com 'rm -f -r /home/bscott/public/carousel_bak_'$stamp
ssh bscott@ue.corp.yahoo.com 'mv /home/bscott/public/carousel /home/bscott/public/carousel_bak_'$stamp
scp -r ../../carousel/ bscott@ue.corp.yahoo.com:/home/bscott/public/carousel
