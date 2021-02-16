using FluentValidation;
using FluentValidation.Results;
using System;

namespace DecoratorWithFluentValidation
{
	class Program
	{
		static void Main(string[] args)
		{
			Customer customer = new Customer() 
			{
				Id = 1, 
				FirstName = "Tiffany", 
				LastName = "Townsend", 
				Address = "22 Acacia Ave.",
				Card = new Card
				{
					CardHolder = "Katherine Roberts",
					CardNumber = "4455 2646 8546 0879",
					CVV = "654",
					ExpirationDate = "1/2024"
				}
			};
			Customer failed = new Customer()
			{
				Id = 0,
				FirstName = "",
				LastName = "",
				Address = "",
				Card = new Card
				{
					CardHolder = "",
					CardNumber = "",
					CVV = "",
					ExpirationDate = ""
				}
			};

			Component<Customer> c = new ConcreteComponent<Customer>(new CustomerValidator());
			ConcreteDecorator<Customer> d1 = new ConcreteDecorator<Customer>(new AnotherCustomerValidator());
			ConcreteDecorator<Customer> d2 = new ConcreteDecorator<Customer>(new OneMoreCustomerValidator());
			ConcreteDecorator<Customer> d3 = new ConcreteDecorator<Customer>(new CardValidator());

			// Link decorators
			d1.SetComponent(c);
			d2.SetComponent(d1);
			d3.SetComponent(d2);

			ValidationResult result = d3.Validate(customer);
			//ValidationResult result = d3.Validate(failed);

			if (!result.IsValid)
			{
				DisplayResults(result);
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("All validations has been passed!!!");
			}
		}

		private static void DisplayResults(ValidationResult result)
		{
			ConsoleColor currentColor = Console.ForegroundColor;
			if (!result.IsValid)
			{
				foreach (var failure in result.Errors)
				{
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write("Property ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Write(failure.PropertyName);
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write(" failed validation. Error was: ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(failure.ErrorMessage);
				}
			}
			Console.ForegroundColor = currentColor;
		}
	}
}
