using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestAssistant
{
    public static void TestingFields(MonoBehaviour component, Type script, string name, string tipe, FieldAttributes attributes, IComparable value = null, bool serializeField = false)
    {
        var myBindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public;

        var field = script.GetField(name, myBindingFlags);
        Assert.IsNotNull(field,
            $"The \"{name}\" field is missing in the \"Player\" class");

        Assert.AreEqual(tipe, field.FieldType.Name,
            $"The \"{name}\" field has an invalid data type");

        if (serializeField)
        {
            Assert.AreEqual("SerializeField", field.CustomAttributes.Where(a => a.AttributeType.Name == "SerializeField").FirstOrDefault()?.AttributeType.Name,
                $"The \"{name}\" field is missing the \"SerializeField\" attribute");
        }

        Assert.AreEqual(attributes, field.Attributes,
            $"The \"{name}\" field has an incorrect access modifier");

        Assert.AreEqual(value, field.GetValue(component),
            $"The \"{name}\" field has an incorrect initial value");
    }

    public static void TestingMethods(Type script, string name, string tipe, MethodAttributes attributes, MyParameterInfo[] parameters)
    {
        var myBindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public;

        var method = script.GetMethod(name, myBindingFlags);
        Assert.IsNotNull(method,
            $"The \"{name}\" method is missing in the \"Player\" class!");

        Assert.AreEqual(tipe, method.ReturnType.Name,
            $"The \"{name}\" method has an invalid data type!");

        Assert.AreEqual(attributes, method.Attributes,
            $"The \"{name}\" method has an incorrect access modifier!");

        int index = 0;
        foreach (var parameter in method.GetParameters())
        {
            Assert.AreEqual(parameters[index].ParameterType, parameter.ParameterType,
                $"In the \"{name}\" method, a parameter with the \"{typeof(Collision2D)}\" data type was expected!");
            Assert.AreEqual(parameters[index].Name, parameter.Name,
                $"In the \"{name}\" method, a parameter with the named\"{typeof(Collision2D)}\" was expected!");

            index++;
        }

        //Assert.AreEqual(parameter, method.GetParameters().First().,
        //    $"The \"{name}\" method has an incorrect parameters");
    }
}

public class MyParameterInfo: ParameterInfo
{
    public MyParameterInfo(Type type, string name)
    {
        this.ClassImpl = type;
        this.NameImpl = name;
    }
}
