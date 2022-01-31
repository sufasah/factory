# Project Information
 
* Project Name: <br/>
Factory Protocol
<br>

* Project Team: <br/>
Fadıl Şahin <br>
Rahim Gökçeli
<br>

* Project Start Date: <br/>
May 2021
<br>

* Project State And Duration: <br/>
Finished, 20 Days
<br>

* Project Description: <br/>
It is supposed to there is a factory that produces products. Some customers order products from that factory until a specified deadline by customer. Customers can log in, see all products, add products to a cart, select a deadline, order the cart and see their orders on a browser UI. Factory employees manage these orders and producing products. Also the employees can see all orders delivered, canceled or in queue, add workcenters that produce products with a speed property and can be active or not, add operations whose workcenters can perform according to these operations' product type constraint on the browser UI. Products are produced from sub products and can be salable. Products can be produced only if there is enough sub products. If there are not enough products for orders ordered by customers, orders can't be delivered. To produce products, workcenters must be scheduled for a specific start and end time interval. While scheduling workcenters, for each product, all workcenters must be scheduled whose operation's product type property matches with the product's type. For example, product "desk" is wanted to be produced by a customer and two "desk"s are ordered. Type of product "desk" is "wood". For "wood" type, there is 2 operations which are "cut" and "attach". There is 3 workcenter "a", "b" and "c" all active. "a" performs "cut" operation, "b" performs "cut" operation and "c" performs "attach" operation (there must be at least one active workcenter per operation otherwise the product can't be produced). While producing 2 "desk"s, for the first desk, workcenters must be chosen per operation like workcenter "a" and "c" ("a" and "b" can't be selected because both are same operation) and scheduled for a time interval. For the second desk, same rule is valid and suppose that workcenter "b" and "c" selected. When all operations done, the product will be produced. 

<br/>

# SETUP

The project consists of front-end, back-end and MySQL8 database. To make whole project running, take the steps below.

1. Install MySQL8 database to the local machine with `UID=root` and `PASSWORD=123` or add a user with these uid and password. (The server must listen on "localhost:3306")
2. Create myfactory database with `'root'` user (root user must have permissions on the database) and import [myfactory.sql](myfactory.sql) dump file into the database or apply migrations at [/BF.DataAccess/Migrations](BF.DataAccess/Migrations/). To apply migrations, open a terminal, install dotnet-ef tool with `'dotnet tool install --global dotnet-ef'` command, change directory to `BF.DataAccess/Migrations` and apply migrations with command `'dotnet ef database update'`.
3. The database is ready. To run the front-end and back-end, open two terminals and change directories to [/BF.WebApi/](BF.WebApi/) and [/BF.FrontEnd/](BF.FrontEnd/). For terminal in 'BF.WebApi', run `'dotnet run'` command. For other terminal, run `'npm run serve'` command
4. Open a browser and open two tabs with urls "http://localhost:8080" and "http://localhost:8080/panel". The first url is customer UI and the second url is employee UI. Customer can register the system but employee must be added to the users table in the database using sql query. If `myfactory.sql` is used at the 2nd step, "c1" & "123" customer and "asd" & "asd" employee account already exist. 
5. The application may not work properly or error because of SSL. To avoid from that, connect the urls at the step 4 and "http://localhost:5000/api/v1/customers". For all tabs, when the browser gives "Your connection is not private" error, click advanced button and then "proceed ... (unsafe)" link. After that you can close the tab which url contains "/api/v1".
6. customer and employee UI are ready to use. You can rat around now with the guidance of project description.

# SCREENSHOTS

## Customer Products
![customer-products](images/customer-products.bmp)

## User Dashboard
![user-dashboard](images/user-dashboard.bmp)

## User Orders
![user-orders](images/user-orders.bmp)

## User Products
![user-products](images/user-products.bmp)

## User Subproducts
![user-sub-products](images/user-sub-products.bmp)

## User Workcenters
![user-workcenter](images/user-workcenters.bmp)

## User Workcenter Operations
![user-workcenter-operations](images/user-workcenter-operations.bmp)

## User Product Not Enough Subproducts
![user-porduct-not-enough-subproduct](images/user-product-not-enough-subproducts.bmp)

## User Product Enough Subproducts
![user-product-enough-subproducts](images/user-product-enough-subproducts.bmp)

## User Producing Product
![user-producing-product](images/user-producing-product.bmp)

## User Producing Done
![user-producing-done](images/user-producing-done.bmp)

## User Workcenters Scheduled
![user-workcenters-scheduled](images/user-workcenters-scheduled.bmp)
