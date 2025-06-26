using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDP.Model
{
    public class MUser
    {
        public int Id { set; get; }
        public string UserName { set; get; }   // 用户名
        public string Password { set; get; }   // 密码
        public string GradeId { set; get; }       // 权限Id
        public string GradeName { set; get; }  // 权限名称
    }
}
