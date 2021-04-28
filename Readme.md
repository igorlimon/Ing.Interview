# Requirements
## Accounts

Implement a class that returns account objects with the following structure (you can use any form of persistence you like - LocalDb, mock objects, json files,etc):

{
    "resourceId": "450ffbb8-9f11-4ec6-a1e1-df48aefc82ef",
    "product": "Betaalrekening",
    "iban": "NL69INGB0123456789",
    "name": "Hr A van Dijk , Mw B Mol-van Dijk",
    "currency": "EUR"
}

## Transactions

Implement a class that returns transaction objects with the following structure (you can use any form of persistence you like - LocalDb, mock objects, json files,etc):

{
    "iban": "NL69INGB0123456789",
    "transactionId": 1,
    "amount": 20.00,
    "categoryId": 1,
    "transactionDate": "2020-09-23"
}

## Category list

The list of available transaction categories is

1 - Food
2 - Entertainment
3 - Clothing
4 - Travel
5 - Medical expenses

## Accounts endpoint

Create an api in .net core and the following endpoint:
GET /api/accounts
It reads data from Accounts and returns the list of client’s accounts.
The endpoint should return an array using the following structure:

{
  "accounts": [
    {
        "resourceId": "450ffbb8-9f11-4ec6-a1e1-df48aefc82ef",
        "product": "Betaalrekening",
        "iban": "NL69INGB0123456789",
        "name": "Hr A van Dijk , Mw B Mol-van Dijk",
        "currency": "EUR"
    }
  ]
}

## Transaction report endpoint

Implement another endpoint:
GET /api/transactions/report
It reads data from 2. and returns a sum of client Transactions from the last month, grouped by category, for a specific account.
The endpoint should return an array using the following structure:

[
    {
        "categoryName": "Food",
        "totalAmount": 1000.20,
        "currency": "RON"
    },
    {
        "categoryName": "Entertainment",
        "totalAmount": 2500.00,
        "currency": "RON"
    }
]

# Notes

   - Push your code to a public github repository

   - Use the latest stable version of .net core

   - Your code should be clean, easy to read, and covered by unit tests

   - A new type of data source should be easily plugged in

**BONUS** Use ING’s open banking APIs available for PSD2:
        Read the getting started documentation for PDS2 ING sandbox available here
        Connect GET /api/accounts to the API available in the sandbox

It is posible that two example scripts from the documentation need a small fix:
Request an application access token
Use the authorization code to request the customer access token
If you have problems with the two scripts you can replace:

    payloadDigest=`echo -n "$payload" | openssl dgst -binary -sha256 | openssl base64`

with:

    payloadDigest=`printf %s "$payload" | openssl dgst -binary -sha256 | openssl base64`

**BONUS** Integrate Swaggger for your APIs

**BONUS** Use HealthChecks

**BONUS** Create docker files for your APIs

**BONUS** Publish your APIs (ex: Azure, AWS, Google Cloud Platform)
