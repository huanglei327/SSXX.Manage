using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSXX.Manage.Models
{
    public class WxUserInfo
    {
        public int u_id { get; set; }
        public string nickName { get; set; }
        
        public string openid { get; set; }

        public int sex { get; set; }

        public string language { get; set; }

        public string city { get; set; }
        public string province { get; set; }

        public string country { get; set; }
        public string headimgurl { get; set; }

        public string subscribe_time { get; set; }

        public string unionid { get; set; }
        public string remark { get; set; }

        public int groupid { get; set; }

        public string tagid_list { get; set; }

        public string subscribe_scene { get; set; }
        public string qr_scene { get; set; }

        public string qr_scene_str { get; set; }
    }
}
