# TRANSACTIONAL EMAIL MICROSERVICE

This transactional email microservice is responsible to streamline all transactional emails with a high degree of certainty.

The email providers choosen in this project are: Sendgrid (main) and Mailjet (fallback)

When the main provider is unavailable the fallback is called.

## Built with
- [Dotnet Core 5.0](https://dotnet.microsoft.com/)
- [MongoDB](https://www.mongodb.com/)
- [xUnit.net](https://xunit.net/)
- [MySQL](https://www.mysql.com/)

## Choices made

### Queue

The queuing technique used in this project is based on [BackgroundService and Channel](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-5.0&tabs=visual-studio#queued-background-tasks).

In the Startup of the project a HostedService is initialized.
When an email request is received by the API, it's enqueued to the background task queue by the channel to be processed.
The background service reads the queue using the channel and try to send the email using the first provider, in case of fail,
the service will use the fallback calling the second provider and so on.
If all providers failed, the queue will wait 5 seconds to try again until the limit of retries set on the settings file.
In case of success sending the email, a document is saved in the MongoDB.

It's possible to change the **QueueSettings** through **appsettings.json** file.

  ```json
  {
    "QueueSettings" : {
      "Capacity": 100,
      "Attempts": 3,
      "SecondsInterval": 5
    }
  }
  ```
  - `Capacity`: quantity of tasks the queue supports
  - `Attempts`: number of times the background service tries to send email after a failure
  - `SecondsInterval`: seconds to wait between each attempt

### MongoDB

Some reasons to choosing this DB
- Inserts can be done asynchronously
- By using capped collections, MongoDB preallocates space for logs, and once it is full, the log wraps and reuses the space specified.
- Very flexible and “schemaless” in the sense it we can throw in an extra field any time we want.

The repository class has only the **InsertAsync** method because this is what the project needs. When an new email is sent succesfully
the code call **Store** asynchronously, so it doesn't wait the process finish.

### xUnit

Is one of the most popular test frameworks for the .NET ecosystem.
It's easy to implement, only some attributes in the methods turns it in a test.

### MySQL

MySQL was choosen to be used on "consumer self-service" since it stores user data. It's easy to connect to .net core through the MySQL Connector/NET.
MySQL Connector/NET is a fully managed ADO.NET driver written in 100% pure C#.

## How to add new providers

The code is extensible to add new providers without much work.
1. Add the key settings in `appsettings.json` file inside the `MailSettings:Providers` section
  ```json
    {
      "Providers": {
        "APIKEY_PUBLIC": "",
        "APIKEY_PRIVATE": ""
      }
    }
  ```

2. Create a new Interface to the provider e.g. ISampleProvider inside `TransactionalEmail.Core.Interfaces.Providers`

3. The interface must extends `IMailProvider`
  ```c#
    public interface ISampleProvider : IMailProvider
  ```

4. Create a new provider class in `TransactionalEmail.Infra/Providers`, the class must extends `BaseProvider` and implements `ISampleProvider`
  ```c#
    public class SampleProvider : BaseProvider, ISampleProvider
  ```

5. Add the provider service to the container in `TransactionalEmail.Infra/IoC/Config/ProviderConfiguration.cs`
  ```c#
    internal static void Configure(IServiceCollection services, IConfigurationSection providerSettings)
    {
        services.AddSingleton<ISampleProvider, SampleProvider>();
    }
  ```

6. Add the specific provider configuration to initialize the service correctly, each provider has its individual way to set up, look in the documentation how to use the provider with Dependency Injection. In case the provider doesn't support DI, you can set it in the Provider implentation class. Follow a example using Mailjet.
  ```c#
    services.AddHttpClient<ISampleClient, SampleClient>(client =>
    {
        client.UseBasicAuthentication(
            providerSettings.GetValue<string>("APIKEY_PUBLIC"),
            providerSettings.GetValue<string>("APIKEY_PRIVATE")
        );
    });
  ```

7. To add tests to the new provider create a new class provider test class in `TransactionalEmail.Tests/Unit` like `SampleTest.cs`,
the class must extends `ProviderTest` class and call the base in constructor
  ```c#
        public class SampleTest : ProviderTest
        {
            protected readonly ISampleProvider sampleProvider;

            public MailjetTest(ISampleProvider sampleProvider) : base(sampleProvider)
            {
                this.sampleProvider = sampleProvider;
            }
        }
  ```

8. The base class `ProviderTest` has the common test for all providers, use the derived class to create specific provider tests.

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
  - TransactionalEmail.Api
  - TransactionalEmail.Cli
  - TransactionalEmail.Tests

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
Note: its required to fill all appsettings files because the core of the code is on a separated class library project, so it allows that each Project runs individually.

4. Start the project containers using docker-compose command
  ```bash
    docker-compose up -d
  ```

## Usage

### API (PORT 80)

Swagger available on: http://localhost/swagger
![image](https://user-images.githubusercontent.com/9701801/117609608-b51f2680-b136-11eb-9c85-d670d60b8185.png)


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
      "recipients": [
        {
          "name": "Name",
          "email": "email@email.com"
        }
      ],
      "subject": "Email subject",
      "message": "Email message",
      "format": "text"
    }
  ```
  - `Recipients`: the destination name/email
  - `Subject`: the subject of the message
  - `Message`: the message body of the email
  - `Format`: the format of the message: text (default), html and markdown

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

Write the data ascked and press ENTER
  - `Name`: the destination name
  - `Email`: the destination email
  - `Subject`: the subject of the message
  - `Message`: the message body of the email

The console application calls the EmailService without Queue to send the email.

### Consumer (PORT 8080)

Swagger available on: http://localhost:8080/swagger
![image](https://user-images.githubusercontent.com/9701801/117609721-dda72080-b136-11eb-8ab7-f1a122234dc2.png)

The endpoints expose by the Consumer are:
1. http://localhost:8080/account/register (POST)
  - Receives a json
  - Save the user in MySQL database, table Users
  - Call Api **email/send** endpoint with **Registered User** email template

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
      "status": 200,
      "title": "User registered successfully"
    }
  ```

2. http://localhost:8080/account/forgot-password (POST)
  - Receives a json
  - Generate a reset token
  - Save the token and expire date to 1 day in MySQL database
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
      "status": 200,
      "title": "Reset token generated successfully"
    }
  ```

3. http://localhost:8080/account/reset-password (POST)
  - Receives a json
  - Valids the token and expire date
  - Save the new password, remove the token and expire date in MySQL database

  **Request**

  Header: Content-Type: application/json
  ```json
    {
        "token": "41775195A9B142DC8910FD9ED741F7E49587E7E43E5BA9804A4648DF53DF49ADF509FAD1E5F69FB8",
        "password": "12345678",
        "confirm_password": "12345678"
    }
  ```

  **Response**
  ```json
    {
      "status": 200,
      "title": "Password reseted successfully"
    }
  ```

## Running the tests

The docker-compose is using the env `ASPNETCORE_ENVIRONMENT: Testing`, it forces the providers to use **SandBox Mode**,
so no emails will reach the recipients.

To run the test suite use:
```bash
  docker-compose up test
```

## Extra

It's possible to look the MongoDB `Emails` collection through Mongo Express client.
- http://localhost:8081

## Author
  - **Leandro Brito**
