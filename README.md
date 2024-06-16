# InvoiceAPI - Basic Invoice API , that can fetch create and generate API .
Used local CSV files for data storage and fetching.
Has 4 API : Invoice,Customer,Product,Category

API:
1) Invoice API :  Has Information related to Invoice.
   Methods:
   a) Getby ID - fetched based on Invoice ID.
   b) Get - Fetches all invoices present.
   c) Post - Used for generating Invoices.

2) Customer : Has Information related to Customers.
  Methods:
  a)GetbyID
  b)getall
  c)Updated
  d)Delete

 3)Product : Holds Item information like price, description ,categoryid etc.
 Methods:
  a)GetbyID
  b)getall
  c)Updated
  d)Delete

  4)Category : Holds information about taxes for each category of Item.
   Methods:
  a)GetbyID
  b)getall
  c)Updated
  d)Delete


  Run from Local:
  CSV files are used in place for Db.

  To Run in Local :
  1) Run the application in local .
  2) Swagger is enabled .
  3) sample local host url: https://localhost:7258/swagger
  4) Sample requests are provided.

Note : have attached a POSTMAN collection
 
