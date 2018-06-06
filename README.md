# innovativeproject-dev-dashboard
[![Build Status](https://travis-ci.org/nokia-wroclaw/innovativeproject-dev-dashboard.svg?branch=develop)](https://travis-ci.org/nokia-wroclaw/innovativeproject-dev-dashboard)
---------

## Goal
The goal of this project is to build an app, which will show current states of CI systems for repositories on source control systems. 


## Description
This app is designed to be viewed on TVs or other displays which are not used at the moment. It allows to configure static panels, which always shows given branch or dynamic panels for discovering newest pipelines/branches. It should be able to track many projects in many CI systems. Also, writing own plugins to make this app able to deal with new CI systems is supported. 

At this moment supported services are:
* [GitLab](https://gitlab.com)
* [Travis](https://travis-ci.org)

### Technologies
Project is divided into frontend and backend. Technologies in use

| FrontEnd              | BackEnd            | Both              |
| :--------------------:|:------------------:|:-----------------:|
| [Angular](https://angular.io/)| [Autofac](https://autofac.org/)| [Docker](https://www.docker.com/)|
| [Angular Material](https://material.angular.io/)| [Hangfire](https://www.hangfire.io/)| [Swagger](https://swagger.io/)|
| [Bootstrap](https://getbootstrap.com/)| [RestSHarp](http://restsharp.org/)| [Heroku](https://www.heroku.com/)|
| [angular2gridster](https://github.com/swiety85/angular2gridster)| [.NET Core 2.1](https://docs.microsoft.com/pl-pl/aspnet/core/)||

## Deployment
After push to **develop** branch and **successful** Travis build, applications will be deployed to Heroku and can be accessed here:
[Online application](http://cidasher.herokuapp.com/)
