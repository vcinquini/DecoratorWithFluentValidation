using FluentValidation;
using FluentValidation.Results;
using System;

namespace DecoratorWithFluentValidation
{
	class Program
	{
		static void Main(string[] args)
		{

			Customer customer = new Customer() { Forename = "Pepe", Surname = "", CVV = "600" };

			CustomerValidator validator1 = new CustomerValidator();
			ValidationResult result = validator1.Validate<Customer>(customer);
			DisplayResults(result);

			CustomerAnotherValidator validator2 = new CustomerAnotherValidator();
			result = validator2.Validate<Customer>(customer);
			DisplayResults(result);

			CustomerOneMoreValidator validator3 = new CustomerOneMoreValidator();
			result = validator3.Validate<Customer>(customer);
			DisplayResults(result);
		}

		private static void DisplayResults(ValidationResult result)
		{
			ConsoleColor currentColor = Console.ForegroundColor;

			if (!result.IsValid)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				foreach (var failure in result.Errors)
				{
					Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
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
