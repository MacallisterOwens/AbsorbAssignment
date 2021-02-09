# Absorb Assignment
A simple program which reads in a customers scanned items, creating a 'basket' with their items, then reads in GroceryCO's current and (if applicable) sale price catalogs. It will determine the effetive price of each item and will print out the customer's recipt to the console.

## Assumptions
The assumed format for both the current price and sale price catalogs is as follows:

Item Name2,Price

Item Name2,Price2

...

E.g.

Apple,0.75

Bacon,2.99

Bagel,1.49

...

## Notes on running the program
The program is run via CheckoutKiosk.main(). The program expects a minimum of two arguments, the first being the path to the customer's scaned items file and the second being the path to the current prices catalog file. The third optional argument is the path to the sale prices catalog file. 
