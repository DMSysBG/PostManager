using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DMSys.Systems;
using Post.Models;

namespace PostManager.Contexts
{
    public class PostContext : DMSys.Data.NpgsqlUtility
    {
        public PostContext()
            : base(xConfig.ConnectionString)
        { }

        public PostModel GetPost(int id)
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
            PostModel model = null;
            using (DataTable dTable = base.FillDataTable(commandText))
            {
                if( dTable.Rows.Count > 0)
                {
                    DataRow dRow = dTable.Rows[0];
                    model = new PostModel()
                    {
                        PostId = TryParse.ToInt32(dRow["id"]),
                        SiteId = TryParse.ToInt32(dRow["n_site_id"]),
                        CategoryId = TryParse.ToInt32(dRow["n_category_id"]),
                        PLink = TryParse.ToString(dRow["post_link"]),
                        PImage = TryParse.ToString(dRow["post_image"]),
                        PTitle = TryParse.ToString(dRow["post_title"]),
                        PText = TryParse.ToString(dRow["post_text"]),
                        PPrice = TryParse.ToDecimal(dRow["post_price"]),
                        TemplateLocationId = TryParse.ToInt32(dRow["n_template_location_id"]),
                        PDate = TryParse.ToDateTime(dRow["post_date"]),
                        SitePostedId = TryParse.ToInt32(dRow["n_site_posted_id"]),
                        PPriceTypeId = TryParse.ToInt32(dRow["post_price_type_id"])
                    };
                }
            }
            return model;
        }

        /// <summary>
        /// Типове цени
        /// </summary>
        public List<ListItemModel> GetPostPriceTypes(bool withUndefined = false)
        {
            List<ListItemModel> model = new List<ListItemModel>();
            string commandText =
@"SELECT ppt.id, ppt.pt_name
 FROM post_price_type ppt
 ORDER BY ppt.pt_name ";

            if (withUndefined)
            {
                model.Add(new ListItemModel() { id = "0", label = "-", abbrev = "" });
            }
            using (DataTable dtDiscounts = FillDataTable(commandText))
            {
                foreach (DataRow drDiscount in dtDiscounts.Rows)
                {
                    model.Add(new ListItemModel()
                    {
                        id = TryParse.ToString(drDiscount["id"]),
                        label = TryParse.ToString(drDiscount["pt_name"]),
                        abbrev = ""
                    });
                }
            }
            return model;
        }

        public void Edit(PostModel model)
        {
            string commandText =
@"UPDATE post
     SET post_link = " + SQLString(model.PLink) + @"
       , post_image = " + SQLString(model.PImage) + @"
       , post_title = " + SQLString(model.PTitle) + @"
       , post_text = " + SQLString(model.PText) + @"
       , post_price = " + SQLDecimal(model.PPrice) + @"
       , post_date = " + SQLDateTime(model.PDate) + @"
       , post_price_type_id = " + SQLInt(model.PPriceTypeId) + @"
 WHERE id = " + SQLInt(model.PostId);

            base.ExecuteNonQuery(commandText);
        }
    }
}