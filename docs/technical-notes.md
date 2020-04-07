## Architecture  

#CoPaymentGateway(API)  
API project. PaymentsController expose two methods: get payment and post payment.  
GetPayment - receive an internalPaymentId (Guid) and returns payment full details (PaymentResponse) or Not Found error    
PostPayment - receive a PaymentRequest and returns an internalPaymentId (Guid)  
Services injection (mediator, cqrs, repositories) and InMemory database definde on Startup.cs  
Packages:  
Swashbuckle/Swagger: web interface to API (allow requests emulation)  
Serilog: application logging tool  
Prometheus: metrics server  
  
#CoPaymentGateway.CQRS.Commands  
CQRS Command (Post action).  
Receive and validate a PaymentRequest (validation agains rules defined on PaymentRequestValidator)  
If valid, inserts payment on Memory Database (table Payments) and generate an internalPaymentId (Guid)  
Send the request to Bank (simulator)  
Update row on database with Bank response. A pair of Guid (internalPaymentId-BankResponse.PaymentId) is now saved on database.  
Returns internalPaymentId  
  
#CoPaymentGateway.CQRS.Queries  
CQRS Query (Get action).  
Receive an internalPaymentId, search database and return PaymentResponse with all details  

