using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace DecoratorWithFluentValidation
{

	/// <summary>
	/// Define a interface para objetos que tem comportamentos adicionados dinamicamente
	/// </summary>
	abstract class Component
	{
		public abstract ValidationResult ValidateCustomer(Customer instance);
	}

	/// <summary>
	/// Define o objeto ao qual comportamentos adicionais podem ser adicionadas
	/// </summary>
	class ConcreteComponent : Component
	{
		/// <summary>
		/// Define qual validador será utilizado neste objeto de comportamento
		/// </summary>
		IValidator<Customer> _validator;

		public ConcreteComponent(IValidator<Customer> validator)
		{
			// armazena a objeto validador usado nesse objeto de comportamento
			_validator = validator;
		}

		public override ValidationResult ValidateCustomer(Customer instance)
		{
			return _validator.Validate(instance);
		}
	}


	/// <summary>
	/// Mantem uma referencia ao objeto de comportamento e define uma interface que segue a interface do comportamento.
	/// </summary>
	abstract class Decorator : Component
	{
		/// <summary>
		/// Referencia ao objeto Component que contém o comportamento 
		/// </summary>
		protected Component component;

		/// <summary>
		/// Recebe o objeto de comportamento e armazena a referencia a ele
		/// </summary>
		/// <param name="component"></param>
		public void SetComponent(Component component)
		{
			this.component = component;
		}

		public override ValidationResult ValidateCustomer(Customer instance)
		{
			return component.ValidateCustomer(instance);
		}
	}

	/// <summary>
	/// Objeto que adiciona comportamento ao objeto de comportamento
	/// </summary>
	class ConcreteDecorator : Decorator
	{
		/// <summary>
		/// // armazena a objeto validador usado nesse objeto de comportamento
		/// </summary>
		IValidator<Customer> _validator;

		/// <summary>
		/// Recebe o objeto de validacao e armazena a referencia a ele
		/// </summary>
		/// <param name="validator"></param>
		public ConcreteDecorator(IValidator<Customer> validator)
		{
			_validator = validator;
		}

		/// <summary>
		/// Chama a referencia do objeto validados armazenado na classe que referencia o comportamento
		/// </summary>
		/// <param name="instance"></param>
		/// <returns></returns>
		public override ValidationResult ValidateCustomer(Customer instance)
		{
			Console.WriteLine($"Passing through ConcreteDecorator using validator {_validator.GetType().FullName}\n");

			// chama o comportamento armazenado no objeto da referncia anterior
			ValidationResult v = base.ValidateCustomer(instance);

			if (v.IsValid)
			{
				// chama o comportamento armazenado neste objeto
				v = _validator.Validate(instance);
			}

			return v;

		}
	}

}