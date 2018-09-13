using SSXX.Manage.Common;
using SSXX.Manage.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSXX.Manage.DAL
{
    /// <summary>
    /// 数据访问类:WxUserInfo
    /// </summary>
    public  class WxUserInfoDAL
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string openid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from wx_user_info");
            strSql.Append(" where openid=@openid");
            SqlParameter[] parameters = {
                new SqlParameter("@openid", openid)
            };
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(WxUserInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into wx_user_info(");
            strSql.Append("nickName,openid,sex,city,province,country,headimgurl,subscribe_time,unionid,remark,groupid,tagid_list,subscribe_scene,qr_scene,qr_scene_str)");
            strSql.Append(" values (");
            strSql.Append("@nickName,@openid,@sex,@city,@province,@country,@headimgurl,@subscribe_time,@unionid,@remark,@groupid,@tagid_list,@subscribe_scene,@qr_scene,@qr_scene_str)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@nickName", model.nickName),
                    new SqlParameter("@openid", model.openid),
                    new SqlParameter("@sex", model.sex),
                    new SqlParameter("@city", model.city),
                    new SqlParameter("@province", model.province),
                    new SqlParameter("@country", model.country),
                    new SqlParameter("@headimgurl", model.headimgurl),
                    new SqlParameter("@subscribe_time",model.subscribe_time),
                    new SqlParameter("@unionid", model.unionid),
                    new SqlParameter("@remark", model.remark),
                    new SqlParameter("@groupid", model.groupid),
                    new SqlParameter("@tagid_list", model.tagid_list),
                    new SqlParameter("@subscribe_scene", model.subscribe_scene),
                    new SqlParameter("@qr_scene", model.qr_scene),
                    new SqlParameter("@qr_scene_str",model.qr_scene_str)};

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(WxUserInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update wx_user_info set ");
            strSql.Append("nickName=SQL2012nickName,");
            strSql.Append("openid=SQL2012openid,");
            strSql.Append("sex=SQL2012sex,");
            strSql.Append("city=SQL2012city,");
            strSql.Append("province=SQL2012province,");
            strSql.Append("country=SQL2012country,");
            strSql.Append("headimgurl=SQL2012headimgurl,");
            strSql.Append("subscribe_time=SQL2012subscribe_time,");
            strSql.Append("unionid=SQL2012unionid,");
            strSql.Append("remark=SQL2012remark,");
            strSql.Append("groupid=SQL2012groupid,");
            strSql.Append("tagid_list=SQL2012tagid_list,");
            strSql.Append("subscribe_scene=SQL2012subscribe_scene,");
            strSql.Append("qr_scene=SQL2012qr_scene,");
            strSql.Append("qr_scene_str=SQL2012qr_scene_str");
            strSql.Append(" where u_id=SQL2012u_id");
            SqlParameter[] parameters = {
                    new SqlParameter("SQL2012nickName", SqlDbType.VarChar,100),
                    new SqlParameter("SQL2012openid", SqlDbType.VarChar,100),
                    new SqlParameter("SQL2012sex", SqlDbType.Int,4),
                    new SqlParameter("SQL2012city", SqlDbType.VarChar,100),
                    new SqlParameter("SQL2012province", SqlDbType.VarChar,100),
                    new SqlParameter("SQL2012country", SqlDbType.VarChar,100),
                    new SqlParameter("SQL2012headimgurl", SqlDbType.VarChar,500),
                    new SqlParameter("SQL2012subscribe_time", SqlDbType.VarChar,100),
                    new SqlParameter("SQL2012unionid", SqlDbType.VarChar,100),
                    new SqlParameter("SQL2012remark", SqlDbType.VarChar,100),
                    new SqlParameter("SQL2012groupid", SqlDbType.Int,4),
                    new SqlParameter("SQL2012tagid_list", SqlDbType.VarChar,100),
                    new SqlParameter("SQL2012subscribe_scene", SqlDbType.VarChar,200),
                    new SqlParameter("SQL2012qr_scene", SqlDbType.VarChar,100),
                    new SqlParameter("SQL2012qr_scene_str", SqlDbType.VarChar,100),
                    new SqlParameter("SQL2012u_id", SqlDbType.Int,4)};
            parameters[0].Value = model.nickName;
            parameters[1].Value = model.openid;
            parameters[2].Value = model.sex;
            parameters[3].Value = model.city;
            parameters[4].Value = model.province;
            parameters[5].Value = model.country;
            parameters[6].Value = model.headimgurl;
            parameters[7].Value = model.subscribe_time;
            parameters[8].Value = model.unionid;
            parameters[9].Value = model.remark;
            parameters[10].Value = model.groupid;
            parameters[11].Value = model.tagid_list;
            parameters[12].Value = model.subscribe_scene;
            parameters[13].Value = model.qr_scene;
            parameters[14].Value = model.qr_scene_str;
            parameters[15].Value = model.u_id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int u_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from wx_user_info ");
            strSql.Append(" where u_id=SQL2012u_id");
            SqlParameter[] parameters = {
                    new SqlParameter("SQL2012u_id", SqlDbType.Int,4)
            };
            parameters[0].Value = u_id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string u_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from wx_user_info ");
            strSql.Append(" where u_id in (" + u_idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public WxUserInfo GetModel(int u_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 u_id,nickName,openid,sex,city,province,country,headimgurl,subscribe_time,unionid,remark,groupid,tagid_list,subscribe_scene,qr_scene,qr_scene_str from wx_user_info ");
            strSql.Append(" where u_id=SQL2012u_id");
            SqlParameter[] parameters = {
                    new SqlParameter("SQL2012u_id", SqlDbType.Int,4)
            };
            parameters[0].Value = u_id;

            WxUserInfo model = new WxUserInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public WxUserInfo DataRowToModel(DataRow row)
        {
            WxUserInfo model = new WxUserInfo();
            if (row != null)
            {
                if (row["u_id"] != null && row["u_id"].ToString() != "")
                {
                    model.u_id = int.Parse(row["u_id"].ToString());
                }
                if (row["nickName"] != null)
                {
                    model.nickName = row["nickName"].ToString();
                }
                if (row["openid"] != null)
                {
                    model.openid = row["openid"].ToString();
                }
                if (row["sex"] != null && row["sex"].ToString() != "")
                {
                    model.sex = int.Parse(row["sex"].ToString());
                }
                if (row["city"] != null)
                {
                    model.city = row["city"].ToString();
                }
                if (row["province"] != null)
                {
                    model.province = row["province"].ToString();
                }
                if (row["country"] != null)
                {
                    model.country = row["country"].ToString();
                }
                if (row["headimgurl"] != null)
                {
                    model.headimgurl = row["headimgurl"].ToString();
                }
                if (row["subscribe_time"] != null)
                {
                    model.subscribe_time = row["subscribe_time"].ToString();
                }
                if (row["unionid"] != null)
                {
                    model.unionid = row["unionid"].ToString();
                }
                if (row["remark"] != null)
                {
                    model.remark = row["remark"].ToString();
                }
                if (row["groupid"] != null && row["groupid"].ToString() != "")
                {
                    model.groupid = int.Parse(row["groupid"].ToString());
                }
                if (row["tagid_list"] != null)
                {
                    model.tagid_list = row["tagid_list"].ToString();
                }
                if (row["subscribe_scene"] != null)
                {
                    model.subscribe_scene = row["subscribe_scene"].ToString();
                }
                if (row["qr_scene"] != null)
                {
                    model.qr_scene = row["qr_scene"].ToString();
                }
                if (row["qr_scene_str"] != null)
                {
                    model.qr_scene_str = row["qr_scene_str"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select u_id,nickName,openid,sex,city,province,country,headimgurl,subscribe_time,unionid,remark,groupid,tagid_list,subscribe_scene,qr_scene,qr_scene_str ");
            strSql.Append(" FROM wx_user_info ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" u_id,nickName,openid,sex,city,province,country,headimgurl,subscribe_time,unionid,remark,groupid,tagid_list,subscribe_scene,qr_scene,qr_scene_str ");
            strSql.Append(" FROM wx_user_info ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM wx_user_info ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.u_id desc");
            }
            strSql.Append(")AS Row, T.*  from wx_user_info T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}
