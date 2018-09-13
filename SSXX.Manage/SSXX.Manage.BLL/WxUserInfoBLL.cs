using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSXX.Manage.Models;
using SSXX.Manage.DAL;
using SSXX.Manage.Common;
using System.Data;

namespace SSXX.Manage.BLL
{
    public class WxUserInfoBLL
    {
        public WxUserInfoDAL userInfoDal = new WxUserInfoDAL();
        public void CheckUserInfo(WxUserInfo wx)
        {
            if (!userInfoDal.Exists(wx.openid))
            {
                try
                {
                    userInfoDal.Add(wx);
                }
                catch(Exception e)
                {
                    Log.WriterLog(e.Source, e.Message);
                }
            }
        }

        public Boolean UserInfoExists(WxUserInfo wx)
        {
            if (userInfoDal.Exists(wx.openid))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<WxUserInfo> QueryUserInfoByID(string openid)
        {
            return JsonHelper.DataSetToIList<WxUserInfo>(userInfoDal.GetList(" openid = '" + openid + "'"), 0);
        }
        public List<WxUserInfo> QueryUserInfoByID(int userid)
        {
            return JsonHelper.DataSetToIList<WxUserInfo>(userInfoDal.GetList(" u_id = '" + userid + "'"), 0);
        }
    }
}
