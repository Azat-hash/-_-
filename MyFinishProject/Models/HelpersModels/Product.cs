using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyFinishProject.Models.HelpersModels
{
    /// <summary>
    /// Сущность продукты
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название продукта
        /// </summary>
        [Display(Name = "Название товара")]
        [Required(ErrorMessage = "Поле является обязательным")]
        [MinLength(1)]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Описание товара
        /// </summary>
        [Display(Name = "Описание товара")]
        [Required(ErrorMessage = "Поле является обязательным")]
        [MinLength(1)]
        public string Descrption { get; set; }

        /// <summary>
        /// Продукция
        /// </summary>
        [Display(Name = "Место производство товара")]
        [Required(ErrorMessage = "Поле является обязательным")]
        [MinLength(1)]
        [MaxLength(50)]
        public string Production { get; set; }

        /// <summary>
        /// Цена товара
        /// </summary>
        [Display(Name = "Цена")]
        [Required()]
        public decimal Price { get; set; }

        /// <summary>
        /// Тип продукта
        /// </summary>
        public ProductType ProductType { get; set; }

        /// <summary>
        /// Фотография
        /// </summary>
        [Display(Name = "Картинка")]
        public byte[] Images { get; set; }

        public List<ApplicationUser> User { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Display(Name = "Ваше имя")]
        [Required(ErrorMessage = "Поле является обязательным")]
        [MinLength(1)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        [Display(Name = "Номер телефона")]
        [Required(ErrorMessage = "Поле номер телефона является обязательным")]
        [MinLength(10,ErrorMessage ="Введите корректный номер телефона")]
        [MaxLength(10,ErrorMessage = "Введите корректный номер телефона")]
        public string PhoneNumber { get; set; }

        /// <summary>
        ///Почта
        /// </summary>
        [Display(Name = "Электронная почта")]
        [Required(ErrorMessage = "Поле электронная почта является обязательным")]
        [MinLength(0)]
        [MaxLength(50)]
        public string Email { get; set; }

        /// <summary>
        /// Список комментариев
        /// </summary>
        public ICollection<Comment> Comments { get; set; }
    }
}