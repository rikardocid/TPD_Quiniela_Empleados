using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ASPNetCore6Identity.Models.Helper
{
    public class Enums
    {

        public enum Roles
        {
            [Display(Name = "Administrador")]
            Administrador,
            [Display(Name = "Cliente")]
            Cliente,
            [Display(Name = "ClienteDependiente")]
            ClienteDependiente
        }

        public enum EstatusCodigoPromocional
        {
            [Display(Name = "Activo")]
            Activo,
            [Display(Name = "Utilizado")]
            Utilizado,
            [Display(Name = "Compartido")]
            Compartido
        }

        public enum EstatusJuego
        {
            [Display(Name = "Pendiente")]
            Pendiente,
            [Display(Name = "Jugado")]
            Jugado
        }

        public enum EstatusAI
        {
            [Display(Name = "Activo")]
            Activo,
            [Display(Name = "Inactivo")]
            Inactivo
        }


        public static IEnumerable<EnumDescriptionAndValue> GetAllEnumsWithChilds()
        {
            var enums = new List<EnumDescriptionAndValue>();
            var order = 0;

            foreach (var type in typeof(Enums).GetNestedTypes())
            {
                var parent = new EnumDescriptionAndValue
                {
                    Name = type.Name,
                    Order = order
                };

                foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
                {
                    var i = 0;
                    var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

                    parent.Childs.Add(new EnumDescriptionAndValue
                    {
                        Name = field.Name,
                        Value = field.GetRawConstantValue().ToString(),
                        Description = attribute == null ? field.Name : attribute.Description,
                        Order = i
                    });

                    i++;
                }

                enums.Add(parent);
                order++;
            }

            return enums;
        }
    }

    public class EnumDescriptionAndValue
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public List<EnumDescriptionAndValue> Childs { get; set; }

        public EnumDescriptionAndValue()
        {
            Childs = new List<EnumDescriptionAndValue>();
        }
    }
}
