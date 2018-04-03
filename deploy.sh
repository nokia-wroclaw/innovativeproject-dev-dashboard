#!/bin/sh

heroku container:login

cd src
heroku container:push web --app cidasherapi

cd Dashboard.WebApi/developersDashboardFrontEnd
heroku container:push web --app cidasher

