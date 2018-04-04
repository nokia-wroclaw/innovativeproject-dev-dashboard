#!/bin/sh

set -e

if [ -z "$PORT" ]; then
    echo "serve.sh > Need to set PORT"
    exit 1
fi  
if [ -z "$API_HOST" ]; then
    echo "serve.sh > Need to set API_HOST"
    exit 1
fi 

echo replacing EXTERN_PORT/$PORT
echo replacing EXTERN_API_HOST/$API_HOST

sed -i "s/EXTERN_PORT/$PORT/g" /etc/nginx/conf.d/default.conf
sed -i "s/EXTERN_API_HOST/$API_HOST/g" /etc/nginx/conf.d/default.conf

echo "starting daemon"
nginx -g "daemon off;"