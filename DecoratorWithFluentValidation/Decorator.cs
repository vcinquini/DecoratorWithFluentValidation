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
	abstract class Component<T>
	{
		public abstract ValidationResult Validate(T instance);
	}

	/// <summary>
	/// Define o objeto ao qual comportamentos adicionais podem ser adicionadas
	/// </summary>
	class ConcreteComponent<T> : Component<T>
	{
		/// <summary>
		/// Define qual validador será utilizado neste objeto de comportamento
		/// </summary>
		IValidator<T> _validator;

		public ConcreteComponent(IValidator<T> validator)
		{
			// armazena a objeto validador usado nesse objeto de comportamento
			_validator = validator;
		}

		public override ValidationResult Validate(T instance)
		{
			return _validator.Validate(instance);
		}
	}


	/// <summary>
	/// Mantem uma referencia ao objeto de comportamento e define uma interface que segue a interface do comportamento.
	/// </summary>
	abstract class Decorator<T> : Component<T>
	{
		/// <summary>
		/// Referencia ao objeto Component que contém o comportamento 
		/// </summary>
		protected Component<T> component;

		/// <summary>
		/// Recebe o objeto de comportamento e armazena a referencia a ele
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
	/// Objeto que adiciona comportamento ao objeto de comportamento
	/// </summary>
	class ConcreteDecorator<T> : Decorator<T>
	{
		/// <summary>
		/// // armazena a objeto validador usado nesse objeto de comportamento
		/// </summary>
		IValidator<T> _validator;

		/// <summary>
		/// Recebe o objeto de validacao e armazena a referencia a ele
		/// </summary>
		/// <param name="validator"></param>
		public ConcreteDecorator(IValidator<T> validator)
		{
			_validator = validator;
		}

		/// <summary>
		/// Chama a referencia do objeto validados armazenado na classe que referencia o comportamento
		/// </summary>
		/// <param name="instance"></param>
		/// <returns></returns>
		public override ValidationResult Validate(T instance)
		{
			Console.WriteLine($"Passing through ConcreteDecorator using validator {_validator.GetType().FullName}\n");

			// chama o comportamento armazenado no objeto da referncia anterior
			ValidationResult v = base.Validate(instance);

			if (v.IsValid)
			{
				// chama o comportamento armazenado neste objeto
				v = _validator.Validate(instance);
			}

			return v;

		}
	}

}