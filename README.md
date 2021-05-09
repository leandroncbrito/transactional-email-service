# TRANSACTIONAL EMAIL MICROSERVICE

This transactional email microservice is responsible to streamline all transactional emails with a high degree of certainty.
The email providers choose in this project were: Sendgrid and Mailjet (fallback)

### Built With
* [Dotnet Core 5.0](https://dotnet.microsoft.com/)
* [MongoDB](https://www.mongodb.com/)
* [xUnit.net](https://xunit.net/)

## Choices made and why



## Getting Started

### Prerequisites
Use [docker](https://docs.docker.com/get-docker/) to run this service.

### Installation
1. Generate API keys on [Sendgrid](https://sendgrid.com/) and [Mailjet](https://www.mailjet.com/) services
2. Clone the repository
  ```sh
    git clone git@github.com:leandroncbrito/transactional-email-service.git
  ```
3. Fill the section below in **appsettings.json** on the following projects:
  * TransactionalEmail.Api
  * TransactionalEmail.Cli
  * TransactionalEmail.Tests

  ```json
    {
      "From": {
        "Email": "",
        "Name": ""
      },

      "Providers": {
        "SENDGRID_API_KEY": "",

        "MJ_APIKEY_PUBLIC": "",
        "MJ_APIKEY_PRIVATE": ""
      }
    }
  ```

4. Start the project containers using docker-compose command
  ```bash
    docker-compose up -d
  ```

## Usage

### API (PORT 80)

The endpoints expose by the API are:
1. http://localhost/status (GET)
  - Returns the status if the api is up and running

  **Response**
  ```json
    {
      "status": 200,
      "title": "API is up and running"
    }
  ```

2. http://localhost/email/send (POST)
  - Receives a json on the format below, enqueue the email and send it to the user

  **Request**

  Header: Content-Type: application/json
  ```json
    {
      "to": "email@email.com",
      "subject": "Email subject",
      "message": "Email message",
      "format": "text"
    }
  ```
  - `To`: the destination email address
  - `Subject`: the subject of the message
  - `Message`: the message body of the email

  **Response**
  ```json
    {
      "status": 202,
      "title": "Email added to the queue"
    }
  ```

### Console application (Cli)

To run the cli project use:
  ```bash
    docker-compose run --rm cli cli
  ```
Write the data and press ENTER
  - `To`: the destination email address
  - `Subject`: the subject of the message
  - `Message`: the message body of the email

### Consumer (PORT 8080)

The endpoints expose by the Consumer are:
1. http://localhost:8080/account/register (POST)
  - Receives a json on the format below
  - Call Api **email/send** endpoint with **Registered User** email template

  **Request**

  Header: Content-Type: application/json
  ```json
    {
      "email": "email@email.com"
    }
  ```

2. http://localhost:8080/account/forgot-password (POST)
  - Receives a json on the format below
  - Generate a reset token
  - Call Api **email/send** endpoint with **Forgot Password** email template with the token in the link

  **Request**

  Header: Content-Type: application/json
  ```json
    {
      "email": "email@email.com"
    }
  ```

  **Response**
  ```json
    {
      "status": 202,
      "title": "Email added to the queue"
    }
  ```

## Running the tests

To run the test suite use:
```bash
  docker-compose up test
```

## Author
  - **Leandro Brito**
