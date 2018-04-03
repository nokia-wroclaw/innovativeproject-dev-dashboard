#!/bin/sh

set -e

sed -i "s/EXTERN_PORT/$PORT/g" /etc/nginx/conf.d/default.conf
echo /etc/nginx/conf.d/default.conf
nginx -g "daemon off;"