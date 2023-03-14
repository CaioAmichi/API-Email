using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace EmailApi.CrossCutting
{
    [ExcludeFromCodeCoverage]
    public static class AssemblyUteis
    {
		private static string ProjectName(string camada) => typeof(AssemblyUteis).Namespace.Replace("CrossCutting", camada);

		

		public static IEnumerable<Type> GetApplicationInterfaces()
		{
			return GetTiposDefinidos("Domain").Where(
				type => type.IsInterface
				&& NamespaceMath(type, "Domain.Applications"));
		}

		public static IEnumerable<Type> GetApplicationClasses()
		{
			return GetTiposDefinidos("Application").Where(
				type => type.IsClass
				&& !type.IsAbstract
				&& type.GetCustomAttribute<CompilerGeneratedAttribute>() == null);
		}

		public static IEnumerable<Type> GetInfrastructureInterfaces()
		{

			var teste = GetTiposDefinidos("Domain");
			return GetTiposDefinidos("Domain").Where(
				type => type.IsInterface
				&& type.Namespace != null
				&& (NamespaceMath(type, "Domain.Gateways")
				|| NamespaceMath(type, "Domain.IRepository")
				|| NamespaceMath(type, "Domain.Servicebus")));
		}

		public static IEnumerable<Type> GetInfrastructureClasses()
		{
			var teste = GetTiposDefinidos("Infrastructure").Where(
				type => type.IsClass
				&& !type.IsAbstract
				&& (NamespaceMath(type, "Infrastructure.Gateways")
					|| NamespaceMath(type, "Infrastructure.Repositories")
					|| NamespaceMath(type, "Infrastructure.Servicebus"))
				&& type.GetCustomAttribute<CompilerGeneratedAttribute>() == null);

			return teste;
		}
		private static bool NamespaceMath(Type type, string nomeCamada)
		{
			return type.Namespace?.EndsWith(ProjectName(nomeCamada)) ?? false;
		}

		private static Type[] GetTiposDefinidos(string nomeCamada)
		{
			return Assembly.Load(ProjectName(nomeCamada)).GetTypes();
		}
		public static IEnumerable<Assembly> GetCurrentAssemblies()
		{
			return new Assembly[]
			{
				Assembly.Load(ProjectName("Api")),
				Assembly.Load(ProjectName("Domain")),
				Assembly.Load(ProjectName("Infrastructure")),
				Assembly.Load(ProjectName("Application")),
				Assembly.Load(ProjectName("CrossCutting"))
			};
		}
		public static Type FindType(Type @interface, IEnumerable<Type> types)
		{
			return types.FirstOrDefault(t => t.GetInterfaces().Contains(@interface));
		}

		public static Type FindInterface(Type type, IEnumerable<Type> interfaces)
		{
			return interfaces.FirstOrDefault(i => type.GetInterfaces().Contains(i));
		}
	}
}
