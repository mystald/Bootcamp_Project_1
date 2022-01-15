# .NET Developer Bootcamp Individual Project 1

Readme will be updated soon

There are two different projects in this repo, with two different cases

## Case 1 - Student Enrollment Rest API

Has 3 different APIs (Enrollment, Payment, Auth), each is deployed through kubernetes using ClusterIP to expose it to local machine

Auth, as the name suggest, is only used for the purpose of authenticating and giving authorization using JWT Token

Enrollment is the main service, where the CRUDs of Student, Course, and Enrollments are, When enrolling a student to a course, this service calls an endpoint in Payment to store the enrollment data.

## Case 2 - Twittor w/ GraphQL

Case-2 (Twittor) is a basic implementation of Microservices with Kafka as its asynchronous communication method between services
There are three main app, the GraphqlAPI which will utilize GraphQL as the main endpoint, NETApp which will act as a service that saves data directly to DB, and LoggingApp for logging events recorded in Kafka

The Kafka itself is deployed to kubernetes with [Chart](https://github.com/bitnami/charts/tree/master/bitnami/kafka)<br>
The Chart is installed using [Helm](https://helm.sh/) , which itself is installed using [Chocolatey](https://chocolatey.org/)
