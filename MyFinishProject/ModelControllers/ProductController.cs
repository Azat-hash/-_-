using MyFinishProject.EnumHelper;
using MyFinishProject.Extensions;
using MyFinishProject.Models;
using MyFinishProject.Models.HelpersModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace MyFinishProject.ModelControllers
{
    /// <summary>
    /// Продукт контроллер
    /// </summary>
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Список товаров
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        /// <summary>
        /// Get: Создание товара
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Product = GetProducts();
            return View();
        }

        /// <summary>
        /// POST: Создание товара
        /// </summary>
        /// <param name="product"></param>
        /// <param name="fileBase"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase fileBase)
        {
            if (!ModelState.IsValid && fileBase != null)
            {
                byte[] imageByte = null;

                using (var binaryReader = new BinaryReader(fileBase.InputStream))
                {
                    imageByte = binaryReader.ReadBytes(fileBase.ContentLength);
                }

                product.Images = imageByte;
                db.Products.Add(product);
                db.SaveChanges();
                
                return RedirectToAction("Index", "Product");
            }

            ViewBag.Product = GetProducts();
            return View(product);
        }

        /// <summary>
        /// GET:Подробнее
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            Product product = db.Products.Include(z => z.Comments).FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return ViewException();
            }

            ViewBag.Favourites = db.Favourites.ToList();

            return View(product);
        }

        /// <summary>
        /// GET:Измение товаров
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return ViewException();
            }

            Product product = db.Products.Find(id);
            ViewBag.Product = GetProducts();

            if (product == null)
            {
                return ViewException();
            }

            return View(product);
        }

        /// <summary>
        /// POST:Измение товаров
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, HttpPostedFileBase fileBase)
        {
            if (ModelState.IsValid)
            {
                if(fileBase != null)
                {
                    byte[] imageByte = null;

                    using (var binary = new BinaryReader(fileBase.InputStream))
                    {
                        imageByte = binary.ReadBytes(fileBase.ContentLength);
                    }
                    product.Images = imageByte;
                }

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Details", new { id = product.Id });
            }
            ViewBag.Product = GetProducts();

            return View(product);
        }

        /// <summary>
        /// GET:Удаление товаров
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            Product product = db.Products.Find(id);

            if (product == null)
            {
                return ViewException();
            }

            return View(product);
        }

        /// <summary>
        /// POST:Удаление товаров
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);

            if (product == null)
            {
                return ViewException();
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return RedirectToAction("Index","Product");
        }

        /// <summary>
        /// POST:Добавление в избранные
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AddToFavorites(int id)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var userId = identity.GetUserId();

            var product = db.Products.FirstOrDefault(z => z.Id == id);
            var favourite = new Favourites()
            {
                UserId = userId,
                ProductId = product.Id
            };

            db.Favourites.Add(favourite);
            db.SaveChanges();

            return RedirectToAction("Details", new { id = id });
        }

        /// <summary>
        /// GET:Список избранных товаров
        /// </summary>
        /// <returns></returns>
        public ActionResult FavouriteIndex()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var userId = identity.GetUserId();

            var listOfFavorits = db.Favourites
                .Include(x => x.User)
                .Include(y => y.Product)
                .Where(x => x.UserId == userId)
                .ToList();

            return View(listOfFavorits);
        }

        /// <summary>
        /// Удаление из избранных
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteFromFavourite(int id)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var userId = identity.GetUserId();
            var favotites = db.Favourites.FirstOrDefault(x => x.Id == id);

            if(favotites == null)
            {
                return ViewException();
            }

            db.Favourites.Remove(favotites);
            db.SaveChanges();

            return RedirectToAction("FavouriteIndex");
        }

        /// <summary>
        /// Поиск товаров
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        public ActionResult Search(string productName)
        {
            var search = db.Products.Where(x => x.Name.Contains(productName)).ToList();

            return View(search);
        }

        /// <summary>
        /// Страничка ошибки
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewException()
        {
            return View();
        }

        /// <summary>
        /// Комментраии
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetComments(int id)
        {
            var comment = db.Comments
                .Include(z => z.User)
                .Where(x => x.ProductId == id)
                .OrderByDescending(d => d.Date);

            return PartialView(comment);
        }

        /// <summary>
        /// Отправка комментария
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SendComment(Comment comment)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var userId = identity.GetUserId();

            if (!ModelState.IsValid)
            {
                return View();
            }

            comment.UserId = userId;
            comment.Date = DateTime.Now;
            db.Comments.Add(comment);
            db.SaveChanges();

            return PartialView();
        }

        /// <summary>
        /// Удаление комментария
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteFromComments(int id)
        {
            Product product = new Product();
            var comment = db.Comments.Find(id);

            if(comment == null)
            {
                return ViewException();
            }
            db.Comments.Remove(comment);
            db.SaveChanges();

            return RedirectToAction("Details",new { id = comment.ProductId});
        }

        /// <summary>
        /// Поличение Enum
        /// </summary>
        /// <returns></returns>
        private SelectList GetProducts()
        {
            var positionsEnum = Enum.GetValues(typeof(ProductType))
                .Cast<ProductType>()
                .Select(v => new SelectListItem
                {
                    Text = v.GetDisplayName(),
                    Value = ((int)v).ToString()
                }).ToList();
            positionsEnum.Insert(0, new SelectListItem { Text = "Не выбрано", Value = null });
            return new SelectList(positionsEnum, "Value", "Text");
        }
    }
}