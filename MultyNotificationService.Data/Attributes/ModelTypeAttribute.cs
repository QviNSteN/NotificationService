using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultyNotificationService.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ModelTypeAttribute : Attribute
    {
        public Type Type { get; set; }

        public ModelTypeAttribute(Type type)
        {
            Type = type;
        }
    }
}
