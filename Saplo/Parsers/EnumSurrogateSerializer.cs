using System;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Saplo.Parsers
{
	/// <summary>
	/// Serializer that handles serialization between enum and string value, ordinary <see cref="DataContractJsonSerializer"/> serialize enum to an int representive.
	/// </summary>
	public class EnumSurrogateSerializer : IDataContractSurrogate
	{
		public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType)
		{
			throw new NotImplementedException();
		}

		public object GetCustomDataToExport(Type clrType, Type dataContractType)
		{
			throw new NotImplementedException();
		}

		public Type GetDataContractType(Type type)
		{
			return type.IsEnum ? typeof (string) : type;
		}

		public object GetDeserializedObject(object obj, Type targetType)
		{
			throw new NotImplementedException();
		}

		public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
		{
			throw new NotImplementedException();
		}

		public object GetObjectToSerialize(object obj, Type targetType)
		{
			if(obj is Enum)
			{
				return GetEnumMemberValue((Enum) obj);
			}
			return obj;
		}

		public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
		{
			throw new NotImplementedException();
		}

		public CodeTypeDeclaration ProcessImportedType(CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit)
		{
			throw new NotImplementedException();
		}

		private string GetEnumMemberValue(Enum @enum)
		{
			var memInfo = @enum.GetType().GetMember(@enum.ToString(CultureInfo.InvariantCulture)).FirstOrDefault();
			if (memInfo != null)
			{
				var attribute = memInfo.GetCustomAttributes(typeof(EnumMemberAttribute), false).Cast<EnumMemberAttribute>().FirstOrDefault();
				if (attribute != null)
				{
					return attribute.Value;
				}
			}

			return @enum.ToString();
		}
	}
}