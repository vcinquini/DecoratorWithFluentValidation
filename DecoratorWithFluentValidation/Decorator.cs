using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace DecoratorWithFluentValidation
{

	/// <summary>
	/// Defines the interface for objects that have behaviors dynamically added
	/// </summary>
	abstract class Component<T>
	{
		public abstract ValidationResult Validate(T instance);
	}

	/// <summary>
	/// Defines the object to which additional behaviors can be added
	/// </summary>
	class ConcreteComponent<T> : Component<T>
	{
		/// <summary>
		/// Defines which validator will be used on this object
		/// </summary>
		IValidator<T> _validator;

		public ConcreteComponent(IValidator<T> validator)
		{
			// Stores the validator object used in that object
			_validator = validator;
		}

		public override ValidationResult Validate(T instance)
		{
			return _validator.Validate(instance);
		}
	}


	/// <summary>
	/// Maintains a reference to the object and defines an interface that follows the behavior's interface.
	/// </summary>
	abstract class Decorator<T> : Component<T>
	{
		/// <summary>
		/// Reference to the Component object that contains the behavior
		/// </summary>
		protected Component<T> component;

		/// <summary>
		/// Sets the object and stores a reference to it
		/// </summary>
		/// <param name="component"></param>
		public void SetComponent(Component<T> component)
		{
			this.component = component;
		}

		public override ValidationResult Validate(T instance)
		{
			return component.Validate(instance);
		}
	}

	/// <summary>
	/// Concrete object that adds behavior to the Decorator object
	/// </summary>
	class ConcreteDecorator<T> : Decorator<T>
	{
		/// <summary>
		/// // Stores the validator used in this object
		/// </summary>
		IValidator<T> _validator;

		/// <summary>
		/// Sets the object and stores a reference to it
		/// </summary>
		/// <param name="validator"></param>
		public ConcreteDecorator(IValidator<T> validator)
		{
			_validator = validator;
		}

		/// <summary>
		/// Calls the prior validator (if any) and them calls the validator itself
		/// </summary>
		/// <param name="instance"></param>
		/// <returns></returns>
		public override ValidationResult Validate(T instance)
		{
			Console.WriteLine($"Passing through ConcreteDecorator using validator {_validator.GetType().FullName}\n");

			// calls the validator from prior instance
			ValidationResult v = base.Validate(instance);

			if (v.IsValid)
			{
				// calls the validator itself
				v = _validator.Validate(instance);
			}

			return v;

		}
	}

}