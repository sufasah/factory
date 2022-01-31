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
It is supposed to there is a factory that produces products. Some customers order products from that factory until a specified deadline from customer. Customers can log in, see all products, add products to a cart, select a deadline, order the cart and see their orders on a browser UI. Factory employees manage these orders and producing products. Also the employees can see all orders delivered, canceled or in queue, add workcenters that produce products with a speed and can be active or not, add operations whose workcenters can perform according to these operations' product type constraint on the browser UI. Products are produced from sub products and only these products can be salable. Products can be produced only if there is enough sub products. If there are not enough products for orders ordered by customers, orders can't be delivered. To produce products, workcenters must be scheduled for a specific time span if they can perform operations that support product type for a product. While producing a product, workcenters for all operation types must be scheduled per product. For example, product "desk" is wanted to be produced by a customer and two "desk"s are ordered. Type of product "desk" is "wood". For "wood" type, there is 2 operations which are "cut" and "attach". There is 3 workcenter "a", "b" and "c" all active. "a" performs "cut" operation, "b" performs "cut" operation and "c" performs "attach" operation (there must be at least one active workcenter per operation otherwise the product can't be produced). While producing 2 "desk"s, for the first desk, workcenters must be chosen per operation like workcenter "a" and "c" ("a" and "b" can't be selected because both are same operation) and scheduled for a time interval. For the second desk, same rule is valid and suppose that workcenter "b" and "c" selected. When all operations done, the product will be produced. 

<br/>

