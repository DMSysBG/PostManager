using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DMSys.Systems;

namespace PostManager.Contexts
{
    public class HomeContext : DMSys.Data.NpgsqlUtility
    {
        public HomeContext()
            : base(xConfig.ConnectionString)
        { }

        /// <summary>
        /// Списък грешки
        /// </summary>
        public List<Models.ExceptionModel> GetSysExceptions()
        {
            string commandText =
@"SELECT ex.id
       , ex.ex_date
       , ex.ex_message
       , ex.stack_trace
       , ex.post_link
       , ex.n_source_id
 FROM sys_exception ex ";

            List<Models.ExceptionModel> model = new List<Models.ExceptionModel>();
            using (DataTable dTable = base.FillDataTable(commandText))
            {
                foreach (DataRow dRow in dTable.Rows)
                {
                    model.Add(new Models.ExceptionModel() {
                        ExId = TryParse.ToInt32(dRow["id"]),
                        ExDate = TryParse.ToDateTime(dRow["ex_date"]),
                        ExMessage = TryParse.ToString(dRow["ex_message"]),
                        StackTrace = TryParse.ToString(dRow["stack_trace"]),
                        PostLink = TryParse.ToString(dRow["post_link"]),
                        SourceId = TryParse.ToInt32(dRow["n_source_id"])
                    });
                }
            }
            return model;
        }

        /// <summary>
        /// Изтрива грешка
        /// </summary>
        public void DeleteSysException(int id)
        {
            string commandText =
@"DELETE FROM sys_exception
  WHERE id = " + base.SQLInt(id);

            base.ExecuteNonQuery(commandText);
        }

        /// <summary>
        /// Невалидни постове
        /// </summary>
        public List<Models.ExceptionModel> GetInvalidPost()
        {
            string commandText =
@"SELECT lip.id
       , lip.post_date
       , lip.msg_invalid
       , lip.post_link
       , lip.n_site_id 
 FROM list_invalid_post() lip ";
            base.FillDataTable(commandText);

            List<Models.ExceptionModel> model = new List<Models.ExceptionModel>();
            using (DataTable dTable = base.FillDataTable(commandText))
            {
                foreach (DataRow dRow in dTable.Rows)
                {
                    model.Add(new Models.ExceptionModel()
                    {
                        ExId = TryParse.ToInt32(dRow["id"]),
                        ExDate = TryParse.ToDateTime(dRow["post_date"]),
                        ExMessage = TryParse.ToString(dRow["msg_invalid"]),
                        StackTrace = "",
                        PostLink = TryParse.ToString(dRow["post_link"]),
                        SourceId = TryParse.ToInt32(dRow["n_site_id"])
                    });
                }
            }
            return model;
        }
    }
}