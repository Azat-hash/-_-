using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyFinishProject.Models.HelpersModels
{
    public class Favourites
    {
        /// <summary>
        /// Идентикатор продукта в избранных
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID пользователья
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// ID продукта
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Ссылка на сущность
        /// </summary>
        public ApplicationUser User { get; set; }

        /// <summary>
        /// Ссылка на сущность
        /// </summary>
        public Product Product { get; set; }
    }
}