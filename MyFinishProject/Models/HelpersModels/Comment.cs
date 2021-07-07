using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyFinishProject.Models.HelpersModels
{
    public class Comment
    {
        /// <summary>
        /// Идентикатор комментария
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        [Required(ErrorMessage ="Оставьте комментарий")]
        [MinLength(1,ErrorMessage ="Минимальная длина 1 символ")]
        [MaxLength(100,ErrorMessage ="Максимальная длина 100 символ")]
        public string Description { get; set; }

        /// <summary>
        /// Дата отправки
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Идентификатор пользователья
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Идентификатор продукта
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Ссылка на сущность
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Ссылка на сущность
        /// </summary>
        public ApplicationUser User { get; set; }
    }
}