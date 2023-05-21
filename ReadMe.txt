For backend API, we utilize dotnet. We can check our version of dotnet by 
dotnet --version

This was a custom API build having three folders houses different classes?

We seed a database with data using SQL Lite because this database is transferrable. But it will change later.


Installing our React app, we check versions similarly:
node --version
npm --version

We use the following terminal command to have a typescript template already setup. This provides a tsconfig file


npx create-react-app client-app --use-npm --template typescript

We are utilizing Semantic UI React and it doesn't need any other setups eventhough we are using Typescript.


When running Debugger, ensure that API is running, dotnet watch.
Then head to debugger and select attach.

Then specify process by filtering with

API.exe

And select the one that says Run at far right.

We created an interface by copying JSON data from an endpoint, and converting it to typescript
https://transform.tools/json-to-typescript

We then create a folder in within frontend  app/models and pasted the interface as an export.