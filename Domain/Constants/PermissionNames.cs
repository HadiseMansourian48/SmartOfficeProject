using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Reflection;

namespace SmartOffice.Domain.Constants
{
    public class PermissionNames
    {
        public static class Employees
        {
            public const string View = "Employee.View";
            public const string Create = "Employee.Create";

        }
        public static class Users
        {
            public const string View = "Users.View";
            public const string Create = "Employee.Create";

        }

        public static IReadOnlyList<string> All() =>
            typeof(PermissionNames).GetNestedTypes(BindingFlags.Public)
                .SelectMany(t => t.GetFields(BindingFlags.Public | System.Reflection.BindingFlags.Static))
                .Where(f => f.IsLiteral && f.FieldType == typeof(string))
                .Select(f => (string)f.GetRawConstantValue()!)
                .ToList();

    }
    public static class RoleNames
    {
        public const string Admin = "admin";
        public const string Employee = "Employee";

    }

}
