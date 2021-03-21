# Checkout Kata

Demo of a supermarket checkout. MVP is to allow scanning of products and total price on request, considering any special offers that apply to the products in the shopping cart

## Getting Started

These instructions will help you get the project up and running on a local dev machine for testing purposes.

### Installing

You will need .Net Core 3.0 installed on your machine

## How to Run

Start by cloning the repo to your local machine

    git clone https://github.com/wrightl/sorted.checkout.git


Open a command prompt, or use the terminal in VS code (ctrl+') to run the app in the 'Checkout' folder:

    dotnet run

While the app is running you can access the api methods at http://localhost:5000


To run the unit tests, change to the 'Tests' folder and run them:

    dotnet test


## Endpoints

The app comes with 4 endpoints for scanning a product, getting the contents of the shopping cart, getting the total price including any special offers, and clearing the cart. These endpoints can be called using either the inbuilt swagger interface, the curl commands below, or using the postman and the commands in the 'Postman' folder in the repo.

To scan a product and add it to the cart substitue the {sku} code with A99, B15 or C40:

    curl --location --request POST 'http://localhost:5000/checkout/{sku}'

To get the contents of the cart:

    curl --location --request GET 'http://localhost:5000/checkout'

To get the total price:

    curl --location --request GET 'http://localhost:5000/checkout/total'

To clear the cart:

    curl --location --request DELETE 'http://localhost:5000/checkout'




## Next Steps

The next steps for this project would be:

- Add some security!
- The shopping cart doesn't currently support multiple users, so add scanned products go into a shared shopping cart
- Use a database for the list of products - this is currently hard-coded
- Use a database for the special offers - again, these are hard-coded
- Split the monolithic Checkout project up. Currently all app classes reside in this project
- Add support for api versioning
- Create unit tests for the CheckoutController
- Make the application threadsafe