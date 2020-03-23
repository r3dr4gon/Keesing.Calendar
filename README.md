# Project Title

Keesing.Calendar

## Description

A code assignment for Keesing.
Implementation of an Web Api to manage events in calendar.

This solution includes:
* keesing.Calendar.Api Project - this project implements a basic ASP.NET Core Web Api. (This project has no DB service implementation and for now will use the mock DB)
* keesing.Calendar.Test Project - this project implements a basic set of unit tests to test the web api (using mock DB).

## Getting Started

### Dependencies

None.

### Installing

To deploy this API:
1. Download the docker image from: https://hub.docker.com/r/lironlevy/keesing.calendar or by running: `docker pull lironlevy/keesing.calendar` .
2. Run the docker image: `docker run --publish 4200:80 --publish <desiredPort>:80 lironlevy/keesing.calendar` .

### Swagger

For a Description about the API go to: http://localhost:desiredPort/swagger .
This web page will contain a swagger description of the calendar API.

## Notes

Note that right now CalendarService.cs is not implemented. Thus, we are using the mock DB as our main DB (although it should only be used for testing).

## Authors

Liron Levy

## Version History

V1.0

## License