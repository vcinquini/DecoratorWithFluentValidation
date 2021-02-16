using FluentValidation;
using FluentValidation.Results;
using System;

namespace DecoratorWithFluentValidation
{
	class Program_
	{
		static void Main(string[] args)
		{
			Customer customer = new Customer() { FirstName = "Pepe", LastName = "" };


			OneMoreCustomerValidator validator = new OneMoreCustomerValidator();
			ValidationResult result = validator.Validate<Customer>(customer);


			if (!result.IsValid)
			{
				DisplayResults(result);
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Green;
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
			Console.ForegroundColor = currentColor;
		}
	}
}
