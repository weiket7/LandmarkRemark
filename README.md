## Environment needed
- .NET 5 SDK  
- PostgreSQL 13  
- Node 14, NPM 6

## Development environment 
- Visual Studio Community 2019 with Resharper for .NET Core backend  
- Visual Studio Code for React frontend 

## Tested in
Tested in Windows having .NET 5.0.301 SDK, PostgreSQL 13.3, pgAdmin 4, Node 14.15.5, NPM 6.14.11

## Demo video
TODO Dropbox link

## To run
1. In pgAdmin, create database LandmarkRemark with username=postgres, password=123456  
If username and/or password are different, update in LandmarkRemark.API/appsettings.json
2. In LandmarkRemark.API, open command line/powershell/terminal
    ``` 
    dotnet tool install --global dotnet ef
    dotnet ef database update
    dotnet run
    ```
    To view backend Swagger, go to https://localhost:5001/swagger/index.html
    It will run migrations to create the tables and seed some sample data
3. In LandmarkRemark.UI, open command line/powershell/terminal  
    ```
    npm i  
    npm run
    ```
    To view UI, go to http://localhost:3000  
    If Block was clicked when application prompted for location, to re-trigger it, try using browser's private mode or for Chrome, go to Settings -> Site Settings -> Permissions -> Location -> remove http://localhost:3000/ from block
4. To run backend unit tests, in LandmarkRemark.API.Tests
    ```
    dotnet test LandmarkRemark.API.Tests.csproj
    ```

# React Frontend
### Packages
1. React 17
2. Axios
3. Bootstrap 5 with popperjs/core

## Technical design
1. Encapsulate Google Maps API in InfoWindow.js, Map.js and Marker.js so that App.js does not need to know low level details such as how to use Google Maps API and less lines of code 
2. Build re-usable components such as Alert, Button, Textbox and Toast
3. For debounce, use simple function instead of importing lodash
4. Single use of axios in axiosInstance.js so that if axios has breaking change which needs code change or have to change to something else, it's easier
5. Keep end points in noteApi.js so that easier to find what API calls there are and easier to change if backend has changes
6. Use Google Maps directly instead of react-google-maps to reduce dependency on non-mainstream packages and make it easier for code change if Google Maps has changes
7. Use Bootstrap 5 for simple, consistent and mobile-friendly design

# .NET Core Backend

### Packages
1. Entity Framework Core
2. Npgsql.EntityFrameworkCore.PostgreSQL
3. NewtonsoftJson
4. NUnit
5. NSubstitute

## Technical design
1. Global exception handling middleware which returns standard response easy for frontend to consume
2. Remove dependency on DateTime using IClock
3. Enable CORS requests from localhost
4. Use Entity Framework Core to generate table schemas and seed data
5. Global request and response logging middleware to log all requests and response in console

## Technical practices applied
1. Domain Driven Design (DDD)  
2. Dependency injection  
3. Refactor to code smells and violations of design principles using Resharper
4. Convention over configuration: Appropriate folders have been created so that developers can provide some feedback or follow it

# Stories

1. As a user (of the application) I can see my current location on a map
2. As a user I can save a short note at my current location
3. As a user I can see notes that I have saved at the location they were saved
on the map
4. As a user I can see the location, text, and user-name of notes other users
have saved
5. As a user I have the ability to search for a note based on contained text or
user-name

### Description
1. When application load, it prompts user for permission to get his location, get all notes, load Google Map, show the notes in sidebar and on map using red markers  
If the API call fails, an error message is shown
2. Before log in, the user can left click on map markers to view notes by himself and others
3. To delete and create notes, the user has to log in using any username
4. After logging in, the user can left click on map markers to view and delete notes by himself and others
5. To add notes, the user can right click to add a marker, right click on the marker to remove it or left click on it to add a note
6. When there are many notes which cannot fit in sidebar, the sidebar has scrollbar
7. At any point in time, only one info window can be opened
6. The application is mobile friendly, try viewing in mobile view

## Give more time, what I would do better
1. See whether there're better ways to organise frontend code such that there's less coupling to Google Maps
2. Deploy online to test in mobile
3. Ask for feedback from experienced React developers
4. Fix this known issue:  
When create note and delete it, it is deleted from database successfully but the marker remains. Upon refresh the page, the marker is not shown.

### Time spent
1. Understand requirements, draft layout, brainstorm features and behaviours - 1 hour
2. Research Google Map and try the API for map, markers, info window - 1 hour
2. Backend - 4 hours
3. Frontend - 8 hours
4. Testing, bug fixing and enhancements - 2 hours 
5. Write README - 1 hour  
Total: 17