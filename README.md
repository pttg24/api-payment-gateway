# api-payment-gateway
API Payment Gateway Project  
[![CircleCI](https://circleci.com/gh/pttg24/api-payment-gateway.svg?style=shield)](https://circleci.com/gh/pttg24/api-payment-gateway)
  
This project is an example of a payment gateway API. It can be applied to emulate the payment process of an ecommerce operation.
  
## Business Workflow
![GitHub Logo](/docs/Workflow.png)  
According to the previous image, the workflow of an online purchase involves 4 actors: shopper, merchant, payment gateway and bank.  
This API is focused on operations between the merchant, payment gateway and bank.  
1. Merchant sends a payment request to the payment gateway and receives an acknowledge message with an InternalId.  
2. Payment Gateway sends a Request to Bank and receives a Bank response that will be assigned with the InternalId previously generated.
3. When Merchant asks for details of payment (with an internalID) Payment Gateway will answer with full details of the transaction.  
![GitHub Logo](/docs/Messages.png)  

## Requirements/Deliverables  
- [x] Product: merchant should be able to process a payment and receive a successful/unsuccessful response  
- [x] Product: merchant should be able to retrieve details of a previously made payment
- [x] Tech: build an API to process a payment and retrieve details of previous payments  
- [x] Tech: build a simulator to mock bank responses  

## Technical Summary  
**Activities**
- [x] Business and requirements analysis  
- [x] Design and development of technical solution
- [x] Setup tech infrastructure  
- [x] Automated and acceptance tests  
  
**Stack**  
* .Net Core 3.0, .Net Standard, Entity Framework Core
* CQRS, Mediator
* xUnit, OpenAPI/Swagger, FluentValidator
* Docker, Serilog, Grafana, Prometheus
* CircleCI, Git, Visual Studio 2019 
  
### How to Run  
Checkout complete folder  
Go to src\CoPaymentGateway folder  
Run  
`docker build -t copaymentgateway -f Dockerfile .`  
`docker-compose up`  
  
API - http://localhost:5000/  
Prometheus - http://localhost:9090  
Grafana - http://localhost:3000/  

Technical decisions, further developments, conclusions, please go to --> [TechNotes](/docs/technical-notes.md)  
  
## Bonus Points  
- [x] Application logging  
- [x] Application metrics
- [x] Containerization  
- [x] Build script / CI
