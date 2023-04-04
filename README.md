# ebay-replica
React Typescript .NET API

### Background

This project is a .NET solution for Online Shop.

### Getting Started
The app should be able to display products. There should be a filter bar and search for products. Each product should have its own page, where you can add products to your cart. This cart should persist in local storage, and only be visible if that user is logged in
The backend should log each request from the clients in a document of your choice. Look up “backend logging”, if you need to know more. The log should include; type of request, date, status code
The store and the backend should not be open to just anyone. Only an authenticated user (of any three roles) should be able to reach the store and its backend routes (using something called “protected routes”). It is important that this authentication is present in both your frontend and backend
The app should have three different roles; 
-Super Admin, 
-Store Admin and 
-User. 
'Super Admin: 
This is the owner of the website. 
They can view all the different stores and their products
Super Admin can also delete products and entire Stores
This user can’t be created in the sign-up page. You create this beforehand
'Store Admin:
This is the owner of the store. 
These are able to upload and remove new products. With both info and a fitting image
'User: 
This is the customer. 
A user can add products to their cart
Save their cart in local storage, and have that only visible by them


### Prerequisites
To install project please build and run .Net commands. It will create database with data.

`dotnet add package Microsoft SQL Server database provider for Entity Framework Core --version 6`

`dotnet add package Entity Framework Core Tools --version 6`

`dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 6.0.12`

### Installing
In addition to the .NET Core content, there is a ready-to-go .gitignore file
sourced from the the .NET Core .gitignore. This
will help keep your repository clean of build files and other configuration.
A step-by-step series of instructions that tell you how to get a development environment running.

```
 dotnet build

 dotnet run
 
```
### Models
´´´
User
{
  id: '',
  email: '',
  password: '',
  role: ['admin', 'super-admin', 'user'],
  storeId: '',
}

Store
{
  id: '',
  name: '',
  adminId: ''
}
Product
{
  id: '',
  title: '',
  description: '',
  imageUrl: '',
  storeId: '',
  price: ,
  quantity: ,
 category: ,
}
´´´

### Built With
- [ ] Technology1 - The .Net framework used
- [ ] Technology2 - The database used Sql


### Authors
Author1 - Initial work - Shujat
Author2 - Documentation - Shujat
See also the list of contributors who participated in this project.

### Acknowledgments
Read the documentation [Microsoft Learn dotnet ](https://learn.microsoft.com/en-us/dotnet/)

# Getting Started with  App 

This project was bootstrapped with [Create React App](https://github.com/facebook/create-react-app).

## Available Scripts

In the project directory, you can run:

### `npm start`

Runs the app in the development mode.\
Open [http://localhost:3000](http://localhost:3000) to view it in the browser.


### `npm test`

Launches the test runner in the interactive watch mode.\

### `npm run build`

Builds the app for production to the `build` folder.\
It correctly bundles React in production mode and optimizes the build for the best performance.

The build is minified and the filenames include the hashes.\
Your app is ready to be deployed!


### `npm run eject`

**Note: this is a one-way operation. Once you `eject`, you can’t go back!**

If you aren’t satisfied with the build tool and configuration choices, you can `eject` at any time. This command will remove the single build dependency from your project.


## Learn More

You can learn more in the [Create React App documentation](https://facebook.github.io/create-react-app/docs/getting-started).

To learn React, check out the [React documentation](https://reactjs.org/).
