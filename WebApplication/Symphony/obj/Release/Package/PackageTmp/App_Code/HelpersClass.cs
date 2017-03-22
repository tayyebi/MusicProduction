using MusicProduction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicProduction.App_Code
{
    public class DropDownList
    {
        public string DisplayMember { get; set; }
        public string ValueMember { get; set; }
    }
    public class HelpersClass
    {
        static DcDataContext dc = new DcDataContext();

        static public List<DropDownList> SubGroups()
        {
            List<DropDownList> output = new List<DropDownList>();
            foreach (var item in dc.SubGroups)
            {
                output.Add(new DropDownList { DisplayMember = item.گروه + " - " + item.زیر_گروه, ValueMember = item.Id.ToString() });
            }
            return output;
        }

        static public List<DropDownList> Products()
        {
            List<DropDownList> output = new List<DropDownList>();
            foreach (var item in dc.Products)
            {
                output.Add(new DropDownList { DisplayMember = item.محصول + " - " + item.زیر_گروه, ValueMember = item.Id.ToString() });
            }
            return output;
        }

        static public List<DropDownList> Posts()
        {
            List<DropDownList> output = new List<DropDownList>();
            foreach (var item in dc.Posts)
            {
                output.Add(new DropDownList { DisplayMember = item.عنوان + "(" + item.تاریخ + ")" + " - " + item.زیر_گروه, ValueMember = item.Id.ToString() });
            }
            return output;
        }

        static public List<Image> Images(int Product = 0, int Post = 0)
        {
            var images = dc.Images.Select(m => new MusicProduction.Models.Image { عکس = m.عکس, Id = m.Id, File_Id = m.File_Id, توضیحات = m.توضیحات, Post_Id= m.Post_Id, Product_Id = m.Product_Id });

            if (Product != 0)
                images = dc.Images.Where(x => x.Product_Id == Product);
            if (Post != 0)
                images = dc.Images.Where(x => x.Post_Id == Post);

            return images.ToList().OrderBy(x => Guid.NewGuid()).Take(6).ToList();
        }
        static public List<Post> ViewPost(int SubGroup = 0)
        {
            var posts = dc.Posts.Select(m => new Post { Id = m.Id, SubGroup_Id = m.SubGroup_Id, عنوان = m.عنوان, تاریخ = m.تاریخ, چکیده = m.چکیده });
            if (SubGroup != 0)
                posts = posts.Where(m => m.SubGroup_Id == SubGroup);

            return posts.OrderByDescending(m => m.Id).Take(25).ToList();
        }
        static public List<Product> ViewProduct()
        {
            return dc.Products.Select(m => new Product { Image_Id = m.Image_Id, Id = m.Id, محصول = m.محصول, زیر_گروه = m.زیر_گروه }).ToList();
        }
    }
}