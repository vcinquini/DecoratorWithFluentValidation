using FluentValidation.Results;
using System;

namespace DecoratorWithFluentValidation
{
	class Program2
	{
		static void Main(string[] args)
		{
			Customer customer = new Customer() { Forename = "Pepe", Surname = "", CVV = "600" };
			Component c = new ConcreteComponent(new CustomerValidator());
			ConcreteDecorator d1 = new ConcreteDecorator(new CustomerAnotherValidator());
			ConcreteDecorator d2 = new ConcreteDecorator(new CustomerOneMoreValidator());

			// Link decorators
			d1.SetComponent(c);
			d2.SetComponent(d1);

			ValidationResult result = d2.ValidateCustomer(customer);

			if (!result.IsValid)
			{
				DisplayResults(result);
			}
			else
			{
				Console.WriteLine("Uh-huuuu!!!");
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
			else
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Uh-huuuu!!!");
			}
			Console.ForegroundColor = currentColor;
		}
	}
}
