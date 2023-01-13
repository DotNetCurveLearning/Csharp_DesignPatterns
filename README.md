# Strategy Pattern

Select an implementation at runtime based on user input without having to extend the class.

## Characteristics

* **Context**: Has a refeence to a strategy and invokes it.
	Ex.: Calls IStrategy.GetTaxFor(order)

* **IStrategy**: Defines the interface for the given strategy.
	Ex.: Defines the contract: GetTaxFor(Order order)

* **Strategy**: A concrete implementation of the strategy.
	Ex.: SwedenSalesTaxStrategy, USAStateSalesTaxStrategy