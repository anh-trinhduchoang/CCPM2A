namespace ShopBack.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using ShopBack.Common;
    using ShopBack.Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ShopBack.Data.ShopBackDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ShopBack.Data.ShopBackDbContext context)
        {
            CreateProductCategorySample(context);
            CreateSlide(context);
            //  This method will be called after migrating to the latest version.
            CreatePage(context);
            CreateContactDetail(context);

            CreateConfigTitle(context);
            CreateFooter(context);
            CreateUser(context);
        }
        private void CreateConfigTitle(ShopBackDbContext context)
        {
            if (!context.SystemConfigs.Any(x => x.Code == "HomeTitle"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeTitle",
                    ValueString = "Trang chủ ShopBack",

                });
            }
            if (!context.SystemConfigs.Any(x => x.Code == "HomeMetaKeyword"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeMetaKeyword",
                    ValueString = "Trang chủ ShopBack",

                });
            }
            if (!context.SystemConfigs.Any(x => x.Code == "HomeMetaDescription"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeMetaDescription",
                    ValueString = "Trang chủ ShopBack",

                });
            }
        }
        private void CreateUser(ShopBackDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ShopBackDbContext()));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ShopBackDbContext()));
            
            var user = new ApplicationUser()
            {
                UserName = "hoanganh",
                Email = "anh.trinhduchoang@gmail.com",
                EmailConfirmed = true,
                BirthDay = DateTime.Now,
                FullName = "Trinh Duc Hoang Anh"
            
            };
            if (manager.Users.Count(x => x.UserName == "hoanganh") == 0)
            {
                manager.Create(user, "123123");

                if (!roleManager.Roles.Any())
                {
                    roleManager.Create(new IdentityRole { Name = "Admin" });
                    roleManager.Create(new IdentityRole { Name = "User" });
                }

                var adminUser = manager.FindByEmail("anh.trinhduchoang@gmail.com");

                manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
            }

        }
        private void CreateProductCategorySample(ShopBack.Data.ShopBackDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
            {
                new ProductCategory() { Name="Đồng hồ cơ",Alias="dong-ho-co",Status=true },
                 new ProductCategory() { Name="Đồng hồ pin",Alias="dong-ho-pin",Status=true },
                  new ProductCategory() { Name="Đồng hồ thông minh",Alias="dong-ho-thong-minh",Status=true },
            };
                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges();
            }

        }
        private void CreateFooter(ShopBackDbContext context)
        {
            if (context.Footers.Count(x => x.ID == CommonConstants.DefaultFooterId) == 0)
            {
                string content = "Footer";
                context.Footers.Add(new Footer()
                {
                    ID = CommonConstants.DefaultFooterId,
                    Content = content
                });
                context.SaveChanges();
            }
        }
        private void CreateSlide(ShopBackDbContext context)
        {
            if (context.Slides.Count() == 0)
            {
                List<Slide> listSlide = new List<Slide>()
                {
                    new Slide() {
                        Name ="Slide 1",
                        DisplayOrder =1,
                        Status =true,
                        Url ="#",
                        Image ="~/Assets/client/images/master-slide-07.png",
                        Content =@" <h2 class=""caption1-slide1 xl-text2 t-center bo14 p-b-6 animated visible-false m-b-22"" data-appear=""fadeInUp"">

                            Leather Bags
                        </h2>


                        <span class=""caption2-slide1 m-text1 t-center animated visible-false m-b-33"" data-appear=""fadeInDown"">
							New Collection 2018
						</span>

						<div class=""wrap-btn-slide1 w-size1 animated visible-false"" data-appear=""zoomIn"">
							<!-- Button -->
							<a href = ""product.html"" class=""flex-c-m size2 bo-rad-23 s-text2 bgwhite hov1 trans-0-4"">
								Shop Now
                            </a> "},
                    new Slide() {
                        Name ="Slide 2",
                        DisplayOrder =2,
                        Status =true,
                        Url ="#",
                        Image ="~/Assets/client/images/master-slide-08.jpg",
                    Content=@" <h2 class=""caption1-slide1 xl-text2 t-center bo14 p-b-6 animated visible-false m-b-22"" data-appear=""fadeInUp"">

                            Leather Bags
                        </h2>


                        <span class=""caption2-slide1 m-text1 t-center animated visible-false m-b-33"" data-appear=""fadeInDown"">
							New Collection 2018
						</span>

						<div class=""wrap-btn-slide1 w-size1 animated visible-false"" data-appear=""zoomIn"">
							<!-- Button -->
							<a href = ""product.html"" class=""flex-c-m size2 bo-rad-23 s-text2 bgwhite hov1 trans-0-4"">
								Shop Now
                            </a> "},
                };
                context.Slides.AddRange(listSlide);
                context.SaveChanges();
            }
        }
        private void CreatePage(ShopBackDbContext context)
        {
            if (context.Pages.Count() == 0)
            {
                try
                {
                    var page = new Page()
                    {
                        Name = "Giới thiệu",
                        Alias = "gioi-thieu",
                        Content = @"Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium ",
                        Status = true

                    };
                    context.Pages.Add(page);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }
                }

            }
        }
        private void CreateContactDetail(ShopBackDbContext context)
        {
            if (context.ContactDetails.Count() == 0)
            {
                try
                {
                    var contactDetail = new ShopBack.Model.Models.ContactDetail()
                    {
                        Name = "ShopBack",
                        Address = "BigC Di An QL1K",
                        Email = "anh.trinhduchoang@gmail.com",
                        Lat = 10.889886,
                        Lng = 106.7734695,
                        Phone = "0967299276",
                        Website = "http://shopback.com.vn",
                        Other = "",
                        Status = true

                    };
                    context.ContactDetails.Add(contactDetail);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }
                }

            }
        }
    }
}

