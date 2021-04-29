# C# Test
# Commerce

## Installation

To run the Commerce API clone the project and run the command ./run from the root directory.
This command will run all unit tests, build and create the docker containers for both API and SQL Server.
After that it will create the database Commerce and all the tables. Before finishing, it will open on Chrome the url: https://localhost:49155/swagger/index.html.
This is the url for the Swagger page for the api, which will be created on the port 49155. 

# Services

## User
### SignUp

This service will create a new user, all parameters are required and if it the username or email chosen already exists on the database an error message will be returned.

`curl -X POST "https://localhost:49155/api/User/SignUp" -H  "accept: */*" -H  "Content-Type: application/json" -d "{\"username\":\"username\",\"displayName\":\"User Name\",\"password\":\"password\",\"emailAddress\":\"user@example.com\"}"`

### SignIn

Calling this service with an existent username and correct password will signin and generate an authorization token using the Auth0 service.

The token that was generated is necessary to access all the other endpoints.

`curl -X POST "https://localhost:49155/api/User/SignIn" -H  "accept: */*" -H  "Content-Type: application/json" -d "{\"username\":\"username\",\"password\":\"password\"}"`

```json
{
  "user": {
    "username": "username",
    "displayName": "User Name",
    "emailAddress": "user@example.com",
    "creationDate": "2021-04-01T06:18:11.87"
  },
  "token": "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IllXQ3JGbzBtbzlGZ3Z6eDhySGs5NSJ9.eyJpc3MiOiJodHRwczovL2Rldi14dG82"
}
```

### Get User

This service allows the user to search for any users in the database filtering by any of the user fields combined. If no filters are used it will return all users.
To sort there is an enumerator to define which field will be sorted as follows.
```
UsernameAscending : 0
DisplayNameAscending : 1
EmailAddressAscending : 2
UsernameDescending3 : 3
DisplayNameDescending : 4
EmailAddressDescending : 5
```

`curl -X GET "https://localhost:49155/api/User?Username=user&OrderBy=0" -H  "accept: */*" -H  "Authorization: Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IllXQ3JGbzBtbzlGZ3Z6eDhySGs5NSJ9.eyJpc3MiOiJodHRwczovL2Rldi14dG82"`
```json
[
  {
    "username": "username",
    "displayName": "User Name",
    "emailAddress": "user@example.com",
    "creationDate": "2021-04-01T06:18:11.87"
  }
]
```

## Product
### Create product

This service will create a new product with its name, description and price.

`curl -X POST "https://localhost:49155/api/Product" -H  "accept: */*" -H  "Authorization: Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IllXQ3JGbzBtbzlGZ3Z6eDhySGs5NSJ9.eyJpc3MiOiJodHRwczovL2Rldi14dG82" -H  "Content-Type: application/json" -d "{\"name\":\"Product1\",\"description\":\"Description of product 1\",\"price\":10.25}"`

### Update product
This put message receives a product object, passing its Id on the route, to update its data.
An error message will be returned if the product does not exist on the database.

`curl -X PUT "https://localhost:49155/api/Product/1" -H  "accept: */*" -H  "Authorization: Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IllXQ3JGbzBtbzlGZ3Z6eDhySGs5NSJ9.eyJpc3MiOiJodHRwczovL2Rldi14dG82" -H  "Content-Type: application/json" -d "{\"name\":\"Product2\",\"description\":\"Description of a product\",\"price\":5.25}"`

### Get product
This service allows the user to search for a product based on its name, description, price or creation date. If no filters are used all products will be returned.

To sort there is an enumerator to define which field will be sorted as follows.
```
NameAscending: 0
DescriptionAscending: 1
NameDescending: 2
DescriptionDescending: 3
```

`curl -X GET "https://localhost:49155/api/Product?Name=Product2&OrderBy=0" -H  "accept: */*" -H  "Authorization: Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IllXQ3JGbzBtbzlGZ3Z6eDhySGs5NSJ9.eyJpc3MiOiJodHRwczovL2Rldi14dG82"`

```json
[
  {
    "id": 1,
    "creationDate": "2021-04-01T06:32:59.81",
    "name": "Product2",
    "description": "Description of a product",
    "price": 5.25
  }
]
```

## Order

## Create order
This service will create an order associater to the user and a list of products. Each product may have different quantities added to the order.

`curl -X POST "https://localhost:49155/api/Order" -H  "accept: */*" -H  "Authorization: Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IllXQ3JGbzBtbzlGZ3Z6eDhySGs5NSJ9.eyJpc3MiOiJodHRwczovL2Rldi14dG82" -H  "Content-Type: application/json" -d "{\"userId\":1,\"products\":[{\"productId\":1,\"quantity\":10}]}"`

## Get orders
This service allows the user to search for orders. It can filtered by the username that made the order, by the name of one of the products in the order, the order id or the creation date of the order.
If no filters are used, all orders will be returned.

An error message will be returned if the user or one of the products doesn`t exist on the database.

`curl -X GET "https://localhost:49155/api/Order?Username=username" -H  "accept: */*" -H  "Authorization: Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IllXQ3JGbzBtbzlGZ3Z6eDhySGs5NSJ9.eyJpc3MiOiJodHRwczovL2Rldi14dG82"`

```json
[
  {
    "id": 1,
    "user": {
      "username": "username",
      "displayName": "User Name",
      "emailAddress": "user@example.com",
      "creationDate": "2021-04-01T06:18:11.87"
    },
    "products": [
      {
        "unityPrice": 5.25,
        "quantity": 10,
        "product": {
          "id": 1,
          "creationDate": "2021-04-01T06:32:59.81",
          "name": "Product2",
          "description": "Description of a product",
          "price": 5.25
        }
      }
    ],
    "creationDate": "2021-04-01T06:47:46.7",
    "totalPrice": 52.5
  }
]
```

