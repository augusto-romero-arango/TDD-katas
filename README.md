# TDD-katas

## Vending Machine

>In this exercise you will build the brains of a vending machine. It will accept money, make change, maintain inventory, and dispense products. All the things that you might expect a vending machine to accomplish. Features are detailed below.
>
>**Accept Coins**
> 
>The vending machine will accept valid coins (nickels, dimes, and quarters) and reject invalid ones (pennies). 
> 
>Coin values:
>
>- Penny - 1 cent
>- Nickel - 5 cents
>- Dime - 10 cents
>- Quarter - 25 cents
>
>When a valid coin is inserted the amount of the coin will be added to the current amount and the display will be updated. When there are no coins inserted, the machine displays INSERT COIN. Rejected coins are placed in the coin return.
>
>**Select Product**
> 
>There are three products: 
>- cola for $1.00, 
>- chips for $0.50, 
>- and candy for $0.65. 
>
>When the respective button is pressed and enough money has been inserted, the product is dispensed and the machine displays THANK YOU for 5 seconds.
> 
>After that, it will display INSERT COIN and the current amount will be set to $0.00. 
> 
>If there is not enough money inserted then the machine displays PRICE and the price of the item for 5 seconds. After that the display will show either INSERT COIN or the current amount entered, as appropriate.
>
>**Make Change**
> 
>When a product is selected that costs less than the amount of money in the machine, then the remaining amount is placed in the coin return.
>
>**Return Coins**
> 
>When the return coins button is pressed, the money the customer has placed in the machine is returned and the display shows INSERT COIN.
>
>**Sold Out**
> 
>When the item selected by the customer is out of stock, the machine displays SOLD OUT for 5 seconds. After that, it will display the amount of money remaining in the machine or INSERT COIN if there is no money in the machine.
>
>**Exact Change Only**
> 
>When the machine is not able to give change for any of the items that it sells, it will display EXACT CHANGE ONLY instead of INSERT COIN.

Fuente: [Samman Technical Coaching](https://sammancoaching.org/kata_descriptions/vending_machine.html)

### Hallazgos

1. Partir de una respuesta clara de la interfaz con el record `VendingMacineRespuesta` me permitió no tener necesidad de depender del estado o los métodos internos de la clase.
2. La refactorización del método `VendingMachine.SeleccionarProducto` no tuvo ningún impacto en las pruebas escritas.
3. Me gustó crear métodos de extensión para las monedas. (`CoinExtensions`). De esa manera se encapsuló la lógica de valorización de las monedas y cálculos contra listas de monedas.
4. La extracción de las clases `Monedero` e `Inventario` se realizó en su mayoría con refactors automatizados, con lo cual no hubo impacto significativo en el código escrito ni las pruebas.

### Posibles continuaciones de la kata

- Implementar los efectos de temporización de 5 segundos para los cambios de estados.
- Implementar listas de precios y productos dinámicos.

## Supermarket receipt

>Write some code that could be used in a supermarket to calculate the total cost of items in a shopping cart and provide a receipt to the customer.
>
>The supermarket has a catalog with different types of products (rice, apples, milk, toothbrushes,…). Each product has a price, and the total price of the shopping cart is the total of all the prices of the items. You get a receipt that details the items you’ve bought, the total price, and any discounts that were applied.
>
>The supermarket runs special deals, e.g.
>
>- Buy two toothbrushes, get one free. Normal toothbrush price is €0.99
>- 20% discount on apples, normal price €1.99 per kilo.
>- 10% discount on rice, normal price €2.49 per bag
>- Five tubes of toothpaste for €7.49, normal price €1.79
>- Two boxes of cherry tomatoes for €0.99, normal price €0.69 per box.
>- These are just examples: the actual special deals change each week.

Fuente: [Samman Technical Coaching](https://sammancoaching.org/kata_descriptions/supermarket_receipt.html)

La implementación realizada está únicamente enfocada en la creación del recibo. No se modeló el catálogo ni el shopping cart.

### Hallazgos

1. Realizar el assert contra el resultado final del recibo me permitió cambiar libremente la implementación en los refactorings sin tropiezos.
2. Mientras implementaba iba dejando TODO con las correcciones que esperaba poder realizar en el refactoring.
3. Disfruté mucho el refactor a partir de este [commit](https://github.com/augusto-romero-arango/TDD-katas/tree/4e932c22e23e173d1a02e598fcb6cdbcbb5cfcfe). Esta implementación estaba muy fea.
4. Refactorizar también es cumplir pequeños pasos y validar que estemos en verde. Para eso se debe evitar hacer cambios grandes que no permitan dejar retroalimentación de la causa del error. Las pruebas funcionan como una malla de salvación.
5. Haga commits pequeños de cada refactor efectivo. Tuve momento en que un cambio que hice dañó el código y me tocó devolverme a un estado correcto conocido.
6. Después de segmentados los descuentos en clases, se podrían probar unitariamente cada descuento para evaluar casos de borde.
7. Sin estar pendiente del coverage. La solución tiene cobertura del 100% ![Code coverage](Assets/img/code-coverage-supermarket-receipt.png)

### Posibles continuaciones de la kata

- Implementar descuento de X% en la segunda unidad idéntica.
- Controlar que no se apliquen descuentos sobre descuentos en caso de que un producto tenga más de un descuento.
- Implementar el catálogo de productos con precios.
- Implementar el carrito de compras.
- Modificar los descuentos para que sean aplicados masivamente y no por el ingreso individual de cada producto.


