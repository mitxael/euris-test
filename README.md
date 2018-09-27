This application is an example of the use of ASP.NET, namely:

    •	MVC 4
    
    •	Entity Framework
    
    •	Linq
    
    •	And MS SQL Server    

FUNCTIONAL ANALYSIS
The web application is aimed to manage a List of products, providing the following features:
    1.	A page for CRUD operations on Product Lists, including the addition/removal of products. 
    2.	A page for CRUD operations on Products, to be linked to Product Lists.

CLASS STRUCTURE
The classes used in this applciation are:
    <Product>
        •	Code – alfanumeric value that identifies the product
        •	Description – string containing information on the product
    <Product List>
        •	Code – alfanumeric value that identifies the list
        •	Description – string containing information on the list
        •	Products – products in the list 
