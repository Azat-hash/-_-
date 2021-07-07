using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MyFinishProject
{
    /// <summary>
    /// Тип продукта
    /// </summary>
    public enum ProductType
    {
        [Description("Транспорты")]
        Car = 1,

        [Description("Одежды")]
        Dress = 2,

        [Description("Недвижимость")]
        The_Prop = 3,

        [Description("Электроники")]
        Electronics = 4,

        [Description("Животные")]
        Animal = 6,

        [Description("Другое")]
        Other = 7
    }
}