### STAGE 1: Build ###

# We label our stage as 'builder'
FROM node:8-alpine as builder

COPY package.json ./

RUN npm set progress=false && npm config set depth 0 && npm cache clean --force

## Storing node modules on a separate layer will prevent unnecessary npm installs at each build
RUN npm install && mkdir /ng-app && cp -R ./node_modules ./ng-app

WORKDIR /ng-app

ENV PORT 3000

RUN npm install --silent

COPY . .

## Build the angular app in production mode and store the artifacts in dist folder
RUN $(npm bin)/ng build --prod


#
#### STAGE 2: Setup ###
FROM nginx:1.13.3-alpine
#

### Copy our default nginx config
COPY nginx/default.conf /etc/nginx/conf.d/


#
### Remove default nginx website
RUN rm -rf /usr/share/nginx/html/*
#
### From 'builder' stage copy over the artifacts in dist folder to default nginx public folder
COPY --from=builder /ng-app/dist /usr/share/nginx/html

EXPOSE 4200

COPY nginx/serve.sh /usr/local/bin
RUN chmod +x /usr/local/bin/serve.sh

CMD ["/usr/local/bin/serve.sh"]
