using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DMSys.Systems;

namespace PostManager.Contexts
{
    public class PostContext : DMSys.Data.NpgsqlUtility
    {
        public PostContext()
            : base(xConfig.ConnectionString)
        { }

        public Models.PostModel GetPost(int id)
        {
            string commandText =
@"SELECT p.id
       , p.n_site_id
       , p.n_category_id
       , p.post_link
       , p.post_image
       , p.post_title
       , p.post_text
       , p.new_post_id
       , p.new_post_transaction_id
       , p.post_price
       , p.n_template_location_id
       , p.post_date
       , p.n_site_posted_id
       , p.post_price_type_id
 FROM post p
 WHERE p.id = " + SQLInt(id);
            Models.PostModel model = null;
            using (DataTable dTable = base.FillDataTable(commandText))
            {
                if( dTable.Rows.Count > 0)
                {
                    DataRow dRow = dTable.Rows[0];
                    model = new Models.PostModel()
                    {
                        PostId = TryParse.ToInt32(dRow["id"]),
                        SiteId = TryParse.ToInt32(dRow["n_site_id"]),
                        CategoryId = TryParse.ToInt32(dRow["n_category_id"]),
                        PostLink = TryParse.ToString(dRow["post_link"]),
                        PostImage = TryParse.ToString(dRow["post_image"]),
                        PostTitle = TryParse.ToString(dRow["post_title"]),
                        PostText = TryParse.ToString(dRow["post_text"]),
                        PostPrice = TryParse.ToDecimal(dRow["post_price"]),
                        TemplateLocationId = TryParse.ToInt32(dRow["n_template_location_id"]),
                        PostDate = TryParse.ToDateTime(dRow["post_date"]),
                        SitePostedId = TryParse.ToInt32(dRow["n_site_posted_id"]),
                        PostPriceTypeId = TryParse.ToInt32(dRow["post_price_type_id"])
                    };
                }
            }
            return model;
        }

        /// <summary>
        /// Типове цени
        /// </summary>
        public List<Models.ListItemModel> GetPostPriceTypes(bool withUndefined = false)
        {
            List<Models.ListItemModel> model = new List<Models.ListItemModel>();
            string commandText =
@"SELECT ppt.id, ppt.pt_name
 FROM post_price_type ppt
 ORDER BY ppt.pt_name ";

            if (withUndefined)
            {
                model.Add(new Models.ListItemModel() { id = "0", label = "-", abbrev = "" });
            }
            using (DataTable dtDiscounts = FillDataTable(commandText))
            {
                foreach (DataRow drDiscount in dtDiscounts.Rows)
                {
                    model.Add(new Models.ListItemModel()
                    {
                        id = TryParse.ToString(drDiscount["id"]),
                        label = TryParse.ToString(drDiscount["pt_name"]),
                        abbrev = ""
                    });
                }
            }
            return model;
        }

        public void Edit(Models.PostModel model)
        {
            string commandText =
@"UPDATE post
     SET post_link
       , post_image
       , post_title
       , post_text
       , post_price
       , post_date " + SQLDateTime(model.PostDate) + @"
       , n_site_posted_id
       , post_price_type_id
 WHERE id = " + SQLInt(model.PostId);
        }
    }
}