## Architecture  

**CoPaymentGateway(API)**    
API project. PaymentsController expose two methods: get payment and post payment.  
GetPayment - receive an internalPaymentId (Guid) and returns payment full details (PaymentResponse) or Not Found error    
PostPayment - receive a PaymentRequest and returns an internalPaymentId (Guid)  
Services injection (mediator, cqrs, repositories) and InMemory database defined on Startup.cs  
Packages:  
Swashbuckle/Swagger: web interface to API (allow requests emulation)  
Serilog: application logging tool  
Prometheus: metrics server  
  
**CoPaymentGateway.CQRS.Commands**    
CQRS Pattern - Commands (Post action).  
Receive and validate a PaymentRequest (validation agains rules defined on PaymentRequestValidator)  
If valid, inserts payment on Memory Database (table Payments) and generate an internalPaymentId (Guid)  
Send the request to Bank (simulator)  
Update row on database with Bank response. A pair of Guid (internalPaymentId-BankResponse.PaymentId) is now saved on database.  
Returns internalPaymentId  
  
**CoPaymentGateway.CQRS.Queries**   
CQRS Pattern - Queries (Get action).  
Receive an internalPaymentId, search database and return PaymentResponse with all details  
  
**CoPaymentGateway.Domain**  
Domain Aggregates (Bank request/response, Payment request/response)  
Status Types Enum (Created 0, Approved 1, Rejected 2) and attributes (name, description)  
Extensions - convert credit card number to masked number  
Validators - using FluentValidator to check Month,Year,Cvv,CardNumber values
Exceptions - InvalidPaymentException
  
**CoPaymentGateway.Infrastructure**  
DataModel(Payments) and definition of inMemory Database context  
Usage of Repository and UnitOfWork patterns to insert, update and get info from datamodel  
FakeBankResponseService to randomly generate Approved and Rejected Responses from Bank  
Packages:  
Microsoft.EntityFrameworkCore.InMemory  
  
**Tests**  
Using xUnit and Moq to mock commands,queries,repositories and services  
  
## Decisions  
**Logging**  
Serilog vs Log4Net: I'm not using Log4net for a long time, so my choice was Serilog.  
  
**Metrics**  
AppMetrics + InfluxDB + Grafana vs Prometheus + Grafana: It was the first time I used any of these tools. Prometheus + Grafana setup and configuration seemed easier to me.  

**CI**  
CircleCi: my option was CircleCi to generate automated builds and run tests for each pull request.  
  
## Further Developments  
- [ ] Publish docker image on automated build  
- [ ] Include integration and performance tests  
- [ ] API client  
- [ ] Authentication  
  
## Conlusions  
It seems a simple project but it is a very demanding project to do in short time.  
I learned a lot about docker containers, automated builds and setup monitorization tools like prometheus and grafana.  
This solution can be considered as a base project but there are a lot of improvements to add/to do.
