using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyFinishProject.Models.HelpersModels
{
    public class Review
    {
       /// <summary>
       /// Идентификатор
       /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Сообщение отзыва
        /// </summary>
        [Required(ErrorMessage = "Заполните полю ")]
        public string Message { get; set; }

        /// <summary>
        /// Дата отправки
        /// </summary>
        public DateTime ReviewDate { get; set; }

        /// <summary>
        /// Иденнтификатор пользователя
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Ссылка на сущность
        /// </summary>
        public ApplicationUser User { get; set; }
    }
}