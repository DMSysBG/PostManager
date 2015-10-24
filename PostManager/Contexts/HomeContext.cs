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

        public void DeleteSysException(int id)
        {
            string commandText =
@"DELETE FROM sys_exception
  WHERE id = " + base.SQLInt(id);

            base.ExecuteNonQuery(commandText);
        }

        public List<Models.ExceptionModel> GetInvalidPost()
        {
            List<Models.ExceptionModel> model = new List<Models.ExceptionModel>();

            string commandText =
@"SELECT ex.id
    , 1 AS ex_type_id
    , ex.ex_date
    , ex.ex_message
    , ex.stack_trace
    , ex.post_link
    , ex.n_source_id
 FROM sys_exception ex
 UNION
 SELECT lip.id
      , 2 AS ex_type_id
    , lip.post_date AS ex_date
    , lip.msg_invalid AS ex_message
    --, lip.stack_trace
    , lip.post_link
    , lip.n_source_id AS n_source_id
 FROM list_invalid_post() lip
n_site_id
";
            base.FillDataTable(commandText);

            return model;
        }
    }
}