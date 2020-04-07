# api-payment-gateway
API Payment Gateway Project  
[![CircleCI](https://circleci.com/gh/pttg24/api-payment-gateway.svg?style=shield)](https://circleci.com/gh/pttg24/api-payment-gateway)
  
This project is an example of a payment gateway API. It can be applied to emulate the payment process of a ecommerce operation.
  
## Business Workflow
![GitHub Logo](/docs/Workflow.png)  
According the previous image, the workflow of an online purchase involves 4 actors: shopper, merchant, payment gateway and bank.  
This API is focused on operations between merchant, payment gateway and bank.  
1. Merchant sends a payment request to payment gateway and receives an acknowledge message with an InternalId.  
2. Payment Gateway sends a Request to Bank and receive a Bank response that will match with the InternalId previously generated.
3. When Merchant asks for details of a payment (with a internalID) Payment Gateway will answer with full details of transaction.
![GitHub Logo](/docs/Messages.png)  

## Requiremtens/Deliverables  
  
## Technical Summary  
  
### How to Run  
  
## Bonus Points  

